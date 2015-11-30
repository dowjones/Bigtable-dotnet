using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Models.Abstraction;
using BigtableNet.Models.Clients;
using Google.Bigtable.Admin.Cluster.V1;

namespace BigtableNet.Models.Mode
{
    public class BigCluster : BigModel
    {
        private BigClusterClient bigClusterClient;
        private Cluster cluster;

        internal BigCluster(BigClusterClient bigClusterClient, Cluster cluster)
        {
            this.bigClusterClient = bigClusterClient;
            this.cluster = cluster;
        }
    }
}
