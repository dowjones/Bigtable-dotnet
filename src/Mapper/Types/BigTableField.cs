using System.Collections.Generic;
using System.Linq;
using BigtableNet.Mapper.Interfaces;

namespace BigtableNet.Mapper.Types
{
    public struct BigTableField<T> : IBigTableField<T>
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

        public long Timestamp { get; set; }

        private List<string> _labels; 

        public IEnumerable<string> Labels
        {
            get { return _labels ?? (_labels = new List<string>()); } 
        }

        public bool HasLabels
        {
            get { return _labels == null || !_labels.Any(); }
        }

        public IEnumerable<T> PreviousValues { get; internal set; }

        public override string ToString()
        {
            return IsSpecified ? _value.ToString() : "{Unspecified}";
        }

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
