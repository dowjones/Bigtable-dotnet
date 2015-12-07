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
    public class BigCluster : BigModel
    {
        private Cluster cluster;

        internal BigCluster(BigClusterClient bigClusterClient, Cluster cluster)
        {
            this.cluster = cluster;
        }
    }
}
