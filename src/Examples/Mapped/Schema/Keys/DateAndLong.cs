using BigtableNet.Mapper.Abstraction;
using BigtableNet.Mapper.Annotations;
using BigtableNet.Mapper.Types;

namespace Examples.Mapped.Schema.Keys
{
    [BigTable(KeySeparator=":")]
    public class DateAndLong
    {
        public BigTableKey<string> StringKey;

        // TODO: Test Required
        [BigTableKey(Ordinal = 0)]
        public BigTableKey<long> LongKey { get; set; }
    }
}
