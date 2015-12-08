using Grpc.Core;

namespace BigtableNet.Common.Customization
{
    /// <summary>
    /// Provides a wraper to Grpc adjustments so that they're easier to find.
    /// </summary>
    public static class BigtableTransportLayer
    {
        public static int SetThreadPoolSize(int threadCount)
        {
            return GrpcEnvironment.SetThreadPoolSize(threadCount);
        }

        public static void DisableLogger()
        {
            GrpcEnvironment.DisableLogging();
        }

        public static void RedirectLogging(GrpcLoggingAdaptor loggingAdaptor )
        {
            GrpcEnvironment.SetLogger(loggingAdaptor);
        }
    }
}
