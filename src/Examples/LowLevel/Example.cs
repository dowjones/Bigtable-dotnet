using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common;
using Examples.Bootstrap;
using Examples.LowLevel.Instrumentation;
using Grpc.Core;

namespace Examples.LowLevel
{
    public static class Example
    {
        public static async Task Run()
        {
            // Disable Grpc logging
            GrpcEnvironment.DisableLogging();

            // Locals
            var client = default(SimpleClient);

            try
            {
                // Get examples config
                var config = Utilities.GetConfig();

                // Create a Native .Net BigTable Client
                client = new SimpleClient(config);

                // Connect to the Googles
                await client.Connect();

                // Get a list of table names
                var tableNames = client.GetTableNames().Result;

                // Ensure pricing table exists
                if (!tableNames.Value.Contains(Constants.PricingTable))
                {
                    // Inform user
                    CommandLine.InformUser("Setup", "Missing example table.  Please run the Examples.Bootstrap project.");

                    // Hard stop
                    return;
                }

                // Show the user
                DisplayTables(tableNames);

                // Wait for keypress
                CommandLine.WaitForUserAndThen("scan for rows");

                // Scan for data
                var rows = client.Scan(Constants.PricingTable, Constants.ScanKeyStart, Constants.ScanKeyEnd).Result;

                // Show the user
                DisplayRows(rows);

                // Wait for keypress
                CommandLine.WaitForUserAndThen("seek for row");

                // Seek for data
                var row = client.Seek(Constants.PricingTable, Constants.SeekKey).Result;

                // Show the user
                DisplayRows(row);
            }
            catch (Exception exception)
            {
                CommandLine.InformUser("Oops", "Example didn't work out as planned");
                CommandLine.RenderException(exception);
            }
            finally
            {
                // Pretty
                Console.WriteLine();

                // Dispose
                if (client != default(SimpleClient))
                    client.Dispose();

                // All done
                CommandLine.WaitForUserAndThen("exit");
            }
        }


        private static void DisplayRows(TimedOperation<Dictionary<string, Dictionary<string, string>>> timedOperation)
        {
            var rows = timedOperation.Value;
            foreach (var row in rows)
            {
                Console.WriteLine(new String('-', 20));
                Console.WriteLine("Key: " + row.Key);
                Console.WriteLine(new String('-', 20));
                foreach (var column in row.Value)
                {
                    Console.WriteLine(column.Key + " = " + column.Value);
                }
                Console.WriteLine(new String('-', 20));
                Console.WriteLine();
            }

            Console.WriteLine("Operation time: {0}ms", timedOperation.ElapsedMilliseconds);
            Console.WriteLine();
        }

        private static void DisplayTables(TimedOperation<IEnumerable<string>> timedOperation)
        {
            Console.WriteLine("Available Tables: " + String.Join(", ", timedOperation.Value));
            Console.WriteLine();
            Console.WriteLine("Operation time: {0}ms", timedOperation.ElapsedMilliseconds);
        }
    }
}
