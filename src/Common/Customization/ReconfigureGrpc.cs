using Grpc.Core;

namespace BigtableNet.Common.Customization
{
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
