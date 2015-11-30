using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BigtableNet.Common
{
    public class BigtableConnectionConfig
    {
        public string Project { get; set; }

        public string Zone { get; set; }

        public string Cluster { get; set; }

        public string CredentialsFile { get; set; }

        public static BigtableConnectionConfig Load(string fileWithPath)
        {
            return LoadAsync(fileWithPath).Result;
        }

        public static async Task<BigtableConnectionConfig> LoadAsync(string fileWithPath)
        {
            using (TextReader reader = File.OpenText(fileWithPath))
            {
                var text = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<BigtableConnectionConfig>(text, Extensions.SerializerSettings);
            }
        }

        public static BigtableConnectionConfig Create(string project, string zone, string cluster, string credentialsFile = null)
        {
            return new BigtableConnectionConfig
            {
                Project = project,
                Zone = zone,
                Cluster = cluster,
                CredentialsFile = credentialsFile
            };
        }
    }
}
