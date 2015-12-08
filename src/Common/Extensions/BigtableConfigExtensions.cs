using System;

namespace BigtableNet.Common.Extensions
{
    public static class BigtableConfigExtensions
    {
        public static string ToProjectId(this BigtableConfig config)
        {
            if (String.IsNullOrEmpty(config.Cluster))
            {
                throw new ApplicationException("Missing or invalid config.Project");
            }

            return String.Format(BigtableConstants.Templates.Project, config.Project);
        }

        public static string ToZoneId(this BigtableConfig config)
        {
            if (String.IsNullOrEmpty(config.Zone))
            {
                return config.ToProjectId();
            }

            return String.Format(BigtableConstants.Templates.Zone, config.ToProjectId(), config.Zone);
        }

        public static string ToClusterId(this BigtableConfig config)
        {
            if (String.IsNullOrEmpty(config.Cluster))
            {
                return config.ToZoneId();
            }

            return String.Format(BigtableConstants.Templates.Cluster, config.ToZoneId(), config.Cluster);
        }
    }
}
