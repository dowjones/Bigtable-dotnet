using System;
using System.Text;

namespace BigtableNet.Mapper.Interfaces
{
    public interface IBigtableFieldSerializer
    {
        bool CanHandleType(Type type);

        byte[] SerializeField(Type type, object value, Encoding encoding);

        object DeserializeField(Type type, byte[] valueBytes, Encoding encoding);
    }
}
