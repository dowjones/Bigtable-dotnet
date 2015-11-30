using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common;
using Grpc.Core;
using Google.Apis.Auth.OAuth2;
using Grpc.Auth;

namespace BigtableNet.Models.Extensions
{
    internal static class ChannelExtensions
    {
        internal static Channel ToAdminChannel(this BigtableCredential credentials)
        {
            // Scope
            var scopedCreds = credentials.GoogleCredentials.CreateScoped(new[] {BigtableConstants.Scopes.Admin});

            // Convert
            var channelCreds = scopedCreds.ToChannelCredentials();

            // Connect
            return new Channel(BigtableConstants.EndPoints.Admin, channelCreds);
        }

        internal static Channel ToClusterChannel(this BigtableCredential credentials)
        {
            // Scope
            var scopedCreds = credentials.GoogleCredentials.CreateScoped(new[] { BigtableConstants.Scopes.ClusterAdmin });

            // Convert
            var channelCreds = scopedCreds.ToChannelCredentials();

            // Connect
            return new Channel(BigtableConstants.EndPoints.Admin, channelCreds);
        }

        internal static Channel ToDataChannel(this BigtableCredential credentials)
        {
            // Scope
            var scopedCreds = credentials.GoogleCredentials.CreateScoped(new[] { BigtableConstants.Scopes.Data });

            // Convert
            var channelCreds = scopedCreds.ToChannelCredentials();

            // Connect
            return new Channel(BigtableConstants.EndPoints.Data, channelCreds);
        }
        internal static Channel ToReadOnlyDataChannel(this BigtableCredential credentials)
        {
            // Scope
            var scopedCreds = credentials.GoogleCredentials.CreateScoped(new[] { BigtableConstants.Scopes.Readonly });

            // Convert
            var channelCreds = scopedCreds.ToChannelCredentials();

            // Connect
            return new Channel(BigtableConstants.EndPoints.Data, channelCreds);
        }
    }
}
