using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Models.Abstraction;
using BigtableNet.Models.Extensions;
using BigtableNet.Models.Types;
using Google.Apis.Auth.OAuth2;
using Google.Bigtable.Admin.Table.V1;

namespace BigtableNet.Models.Clients
{
    public class BigAdminClient : BigClient
    {
        #region - Private Member Variables -

        private readonly BigtableTableService.BigtableTableServiceClient _client;

        #endregion

        #region - Construction -

        public BigAdminClient(BigtableCredentials credentials, string project, string zone, string cluster, bool isReadOnly) : this(credentials, new BigtableConfig(project, zone, cluster), isReadOnly)
        {
        }

        public BigAdminClient(BigtableCredentials credentials, BigtableConfig config, bool isReadOnly = false) : base(config, credentials.CreateAdminChannel)
        {
            // Create
            _client = new BigtableTableService.BigtableTableServiceClient(Channel);
        }

        #endregion

        #region - Create Table Signature -

        /// <summary>
        /// Creates a table based on the prototype <see cref="BigTable"/>.  
        /// The initial split-keys can be passed in to specify tablets that should be pre-initialized.
        /// In general, it is best to allow the table to split automatically unless you understand BigTable indexing thoroughly.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="splitKeys"></param>
        /// <returns></returns>
        public async Task<BigTable> CreateTableAsync(BigTable table, IEnumerable<string> splitKeys = null)
        {
            return await CreateTableAsync(table.Name, table.Families, table.Encoding, splitKeys);
        }

        /// <summary>
        /// Creates a table based on the name and family names specified.  BigFamily defaults are used for column family configuration.
        /// The initial split-keys can be passed in to specify tablets that should be pre-initialized.
        /// In general, it is best to allow the table to split automatically unless you understand BigTable indexing thoroughly.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="familes"></param>
        /// <param name="encoding"></param>
        /// <param name="splitKeys"></param>
        /// <returns></returns>
        public async Task<BigTable> CreateTableAsync(string name, IEnumerable<string> familes = null, Encoding encoding = null, IEnumerable<string> splitKeys = null)
        {
            return await CreateTableAsync(name, (familes ?? Enumerable.Empty<string>()).Select(family => new BigFamily(family)), encoding, splitKeys);
        }

        /// <summary>
        /// Creates a table based on the name and families specified.
        /// The initial split-keys can be passed in to specify tablets that should be pre-initialized.
        /// In general, it is best to allow the table to split automatically unless you understand BigTable indexing thoroughly.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="familes"></param>
        /// <param name="encoding"></param>
        /// /// <param name="splitKeys"></param>
        /// <returns></returns>
        public async Task<BigTable> CreateTableAsync(string name, IEnumerable<BigFamily> familes, Encoding encoding = null, IEnumerable<string> splitKeys = null)
        {
            encoding = encoding ?? BigModel.DefaultEncoding;

            var request = new CreateTableRequest
            {
                Name = ClusterUri,
                TableId = name,
                Table = new Table()
            };
            if (splitKeys != null)
            {
                foreach (var key in splitKeys)
                {
                    request.InitialSplitKeys.Add(key);
                }
            }
            if (familes != null)
            {
                foreach (var family in familes)
                {
                    request.Table.ColumnFamilies.Add( family.Name, family.AsApiObject() );
                }
            }
            // This is suppose to support MICROS, but the protobuf doesn't specify it.
            // My guess is the value for that is is 1.
            request.Table.Granularity = Table.Types.TimestampGranularity.MILLIS;
            var response = await _client.CreateTableAsync(request);
            await Task.Yield();
            return new BigTable(response, encoding, ClusterUri);
        }

        #endregion

        #region - Get Tables Signature -

        public async Task<BigTable> GetTableAsync(BigTable table)
        {
            return await GetTableAsync(table.Name, table.Encoding);
        }

