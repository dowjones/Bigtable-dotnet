using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common;

namespace BigtableNet.Models.Extensions
{
    internal static class EnumerableExtensions
    {
        internal static IEnumerable<string> ToScope(this BigtablePermissions permissions)
        {
            // Readonly is returned without accumulation
            if (permissions == BigtablePermissions.ReadOnly)
                return new[] { BigtableConstants.Scopes.Readonly };

            // Locals
            var results = new List<string>();

            // Add data
            if ((permissions & BigtablePermissions.Data) != 0)
                results.Add(BigtableConstants.Scopes.Data);

            // Add admin
            if ((permissions & BigtablePermissions.Admin) != 0)
                results.Add(BigtableConstants.Scopes.Admin);

            // Add cluster admin
            if ((permissions & BigtablePermissions.ClusterAdmin) != 0)
                results.Add(BigtableConstants.Scopes.ClusterAdmin);

            return results;
        }
    }
}
