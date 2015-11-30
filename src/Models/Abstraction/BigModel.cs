using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigtableNet.Models.Abstraction
{
    public abstract class BigModel
    {
        public string Name { get; internal set; }

        public string Id { get; internal set; }
    }
}
