using System.Text;

namespace BigtableNet.Mapper.Interfaces
{
    public interface IBigtableKeySerializer<in T>
    {
        byte[] SerializeKey(T instance, Encoding encoding);

        void DeserializeKey(T instance, byte[] keyBytes, Encoding encoding);
    }
}
