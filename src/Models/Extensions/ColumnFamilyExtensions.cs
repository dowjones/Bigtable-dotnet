using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Models.Types;
using Google.Bigtable.Admin.Table.V1;
using Google.Protobuf;

namespace BigtableNet.Models.Extensions
{
    public static class ColumnFamilyExtensions
    {
        public static ColumnFamily ToColumnFamilyPrototype(this RetentionPolicy policy)
        {
            var result = new ColumnFamily
            {
                GcRule = new GcRule(),
            };
            switch (policy.Duration)
            {
                case DurationTypes.Versions:
                    if( policy.MaxAge > Int32.MaxValue )
                        throw new ArgumentOutOfRangeException("Revisions are limited to " + Int32.MaxValue);

                    result.GcRule.MaxNumVersions =  policy.MaxAge > 0 ? (int)policy.MaxAge : 1;
                    break;

                case DurationTypes.Milliseconds:
                    var value = policy.MaxAge/1000000; // 1000 to micro, 1000 to milli
                    if (value > Int32.MaxValue)
                        throw new ArgumentOutOfRangeException("Milliseconds are limited to " + Int32.MaxValue);

                    result.GcRule.MaxAge = new Duration { Nanos = (int)value };
                    break;

                case DurationTypes.Seconds:
                    result.GcRule.MaxAge = new Duration { Seconds = policy.MaxAge };
                    break;
                case DurationTypes.Minutes:
                    result.GcRule.MaxAge = new Duration { Seconds = policy.MaxAge * 60 };
                    break;
                case DurationTypes.Hours:
                    result.GcRule.MaxAge = new Duration { Seconds = policy.MaxAge * 60 * 60 };
                    break;
                case DurationTypes.Days:
                    result.GcRule.MaxAge = new Duration { Seconds = policy.MaxAge * 60 * 60 * 24 };
                    break;
                case DurationTypes.Years:
                    result.GcRule.MaxAge = new Duration { Seconds = policy.MaxAge * 60 * 60 * 24 * 365 };
                    break;
            }

            return result;
        }
    }
}
