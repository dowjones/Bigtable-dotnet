using System;
using System.Text;
using BigtableNet.Models.Abstraction;

namespace BigtableNet.Mapper.Annotations
{
    public class BigTableAttribute : Attribute
    {
        private Encoding _encoder;

        internal Encoding Encoding
        {
            get { return _encoder ?? (_encoder = (Encoding)Activator.CreateInstance(BigModel.DefaultEncoding.GetType())); }
        }

        public string TableName { get; set; }
        public BigTableAttribute() { }

        public BigTableAttribute(string tableName)
        {
            TableName = tableName;
            EncodingType = BigModel.DefaultEncoding.GetType();
        }

        public string KeySeparator { get; set; }

        public Type KeySerializer { get; set; }

        public Type EncodingType { get; set; }
    }
}
