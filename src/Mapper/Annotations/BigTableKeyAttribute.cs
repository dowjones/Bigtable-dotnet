using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Mapper.Abstraction;

namespace BigtableNet.Mapper.Annotations
{
    public class BigTableKeyAttribute : BigTableFieldAnnotation
    {
        public int Ordinal { get; set; }

        public BigTableKeyAttribute()
        {
            Ordinal = int.MaxValue;
        }
    }
}
