using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Models.Abstraction;
using BigtableNet.Models.Extensions;
using Google.Bigtable.Admin.Table.V1;

namespace BigtableNet.Models
{
    public class BigAdminClient : BigClient
    {
        
        private readonly BigtableTableService.BigtableTableServiceClient _client;

        internal string ClusterUri { get; private set;  } 

        public BigAdminClient(BigtableCredential credentials, BigtableConnectionConfig config) : base(config)
        {
            // Create
            ClusterUri = config.ToClusterUri();
            _client = new BigtableTableService.BigtableTableServiceClient(credentials.ToAdminChannel());
        }


        public async Task<BigTable> CreateTableAsync(string name)
        {
            var request = new CreateTableRequest
            {
                Name = ClusterUri,
                TableId = name,
            };
            var response = await _client.CreateTableAsync(request);
            return new BigTable(this, response);
        }

        public async Task<IEnumerable<BigTable>> ListTablesAsync()
        {
            var request = new ListTablesRequest { Name = ClusterUri };
            var response = await _client.ListTablesAsync(request);
            return response.Tables.Select(table => new BigTable(this, table));
        }

        public async Task<BigTable> GetTableAsync( string name )
        {
            var request = new GetTableRequest {Name = name.ToTableUri(ClusterUri)};
            var response = await _client.GetTableAsync(request);
            return new BigTable(this, response);
        }

        public async Task DeleteTableAsync(BigTable table)
        {
            var request = new DeleteTableRequest {Name = table.Name.ToTableUri(ClusterUri)};
            await _client.DeleteTableAsync(request);
        }

        public async Task RenameTableAsync(BigTable table, string name)
        {
            var request = new RenameTableRequest {Name = table.Name.ToTableUri(ClusterUri), NewId = name };
            await _client.RenameTableAsync(request);
            table.Name = name;
        }

        public async Task<BigFamily> CreateFamilyAsync( BigTable table, string name, GcPolicy policy )
        {
            var request = new CreateColumnFamilyRequest
            {
                Name = table.Name.ToTableUri(ClusterUri),
                ColumnFamilyId = name,
                ColumnFamily = policy.ToColumnFamily(name)
            };

            var response = await _client.CreateColumnFamilyAsync(request);
            return new BigFamily(this, table, response);
        }

        public async Task<BigFamily> UpdateFamilyAsync(BigFamily family, GcPolicy policy)
        {
            var response = await _client.UpdateColumnFamilyAsync(policy.ToColumnFamily(family.Name));
            return new BigFamily(this, family.Table, response);
        }
        public async Task DeleteFamilyAsync(BigFamily family)
        {
            var request = new DeleteColumnFamilyRequest
            {
                Name = family.Name.ToFamilyUri(ClusterUri, family.Table.Name)
            };

            await _client.DeleteColumnFamilyAsync(request);
        }
    }
}
