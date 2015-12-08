using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Common.Extensions;
using Examples.Bootstrap;
using Examples.LowLevel.Instrumentation;
using Google.Apis.Auth.OAuth2;
using Google.Bigtable.Admin.Table.V1;
using Google.Bigtable.V1;
using Grpc.Auth;
using Grpc.Core;


namespace Examples.LowLevel
{
    // Make the code slightly easier to read
    using ScanResults = Dictionary<string, Dictionary<string, string>>;

    /// <summary>
    /// This class contains instrumentation and is missing caching so that it serves as both a functional example 
    /// of consuming the native Bigtable protobuf-based grpc implementation, and end-to-end which was used while 
    /// developing this solution.  This example is missing service caching, proper logging, and other mature
    /// facilities.  I would suggest using the <see cref="BigtableNet.Models"/> over native implementation.
    /// </summary>
    public class SimpleClient : IDisposable
    {
        private readonly string _bigTableId;
        private readonly string _pemFile;

        private Channel _channel;

        public SimpleClient(BigtableConfig config) : this(config.Project, config.Zone, config.Cluster)
        {
            // Chained
            
        }

        public SimpleClient(string project, string zone, string cluster)
        {
            var path = Directory.GetParent(Process.GetCurrentProcess().MainModule.FileName);
            _pemFile = Path.Combine(path.ToString(), "google-bigtable.pem" );
            Console.WriteLine("Using pem file: " + _pemFile);
            var projectId = String.Format(BigtableConstants.Templates.Project, project);
            var zoneId = String.Format(BigtableConstants.Templates.Zone, projectId, zone);
            _bigTableId = String.Format(BigtableConstants.Templates.Cluster, zoneId, cluster);
            Console.WriteLine("Client created for {0} in zone {1}, in the {2} cluster", project, zone, cluster);
        }

        public async Task<TimedOperation> Connect()
        {
            if (_channel != null)
                throw new NotSupportedException("Channel already connected.");

            var operation = await AcquireChannel(_pemFile);
            _channel = operation.Value;

            Console.WriteLine("[BigTable] Connection opened in {0}ms", operation.ElapsedMilliseconds);
            Console.WriteLine();
            return operation;
        }

        public async Task Disconnect()
        {
            if (_channel == null)
                return;

            var operation = await new TimedOperation().MeasureVoidAsync(async () =>
            {
                await _channel.ShutdownAsync();
                _channel = null;
            });

            Console.WriteLine("[BigTable] Connection closed in {0}ms", operation.ElapsedMilliseconds);
        }

        public async Task<TimedOperation<ScanResults>> Scan(string tableName, string start, string end)
        {
            return await ExecuteScan(new ReadRowsRequest
            {
                TableName = ConstructTableResource(tableName),
                RowRange = new RowRange
                {
                    StartKey = start.ToByteString(),
                    EndKey = end.ToByteString(),
                }
            });
        }

        public async Task<TimedOperation<ScanResults>> Seek(string tableName, string key)
        {
            return await ExecuteScan(new ReadRowsRequest
            {
                TableName = ConstructTableResource(tableName),
                RowKey = key.ToByteString(),
            });
        }

        public async Task<TimedOperation<IEnumerable<string>>> GetTableNames()
        {
            try
            {
                return await new TimedOperation<IEnumerable<string>>().MeasureAsync(async () =>
                {
                    var service = new BigtableTableService.BigtableTableServiceClient(_channel);
                    var request = new ListTablesRequest { Name = _bigTableId };
                    var response = await service.ListTablesAsync(request);
                    return response.Tables.Select(DeconstructTableResource);
                });
            }
            catch (Exception exception)
            {
                ReportServiceFault(exception);
                throw;
            }
        }

        private async Task<TimedOperation<ScanResults>> ExecuteScan(ReadRowsRequest scanRequest)
        {
            try
            {
                return await new TimedOperation<ScanResults>().MeasureAsync(async () =>
                {
                    var service = new BigtableService.BigtableServiceClient(_channel);
                    var results = new ScanResults();
                    using (var scanResults = service.ReadRows(scanRequest))
                    using (var responseStream = scanResults.ResponseStream)
                    {
                        while (await responseStream.MoveNext())
                        {
                            AccumulateRows(responseStream.Current, results);
                        }
                    }
                    return results;
                });
            }
            catch (Exception exception)
            {
                ReportServiceFault(exception);
                throw;
            }
        }

        private static async Task<TimedOperation<Channel>> AcquireChannel(string pemFile)
        {
            try
            {
                return await new TimedOperation<Channel>().MeasureAsync(async () =>
                {
                    Environment.SetEnvironmentVariable(BigtableConstants.EnvironmentVariables.SslRootFilePath, pemFile);

                    var credentials = await GoogleCredential.GetApplicationDefaultAsync();
                    var scopedCreds = credentials.CreateScoped(new[]
                    {
                        BigtableConstants.Scopes.Admin,
                        BigtableConstants.Scopes.Data,
                    });

                    var channelCreds = scopedCreds.ToChannelCredentials();
                    return new Channel(BigtableConstants.EndPoints.Admin, channelCreds);
                });
            }
            catch (Exception exception)
            {
                ReportServiceFault(exception);
                throw;
            }
        }

        private static void AccumulateRows(ReadRowsResponse result, ScanResults results)
        {
            var row = new Dictionary<string, string>();
            var rowKey = result.RowKey.FromByteString();
            foreach (var chunk in result.Chunks)
            {
                if (chunk.RowContents != null)
                {
                    foreach (var col in chunk.RowContents.Columns)
                    {
                        var columnName = col.Qualifier.FromByteString();
                        row.Add(columnName, "(empty)");
                        foreach (var cell in col.Cells)
                        {
                            row[columnName] = cell.Value.FromByteString();
                        }
                    }
                }
            }
            results.Add(rowKey, row);
        }

        private string ConstructTableResource(string tableName)
        {
            return String.Concat(_bigTableId, BigtableConstants.Templates.TableAdjunct, tableName);
        }

        private string DeconstructTableResource(Table table)
        {
            return table.Name.Replace(_bigTableId + BigtableConstants.Templates.TableAdjunct, "");
        }

        private static void ReportServiceFault(Exception exception)
        {
            CommandLine.InformUser("Oops", "SimpleClient encountered an exception");
            CommandLine.RenderException(exception);
        }

        public void Dispose()
        {
            Disconnect().Wait();
        }
    }
}
