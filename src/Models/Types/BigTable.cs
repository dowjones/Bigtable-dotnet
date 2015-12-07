using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Models.Abstraction;
using BigtableNet.Models.Clients;
using Google.Bigtable.Admin.Table.V1;

namespace BigtableNet.Models.Types
{
    /// <summary>
    /// Based on the golang bigtable package
    /// </summary>
    public class BigTable : BigModel
    {
        //private IEnumerable<BigFamily> _familes;

        public Encoding Encoding { get; set; }

        public IEnumerable<BigFamily> Families { get; set; }

        public IEnumerable<string> FamilyNames { get; private set; }

        public BigTable(string name, Encoding encoding = null)
        {
            Name = name;
            Encoding = encoding ?? BigModel.DefaultEncoding;
            Families = new List<BigFamily>();
            HookupFamilyNames();
        }

        internal BigTable(Table table, Encoding encoding, string clusterUir)
        {
            Encoding = encoding;
            var prefix = clusterUir + BigtableConstants.Templates.TableAdjunct;
            Name = table.Name.Replace(prefix, "");
            Families = table.ColumnFamilies.Select(family => new BigFamily(family.Value, prefix + Name)).ToArray();
            HookupFamilyNames();
        }

        /// <summary>
        ///  This is a silly optimization, but maybe people just want the names in all cases.
        ///  Reconnection FamilyNames to Familes.
        /// </summary>
        private void HookupFamilyNames()
        {
            FamilyNames = Families.Select(family => family.Name);
        }
    }
}
