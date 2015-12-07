using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Mapper.Annotations;
using BigtableNet.Mapper.Types;
using Examples.Bootstrap;

namespace Examples.Mapped.Schema.Tables
{
    [BigTable(Constants.PricingTable, KeySeparator=":")]
    public class Pricing
    {
        public BigTableKey<long> Id;

        public BigTableKey<int> Date;

        [ColumnFamily("day")]
        public BigTableField<double?> Open;

        public BigTableField<double?> High;

        public BigTableField<double?> Low;

        public BigTableField<double?> Close;
    }

    [ColumnFamily("day")]
    [BigTable(Constants.PricingTable, KeySeparator = ":")]
    public class SimplePricing
    {
        [BigTableKey]
        public long Id;

        [BigTableKey]
        public int Date;

        
        public double? Open;

        public double? High;

        public double? Low;

        public double? Close;
    }
}
