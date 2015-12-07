using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;

namespace Examples.LowLevel
{
    /// <summary>
    /// Centralized so that changing serialization can be done in one place
    /// </summary>
    static class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ByteString ToByteString(this string value)
        {
            return ByteString.CopyFromUtf8(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FromByteString(this ByteString value)
        {
            return value.ToStringUtf8();
        }
    }
}
