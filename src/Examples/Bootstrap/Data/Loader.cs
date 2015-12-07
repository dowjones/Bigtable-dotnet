using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Common.Extensions;
using BigtableNet.Models.Clients;
using BigtableNet.Models.Types;
using Grpc.Core;

namespace Examples.Bootstrap.Data
{
    class Loader : IDisposable
    {
        private const int BatchAndPoolSize = 10;
        private readonly BigAdminClient _adminClient;
        private readonly BigDataClient _dataClient;

        internal bool NoExampleTables;
        internal bool NonexampleTables;
        internal bool SomeExampleTables;
        internal bool AllExampleTables;
        private IEnumerable<BigTable> _existingTables;

        public Loader(BigtableCredentials credentials, BigtableConfig config)
        {
            GrpcEnvironment.SetThreadPoolSize(BatchAndPoolSize);

            _adminClient = new BigAdminClient(credentials, config);
            _dataClient = new BigDataClient(credentials, config);
        }

        public async Task Initialize()
        {
            _existingTables = await _adminClient.ListTablesAsync();
            var tableNames = _existingTables.Select(x => x.Name).OrderBy(x => x);
            var exampleNames = Constants.ExamplesTables.OrderBy(x => x);
            NonexampleTables = tableNames.All(t => exampleNames.Contains(t));
            if (exampleNames.All(tableName => tableNames.Contains(tableName)))
            {
                AllExampleTables = true;
            }
            else if (exampleNames.Any(tableName => tableNames.Contains(tableName)))
            {
                SomeExampleTables = true;
            }
            else
            {
                NoExampleTables = true;
            }
        }

        private async Task LoadTables()
        {
            if (NonexampleTables)
            {
                if (!AskUserPermission("Other tables exist in this cluster.", "They will not be modified, but, to be sure..."))
                    return;
            }

            await LoadPricing();
        }


        public async Task LoadPricing()
        {
            var tableName = Constants.PricingTable;
            await CreateTable(tableName);

            CommandLine.InformUser("Loader", "Generating data for " + tableName);
            var rows = Generator.GeneratePricing(Constants.PricingIdsToCreate, Constants.PricingRowsPerId);

            var count = 0;
            const int batchSize = BatchAndPoolSize;
            const int total = Constants.PricingIdsToCreate*Constants.PricingRowsPerId/batchSize;
            var cursor = Console.CursorTop;
            foreach (var batch in rows.Batch(batchSize))
            {
                var rowTasks = batch.Select(row => Task.Run(() =>
                {
                    // Get key
                    var key = (string) row["Key"];

                    // Remove from hash for ease
                    row.Remove("Key");

                    // Write the row
                    _dataClient.WriteRowAsync(tableName, key, row.Select(kv => BigChange.CreateCellUpdate(Constants.ColumnFamilyName, kv.Key, kv.Value.ToString()))).Wait();


                })).ToArray();

                // Keep user updated
                Console.CursorTop = cursor;
                CommandLine.InformUser("Loader", String.Format("Writing {0}: {1}%", tableName, count++ * 100 / total));

                Task.WhenAll(rowTasks).Wait();      // silly syntax to avoid resharper warning
            }

            // Keep user updated
            Console.CursorTop = cursor;
            CommandLine.InformUser("Loader", String.Format("Wrote {0}: 100%     ", tableName));
        }

        private async Task CreateTable(string tableName)
        {
            if (AllExampleTables)
            {
                // Drop & Recreate
                await _adminClient.DeleteTableAsync(tableName);
                CommandLine.InformUser("Loader", "Dropped " + tableName);
            }
            if (AllExampleTables || _existingTables.All(t => t.Name != tableName))
            {
                // Create
                await _adminClient.CreateTableAsync(tableName, new[] {Constants.ColumnFamilyName});
                CommandLine.InformUser("Loader", "Created " + tableName);
            }
        }

        public static async Task Run(BigtableConfig config)
        {
            GrpcEnvironment.DisableLogging();
            try
            {
                var credentials = await BigtableCredentials.UseApplicationDefaultCredentialsAsync();

                // Admin client
                using (var loader = new Loader(credentials, config))
                {
                    // Examine cluster
                    await loader.Initialize();

                    // Ensure pricing table exists
                    if (loader.NoExampleTables)
                    {
                        var state = "Missing example tables.";
                        var question = "Would you like to create and populate the example tables?";
                        if (!AskUserPermission(state, question)) return;
                    }
                    else if (loader.SomeExampleTables)
                    {
                        var state = "Missing some of the example tables.";
                        var question = "Would you like to create and populate the missing tables?";
                        if (!AskUserPermission(state, question)) return;
                    }
                    else
                    {
                        var state = "Example tables are present.";
                        var question = "Would you like to DROP, recreate, and populate the example tables?";
                        if (!AskUserPermission(state, question)) return;
                    }
                    await loader.LoadTables();
                }
            }
            catch (Exception exception)
            {
                // Notify user
                CommandLine.InformUser("Oops", "Bootstrapping did not work out as planned.");
                CommandLine.RenderException(exception);

                // Giveup
                return;
            }
            finally
            {
                // Give user a chance to read screen
                if (Debugger.IsAttached)
                {
                    CommandLine.WaitForUserAndThen("exit");
                }
            }

        }

        private static bool AskUserPermission(string state, string question)
        {
            Console.WriteLine();
            CommandLine.InformUser("Setup", state);
            if (!CommandLine.ValidateIntent(question))
            {
                CommandLine.InformUser("Setup", "Aborted");
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            _adminClient.Dispose();
            _dataClient.Dispose();
        }
    }
}
