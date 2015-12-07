using System;

namespace BigtableNet.Common.Extensions
{
    public static class BigtableConfigExtensions
    {
        public static string ToProjectUri(this BigtableConfig config)
        {
            if (String.IsNullOrEmpty(config.Cluster))
            {
                throw new ApplicationException("Missing or invalid config.Project");
            }

            return String.Format(BigtableConstants.Templates.Project, config.Project);
        }

        public static string ToZoneUri(this BigtableConfig config)
        {
            if (String.IsNullOrEmpty(config.Zone))
            {
                return config.ToProjectUri();
            }

            return String.Format(BigtableConstants.Templates.Zone, config.ToProjectUri(), config.Zone);
        }

        public static string ToClusterUri(this BigtableConfig config)
        {
            if (String.IsNullOrEmpty(config.Cluster))
            {
                return config.ToZoneUri();
            }

            return String.Format(BigtableConstants.Templates.Cluster, config.ToZoneUri(), config.Cluster);
        }
    }
}
