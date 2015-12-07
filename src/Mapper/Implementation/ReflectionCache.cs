using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common.Extensions;
using BigtableNet.Mapper.Abstraction;
using BigtableNet.Mapper.Annotations;
using BigtableNet.Mapper.Interfaces;
using BigtableNet.Mapper.Types;
using BigtableNet.Models.Abstraction;
using BigtableNet.Models.Clients;
using BigtableNet.Models.Types;
using Newtonsoft.Json;

namespace BigtableNet.Mapper.Implementation
{
    public delegate byte[] KeyGetterDelegate( object instance );

    public delegate void KeySetterDelegate(object instance, byte[] bytes);

    public delegate void FieldSetterDelegate(object instance, object value);

    public delegate object FieldGetterDelegate(object instance);

    public delegate bool IsSpecifiedDelegate(object instance);



    /// <summary>
    /// This class is a HOT MESS.  It needs some TLC.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification="I disagree. _x = private, X = non-private, therefore _X should be private static, because it's private to the class but non-private between instances.")]
    public class ReflectionCache : IBigtableKeySerializer<object>
    {
        private static IInjectableServiceLocator _injectableServiceLocator;

        private static readonly Type _BigtableFieldInterfaceType;
        private static readonly MethodInfo _BigtableFieldGetter;

        private static readonly object _Mutex = new {};
        private static readonly Dictionary<Type,ReflectionCache> _Cache = new Dictionary<Type, ReflectionCache>();
        private static readonly Dictionary<Type, FieldAccess> _FieldAccess = new Dictionary<Type, FieldAccess>();
        private static readonly Dictionary<Type, KeyAccess> _KeyAccess = new Dictionary<Type, KeyAccess>();

        /// <summary>
        /// When no serializer is registered for a particular type, Json.NET will be used.
        /// You can control the settings here.
        /// </summary>
        public static JsonSerializerSettings JsonSettings { get; set; }

        private readonly ConcurrentDictionary<Type,object> _Adjuncts = new ConcurrentDictionary<Type, object>();

        private readonly object _keySerializer;
        private readonly MethodInfo _serializeKeyMethod;
        private readonly MethodInfo _deserializeKeyMethod;
        private readonly Type _type;

        private readonly ConstructorInfo _ctor;

        public string TableName { get; private set; }

        public string KeyFieldSeparator { get; private set; }

        public string[] KeyMembers { get; private set; }

        public string[] RequiredFields { get; private set; }

        public string[] FieldNames { get; private set; }
        public string[] FamilyNames { get; private set; }

        public Dictionary<string, string> FieldNameLookup { get; private set; }

        public Dictionary<string, Type> MemberTypes { get; private set; }
        public Dictionary<string, Type> FieldTypes { get; private set; }


        public Dictionary<string, FieldSetterDelegate> Setters { get; private set; }

        public Dictionary<string, FieldGetterDelegate> Getters { get; private set; }

        public Dictionary<string, IsSpecifiedDelegate> IsSpecified { get; private set; }

        public Dictionary<string, bool> IsBigWrapper { get; private set; }

        public Encoding TableEncoding { get; set; }

        public KeyGetterDelegate KeyGetter { get; private set; }

        public KeySetterDelegate KeySetter { get; private set; }



        static ReflectionCache()
        {
            _BigtableFieldInterfaceType = typeof (IBigTableField);
            _BigtableFieldGetter = typeof(IBigTableField<>).GetProperty("Value").GetGetMethod();
            JsonSettings = new JsonSerializerSettings();
        }
        /// <summary>
        /// Set this to an implementation of IServiceLocator
        /// </summary>
        public static IInjectableServiceLocator InjectableServiceLocator
        {
            get
            {
                // If not provided, simply use Activator
                return _injectableServiceLocator ?? (_injectableServiceLocator = new DefaultServiceLocator());
            }
            set { _injectableServiceLocator = value; }
        }
        public static ReflectionCache For<T>()
        {
            return For(typeof (T));
        }

