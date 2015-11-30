using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Mapper.Interfaces;

namespace BigtableNet.Mapper.Types
{
    public struct BigTableField<T> : IBigTableProperty<T>
    {
        private T _value;

        public T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                IsSpecified = true;
            }
        }

        public bool IsSpecified { get; set; }


        long Timestamp { get; set; }


        public static implicit operator T(BigTableField<T> instance)
        {
            return instance.Value;
        }

        public static implicit operator BigTableField<T>(T value)
        {
            return new BigTableField<T> { Value = value, IsSpecified = true };
        }
    }
}
