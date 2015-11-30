using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Models.Abstraction;
using Google.Bigtable.Admin.Table.V1;

namespace BigtableNet.Models
{
    /// <summary>
    /// Based on the golang bigtable package
    /// </summary>
    public class BigTable : BigModel
    {
        private BigtableConnectionConfig _config;
        private BigtableCredential _permissions;
        private Table _table;
        private BigAdminClient _adminClient;
        private BigtableTableService.BigtableTableServiceClient _client;
        private IEnumerable<BigFamily> _familes;
        private string _name;

        public Encoding Encoding { get; set; }


        internal BigTable(BigAdminClient adminClient, Table table)
        {
            Encoding = Encoding.UTF8;


            _adminClient = adminClient;
            _table = table;

            Name = table.Name.Replace(adminClient.ClusterUri + BigtableConstants.Templates.TableAdjunct, "");
        }

        public void Delete()
        {
            DeleteAsync().Wait();
        }
        public async Task DeleteAsync()
        {
            await _adminClient.DeleteTableAsync(this);
        }

        

    }
}
