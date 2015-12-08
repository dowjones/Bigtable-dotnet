using System;
using BigtableNet.Mapper.Abstraction;
using BigtableNet.Models.Abstraction;
using BigtableNet.Models.Interfaces;
using BigtableNet.Models.Types;

namespace BigtableNet.Mapper.Annotations
{
    /// <summary>
    /// You may specify a simple retention policy directly, or provide a type that implements <see cref="IRetentionPolicy"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property)]
    public class ColumnFamilyAttribute : BigTableFieldAnnotation
    {
        internal string Name { get; private set; }
        internal RetentionPolicy RetentionPolicy;
        internal Type RetentionPolicyType;

        public ColumnFamilyAttribute( string name = null )
        {
            Name = name ?? BigModel.DefaultColumnFamilyName;
            RetentionPolicy = new RetentionPolicy();
        }
        public ColumnFamilyAttribute(long maxAge, DurationTypes units)
        {
            RetentionPolicy = new RetentionPolicy(maxAge, units);
        }

        public ColumnFamilyAttribute(Type retentionPolicyType)
        {
            RetentionPolicyType = retentionPolicyType;
        }
    }
}
