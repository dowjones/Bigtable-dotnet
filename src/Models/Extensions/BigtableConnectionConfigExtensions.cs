using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common;

namespace BigtableNet.Models.Extensions
{
    internal static class BigtableConnectionConfigExtensions
    {
        internal static string ToProjectUri(this BigtableConnectionConfig config)
        {
            return String.Format(BigtableConstants.Templates.Project, config.Project);
        }

        internal static string ToZoneUri(this BigtableConnectionConfig config)
        {
            return String.Format(BigtableConstants.Templates.Zone, config.ToProjectUri(), config.Zone);
        }

        internal static string ToClusterUri(this BigtableConnectionConfig config)
        {
            return String.Format(BigtableConstants.Templates.Cluster, config.ToZoneUri(), config.Cluster);
        }
    }
}
