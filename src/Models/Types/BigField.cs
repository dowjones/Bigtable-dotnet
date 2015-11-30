using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Bigtable.V1;
using Google.Protobuf.Collections;

namespace BigtableNet.Models.Types
{
    public class BigField
    {
        public BigTable Table { get; private set; }

        public string ColumnName { get; private set; }

        public IEnumerable<string> Labels { get; private set; }

        public byte[] Value { get; private set; }

        public string StringValue { get; private set; }

        public long Timestamp { get; private set; }


        public BigField(BigTable table, string columnName, Cell cell)
        {
            Table = table;
            ColumnName = columnName;
            Labels = cell.Labels;
            Value = cell.Value.ToByteArray();
            StringValue = cell.Value.ToString(table.Encoding);
            Timestamp = cell.TimestampMicros;
        }
    }
}
