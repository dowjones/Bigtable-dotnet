using System;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Models.Clients;

namespace Examples.Modeled
{
    class Docs
    {
        public async Task Readme(string configFile)
        {
            // Load configuration
            var config = BigtableConfig.Load(configFile);

            // Create credentials
            var credentials = await BigtableCredentials.UseApplicationDefaultCredentialsAsync();

            // Data client
            using (var dataClient = new BigDataClient(credentials, config))
            {
                // Scan myTable
                var rows = await dataClient.GetRowsAsync(
                    tableName: "myTable", 
                    startKey: "abc", 
                    endKey: "def", 
                    rowLimit: 20
                );

                // Show the user
                foreach (var row in rows)
                {
                    // Write key to console
                    Console.WriteLine("Key: " + row.KeyString);

                    // Iterate the families which were returned
                    foreach (var family in row.FieldsByFamily)
                    {
                        // Results are organized by family
                        var familyName = family.Key;

                        // Iterate the fields in this family
                        foreach (var field in family.Value)
                        {
                            var fieldName = field.Key;

                            // Iterate the versions of the field
                            foreach (var cell in field.Value)
                            {
                                // Write to console
                                Console.WriteLine("{0}:{1} = {2}", familyName, cell.ColumnName, cell.StringValue);
                            }
                        }
                    }
                }
            }
        }
    }
}
