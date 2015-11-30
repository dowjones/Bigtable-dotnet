using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;

namespace BigtableNet.Models.Extensions
{
    public static class ByteArrayExtensions
    {
        public static ByteString ToByteString(this byte[] data)
        {
            if( data.Length == 0 )
                return ByteString.Empty;

            return ByteString.CopyFrom(data,0,data.Length);
        }
    }
}
