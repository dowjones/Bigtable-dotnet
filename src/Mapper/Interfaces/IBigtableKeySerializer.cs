using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Models.Types;

namespace BigtableNet.Mapper.Interfaces
{
    public interface IBigtableKeySerializer<in T>
    {
        byte[] SerializeKey(T instance, Encoding encoding);

        void DeserializeKey(T instance, byte[] keyBytes, Encoding encoding);
    }
}
