using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using BigtableNet.Common;
using Examples.Bootstrap;
using Examples.Web.Helpers;
using Nancy;
using Nancy.Hosting.Self;

namespace Examples.Web
{
    public static class Example
    {
        public static void Run(int port)
        {
            try
            {
                // Use DI container
                using (var container = BuildContainer())
                {
                    // Bootstrap the client factory with the config
                    var clientFactory = container.Resolve<IClientFactory>();

                    // Start Nancy Web Host
                    StaticConfiguration.DisableErrorTraces = false;
                    var uri = new Uri(string.Format("http://localhost:{0}", port));
                    var hostConfig = new HostConfiguration {AllowChunkedEncoding = false};
                    var host = new NancyHost(uri, new Bootstrapper(container), hostConfig);
                    host.Start();

                    // Run until user exits
                    CommandLine.InformUser("Web", "Listening for requests on: {0}", uri);
                    CommandLine.WaitForUserAndThen("exit");
                }
            }
            catch (Exception exception)
            {
                CommandLine.InformUser("Oops", "Example didn't work out as planned");
                CommandLine.RenderException(exception);

                // Wait for developer to review exception
                if (Debugger.IsAttached)
                {
                    CommandLine.WaitForUserAndThen("exit");
                }
            }
        }

        private static IContainer BuildContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<ClientFactory>().As<IClientFactory>().SingleInstance();
            builder.RegisterType<TableNameCache>().As<ITableNameCache>().SingleInstance();

            return builder.Build();
        }
    }
}
