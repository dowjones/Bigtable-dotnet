namespace BigtableNet.Common
{
    /// <summary>
    /// A collection of constants as defined by the various portions of gRPC, protobufs, and the Bigtable API.
    /// </summary>
    public static class BigtableConstants
    {
        public static class Templates
        {
            // Each new term uses the last term as {0}
            public const string Project = "projects/{0}";
            public const string Zone = "{0}/zones/{1}";
            public const string Cluster = "{0}/clusters/{1}";
            public const string Table = "{0}/tables/{1}";
            public const string Family = "{0}/columnFamilies/{1}";

            // These are concatenated to the 
            public const string TableAdjunct = "/tables/";
            public const string FamilyAdjunct = "/columnFamilies/";
        }
        public static class EndPoints
        {
            public const string Admin = "bigtabletableadmin.googleapis.com";
            public const string Data = "bigtable.googleapis.com";
        }
        public static class EnvironmentVariables
        {
            /// <summary>
            /// Wehre gRPC will look for the .pem file
            /// </summary>
            public const string SslRootFilePath = "GRPC_DEFAULT_SSL_ROOTS_FILE_PATH";

            /// <summary>
            /// Where Google-code will look for credentials file
            /// </summary>
            public const string ApplicationCredentialsFilePath = "GOOGLE_APPLICATION_CREDENTIALS";
        }
        public static class Scopes
        {
            // What about this one?
            //"https://www.googleapis.com/auth/cloud-platform"
            
            /// <summary>
            /// AdminScope is the OAuth scope for Cloud Bigtable table admin operations.
            /// </summary>
            public static readonly string Admin = "https://www.googleapis.com/auth/bigtable.admin.table";

            /// <summary>
            /// Scope is the OAuth scope for Cloud Bigtable data operations.
            /// </summary>
            public static readonly string Data = "https://www.googleapis.com/auth/bigtable.data";

            /// <summary>
            /// ReadonlyScope is the OAuth scope for Cloud Bigtable read-only data operations.
            /// </summary>
            public static readonly string Readonly = "https://www.googleapis.com/auth/cloud-bigtable.data.readonly";

            /// <summary>
            /// ClusterAdminScope is the OAuth scope for Cloud Bigtable cluster admin operations.
            /// </summary>
            public static readonly string ClusterAdmin = "https://www.googleapis.com/auth/bigtable.admin.cluster";

            /// <summary>
            /// All scopes, used for service credentials
            /// </summary>
            public static readonly string[] All = {Admin, Data, ClusterAdmin};
        }
    }
}