        [SuppressMessage("ReSharper", "InconsistentlySynchronizedField",Justification = "I thought this through")]
        public static ReflectionCache For(Type type)
        {
            // The double-check (anti)pattern works in certain circumstances against
            // .Net 2.0 CLR or better, and Mono.  This small optimization will prevent
            // lock acquisition at the expense of a hashtable lookup.  I estimate that
            // there will be few enough types for this to be a net gain.  Internally,
            // Lazy does this.  Why not just use Lazy?  I don't need the extra boxing,
            // this code can be specific and doesn't need the extra steps in Lazy:
            // http://referencesource.microsoft.com/#mscorlib/system/Lazy.cs,314
            if (!_Cache.ContainsKey(type))
            {
                lock (_Mutex)
                {
                    if (!_Cache.ContainsKey(type))
                    {
                        var cached = new ReflectionCache(type);
                        _Cache.Add(type, cached);
                        return cached;
                    }
                }
            }

            return _Cache[type];
        }


        public ReflectionCache(Type type)
        {
            // Store type
            _type = type;

            // Extract table name, encoding, and key separator
            var bigtable = _type.GetCustomAttribute<BigTableAttribute>();
            if (bigtable != null)
            {
                TableName = bigtable.TableName;//.UnCamelCase();
                TableEncoding = bigtable.Encoding;
                KeyFieldSeparator = bigtable.KeySeparator ?? BigDataClient.DefaultKeySeparator;

                if (bigtable.KeySerializer != default(Type))
                {
                    _keySerializer = InjectableServiceLocator.LocateService(bigtable.KeySerializer);
                }
            }

            // Store
            TableName = TableName ?? _type.Name.UnCamelCase();
            TableEncoding = TableEncoding ?? BigModel.DefaultEncoding;
            KeyFieldSeparator = KeyFieldSeparator ?? BigDataClient.DefaultKeySeparator;

            // Extract key serializer and serializing methods
            var serializerType = typeof(IBigtableKeySerializer<>).MakeGenericType(_type);
            if (_keySerializer == null)
            {
                _keySerializer = InjectableServiceLocator.CanCreate(serializerType) ? InjectableServiceLocator.LocateService(serializerType) : this;
            }

            // Store key serialization
            _serializeKeyMethod = serializerType.GetMethod("SerializeKey");
            _deserializeKeyMethod = serializerType.GetMethod("DeserializeKey");
            KeyGetter = (instance) => (byte[])_serializeKeyMethod.Invoke(_keySerializer, new [] { instance, TableEncoding });
            KeySetter = (instance, bytes) => _deserializeKeyMethod.Invoke(_keySerializer, new [] {instance, bytes, TableEncoding});

            // Locals
            var fieldNames = new List<string>();
            var familyNames = new List<string>();
            var keyMembers = new List<string>();
            var requiredFields = new List<string>();
            var keyOrder = new Dictionary<string, int>();

            // Initialize instance member
            Setters = new Dictionary<string, FieldSetterDelegate>();
            Getters = new Dictionary<string, FieldGetterDelegate>();
            IsSpecified = new Dictionary<string, IsSpecifiedDelegate>();
            IsBigWrapper = new Dictionary<string, bool>();
            MemberTypes = new Dictionary<string, Type>();
            FieldTypes = new Dictionary<string, Type>();
            FieldNameLookup = new Dictionary<string, string>();

            // Find all members on instance, does not support inheritance
            var members = _type
                .GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Union(_type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Cast<MemberInfo>()).ToArray();

            // Column family will carry over so it does not need to be applied to each and every member
            // TODO: Document this behavior
            var columnFamily = BigModel.DefaultColumnFamilyName;
            var defaultColumnFamilyAttribute = _type.GetCustomAttribute<ColumnFamilyAttribute>();
            if (defaultColumnFamilyAttribute != null)
                columnFamily = defaultColumnFamilyAttribute.Name;

            // Iterate instance members
            foreach (var member in members)
            {
                // Determine field name
                var bigtableField = member.GetCustomAttribute<BigTableFieldAttribute>();
                var memberName = member.Name;
                var fieldName = default(string);
                if (bigtableField == null)
                {
                    fieldName = memberName;
                }
                else
                {
                    fieldName = bigtableField.Name;
                }
                if (!fieldName.IsValidBigtableQualifier())
                {
                    throw new NotSupportedException(String.Format("The qualifier {0} on {1} is not valid", fieldName, TableName));
                }


                // Get key information
                var dataType = default(Type);
                var bigtableKey = member.GetCustomAttribute<BigTableKeyAttribute>();
                var isKey = false;

                // Extract accessors
                if (!ExtractAccessors(member, fieldName, ref dataType, ref isKey))
                {
                    // If this wasn't a field or property, skip the rest for this member
                    continue;
                }

                // Extract key information
                if (isKey )
                {
                    // ReSharper disable once MergeConditionalExpression 
                    // bug: https://youtrack.jetbrains.com/issue/RSRP-451492
                    keyOrder.Add(memberName, bigtableKey != null ? bigtableKey.Ordinal : int.MaxValue);
                    keyMembers.Add(memberName);
                }
                else
                {
                    // Determine column family
                    var columnFamilyAttribute = member.GetCustomAttribute<ColumnFamilyAttribute>();
                    if (columnFamilyAttribute != null)
                    {
                        // Carry column families over to next field
                        columnFamily = columnFamilyAttribute.Name ?? columnFamily;
                    }

                    familyNames.Add(columnFamily);
                    fieldNames.Add(fieldName);
                }

                FieldNameLookup.Add(memberName, fieldName);

                // Store field information
                MemberTypes.Add(memberName, dataType);
                FieldTypes.Add(fieldName, dataType);

                // Extract requiredness
                if (member.GetCustomAttributes(typeof(RequiredAttribute), true).Any())
                    requiredFields.Add(member.Name);
            }

            // Ensure we have keys
            if (!keyMembers.Any())
            {
                throw new MissingPrimaryKeyException(String.Format("Table {0} did not specify any keys.", TableName));
            }

            // Grab constructor for faster creation
            _ctor = _type.GetConstructor(Type.EmptyTypes);

            // Store server field names
            FieldNames = fieldNames.ToArray();
            FamilyNames = familyNames.ToArray();
            RequiredFields = requiredFields.ToArray();

            // Order the keys
            if (keyOrder.Any())
            {
                KeyMembers = keyMembers
                    .OrderBy(key => keyOrder.ContainsKey(key) ? keyOrder[key] : Int32.MaxValue)
                    .ToArray();
            }
            else
            {
                KeyMembers = keyMembers.ToArray();
            }
        }

