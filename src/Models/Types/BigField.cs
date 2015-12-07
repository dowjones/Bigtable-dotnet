using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Google.Bigtable.V1;
using Google.Protobuf;
using Google.Protobuf.Collections;

namespace BigtableNet.Models.Types
{
    /// <summary>
    /// Name: [-_.a-zA-Z0-9]{1,64}
    /// </summary>
    public class BigField
    {
        public string ColumnName { get; private set; }

        public IEnumerable<string> Labels { get; private set; }

        public byte[] Value { get; set; }

        public string StringValue
        {
            get { return _encoding.GetString(Value); }
            set { Value = _encoding.GetBytes(value); }
        }

        public long Timestamp { get; private set; }

        private readonly Encoding _encoding;


        public BigField(string columnName, Encoding encoding)
        {
            _encoding = encoding;
            ColumnName = columnName;
        }

        internal BigField(string columnName, Encoding encoding, Cell cell) : this(columnName, encoding)
        {
            Labels = cell.Labels;
            Value = cell.Value.ToByteArray();
            Timestamp = cell.TimestampMicros;
        }

        public BigField(BigTable table, string columnName, string value) : this(columnName, table.Encoding)
        {
            ColumnName = columnName;
            StringValue = value;
        }

        public BigField(BigTable table, string columnName, byte[] value) : this(columnName, table.Encoding)
        {
            ColumnName = columnName;
            Value = value;
        }

        public BigField(string columnName, string value, Encoding encoding) : this(columnName, encoding)
        {
            StringValue = value;
        }

        public BigField(string columnName, byte[] value, Encoding encoding) : this(columnName, encoding)
        {
            Value = value;
        }

        public override string ToString()
        {
            return String.Format("[{0}] {1} = {2}\t{3}", Timestamp, ColumnName, Labels.Any() ? String.Concat("[", String.Join(", ", Labels), "]") : String.Empty);
        }
    }
}
