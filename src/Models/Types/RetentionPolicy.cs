using System;
using Google.Bigtable.Admin.Table.V1;

namespace BigtableNet.Models.Types
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// // Garbage collection expression specified by the following grammar:
    /// //   GC = EXPR
    /// //      | "" ;
    /// //   EXPR = EXPR, "||", EXPR              (* lowest precedence *)
    /// //        | EXPR, "&&", EXPR
    /// //        | "(", EXPR, ")"                (* highest precedence *)
    /// //        | PROP ;
    /// //   PROP = "version() >", NUM32
    /// //        | "age() >", NUM64, [ UNIT ] ;
    /// //   NUM32 = non-zero-digit { digit } ;    (* # NUM32 &lt;= 2^32 - 1 *)
    /// //   NUM64 = non-zero-digit { digit } ;    (* # NUM64 &lt;= 2^63 - 1 *)
    /// //   UNIT =  "d" | "h" | "m"  (* d=days, h=hours, m=minutes, else micros *)
    /// // GC expressions can be up to 500 characters in length
    /// //
    /// // The different types of PROP are defined as follows:
    /// //   version() - cell index, counting from most recent and starting at 1
    /// //   age() - age of the cell (current time minus cell timestamp)
    /// //
    /// // Example: "version() > 3 || (age() > 3d && version() > 1)"
    /// //   drop cells beyond the most recent three, and drop cells older than three
    /// //   days unless they're the most recent cell in the row/column
    /// //
    /// // Garbage collection executes opportunistically in the background, and so
    /// // it's possible for reads to return a cell even if it matches the active GC
    /// // expression for its family.
    /// GcExpression string `protobuf:"bytes,2,opt,name=gc_expression" json:"gc_expression,omitempty"`
    /// // Garbage collection rule specified as a protobuf.
    /// // Supersedes `gc_expression`.
    /// // Must serialize to at most 500 bytes.
    /// //
    /// // NOTE: Garbage collection executes opportunistically in the background, and
    /// // so it's possible for reads to return a cell even if it matches the active
    /// // GC expression for its family.
    /// GcRule* GcRule `protobuf:"bytes,3,opt,name=gc_rule" json:"gc_rule,omitempty"`
    ///</remarks>
    public struct RetentionPolicy
    {
        // 500 byte limit
        private string _expression;

        public long MaxAge { get; private set; }

        public DurationTypes Duration { get; private set; }

        public RetentionPolicy(long maxAge, DurationTypes units ) : this()
        {
            MaxAge = maxAge;
            Duration = units;
        }

        internal RetentionPolicy(GcRule gcRule, string gcExpression) : this()
        {
            if (gcRule.MaxNumVersions != 0)
            {
                Duration = DurationTypes.Versions;
                MaxAge = gcRule.MaxNumVersions;
            }
            else if (gcRule.MaxAge.Nanos != 0)
            {
                // The protobufs I've seen don't support nanos
                // But there are artifacts (like .Nanos) all over
                // that make me think it would work.  Maybe as part 
                // of the release-to-world, that feature was depreciated.
                // It probably works, though, so handle it.
                MaxAge = gcRule.MaxAge.Nanos / 1000;
                Duration = DurationTypes.Milliseconds;
            }
            else if( gcRule.MaxAge.Seconds != 0)
            {
                var span = TimeSpan.FromSeconds(gcRule.MaxAge.Seconds);
                if (span.TotalDays > 365)
                {
                    Duration = DurationTypes.Years;
                    MaxAge = (long)(span.TotalDays / 365);
                }
                else if (span.TotalDays > 0)
                {
                    Duration = DurationTypes.Days;
                    MaxAge = (long)span.TotalDays;
                }
                else if (span.TotalHours > 0)
                {
                    Duration = DurationTypes.Hours;
                    MaxAge = (long)span.TotalHours;
                }
                else if (span.TotalMinutes > 0)
                {
                    Duration = DurationTypes.Minutes;
                    MaxAge = (long)span.TotalMinutes;
                }
                else if (span.TotalSeconds > 0)
                {
                    Duration = DurationTypes.Seconds;
                    MaxAge = (long)span.TotalSeconds;
                }
            }
            if (!String.IsNullOrEmpty(gcExpression))
            {
                _expression = gcExpression;
            }
        }
    }


    public enum DurationTypes
    {
        /// <summary>
        /// Limited to Int32.MaxValue
        /// </summary>
        Versions,

        /// <summary>
        /// In the events you want your data deleted before you can read it
        /// </summary>
        Milliseconds,

        /// <summary>
        /// Seconds, e.g. 60s
        /// </summary>
        Seconds,

        /// <summary>
        /// Minutes, e.g. 90m
        /// </summary>
        Minutes,

        /// <summary>
        /// Hours, e.g. 36h
        /// </summary>
        Hours,

        /// <summary>
        /// Days, e.g., 15d
        /// </summary>
        Days,

        /// <summary>
        /// Non-leap years, e.g. 10y = 3650 days
        /// </summary>
        Years,
    }

}