        private bool ExtractAccessors(MemberInfo member, string fieldName, ref Type dataType, ref bool isKey)
        {
            // Locals
            var getter = default(FieldGetterDelegate);
            var setter = default(FieldSetterDelegate);

            // Homogenize fields and properties
            switch (member.MemberType)
            {
                case System.Reflection.MemberTypes.Field:
                    var field = ((FieldInfo) member);
                    dataType = field.FieldType;
                    getter = field.GetValue;
                    setter = field.SetValue;
                    break;

                case System.Reflection.MemberTypes.Property:
                    var property = ((PropertyInfo) member);
                    dataType = property.PropertyType;
                    getter = property.GetValue;
                    setter = property.SetValue;
                    break;

                default:
                    // Ignore other member types
                    return false;
            }

            // Inspect fields for key, specifiable/actual data type
            var bigtableKey = member.GetCustomAttribute<BigTableKeyAttribute>();
            var genDataType = dataType.IsGenericType ? dataType.GetGenericTypeDefinition() : null;
            var isBigField = genDataType == typeof(BigTableField<>);
            var isBigKey = genDataType == typeof(BigTableKey<>);
            isKey = bigtableKey != null || genDataType == typeof(BigTableKey<>);
            var isBigWrapper = isBigField || isBigKey;
            IsBigWrapper.Add(fieldName,isBigWrapper);
            
            // Store accessors
            if (isBigWrapper)
            {
                var subDataType = dataType.GenericTypeArguments[0];
                var access = isKey ? (BigAccess) GetKeyAccess(subDataType) : GetFieldAccess(subDataType);
                IsSpecified.Add(fieldName, target => ((IBigTableField)getter(target)).IsSpecified);
                Getters.Add(fieldName, target => access.ValueGetter(getter(target)));
                var useDataType = dataType;
                Setters.Add(fieldName, (target, value) =>
                {
                    var newValue = Activator.CreateInstance(useDataType);

                    access.ValueSetter(newValue, value);
                    setter(target,newValue);
                });

                dataType = subDataType;
            }
            else
            {
                IsSpecified.Add(fieldName, target => true);
                Getters.Add(fieldName, getter);
                Setters.Add(fieldName, setter);
            }

            // Indicate is property of field
            return true;
        }

