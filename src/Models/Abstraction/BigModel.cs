using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigtableNet.Models.Abstraction
{
    public abstract class BigModel
    {
        public static Encoding DefaultEncoding { get; set; }

        public static string DefaultColumnFamilyName { get; set; }



        public string Name { get; internal set; }

        public string Id { get; internal set; }

        static BigModel()
        {
            DefaultEncoding = Encoding.UTF8;
            DefaultColumnFamilyName = "f";
        }

        public override string ToString()
        {
            return GetType().Name.Substring(3) + ": " + Name;
        }

    }
}
