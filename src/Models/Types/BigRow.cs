using System;
using System.Collections.Generic;
using System.Linq;
using BigtableNet.Models.Clients;
using Google.Bigtable.V1;
using Google.Protobuf;

namespace BigtableNet.Models.Types
{
    public class BigRow
    {
        private BigDataClient _dataClient;

        private Family row;

        public BigTable Table { get; private set; }

        public byte[] Key { get; set; }

        public string KeyString { get; set; }

        public Dictionary<string, IEnumerable<BigField>> Fields { get; set; }

        public BigRow(BigDataClient dataClient, BigTable table)
        {
            // Store
            _dataClient = dataClient;
            Table = table;

            // Create
            Fields = new Dictionary<string, IEnumerable<BigField>>();
        }

        public BigRow(BigDataClient dataClient, BigTable table, ReadRowsResponse row) : this(dataClient,table)
        {
            InflateRow(row);
        }

        public BigRow(BigDataClient dataClient, BigTable table, Family changeset) : this(dataClient, table)
        {
            var fields = InflateFields(changeset);
            Fields.Add(String.Empty, fields);
        }

        private void InflateRow(ReadRowsResponse row)
        {
            Key = row.RowKey.ToByteArray();
            KeyString = row.RowKey.ToString(Table.Encoding);

            foreach (var chunk in row.Chunks)
            {
                if (chunk.RowContents != null)
                {
                    var contents = chunk.RowContents;
                    var family = contents.Name;
                    var fields = InflateFields(contents);
                    Fields.Add(family, fields);
                }
            }
        }

        private IEnumerable<BigField> InflateFields(Family contents)
        {
            List<BigField> fields = new List<BigField>();
            foreach (var col in contents.Columns)
            {
                var columnName = col.Qualifier.ToString(Table.Encoding);
                fields.AddRange(col.Cells.Select(cell => new BigField(Table, columnName, cell)));
            }
            return fields;
        }

        public class Sample
        {
            public BigTable Table { get; private set; }

            public byte[] Key { get; private set; }

            public string KeyString { get; private set; }

            public long Offset { get; private set; }

            public Sample(BigTable table, ByteString key, long offset)
            {
                Table = table;
                Key = key.ToByteArray();
                KeyString = key.ToString(table.Encoding);
                Offset = offset;
            }
        }
    }
}
