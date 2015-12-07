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
        internal static string ToClusterUri(this string clusterName, string zoneUri)
        {
            return String.Format(BigtableConstants.Templates.Cluster, zoneUri, clusterName);
        }

        internal static string ToTableUri(this string tableName, string clusterUri)
        {
            return String.Format(BigtableConstants.Templates.Table, clusterUri, tableName);
        }

        internal static string ToFamilyUri(this string familyName, string clusterUri, string tableName)
        {
            return String.Format(BigtableConstants.Templates.Family, tableName.ToTableUri(clusterUri), familyName);
        }

        public static ByteString ToByteString(this string value, Encoding encoding)
        {
            if( String.IsNullOrEmpty(value) )
                return ByteString.Empty;

            return ByteString.CopyFrom(value, encoding);
        }

    }
}
