using BigtableNet.Mapper.Interfaces;

namespace BigtableNet.Mapper.Types
{
    public struct BigTableKey<T>: IBigTableField<T>
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

        public override string ToString()
        {
            return IsSpecified ? _value.ToString() : "{Unspecified}";
        }

        public static implicit operator T(BigTableKey<T> instance)
        {
            return instance.Value;
        }

        public static implicit operator BigTableKey<T>(T value)
        {
            return new BigTableKey<T> {Value = value, IsSpecified = true};
        }
    }
}
