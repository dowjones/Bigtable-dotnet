using System;
using System.Diagnostics;
using System.IO;
using System.Security.Authentication;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Grpc.Auth;
using Grpc.Core;
using System.Security.Cryptography.X509Certificates;

namespace BigtableNet.Common
{
    /// <summary>
    /// This class provide a single point of access for the different ways you can configure credentials.
    /// With nothing set Application Default Credentials will attempt to find the needed files through it's 
    /// internal paths search.
    /// </summary>
    public class BigtableCredentials
    {
        private readonly GoogleCredential _googleCredentials;
        private readonly ComputeCredential _computeCredentials;
        private readonly ServiceAccountCredential _serviceCredentials;

        /// <summary>
        /// Scopes can be used
        /// </summary>
        public BigtableCredentials(GoogleCredential googleCredentials)
        {
            _googleCredentials = googleCredentials;
        }

        /// <summary>
        /// Scoped cannot be used
        /// </summary>
        public BigtableCredentials(ComputeCredential computeCredentials)
        {
            _computeCredentials = computeCredentials;
        }

        /// <summary>
        /// Scoped cannot be used, but full access is specified.
        /// UNTESTED
        /// </summary>
        public BigtableCredentials(string certificateFile, string serviceAccountEmail, string code)
        {
            // Contract
            certificateFile = Path.GetFullPath(certificateFile);
            if (!File.Exists(certificateFile))
                throw new FileNotFoundException("Can't find specified credentials file.", certificateFile);

            // Load certificate and assemble credentials by hand
            var certificate = new X509Certificate2(certificateFile, code, X509KeyStorageFlags.Exportable);
            _serviceCredentials = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(serviceAccountEmail)
                {
                    Scopes = BigtableConstants.Scopes.All
                }.FromCertificate(certificate));
        }

        /// <summary>
        /// Scoped cannot be used, but full access is specified.
        /// UNTESTED
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="serviceAccountEmail"></param>
        public BigtableCredentials(string privateKey, string serviceAccountEmail)
        {
            // Load certificate and assemble credentials by hand
            _serviceCredentials = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(serviceAccountEmail)
                {
                    Scopes = BigtableConstants.Scopes.All
                }.FromPrivateKey(privateKey));
        }

        /// <summary>
        /// Provided for dependency injection, this will use default application credentials.
        /// </summary>
        public BigtableCredentials()
        {
            // This is totally cheating
            _googleCredentials = UseApplicationDefaultCredentialsAsync().Result._googleCredentials;
        }

        /// <summary>
        /// Uses the file specified.
        /// </summary>
        public BigtableCredentials(string filename)
        {
            // Under-lying mechanism will path-search, so explode because we won't have loaded this file
            filename = Path.GetFullPath(filename);
            if( !File.Exists(filename)) 
                throw new FileNotFoundException("Can't find specified credentials file.", filename);

            // Set credentials path
            SetCredentialsFilePath(filename);

            // This is totally cheating
            _googleCredentials = UseEnvironmentAsync().Result._googleCredentials;
        }

        /// <summary>
        /// Uses GOOGLE_APPLICATION_CREDENTIALS to locate key file.
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

        public Channel CreateAdminChannel()
        {
            // Get channel creds
            var channelCreds = CreatedScopedChannelCredentials(BigtableConstants.Scopes.Admin);

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
            // Get channel creds
            var channelCreds = CreatedScopedChannelCredentials(isReadOnly ? BigtableConstants.Scopes.Readonly : BigtableConstants.Scopes.Data);

            // Connect
            return new Channel(BigtableConstants.EndPoints.Data, channelCreds);
        }


        // Test this, I'm not entirely sure this is the right end-point
        // UNTESTED
        public Channel CreateClusterChannel()
        {
            // Get channel creds
            var channelCreds = CreatedScopedChannelCredentials(BigtableConstants.Scopes.ClusterAdmin);
            
            // Connect
            return new Channel(BigtableConstants.EndPoints.Admin, channelCreds);
        }

        private ChannelCredentials CreatedScopedChannelCredentials(params string[] scopes)
        {
            if (_computeCredentials != null)
            {
                return  _computeCredentials.ToChannelCredentials();
            }
            else if (_googleCredentials != null)
            {
                return _googleCredentials.CreateScoped(scopes).ToChannelCredentials();
            }
            else if (_serviceCredentials != null)
            {
                return _serviceCredentials.ToChannelCredentials();
            }
            else
            {
                throw new InvalidCredentialException("Credentials were not set");
            }
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

    }
}
