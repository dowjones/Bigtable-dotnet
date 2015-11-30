using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using BigtableNet.Common;
using Google.Apis.Auth.OAuth2;

namespace BigtableNet.Models
{
    public class BigtableCredential
    {
        public GoogleCredential GoogleCredentials { get; private set; }

        private BigtableCredential(GoogleCredential googleCredentials)
        {
            GoogleCredentials = googleCredentials;
        }

        public static async Task<BigtableCredential> UseConfigAsync(BigtableConnectionConfig config)
        {
            if (String.IsNullOrEmpty(config.CredentialsFile))
                throw new ArgumentNullException("config.Credentials", "Missing credentials path and file");

            var path = Path.GetFullPath(config.CredentialsFile);
            SetJsonCredentialsFilePath(path);

            return await UseEnvironmentAsync();
        }

        public static async Task<BigtableCredential> UseEnvironmentAsync()
        {
            // Find deployment dir
            var path = Path.GetFullPath(Process.GetCurrentProcess().MainModule.FileName);

            // Set pem file environment variable
            SetDefaultSslRootFilePath(path);

            // Make sure user did it right!
            EnsureEnvironmentVariableExists(BigtableConstants.EnvironmentVariables.ApplicationCredentialsFilePath);
            EnsureEnvironmentVariableExists(BigtableConstants.EnvironmentVariables.SslRootFilePath);

            // Get credential
            var credentials = await GoogleCredential.GetApplicationDefaultAsync();

            // Return results
            return new BigtableCredential(credentials);
        }


        public static async Task<BigtableCredential> UseApplicationDefaultCredentialsAsync(BigtablePermissions permissions)
        {
            // Find deployment dir
            var path = Directory.GetParent(Process.GetCurrentProcess().MainModule.FileName).ToString();

            // Set pem file environment variable
            SetDefaultSslRootFilePath(path);

            // Make sure user did it right!
            if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable(BigtableConstants.EnvironmentVariables.ApplicationCredentialsFilePath)))
                throw new ApplicationException("The {0} environment variable must be set to the path of your key file.");

            // Get credential
            var credentials = await GoogleCredential.GetApplicationDefaultAsync();

            // Return results
            return new BigtableCredential(credentials);
        }

        private static void SetJsonCredentialsFilePath(string path)
        {
            Environment.SetEnvironmentVariable(BigtableConstants.EnvironmentVariables.ApplicationCredentialsFilePath, path);
        }

        private static void SetDefaultSslRootFilePath(string path)
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
