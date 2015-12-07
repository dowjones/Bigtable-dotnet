using BigtableNet.Mapper.Abstraction;
using BigtableNet.Mapper.Annotations;
using Examples.Mapped.Schema.ColumnFamilies;
using Examples.Mapped.Schema.Keys;

namespace Examples.Mapped.Schema.Tables
{
    [BigTable("complex")]
    class SomeComplexTable : ComplexTableBase<DateAndLong>
    {
        public SomeComplexTable()
        {
            AddFamily<Cf1>();
            AddFamily<Cf2>();
        }
    }
}
