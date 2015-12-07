using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BigtableNet.Models.Clients;
using Google.Bigtable.V1;
using Google.Protobuf;

namespace BigtableNet.Models.Types
{
    public class BigRow
    {
        private readonly string _tableName;

        private readonly Encoding _encoding;

        /// <summary>
        /// A Dictionary of Family Names containing 
        /// a Dictionary of Field Qualifiers containing
        /// a group <see cref="BigFields"/> for that cell.
        /// {Family,{ColumnName,Fields[]}}
        /// </summary>
        public Dictionary<string, Dictionary<string, IGrouping<string, BigField>>> FieldsByFamily { get; private set; }

        public byte[] Key { get; set; }

        public string KeyString
        {
            get { return _encoding.GetString(Key); }
            set { Key = _encoding.GetBytes(value); }
        }


        public IEnumerable<string> FamilyStrings
        {
            get { return FieldsByFamily.Keys; }
        }

        public IEnumerable<string> AllColumnNames
        {
            get { return FieldsByFamily.Values.SelectMany( field => field.Keys ); }
        }

        private BigRow()
        {
            // Create
            FieldsByFamily = new Dictionary<string, Dictionary<string, IGrouping<string, BigField>>>();
        }

        public BigRow(string tableName, Encoding encoding) : this()
        {
            // Store
            _tableName = tableName;
            _encoding = encoding;
        }

        public BigRow(BigTable table) : this(table.Name, table.Encoding)
        {
        }
        
        public BigRow(BigTable table, ReadRowsResponse row) : this(table)
        {
            InflateRow(row, table.Encoding);
        }

        public BigRow(string tableName, Encoding encoding, ReadRowsResponse row)  : this( tableName, encoding )
        {
            InflateRow(row, encoding);
        }

        public BigRow(BigTable table, Family changeset) : this(table.Name, table.Encoding, changeset)
        {
        }

        public BigRow(string tableName, Encoding encoding, Family changeset) : this(tableName, encoding)
        {
            var fields = InflateFields(changeset,encoding);
            FieldsByFamily.Add(String.Empty, fields);
        }

        public IEnumerable<string> GetFamilyNames()
        {
            return FieldsByFamily.Keys;
        }

        public IEnumerable<BigField> GetFields(string family = null, string columnName = null)
        {
            if (family == null)
            {
                return FieldsByFamily.Values.SelectMany(field => field.Values.SelectMany(cell => cell));
            }

            if (columnName == null)
            {
                return FieldsByFamily[family].Values.SelectMany(field => field);
            }

            return FieldsByFamily[family][columnName];
        }

        public IEnumerable<BigField> GetValues(string family, string columnName)
        {
            if (!FieldsByFamily.ContainsKey(family))
                throw new MissingFieldException(String.Format("Row does not have family {0} in table {1}", family, _tableName));

            var familyValues = FieldsByFamily[family];

            if (!familyValues.ContainsKey(columnName))
                throw new MissingFieldException(String.Format("Row does not have field {2} in family {0} in table {1}", family, _tableName, columnName));

            return familyValues[columnName];
        }


        private void InflateRow(ReadRowsResponse row, Encoding encoding)
        {
            Key = row.RowKey.ToByteArray();
            KeyString = row.RowKey.ToString(_encoding);

            foreach (var chunk in row.Chunks)
            {
                if (chunk.RowContents != null)
                {
                    var contents = chunk.RowContents;
                    var family = contents.Name;
                    var fields = InflateFields(contents, encoding);
                    FieldsByFamily.Add(family, fields);
                }
            }
        }

        private Dictionary<string, IGrouping<string, BigField>> InflateFields(Family contents, Encoding encoding)
        {
            return contents.Columns
                .Select(column => new { column, columnName = column.Qualifier.ToString(_encoding) })
                .Select(table => table.column.Cells.Select(cell => new BigField(table.columnName, encoding, cell)))
                .SelectMany(x => x.ToArray())
                .GroupBy(x => x.ColumnName)
                .ToDictionary(x => x.Key);
        }

        public class Sample
        {
            public byte[] Key { get; private set; }

            public string KeyString { get; private set; }

            public long Offset { get; private set; }

            // Remove with observ
            public Sample(BigTable table, ByteString key, long offset) : this(key, offset, table.Encoding)
            {
            }

            public Sample(ByteString key, long offset, Encoding encoding)
            {
                Key = key.ToByteArray();
                KeyString = key.ToString(encoding);
                Offset = offset;
            }
        }
    }
}
