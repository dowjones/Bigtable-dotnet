using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Mapper.Abstraction;

namespace BigtableNet.Mapper.Annotations
{
    public class BigTableFieldAttribute : BigTableFieldAnnotation
    {
        internal string Name { get; set; }

        public BigTableFieldAttribute()
        {
            
        }
        public BigTableFieldAttribute(string name)
        {
            Name = name;
        }
    }
}
