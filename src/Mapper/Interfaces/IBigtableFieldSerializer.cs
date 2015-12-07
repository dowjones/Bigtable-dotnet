using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigtableNet.Mapper.Interfaces
{
    public interface IBigtableFieldSerializer
    {
        bool CanHandleType(Type type);

        byte[] SerializeField(object value, Encoding encoding);

        object DeserializeField(byte[] keyBytes, Encoding encoding);
    }
}
