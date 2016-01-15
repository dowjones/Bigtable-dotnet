using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer.Collaboration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Models;
using BigtableNet.Models.Clients;
using BigtableNet.Models.Extensions;
using BigtableNet.Models.Types;
using Examples.Bootstrap;
using Google.Bigtable.V1;
using Grpc.Core;

namespace Examples.Modeled
{
    public static class Example
    {
        public static async Task Run()
        {
            // Disable Grpc logging
            GrpcEnvironment.DisableLogging();

            try
            {
                // Create config
                var config = Utilities.GetConfig();

                // Create credentials
                var credentials = await BigtableCredentials.UseApplicationDefaultCredentialsAsync();

                // Admin client
                using (var adminClient = new BigAdminClient(credentials, config))
                {
                    // Get tables
                    var tables = (await adminClient.ListTablesAsync()).ToArray();

                    // Ensure pricing table exists
                    if (tables.All(t => t.Name != Constants.PricingTable))
                    {
                        // Inform user
                        CommandLine.InformUser("Setup", "Missing example table.  Please run the Examples.Bootstrap project.");

                        // Hard stop
                        return;
                    }

                    // Show user
                    CommandLine.DisplayTables(tables);
                }

                // Wait for keypress
                CommandLine.WaitForUserAndThen("scan for rows");

                // Data client
                using (var dataClient = new BigDataClient(credentials, config))
                {
                    // Target
                    var pricingTable = new BigTable(Constants.PricingTable);

                    // Scan pricing table
                    var pricing = await dataClient.GetRowsAsync(pricingTable, Constants.ScanKeyStart, Constants.ScanKeyEnd, Constants.ScanLimit);

                    // Show the user
                    CommandLine.DisplayRows(pricing);

                    // Wait for keypress
                    CommandLine.WaitForUserAndThen("observe rows");

                    // Test observables
                    var waitSource = new CancellationTokenSource();
                    // ReSharper disable once MethodSupportsCancellation
                    var waitForObservation = Task.Run(() => Task.Delay(-1, waitSource.Token));
                    // Create a subscriber
                    var subscriber = new TestSubscriber(()=>waitSource.Cancel());
                    // ReSharper disable once MethodSupportsCancellation
                    var observable = dataClient.ObserveRows(pricingTable, Constants.ScanKeyStart, Constants.ScanKeyEnd, Constants.ScanLimit);
                    using (observable.Subscribe(subscriber))
                    {
                        //await Task.Delay(1000);
                        Task.WaitAny(waitForObservation);
                    }

                    // Wait for keypress
                    CommandLine.WaitForUserAndThen("seek for row");

                    // Seek for data
                    var row = await dataClient.GetRowAsync(pricingTable, Constants.SeekKey);

                    // Show the user
                    CommandLine.DisplayRows(new[] {row});
                }
            }
            catch (Exception exception)
            {
                CommandLine.InformUser("Oops", "Example didn't work out as planned");
                CommandLine.RenderException(exception);
            }
            finally
            {
                // All done
                CommandLine.WaitForUserAndThen("exit");
            }
        }
    }
}
