//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BigtableNet.Native
//{
//    /// <summary>
//    /// A collection of constants as defined by the various portions of gRPC, protobufs, and the Bigtable API.
//    /// </summary>
//    public static class Constants
//    {
//        public static class Scopes
//        {
//            public static readonly string BigtableAdminScope = "https://www.googleapis.com/auth/bigtable.admin.table";
//            public static readonly string BigtableScope = "https://www.googleapis.com/auth/bigtable.data";

//            /// <summary>
//            /// The OAuth scope required to perform administrator actions such as creating tables.
//            /// </summary>
//            public static string CLOUD_BIGTABLE_ADMIN_SCOPE = "https://www.googleapis.com/auth/cloud-bigtable.admin";
//            /// <summary>
//            /// The OAuth scope required to read data from tables.
//            /// </summary>
//            public static string CLOUD_BIGTABLE_READER_SCOPE = "https://www.googleapis.com/auth/cloud-bigtable.data.readonly";

//            /// <summary>
//            /// The OAuth scope required to write data to tables.
//            /// </summary>
//            public static string CLOUD_BIGTABLE_WRITER_SCOPE = "https://www.googleapis.com/auth/cloud-bigtable.data";

//            /// <summary>
//            /// Scopes required to read and write data from tables.
//            /// </summary>
//            public static string[] CLOUD_BIGTABLE_READ_WRITE_SCOPES =
//            {
//                CLOUD_BIGTABLE_READER_SCOPE,
//                CLOUD_BIGTABLE_WRITER_SCOPE
//            };


//            /// <summary>
//            /// Scopes required for full access to cloud bigtable.
//            /// </summary>
//            public static string[] CLOUD_BIGTABLE_ALL_SCOPES =
//            {
//                CLOUD_BIGTABLE_READER_SCOPE,
//                CLOUD_BIGTABLE_WRITER_SCOPE,
//                CLOUD_BIGTABLE_ADMIN_SCOPE
//            };
//        }
//    }
//}
