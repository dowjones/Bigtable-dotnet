using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Mapper.Abstraction;
using BigtableNet.Models;

namespace BigtableNet.Mapper.Annotations
{
    public class ColumnFamilyAttribute : BigTablePropertyAnnotation
    {
        private GcPolicy _gcPolicy = new GcPolicy();

        public int MaxRevisions
        {
            set { _gcPolicy = new GcPolicy(value, DurationTypes.Revisions); }
        }

        public ColumnFamilyAttribute(long maxAge, DurationTypes units)
        {
            _gcPolicy = new GcPolicy(maxAge,units);
        }
    }
}
