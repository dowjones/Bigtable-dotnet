using System;
using System.Collections.Generic;
using System.Text;
using BigtableNet.Mapper.Abstraction;
using BigtableNet.Mapper.Annotations;
using BigtableNet.Mapper.Interfaces;
using BigtableNet.Mapper.Types;

namespace Examples.Mapped.Schema.Keys
{
    [BigTable(KeySeparator=":", KeySerializer = typeof(Serializer), EncodingType = typeof(ASCIIEncoding))]
    public class CustomizedKey
    {
        public BigTableKey<string> StringKey;

        public BigTableKey<long> LongKey;

        public class Serializer : IBigtableKeySerializer<CustomizedKey>
        {
            public byte[] SerializeKey(CustomizedKey instance, Encoding encoding)
            {
                if( !instance.StringKey.IsSpecified || instance.StringKey.Value.Length == 0 )
                    throw new ArgumentOutOfRangeException("StringKey", "Must be specified");
                var k1Bytes = encoding.GetBytes(instance.StringKey);
                if( k1Bytes.Length > 16)
                    throw new ArgumentOutOfRangeException("StringKey", "Must be 16 bytes or less");

                var k2Bytes = BitConverter.GetBytes(instance.LongKey);
                var result = new byte[16 + 8];
                Array.Copy(k1Bytes,result, k1Bytes.Length);
                Array.Copy(k2Bytes, 0, result, 16, k2Bytes.Length);

                return result;
            }

            public void DeserializeKey(CustomizedKey instance, byte[] keyBytes, Encoding encoding)
            {
                if( keyBytes.Length != 24 )
                    throw new ArgumentOutOfRangeException(instance.GetType().Name + ":key", "Not 24 bytes");

                var k1Bytes = new byte[16];

                Array.Copy(keyBytes, k1Bytes, 16);

                instance.StringKey = encoding.GetString(k1Bytes);
                instance.LongKey = BitConverter.ToInt64(keyBytes,16);
            }
        }
    }
}
