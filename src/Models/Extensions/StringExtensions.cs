using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Models.Clients;
using Google.Protobuf;

namespace BigtableNet.Models.Extensions
{
    public static class StringExtensions
    {
        internal static string ToClusterId(this string clusterName, string zoneId)
        {
            return String.Format(BigtableConstants.Templates.Cluster, zoneId, clusterName);
        }

        internal static string ToTableId(this string tableName, string clusterId)
        {
            return String.Format(BigtableConstants.Templates.Table, clusterId, tableName);
        }

        internal static string ToFamilyId(this string familyName, string clusterId, string tableName)
        {
            return String.Format(BigtableConstants.Templates.Family, tableName.ToTableId(clusterId), familyName);
        }

        public static ByteString ToByteString(this string value, Encoding encoding)
        {
            if( String.IsNullOrEmpty(value) )
                return ByteString.Empty;

            return ByteString.CopyFrom(value, encoding);
        }

    }
}
