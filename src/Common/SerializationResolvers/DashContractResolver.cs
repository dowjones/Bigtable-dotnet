using BigtableNet.Common.Extensions;
using Newtonsoft.Json.Serialization;

namespace BigtableNet.Common.SerializationResolvers
{
    /// <summary>
    /// Dashes should totally auto-map to camelCase.
    /// </summary>
    class DashContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.UnCamelCase();
        }
    }
}