        public async Task<BigTable> GetTableAsync(string name, Encoding encoding = null)
        {
            encoding = encoding ?? BigModel.DefaultEncoding;
            var request = new GetTableRequest { Name = name.ToTableUri(ClusterUri) };
            var response = await _client.GetTableAsync(request);
            await Task.Yield();
            return new BigTable(response, encoding, ClusterUri);
        }

        #endregion

        #region - List Tables Signature -

        /// <summary>
        /// Retrives a list of tables from the server.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BigTable>> ListTablesAsync( Encoding encoding = null )
        {
            encoding = encoding ?? BigModel.DefaultEncoding;
            var request = new ListTablesRequest { Name = ClusterUri };
            var response = await _client.ListTablesAsync(request);
            await Task.Yield();
            var results = response.Tables.Select(table => new BigTable(table, encoding, ClusterUri));
            return results.ToArray();
        }


        #endregion

        #region - Delete Table Signature -

        public async Task DeleteTableAsync(BigTable table)
        {
            await DeleteTableAsync(table.Name);
        }

        public async Task DeleteTableAsync(string tableName)
        {
            var request = new DeleteTableRequest {Name = tableName.ToTableUri(ClusterUri)};
            await _client.DeleteTableAsync(request);
            await Task.Yield();
        }

        #endregion

        #region - Rename Table Signature -

        public async Task RenameTableAsync(BigTable table, string name)
        {
            await RenameTableAsync(table.Name, name);
            table.Name = name;
        }
        public async Task RenameTableAsync(string tableName, string name)
        {
            var request = new RenameTableRequest {Name = tableName.ToTableUri(ClusterUri), NewId = name };
            await _client.RenameTableAsync(request);
            await Task.Yield();
        }

        #endregion

        #region - Create Family Signature -

        public async Task<BigFamily> CreateFamilyAsync(BigTable table, string name, RetentionPolicy policy)
        {
            return await CreateFamilyAsync(table.Name, name, policy);
        }

        public async Task<BigFamily> CreateFamilyAsync(string tableName, string name, RetentionPolicy policy)
        {
            var tableUri = tableName.ToTableUri(ClusterUri);
            var request = new CreateColumnFamilyRequest
            {
                Name = tableUri,
                ColumnFamilyId = name,
                ColumnFamily = policy.ToColumnFamilyPrototype()
            };

            var response = await _client.CreateColumnFamilyAsync(request);
            await Task.Yield();
            return new BigFamily(response, tableUri);
        }

        #endregion

        #region - Update Family Signature -

        public async Task<BigFamily> UpdateFamilyAsync(BigTable table, BigFamily family, RetentionPolicy policy)
        {
            return await UpdateFamilyAsync(table.Name, family.Name, policy);
        }

        public async Task<BigFamily> UpdateFamilyAsync(string tableName, BigFamily family, RetentionPolicy policy)
        {
            return await UpdateFamilyAsync(tableName, family.Name, policy);
        }

        public async Task<BigFamily> UpdateFamilyAsync(string tableName, string familyName, RetentionPolicy policy)
        {
            var columnFamily = policy.ToColumnFamilyPrototype();
            columnFamily.Name = familyName.ToFamilyUri(ClusterUri, tableName);
            var response = await _client.UpdateColumnFamilyAsync(columnFamily);
            await Task.Yield();
            return new BigFamily(response, tableName.ToTableUri(ClusterUri));
        }

        #endregion

        #region - Delete Family Signatures -

        public async Task DeleteFamilyAsync(BigTable table, BigFamily family)
        {
            await DeleteFamilyAsync(table.Name, family.Name);
        }

        public async Task DeleteFamilyAsync(string tableName, BigFamily family)
        {
            await DeleteFamilyAsync(tableName, family.Name);
        }

        public async Task DeleteFamilyAsync(string tableName, string familyName)
        {
            var request = new DeleteColumnFamilyRequest
            {
                Name = familyName.ToFamilyUri(ClusterUri, tableName)
            };

            await _client.DeleteColumnFamilyAsync(request);
            await Task.Yield();
        }

        #endregion
    }
}
