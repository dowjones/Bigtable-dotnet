using System;
using System.Collections.Generic;
using System.Data;
using BigtableNet.Models.Types;

namespace BigtableNet.Mapper.Abstraction
{
    public abstract class ComplexTableBase<TKeyset>
    {
        private readonly Dictionary<Type, object> _families = new Dictionary<Type, object>();

        public TKeyset Keyset { get; set; }

        public void AddFamily<T>()
        {
            var type = typeof(T);
            if (_families.ContainsKey(type))
                throw new DuplicateNameException(String.Format("Adding Family {0} to {1}", type.Name, GetType().Name));
            _families.Add(type, default(T));
        }

        public T GetFamily<T>()
        {
            var type = typeof(T);
            if (!_families.ContainsKey(type))
                return default(T);

            return (T)_families[type];
        }

        public void SetFamily<T>(BigRow row)
        {
            
        }
    }
}
