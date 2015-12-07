using BigtableNet.Mapper.Annotations;
using BigtableNet.Mapper.Types;
using BigtableNet.Models.Types;

namespace Examples.Mapped.Schema.ColumnFamilies
{
    [ColumnFamily(30, DurationTypes.Minutes)]
    public class Cf2
    {
        [BigTableField("sval")]
        public BigTableField<string> StringValue { get; set; }

        [BigTableField("ival")]
        public BigTableField<int> IntValue { get; set; }
    }
}
