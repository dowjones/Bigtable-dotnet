using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Models.Abstraction;
using BigtableNet.Models.Clients;
using Google.Bigtable.Admin.Table.V1;

namespace BigtableNet.Models
{
    public class BigFamily : BigModel
    {
        private ColumnFamily _family;
        private BigAdminClient _adminClient;
        internal BigTable Table;
        public GcPolicy GcPolicy { get; private set; }

        internal BigFamily(BigAdminClient adminClient, BigTable table, ColumnFamily family)
        {
            _adminClient = adminClient;
            _family = family;
            Table = table;
        }

        public static BigFamily Lookup(BigDataClient dataClient, string name)
        {
            throw new NotImplementedException();
        }
    }
}
