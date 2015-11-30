using System;

namespace BigtableNet.Mapper.Annotations
{
    public class BigTableAttribute : Attribute
    {
        public string Name { get; set; }
        public BigTableAttribute() { }

        public BigTableAttribute(string name)
        {
            Name = name;
        }
        public string KeySeparator
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Type KeySerializer
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
