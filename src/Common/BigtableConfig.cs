using System.IO;
using System.Threading.Tasks;
using BigtableNet.Common.SerializationResolvers;
using Newtonsoft.Json;

namespace BigtableNet.Common
{
    public class BigtableConfig
    {
        // Allow serializer to be configured, for whatever reason.
        public static JsonSerializerSettings SerializerSettings { get; set; }

        public string Project { get; set; }

        public string Zone { get; set; }

        public string Cluster { get; set; }

        static BigtableConfig()
        {
            SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DashContractResolver()
            };
        }

        public BigtableConfig(string project, string zone, string cluster)
        {
            Project = project;
            Zone = zone;
            Cluster = cluster;
        }

        public void Save(string fileWithPath)
        {
            SaveAsync(fileWithPath).Wait();
        }

        public async Task SaveAsync(string fileWithPath)
        {
            using (TextWriter writer = new StreamWriter(File.OpenWrite(fileWithPath)))
            {
                var text = JsonConvert.SerializeObject(this, SerializerSettings);
                await writer.WriteAsync(text);
            }
        }


        public static BigtableConfig Load(string fileWithPath)
        {
            return LoadAsync(fileWithPath).Result;
        }

        public static async Task<BigtableConfig> LoadAsync(string fileWithPath)
        {
            using (TextReader reader = File.OpenText(fileWithPath))
            {
                var text = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<BigtableConfig>(text, SerializerSettings);
            }
        }
    }
}
