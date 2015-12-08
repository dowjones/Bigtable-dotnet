using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Autofac;
using BigtableNet.Common;
using Examples.Bootstrap;
using Examples.Web.Helpers;
using Nancy;
using Nancy.Hosting.Self;

namespace Examples.Web
{
    class EntryPoint
    {
        private const int Port = 8913;

        static void Main( string[] args )
        {
            // Custom port?
            var port = Port;
            if (args != null && args.Any())
            {
                if (!int.TryParse(args[0], out port))
                {
                    CommandLine.InformUser("Oops", "Usage: Examples.Web [port]");
                }
            }

            // Run example
            Example.Run(port);
        }
    }
}
