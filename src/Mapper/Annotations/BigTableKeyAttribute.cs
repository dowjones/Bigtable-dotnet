using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Mapper.Abstraction;

namespace BigtableNet.Mapper.Annotations
{
    public class BigTableKeyAttribute : BigTablePropertyAnnotation
    {
        public string DefaultKeySeparator { get; set; }

        public BigTableKeyAttribute()
        {
            DefaultKeySeparator = ":";
        }
    }
}
