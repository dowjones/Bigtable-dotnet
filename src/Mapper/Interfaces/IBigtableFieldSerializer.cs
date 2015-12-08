using System;
using System.Text;

namespace BigtableNet.Mapper.Interfaces
{
    public interface IBigtableFieldSerializer
    {
        bool CanHandleType(Type type);

        byte[] SerializeField(object value, Encoding encoding);

        object DeserializeField(byte[] keyBytes, Encoding encoding);
    }
}
