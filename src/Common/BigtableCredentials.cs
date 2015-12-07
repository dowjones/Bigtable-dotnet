using System;
using System.Diagnostics;
using System.IO;
using System.Security.Authentication;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Grpc.Auth;
using Grpc.Core;

namespace BigtableNet.Common
{
    /// <summary>
    /// Use of this class is optional.  This will wrap the GoogleCredentials object, using the necironment or allowing a path to be specified.
    /// </summary>
    public class BigtableCredentials
    {
        public GoogleCredential GoogleCredentials { get; private set; }
        public ComputeCredential ComputeCredentials { get; private set; }

        public BigtableCredentials(GoogleCredential googleCredentials)
        {
            GoogleCredentials = googleCredentials;
        }

        public BigtableCredentials(ComputeCredential computeCredentials)
        {
            
        }
        /// <summary>
        /// Provided for dependency injection, this will use default application credentials.
        /// </summary>
        public BigtableCredentials()
        {
            GoogleCredentials = UseApplicationDefaultCredentialsAsync().Result.GoogleCredentials;
        }

        /// <summary>
        /// Provided for dependency injection, this will use default application credentials, but will set the environment variable.
        /// </summary>
        public BigtableCredentials(string fileName)
        {
            SetCredentialsFilePath(fileName);
            GoogleCredentials = UseApplicationDefaultCredentialsAsync().Result.GoogleCredentials;
        }

        /// <summary>
        /// Uses GOOGLE_APPLICATION_CREDENTIALS to locate file
        /// </summary>
        /// <returns></returns>
        public static async Task<BigtableCredentials> UseEnvironmentAsync()
        {
            // Hookup .pem file
            SetDefaultSslKeyFilePath();

            // Make sure user did it right!
            EnsureEnvironmentVariableExists(BigtableConstants.EnvironmentVariables.ApplicationCredentialsFilePath);
            EnsureEnvironmentVariableExists(BigtableConstants.EnvironmentVariables.SslRootFilePath);

            // Get credential
            var credentials = await GoogleCredential.GetApplicationDefaultAsync();

            // Return results
            return new BigtableCredentials(credentials);
        }

        /// <summary>
        /// Will use compute credentials if running on compute engine, otherwise will use environment variables
        /// </summary>
        /// <returns></returns>
        public static async Task<BigtableCredentials> UseApplicationDefaultCredentialsAsync()
        {
            if (await ComputeCredential.IsRunningOnComputeEngine())
            {
                // Hookup .pem file
                SetDefaultSslKeyFilePath();

                // Get credential
                var credentials = new ComputeCredential(new ComputeCredential.Initializer());

                // Return results
                return new BigtableCredentials(credentials);
            }

            // Use environment
            return await UseEnvironmentAsync();
        }


        private static void SetDefaultSslKeyFilePath()
        {
            // Find deployment dir
            var path = Directory.GetParent(Process.GetCurrentProcess().MainModule.FileName).ToString();

            // Set pem file environment variable
            SetSslKeyFilePath(path);
        }

        public static void SetSslKeyFilePath(string path)
        {
            Environment.SetEnvironmentVariable(BigtableConstants.EnvironmentVariables.SslRootFilePath, Path.Combine(path, "google-bigtable.pem"));
        }

        public static void SetCredentialsFilePath(string path)
        {
            Environment.SetEnvironmentVariable(BigtableConstants.EnvironmentVariables.SslRootFilePath, Path.Combine(path, "google-bigtable.pem"));
        }

        private static void EnsureEnvironmentVariableExists(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ApplicationException(String.Format("The {0} environment variable must be set.", name));
        }











        public Channel CreateAdminChannel()
        {
            // Locals
            var channelCreds = default(ChannelCredentials);

            // Get channel creds
            if (GoogleCredentials != null)
            {
                channelCreds = GoogleCredentials.CreateScoped(new[] { BigtableConstants.Scopes.Admin }).ToChannelCredentials();
            }
            else if (ComputeCredentials != null)
            {
                channelCreds = ComputeCredentials.ToChannelCredentials();
            }
            else
            {
                throw new InvalidCredentialException("Credentials were not set");
            }

            // Connect
            return new Channel(BigtableConstants.EndPoints.Admin, channelCreds);
        }

        /// <summary>
        /// If ComputeCredentials are used, readonly will not be honored
        /// </summary>
        /// <param name="isReadOnly"></param>
        /// <returns></returns>
        public Channel CreateDataChannel( bool isReadOnly )
        {
            // Locals
            var channelCreds = default(ChannelCredentials);

            // Get channel creds
            if (GoogleCredentials != null)
            {
                channelCreds = GoogleCredentials.CreateScoped(isReadOnly ? new[] { BigtableConstants.Scopes.Readonly } : new[] { BigtableConstants.Scopes.Data }).ToChannelCredentials();
            }
            else if (ComputeCredentials != null)
            {
                channelCreds = ComputeCredentials.ToChannelCredentials();
            }
            else
            {
                throw new InvalidCredentialException("Credentials were not set");
            }

            // Connect
            return new Channel(BigtableConstants.EndPoints.Data, channelCreds);
        }

        public Channel CreateClusterChannel()
        {
            // Locals
            var channelCreds = default(ChannelCredentials);

            // Get channel creds
            if (GoogleCredentials != null)
            {
                channelCreds = GoogleCredentials.CreateScoped(new[] { BigtableConstants.Scopes.ClusterAdmin }).ToChannelCredentials();
            }
            else if (ComputeCredentials != null)
            {
                channelCreds = ComputeCredentials.ToChannelCredentials();
            }
            else
            {
                throw new InvalidCredentialException("Credentials were not set");
            }

            // Connect
            return new Channel(BigtableConstants.EndPoints.Admin, channelCreds);
        }
    }
}
