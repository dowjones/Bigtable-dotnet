using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Models.Abstraction;
using BigtableNet.Models.Extensions;
using BigtableNet.Models.Mode;
using BigtableNet.Models.Types;
using Google.Bigtable.Admin.Cluster.V1;
using Google.Bigtable.Admin.Table.V1;
using Google.Longrunning;
using Grpc.Core;

namespace BigtableNet.Models.Clients
{
    public class BigClusterClient : BigClient
    {
        private readonly BigtableClusterService.BigtableClusterServiceClient _client;

        internal string ProjectUri { get; private set; }

        public BigClusterClient(BigtableCredential credentials, BigtableConnectionConfig config) : base(config)
        {
            // Create
            ProjectUri = config.ToProjectUri();
            _client = new BigtableClusterService.BigtableClusterServiceClient(credentials.ToClusterChannel());
            
        }

        public async Task<IEnumerable<BigCluster>> ListClustersAsync(Action<BigZone> failedZonesHandler = null)
        {
            var request = new ListClustersRequest { Name = ProjectUri };
            var response = await _client.ListClustersAsync(request);

            if (failedZonesHandler != null)
            {
                foreach (var failedZone in response.FailedZones)
                {
                    failedZonesHandler(new BigZone(this, failedZone));
                }
            }

            return response.Clusters.Select(cluster => new BigCluster(this, cluster));
        }

        public async Task<BigCluster> GetClusterAsync(string name, Action<Metadata.Entry> metadataHandler = null)
        {
            var request = new GetClusterRequest { Name = name.ToClusterUri(Config.ToZoneUri()) };
            var response = _client.GetClusterAsync(request);

            if (metadataHandler != null)
            {
                var headers = await response.ResponseHeadersAsync;
                foreach (var header in headers)
                    metadataHandler(header);
            }

            var results = await response.ResponseAsync;
            return new BigCluster(this, results);

        }

        public async Task DeleteClusterAsync(string name, Action<Metadata.Entry> metadataHandler = null)
        {
            var request = new DeleteClusterRequest { Name = name.ToClusterUri(Config.ToZoneUri()) };
            var response = _client.DeleteClusterAsync(request);

            if (metadataHandler != null)
            {
                var headers = await response.ResponseHeadersAsync;
                foreach (var header in headers)
                    metadataHandler(header);
            }

            await response.ResponseAsync;
        }

        public async Task<BigCluster> CreateClusterAsync(string name, Action<Metadata.Entry> metadataHandler = null)
        {
            var request = new CreateClusterRequest
            {
                Name = Config.ToZoneUri(),
                ClusterId = name
            };

            var response = _client.CreateClusterAsync(request);

            if (metadataHandler != null)
            {
                var headers = await response.ResponseHeadersAsync;
                foreach (var header in headers)
                    metadataHandler(header);
            }

            var results = await response.ResponseAsync;
            return new BigCluster(this, results);

        }

        public async Task<IEnumerable<BigZone>> ListZonesAsync()
        {
            var request = new ListZonesRequest { Name = ProjectUri };
            var response = await _client.ListZonesAsync(request);
            return response.Zones.Select(Zone => new BigZone(this, Zone));
        }
    }
}
