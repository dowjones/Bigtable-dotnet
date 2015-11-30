using System;

namespace BigtableNet.Common
{
    [Flags]
    public enum BigtablePermissions
    {
        None = 0,
        ReadOnly = 1,
        Data = 2,
        Admin = 4,
        ClusterAdmin = 8,

        Full = Data | Admin | ClusterAdmin
    }
}