        /// <summary>
        /// Caches some T that is related to this type (like BigTable instance)
        /// </summary>
        public T Adjunct<T>(Func<T> factory)
        {
            return (T)_Adjuncts.GetOrAdd(typeof(T), _ => factory());
        }

        /// <summary>
        /// Create and populate an instance of the cached type
        /// </summary>
        public object CreateInstance(byte[] key, Func<string, string, IEnumerable<BigField>> valueGetter)
        {
            // Create an instance
            var instance = _ctor.Invoke(new object[] {});

            // Deserialize key
            KeySetter(instance, key);

            // Chain
            return PopulateInstance(key, valueGetter, instance);

        }
        public object PopulateInstance(byte[] key, Func<string, string, IEnumerable<BigField>> valueGetter, object instance)
        {
            Dictionary<long, object> versions = new Dictionary<long, object>();

            // Set families
            for (int index = 0; index < FieldNames.Length; index++)
            {
                var family = FamilyNames[index];
                var field = FieldNames[index];
                
                var values = valueGetter(family, field).ToArray();
                if (!values.Any())
                {
                    continue;
                }

                // Default is most recent timestamp
                // Doc says even with an opposing retention policy
                // GC is background and opportunistic, therefore you may
                // received versions sometimes.  
                var orderedValues = values.OrderByDescending(item => item.Timestamp);
                var cell = orderedValues.First();

                // TODO: Cache these
                // Find a serializer for this type
                var valueType = FieldTypes[field];
                var serializers = BigtableReader.FieldSerializers.Where(s => s.CanHandleType(valueType)).ToArray();
                // Deserialize
                var actualValue = serializers.Any() ? serializers.First().DeserializeField(cell.Value, TableEncoding) : JsonConvert.DeserializeObject(TableEncoding.GetString(cell.Value), valueType, JsonSettings);

                // Set the current field's value
                Setters[field](instance, actualValue);
                // Only BigField
                if (false && IsBigWrapper[field])
                {
                    // Need member
                    var access = GetFieldAccess(MemberTypes[field]);
                    // Store labels
                    if (cell.Labels != null && cell.Labels.Any())
                    {
                        var labels = access.LabelsGetter(instance);
                        labels.AddRange(cell.Labels);
                    }

                    // Previous revisions
                    var previousValues = orderedValues.Take(1);
                    foreach (var value in previousValues)
                    {
                        // Collate versioned fields into a versioned object
                        if(!versions.ContainsKey(value.Timestamp))
                            versions.Add(value.Timestamp, _ctor.Invoke(new object[] { }));

                        // Get version for timestamp
                        var version = versions[value.Timestamp];

                        // Set the current field's value
                        Setters[field](version, value);

                        // Field have labels?
                        if (value.Labels != null && value.Labels.Any())
                        {
                            var labels = access.LabelsGetter(version);
                            labels.AddRange(value.Labels);
                        }
                    }
                }
            }

            if (versions.Any())
            {
                // TODO: Store versions
            }

            return instance;
        }

