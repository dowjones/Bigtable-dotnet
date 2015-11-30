using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigtableNet.Common
{
    public class GcPolicy
    {
        public long MaxAge { get; private set; }

        public DurationTypes Duration { get; private set; }

        public GcPolicy()
        {
            
        }

        public GcPolicy(long maxAge, DurationTypes units )
        {
            MaxAge = maxAge;
            Duration = units;
        }
    }


    public enum DurationTypes
    {
        /// <summary>
        /// Limited to Int32.MaxValue
        /// </summary>
        Revisions,

        /// <summary>
        /// In the events you want your data deleted before you can read it
        /// </summary>
        Microseconds,

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
