using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigtableNet.Mapper.Abstraction
{
    public class BigTablePropertyAnnotation : Attribute
    {
        public int Order { get; set; }
    }
}
