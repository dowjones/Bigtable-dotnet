using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common.SerializationResolvers;
using Newtonsoft.Json;

namespace BigtableNet.Common
{
    public static class Extensions
    {

        internal static readonly JsonSerializerSettings SerializerSettings;

        static Extensions()
        {
            SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DashContractResolver()
            };
        }


        public static void Save(this BigtableConnectionConfig config, string fileWithPath)
        {
            config.SaveAsync(fileWithPath).Wait();
        }

        public static async Task SaveAsync(this BigtableConnectionConfig config, string fileWithPath)
        {
            using (TextWriter writer = new StreamWriter(File.OpenWrite(fileWithPath)))
            {
                var text = JsonConvert.SerializeObject(config, SerializerSettings);
                await writer.WriteAsync(text);
            }
        }
    }
}
