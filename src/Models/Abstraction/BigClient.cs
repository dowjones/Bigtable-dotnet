using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Models.Extensions;
using Google.Bigtable.Admin.Table.V1;

namespace BigtableNet.Models.Abstraction
{
    public class BigClient
    {
        protected readonly BigtableConnectionConfig Config;


        public string Project { get { return Config.Project; } }

        public string Zone { get { return Config.Zone; } }

        public string Cluster { get { return Config.Cluster; } }

        public BigClient(BigtableConnectionConfig config)
        {
            // Store
            //_credentials = credentials;
            Config = config;
        }
    }
}