        private KeyAccess GetKeyAccess(Type fieldType)
        {
            // See other note about double-check
            // ReSharper disable once InconsistentlySynchronizedField
            if (!_KeyAccess.ContainsKey(fieldType))
            {
                lock (_KeyAccess)
                {
                    if (!_KeyAccess.ContainsKey(fieldType))
                    {
                        _KeyAccess.Add(fieldType, new KeyAccess(fieldType));
                    }
                }
            }

            return _KeyAccess[fieldType];
        }
        private FieldAccess GetFieldAccess(Type fieldType)
        {
            // See other note about double-check
            // ReSharper disable once InconsistentlySynchronizedField
            if (!_FieldAccess.ContainsKey(fieldType))
            {
                lock (_FieldAccess)
                {
                    if (!_FieldAccess.ContainsKey(fieldType))
                    {
                        _FieldAccess.Add(fieldType, new FieldAccess(fieldType));
                    }
                }
            }

            return _FieldAccess[fieldType];
        }

        /// <summary>
        /// Default key serializer
        /// </summary>
        byte[] IBigtableKeySerializer<object>.SerializeKey(object instance, Encoding encoding)
        {
            // Contract
            if (instance == null)
            {
                throw new InvalidOperationException("Can not serialize keys on a null reference.");
            }
            
            // Locals
            //bool missingKey = false;
            var bytes = new List<byte[]>();
            var separator = encoding.GetBytes(KeyFieldSeparator);

            // Iterate keys
            for ( int index = 0; index < KeyMembers.Length; index++)
            {
                // Localize
                var memberName = KeyMembers[index];
                var fieldName = FieldNameLookup[memberName];
                var required = RequiredFields.Contains(memberName);
                var value = Getters[fieldName](instance);

                // 
                if (value == null)
                {
                    if( required )
                        throw new SerializationException(String.Format("The member {0} on type {1} is marked as required but was not specified", memberName, _type));
                    bytes.Add(separator);
                    //missingKey = true;
                    continue;
                }
                //if (missingKey)
                //{
                //    // Programmer exception
                //    var exception = new MissingFieldException(_type.Name, memberName);
                //    // User exception // TODO: I think I designed around this
                //    throw new SerializationException("The default serializer insists that the primary keys are filled in order.  Missing keys are okay as long as all subsequent keys are missing as well.", exception);
                //}
                var valueType = value.GetType();
                var serializers = BigtableReader.FieldSerializers.Where(s => s.CanHandleType(valueType)).ToArray();
                if (serializers.Any())
                {
                    bytes.Add(serializers.First().SerializeField(value, encoding));
                }
                else
                {
                    if( IsBigWrapper[fieldName] )
                    {
                        if (!IsSpecified[fieldName](instance))
                        {
                            bytes.Add(new byte[0]);
                            continue;
                        }
                        //var fieldType = MemberTypes[memberName];
                        //var access = GetKeyAccess(fieldType);
                        //value = access.ValueGetter(value);
                    }
                    var serialized = JsonConvert.SerializeObject(value, JsonSettings);
                    bytes.Add(encoding.GetBytes(serialized));
                }
            }
            var bytesSize = bytes.Count - 1;
            var sepLength = (bytesSize) * separator.Length;
            var results = new byte[sepLength + bytes.Sum(b => b.Length)];
            var ptr = 0;
            for (int index = 0; index < bytes.Count; index++)
            {
                // Get field bytes
                var source = bytes[index];

                // Copy to result array in correct position
                Array.Copy(source, 0, results, ptr, source.Length);

                // Move ptr forward
                ptr += source.Length;
                if (index < bytesSize)
                {
                    // Copy in separator
                    Array.Copy(separator, 0, results, ptr, sepLength);

                    // Move ptr foward
                    ptr += sepLength;
                }
            }

            // Tada!
            return results;
        }

