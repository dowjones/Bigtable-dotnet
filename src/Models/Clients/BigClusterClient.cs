using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Common.Extensions;
using BigtableNet.Models.Abstraction;
using BigtableNet.Models.Extensions;
using BigtableNet.Models.Types;
using Google.Apis.Auth.OAuth2;
using Google.Bigtable.Admin.Cluster.V1;

namespace BigtableNet.Models.Clients
{
    public class BigClusterClient : BigClient
    {
        private readonly BigtableClusterService.BigtableClusterServiceClient _client;

        internal string ProjectUri { get; private set; }


        public BigClusterClient(BigtableCredentials credentials, string project, string zone, string cluster, bool isReadOnly) : this(credentials, new BigtableConfig(project, zone, cluster), isReadOnly)
        {
        }

        public BigClusterClient(BigtableCredentials credentials, BigtableConfig config, bool isReadOnly = false) : base(config, credentials.CreateClusterChannel)
        {
            // Create
            _client = new BigtableClusterService.BigtableClusterServiceClient(Channel);
        }


        public async Task<IEnumerable<BigCluster>> ListClustersAsync(Action<BigZone> failedZonesHandler = null)
        {
            var request = new ListClustersRequest { Name = ProjectUri };
            var response = await _client.ListClustersAsync(request);
            await Task.Yield();

            if (failedZonesHandler != null)
            {
                foreach (var failedZone in response.FailedZones)
                {
                    failedZonesHandler(new BigZone(this, failedZone));
                }
            }

            return response.Clusters.Select(cluster => new BigCluster(this, cluster));
        }

        public async Task<BigCluster> GetClusterAsync(string name)
        {
            var request = new GetClusterRequest { Name = name.ToClusterUri(Config.ToZoneUri()) };
            var response = await _client.GetClusterAsync(request);
            await Task.Yield();
            return new BigCluster(this, response);
        }

        public async Task DeleteClusterAsync(string name)
        {
            var request = new DeleteClusterRequest { Name = name.ToClusterUri(Config.ToZoneUri()) };
            await _client.DeleteClusterAsync(request);
            await Task.Yield();
        }

        public async Task<BigCluster> CreateClusterAsync(string name)
        {
            var request = new CreateClusterRequest
            {
                Name = Config.ToZoneUri(),
                ClusterId = name
            };

            var response = await _client.CreateClusterAsync(request);
            await Task.Yield();

            return new BigCluster(this, response);

        }

        public async Task<IEnumerable<BigZone>> ListZonesAsync()
        {
            var request = new ListZonesRequest { Name = ProjectUri };
            var response = await _client.ListZonesAsync(request);
            await Task.Yield();
            return response.Zones.Select(zone => new BigZone(this, zone));
        }
    }
}
