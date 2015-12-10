using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common.Extensions;
using BigtableNet.Models.Abstraction;
using BigtableNet.Models.Clients;
using BigtableNet.Models.Extensions;
using Google.Bigtable.V1;

namespace BigtableNet.Models.Types
{
    public abstract class BigChange
    {
        public static BigChange CreateCellUpdate(string familyName, string columnName, byte[] newValue, long timestamp = 0, Encoding encoding = null)
        {
            return new UpdateCellRequest
            {
                Encoding = encoding ?? BigModel.DefaultEncoding,
                FamilyName = familyName,
                ColumnName = columnName,
                Value = newValue,
                Timestamp = timestamp == 0 ? UpdateCellRequest.CurrentTimestamp : timestamp
            };
        }

        public static BigChange CreateCellUpdate(string familyName, string columnName, string newValue, long timestamp = 0, Encoding encoding = null)
        {
            return new UpdateCellRequest
            {
                Encoding = encoding ?? BigModel.DefaultEncoding,
                FamilyName = familyName,
                ColumnName = columnName,
                StringValue = newValue,
                Timestamp = timestamp == 0 ? UpdateCellRequest.CurrentTimestamp : timestamp
            };
        }

        public static BigChange CreateCellDeletion(string familyName, string columnName, long startMicros = 0, long endmicros = 0)
        {
            return new DeleteCellRequest
            {
                ColumnName = columnName,
                FamilyName = familyName,
                StartMicros = startMicros,
                EndMicros = endmicros
            };
        }

        public static BigChange CreateRowDeletion()
        {
            return new DeleteRowRequest();
        }

        public static BigChange CreateFamilyDeletion(string familyName)
        {
            return new DeleteFamilyRequest
            {
                FamilyName = familyName
            };
        }


        internal abstract Mutation AsApiObject();

        public abstract class FamilyChangeRequest : BigChange
        {
            public string FamilyName { get; set; }

        }

        public abstract class CellChangeRequest : FamilyChangeRequest
        {
            public string ColumnName { get; set; }

            internal Encoding Encoding { get; set; }
        }


        public class DeleteRowRequest : BigChange
        {
            internal override Mutation AsApiObject()
            {
                return new Mutation
                {
                    DeleteFromRow = new Mutation.Types.DeleteFromRow()
                };
            }
        }

        public class DeleteFamilyRequest : FamilyChangeRequest
        {
            internal override Mutation AsApiObject()
            {
                return new Mutation
                {
                    DeleteFromFamily = new Mutation.Types.DeleteFromFamily
                    {
                        FamilyName = FamilyName,
                    }
                };
            }
        }

        public class DeleteCellRequest : CellChangeRequest
        {
            public long StartMicros { get; set; }

            public long EndMicros { get; set; }

            internal override Mutation AsApiObject()
            {
                return new Mutation
                {
                    DeleteFromColumn = new Mutation.Types.DeleteFromColumn
                    {
                        FamilyName = FamilyName,
                        ColumnQualifier = ColumnName.ToByteString(Encoding),
                        TimeRange = new TimestampRange
                        {
                            StartTimestampMicros = StartMicros,
                            EndTimestampMicros = EndMicros
                        }
                    }
                };
            }
        }

        public class UpdateCellRequest : CellChangeRequest
        {
            public static bool UseUtcTimestamps { get; set; }

            public static long CurrentTimestamp
            {
                get { return UseUtcTimestamps ? DateTime.UtcNow.ToUnixTime() : DateTime.Now.ToUnixTime(); }
            }

            public byte[] Value { get; set; }

            public string StringValue
            {
                get { return Encoding.GetString(Value); }
                set { Value = Encoding.GetBytes(value); }
            }

            public long Timestamp { get; set; }

            internal override Mutation AsApiObject()
            {
                return new Mutation
                {
                    SetCell = new Mutation.Types.SetCell
                    {
                        FamilyName = FamilyName,
                        ColumnQualifier = ColumnName.ToByteString(Encoding),
                        Value = Value.ToByteString(),
                        TimestampMicros = ( Timestamp == 0 ? DateTime.UtcNow.ToUnixTime() : Timestamp ) * 1000
                    }
                };
            }
        }

        public abstract class FromRead
        {

            public static FromRead CreateCellIncrement(string familyName, string columnName, int value, Encoding encoding = null)
            {
                return CreateCellIncrement(familyName, columnName, (long)value);
            }

            public static FromRead CreateCellIncrement(string familyName, string columnName, long value, Encoding encoding = null)
            {
                return new IncrementCellRequest
                {
                    Encoding = encoding ?? BigModel.DefaultEncoding,
                    ColumnName = columnName,
                    FamilyName = familyName,
                    Value = value
                };
            }

            public static FromRead CreateCellAppend(string familyName, string columnName, byte[] value, Encoding encoding = null)
            {
                return new AppendCellRequest
                {
                    Encoding = encoding ?? BigModel.DefaultEncoding,
                    ColumnName = columnName,
                    FamilyName = familyName,
                    Value = value
                };
            }

            public static FromRead CreateCellAppend(string familyName, string columnName, string value, Encoding encoding = null)
            {
                return new AppendCellRequest
                {
                    Encoding = encoding ?? BigModel.DefaultEncoding,
                    ColumnName = columnName,
                    FamilyName = familyName,
                    StringValue = value
                };
            }


            internal abstract ReadModifyWriteRule AsApiObject();

            public abstract class BaseClass : FromRead
            {
                public string FamilyName { get; set; }

                public string ColumnName { get; set; }

                internal Encoding Encoding { get; set; }
            }

            public class IncrementCellRequest : BaseClass
            {
                public long Value { get; set; }

                internal override ReadModifyWriteRule AsApiObject()
                {
                    return new ReadModifyWriteRule
                    {
                        ColumnQualifier = ColumnName.ToByteString(Encoding),
                        FamilyName = FamilyName,
                        IncrementAmount = Value
                    };
                }
            }

            public class AppendCellRequest : BaseClass
            {
                public byte[] Value { get; set; }

                public string StringValue
                {
                    get { return Encoding.GetString(Value); }
                    set { Value = Encoding.GetBytes(value); }
                }

                public long Timestamp { get; set; }

                internal override ReadModifyWriteRule AsApiObject()
                {
                    return new ReadModifyWriteRule
                    {
                        ColumnQualifier = ColumnName.ToByteString(Encoding),
                        FamilyName = FamilyName,
                        AppendValue = Value.ToByteString()
                    };
                }
            }

        }

    }
}
