using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Models;
using BigtableNet.Models.Clients;
using Examples.Bootstrap;

namespace Examples.Web.Helpers
{
    public interface IClientFactory
    {
        BigAdminClient GetAdminClient();
        BigDataClient GetDataClient();
    }



    public class ClientFactory : IClientFactory
    {
        private readonly BigtableConfig _config;
        private readonly BigtableCredentials _credentials;

        public ClientFactory()
        {
            _config = Utilities.GetConfig();
            _credentials = BigtableCredentials.UseApplicationDefaultCredentialsAsync().Result;
        }

        public BigAdminClient GetAdminClient()
        {
            return new BigAdminClient(_credentials, _config);
        }


        public BigDataClient GetDataClient()
        {
            return new BigDataClient(_credentials, _config);
        }
    }
}
