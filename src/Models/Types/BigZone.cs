using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Models.Abstraction;
using BigtableNet.Models.Clients;
using Google.Bigtable.Admin.Cluster.V1;

namespace BigtableNet.Models.Types
{
    public class BigZone : BigModel
    {
        private BigClusterClient bigClusterClient;
        private Zone failedZone;

        internal BigZone(BigClusterClient bigClusterClient, Zone failedZone)
        {
            this.bigClusterClient = bigClusterClient;
            this.failedZone = failedZone;
        }
    }
}