        /// <summary>
        /// Default key deserializer
        /// </summary>
        void IBigtableKeySerializer<object>.DeserializeKey(object instance, byte[] keyBytes, Encoding encoding)
        {
            if (instance == null)
            {
                throw new InvalidOperationException("Can not deserialize keys on a null reference of type " + _type.SimpleName());
            }

            // Locals
            //var bytes = new List<byte[]>();
            var separator = encoding.GetBytes(KeyFieldSeparator);

            var keys = new List<byte[]>();

            // This is not pretty.  
            // TODO: Is there a better way?

            // Split key
            var prevIndex = 0;
            for (int index = 0; index < keyBytes.Length; index++)
            {
                var foundKey = true;
                for (int kIndex = 0; kIndex < separator.Length; kIndex++)
                {
                    if (keyBytes[index + kIndex] != separator[kIndex])
                    {
                        foundKey = false;
                        break;
                    }
                }
                if (foundKey || index == keyBytes.Length - 1)
                {
                    // TODO: Account for multibyte separators
                    var size = index == keyBytes.Length - 1 ? keyBytes.Length - prevIndex : index - prevIndex;
                    var key = new byte[size]; // mmmm, byte sized

                    // A zero byte key is okay, and must be accounted
                    // in order for the key deserialer to find the right slots
                    if (size > 0)
                    {
                        Array.Copy(keyBytes, prevIndex, key, 0, size);
                    }
                    keys.Add(key);
                    index += separator.Length;
                    prevIndex = index;
                }
            }

            // Ensure we have some key data
            if (!keys.Any())
            {
                throw new SerializationException("No keys found while deserializing " + _type.SimpleName());
            }

            // Populate the key fields on the POCO
            for (int index = 0; index < KeyMembers.Length; index++)
            {
                // Localize
                var member = KeyMembers[index];
                var required = RequiredFields.Contains(member);

                // Are there more key fields than we have keys?
                if (index >= keys.Count)
                {
                    // No more key bytes available, was this required?
                    if( required )
                        throw new KeyNotFoundException(String.Format("Member {0} on type {1} is marked as required but is missing in the deserialization stream.", member, _type));

                    // Non-required field, continue to check the rest of the fields
                    continue;
                }

                // Localize this key's value
                var valueBytes = keys[index];
                //var value = Getters[field](instance);

                // Missing value?
                if (valueBytes == null)
                {
                    if (required)
                        throw new SerializationException(String.Format("Member {0} on type {1} is marked as required but was not specified", member, _type));

                    // Continue to next key field
                    continue;
                }

                // Get destination type
                var valueType = MemberTypes[member];

                // Find a serializer for this type
                var serializers = BigtableReader.FieldSerializers.Where(s => s.CanHandleType(valueType)).ToArray();

                // Deserialize
                var value = serializers.Any() ? serializers.First().DeserializeField(valueBytes, encoding) : JsonConvert.DeserializeObject(TableEncoding.GetString(valueBytes), valueType, JsonSettings);

                // Store value
                var fieldName = FieldNameLookup[member];
                Setters[fieldName](instance, value);
            }
        }

        class BigAccess
        {
            internal readonly Action<object, object> PreviousValuesSetter;
            internal readonly Func<object, List<string>> LabelsGetter;
            internal readonly Func<object, object> ValueGetter;
            internal readonly Action<object, object> ValueSetter;

            public BigAccess(Type baseType, Type type) 
            {
                var bigFieldType = baseType.MakeGenericType(new[] { type });
                var previousValueProp = bigFieldType.GetProperty("PreviousValues");
                PreviousValuesSetter = (instance, value) => previousValueProp.SetValue(instance, value);
                var labelsGetterProp = bigFieldType.GetProperty("Labels");
                LabelsGetter = (instance) => (List<string>)labelsGetterProp.GetValue(instance);
                var valueProp = bigFieldType.GetProperty("Value");
                ValueGetter = (instance) => valueProp.GetValue(instance);
                ValueSetter = (instance, value) => valueProp.SetValue(instance, value);
            }
        }

        class FieldAccess : BigAccess
        {
            public FieldAccess(Type type) : base(typeof(BigTableField<>),type)
            {

            }
        }
        class KeyAccess : BigAccess
        {
            public KeyAccess(Type type) : base(typeof(BigTableKey<>),type)
            {

            }
        }

        /// <summary>
        /// Default service locator
        /// This exists only so it may be replaced by a custom implementation, or Autofac, etc.
        /// </summary>
        class DefaultServiceLocator : IInjectableServiceLocator
        {
            public bool CanCreate(Type type)
            {
                return !(type.IsAbstract || type.IsInterface);
            }

            public object LocateService(Type type)
            {
                return Activator.CreateInstance(type);
            }
        }
    }
}
