using BigtableNet.Mapper.Annotations;
using BigtableNet.Mapper.Types;
using BigtableNet.Models.Types;

namespace Examples.Mapped.Schema.ColumnFamilies
{
    [ColumnFamily(2, DurationTypes.Versions)]
    public class Cf1
    {
        public BigTableField<string> StringValue { get; set; }

        public BigTableField<int> IntValue { get; set; }
    }
}
