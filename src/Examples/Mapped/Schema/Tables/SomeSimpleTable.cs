using BigtableNet.Mapper.Abstraction;
using BigtableNet.Mapper.Annotations;
using BigtableNet.Mapper.Types;
using Examples.Mapped.Schema.Keys;

namespace Examples.Mapped.Schema.Tables
{
    [BigTable("simple")]
    class SomeSimpleTable 
    {
        public BigTableKey<int> IntKey { get; set; }

        public BigTableKey<string> StringKey { get; set; }

        [BigTableField("s")]
        public BigTableField<string> StringValue { get; set; }

        [BigTableField("i")]
        public BigTableField<int?> NullableIntValue { get; set; } 
    }
}
