#region - Using Directives -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Common.Customization;
using BigtableNet.Common.Implementation;
using BigtableNet.Models.Abstraction;
using BigtableNet.Models.Extensions;
using BigtableNet.Models.Types;
using Google.Apis.Auth.OAuth2;
using Google.Bigtable.V1;
using Grpc.Core;

#endregion

namespace BigtableNet.Models.Clients
{
    public class BigDataClient : BigClient
    {
        #region - Private Members Variables -

        private readonly BigtableService.BigtableServiceClient _client;

        #endregion

        #region - Public Static Members Variables -

        public static string DefaultKeySeparator { get; set; }

        #endregion

        #region - Construction -


        public BigDataClient(BigtableCredentials credentials, string project, string zone, string cluster, bool isReadOnly) : this(credentials, new BigtableConfig(project, zone, cluster), isReadOnly)
        {
        }

        public BigDataClient(BigtableCredentials credentials, BigtableConfig config, bool isReadOnly = false) : base(config, () => credentials.CreateDataChannel(isReadOnly))
        {
            // Create
            _client = new BigtableService.BigtableServiceClient(Channel);
        }

        internal BigDataClient(BigtableConfig config, Func<Channel> channelCreator) : base(config, channelCreator)
        {
            // Create
            _client = new BigtableService.BigtableServiceClient(Channel);
        }

        static BigDataClient()
        {
            DefaultKeySeparator = "#";
        }

        #endregion
        
        #region - Public Static Functionality -

        public static int SetThreadPoolSize(int threadCount)
        {
            return GrpcEnvironment.SetThreadPoolSize(threadCount);
        }

        public static void DisableLogger()
        {
            GrpcEnvironment.DisableLogging();
        }

        public static void RedirectLogging(GrpcLoggingAdaptor loggingAdaptor)
        {
            GrpcEnvironment.SetLogger(loggingAdaptor);
        }

        #endregion

        // Read signatures

        #region - Get Row Signatures -

        public async Task<BigRow> GetRowAsync(BigTable table, string key, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetRowAsync(table.Name, key, table.Encoding, cancellationToken);
        }

        public async Task<BigRow> GetRowAsync(BigTable table, byte[] key, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetRowAsync(table.Name, key, table.Encoding, cancellationToken);
        }

        public async Task<BigRow> GetRowAsync(string tableName, string key, Encoding encoding = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Defaultable parameters
            cancellationToken = cancellationToken == default(CancellationToken) ? CancellationToken.None : cancellationToken;
            encoding = encoding ?? BigModel.DefaultEncoding;

            // Create request
            var request = new ReadRowsRequest
            {
                TableName = tableName.ToTableId(ClusterId),
                RowKey = key.ToByteString(encoding)
            };

            // Chain
            return await ConvertRow(tableName, encoding, request, cancellationToken);
        }

        public async Task<BigRow> GetRowAsync(string tableName, byte[] key, Encoding encoding = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Defaultable parameters
            cancellationToken = cancellationToken == default(CancellationToken) ? CancellationToken.None : cancellationToken;
            encoding = encoding ?? BigModel.DefaultEncoding;

            // Create request
            var request = new ReadRowsRequest
            {
                TableName = tableName.ToTableId(ClusterId),
                RowKey = key.ToByteString(),
            };

            // Chain
            return await ConvertRow(tableName, encoding, request, cancellationToken);
        }

        #endregion

        #region - Get Rows Signatures -


        public async Task<IEnumerable<BigRow>> GetRowsAsync(BigTable table, string startKey = "", string endKey = "", long rowLimit = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetRowsAsync(table.Name, startKey, endKey, rowLimit, table.Encoding, cancellationToken);
        }
        public async Task<IEnumerable<BigRow>> GetRowsAsync(BigTable table, byte[] startKey = null, byte[] endKey = null, long rowLimit = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetRowsAsync(table.Name, startKey, endKey, rowLimit, table.Encoding, cancellationToken);
        }

        public async Task<IEnumerable<BigRow>> GetRowsAsync(string tableName, string startKey = "", string endKey = "", long rowLimit = 0, Encoding encoding = default(Encoding), CancellationToken cancellationToken = default(CancellationToken))
        {
            // Defaultable parameters
            cancellationToken = cancellationToken == default(CancellationToken) ? CancellationToken.None : cancellationToken;
            encoding = encoding ?? BigModel.DefaultEncoding;

            // Create request
            var request = new ReadRowsRequest
            {
                TableName = tableName.ToTableId(ClusterId),
                NumRowsLimit = rowLimit,
                RowRange = new RowRange
                {
                    StartKey = startKey.ToByteString(encoding),
                    EndKey = endKey.ToByteString(encoding),
                }
            };

            // Chain
            return await ConvertRows(tableName, encoding, request, cancellationToken);
        }

        public async Task<IEnumerable<BigRow>> GetRowsAsync(string tableName, byte[] startKey = null, byte[] endKey = null, long rowLimit = 0, Encoding encoding = default(Encoding), CancellationToken cancellationToken = default(CancellationToken))
        {
            // Defaultable parameters
            cancellationToken = cancellationToken == default(CancellationToken) ? CancellationToken.None : cancellationToken;
            encoding = encoding ?? BigModel.DefaultEncoding;

            // Create request
            var request = new ReadRowsRequest
            {
                TableName = tableName.ToTableId(ClusterId),
                NumRowsLimit = rowLimit,
                RowRange = new RowRange
                {
                    StartKey = (startKey ?? new byte[0]).ToByteString(),
                    EndKey = (endKey ?? new byte[0]).ToByteString(),
                }
            };

            // Chain
            return await ConvertRows(tableName, encoding, request, cancellationToken);
        }

        public async Task<IEnumerable<BigRow>> GetRowsAsync(BigTable table, RowFilter filter, long rowLimit = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetRowsAsync(table.Name, filter, rowLimit, table.Encoding, cancellationToken);
        }

        public async Task<IEnumerable<BigRow>> GetRowsAsync(string tableName, RowFilter filter, long rowLimit = 0, Encoding encoding = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Defaultable parameters
            cancellationToken = cancellationToken == default(CancellationToken) ? CancellationToken.None : cancellationToken;
            encoding = encoding ?? BigModel.DefaultEncoding;

            // Create request
            var request = new ReadRowsRequest
            {
                TableName = tableName.ToTableId(ClusterId),
                NumRowsLimit = rowLimit,
                Filter = filter,
            };

            // Chain
            return await ConvertRows(tableName, encoding, request, cancellationToken);
        }

        #endregion

        #region - Get Unsorted Rows Signatures -

        public async Task<IEnumerable<BigRow>> GetUnsortedRowsAsync(BigTable table, string startKey = "", string endKey = "", CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetUnsortedRowsAsync(table.Name, startKey, endKey, table.Encoding, cancellationToken);
        }
        public async Task<IEnumerable<BigRow>> GetUnsortedRowsAsync(string tableName, string startKey = "", string endKey = "", Encoding encoding = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken = cancellationToken == default(CancellationToken) ? CancellationToken.None : cancellationToken;
            encoding = encoding ?? BigModel.DefaultEncoding;

            var request = new ReadRowsRequest
            {
                TableName = tableName.ToTableId(ClusterId),
                AllowRowInterleaving = true,
                RowRange = new RowRange
                {
                    StartKey = startKey.ToByteString(encoding),
                    EndKey = endKey.ToByteString(encoding),
                }
            };

            return await ConvertRows(tableName, encoding, request, cancellationToken);
        }

        public async Task<IEnumerable<BigRow>> GetUnsortedRowsAsync(BigTable table, byte[] startKey = null, byte[] endKey = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetUnsortedRowsAsync(table.Name, startKey, endKey, table.Encoding, cancellationToken);
        }
        public async Task<IEnumerable<BigRow>> GetUnsortedRowsAsync(string tableName, byte[] startKey = null, byte[] endKey = null, Encoding encoding = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken = cancellationToken == default(CancellationToken) ? CancellationToken.None : cancellationToken;
            encoding = encoding ?? BigModel.DefaultEncoding;

            var request = new ReadRowsRequest
            {
                TableName = tableName.ToTableId(ClusterId),
                AllowRowInterleaving = true,
                RowRange = new RowRange
                {
                    StartKey = (startKey ?? new byte[0]).ToByteString(),
                    EndKey = (endKey ?? new byte[0]).ToByteString(),
                }
            };

            return await ConvertRows(tableName, encoding, request, cancellationToken);
        }

        public async Task<IEnumerable<BigRow>> GetUnsortedRowsAsync(BigTable table, RowFilter filter, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetUnsortedRowsAsync(table.Name, filter, table.Encoding, cancellationToken);
        }
        public async Task<IEnumerable<BigRow>> GetUnsortedRowsAsync(string tableName, RowFilter filter, Encoding encoding = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken = cancellationToken == default(CancellationToken) ? CancellationToken.None : cancellationToken;
            encoding = encoding ?? BigModel.DefaultEncoding;


            var request = new ReadRowsRequest
            {
                TableName = tableName.ToTableId(ClusterId),
                AllowRowInterleaving = true,
                Filter = filter,
            };

            return await ConvertRows(tableName, encoding, request, cancellationToken);
        }

        #endregion

        #region - Sample Rows Signatures -



        public async Task<IEnumerable<BigRow.Sample>> SampleRowKeysAsync(BigTable table)
        {
            return await SampleRowKeysAsync(table.Name);
        }
        public async Task<IEnumerable<BigRow.Sample>> SampleRowKeysAsync(string tableName, Encoding encoding = null)
        {
            return await SampleRowKeysAsync(tableName, encoding ?? BigModel.DefaultEncoding, CancellationToken.None);
        }

        public async Task<IEnumerable<BigRow.Sample>> SampleRowKeysAsync(BigTable table, CancellationToken cancellationToken)
        {
            return await SampleRowKeysAsync(table.Name, table.Encoding, cancellationToken);
        }
        public async Task<IEnumerable<BigRow.Sample>> SampleRowKeysAsync(string tableName, Encoding encoding, CancellationToken cancellationToken)
        {
            var request = new SampleRowKeysRequest
            {
                TableName = tableName.ToTableId(ClusterId)
            };
            var response = _client.SampleRowKeys(request, cancellationToken: cancellationToken);
            await response.ResponseHeadersAsync;
            await Task.Yield();
            var stream = response.ResponseStream;
            var results = new List<BigRow.Sample>();

            while (await stream.MoveNext(cancellationToken))
            {
                var item = stream.Current;
                results.Add(new BigRow.Sample(item.RowKey, item.OffsetBytes, encoding));
            }

            await Task.Yield();
            return results;
        }

        #endregion

        // Observer Read signatures

        #region - Observe Rows Signatures -

        public IObservable<BigRow> ObserveRows(BigTable table, string startKey = "", string endKey = "", long rowLimit = 0)
        {
            return BigRowObservable(table, new ReadRowsRequest
            {
                TableName = table.Name.ToTableId(ClusterId),
                NumRowsLimit = rowLimit,
                RowRange = new RowRange
                {
                    StartKey = startKey.ToByteString(table.Encoding),
                    EndKey = endKey.ToByteString(table.Encoding),
                }
            });
        }

        public IObservable<BigRow>  ObserveRows(BigTable table, byte[] startKey = null, byte[] endKey = null, long rowLimit = 0)
        {
            return BigRowObservable(table, new ReadRowsRequest
            {
                TableName = table.Name.ToTableId(ClusterId),
                NumRowsLimit = rowLimit,
                RowRange = new RowRange
                {
                    StartKey = startKey.ToByteString(),
                    EndKey = endKey.ToByteString(),
                }
            });
        }

        public IObservable<BigRow> ObserveRows(BigTable table, RowFilter filter, long rowLimit = 0)
        {
            return BigRowObservable(table, new ReadRowsRequest
            {
                TableName = table.Name.ToTableId(ClusterId),
                NumRowsLimit = rowLimit,
                Filter = filter
            });
        }

        #endregion

        #region - Observable Unsorted Rows Signatures -

        public IObservable<BigRow> ObserveUnsortedRows(BigTable table, string startKey = "", string endKey = "")
        {
            return BigRowObservable(table, new ReadRowsRequest
            {
                TableName = table.Name.ToTableId(ClusterId),
                AllowRowInterleaving = true,
                RowRange = new RowRange
                {
                    StartKey = startKey.ToByteString(table.Encoding),
                    EndKey = endKey.ToByteString(table.Encoding),
                }
            });
        }

        public IObservable<BigRow> ObserveUnsortedRows(BigTable table, byte[] startKey = null, byte[] endKey = null)
        {
            return BigRowObservable(table, new ReadRowsRequest
            {
                TableName = table.Name.ToTableId(ClusterId),
                AllowRowInterleaving = true,
                RowRange = new RowRange
                {
                    StartKey = (startKey ?? new byte[0]).ToByteString(),
                    EndKey = (endKey ?? new byte[0]).ToByteString(),
                }
            });
        }

        public IObservable<BigRow> ObserveUnsortedRows(BigTable table, RowFilter filter)
        {
            return BigRowObservable(table, new ReadRowsRequest
            {
                TableName = table.Name.ToTableId(ClusterId),
                AllowRowInterleaving = true,
                Filter = filter
            });
        }

        #endregion

        #region - Observable Sample Row Keys Signatures -

        public IObservable<BigRow.Sample> ObserveSampleRowKeys(BigTable table, CancellationToken cancellationToken, Action<IObservable<BigRow.Sample>> blockingSubscriber)
        {
            return BigRowSampleObservable(table, new SampleRowKeysRequest
            {
                TableName = table.Name.ToTableId(ClusterId)
            });
        }

        #endregion

        // Write signatures

        #region - Write Signatures -

        // -- // -- Write Operations -- // -- //

        public async Task WriteRowAsync(BigTable table, string key, IEnumerable<BigChange> changes, Encoding encoding = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            await WriteRowAsync(table.Name, key, changes, table.Encoding, cancellationToken);
        }

        public async Task WriteRowAsync(BigTable table, byte[] key, IEnumerable<BigChange> changes, CancellationToken cancellationToken = default(CancellationToken))
        {
            await WriteRowAsync(table.Name, key, changes, cancellationToken);
        }

        public async Task WriteRowAsync(string tableName, string key, IEnumerable<BigChange> changes, Encoding encoding = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken = cancellationToken == default(CancellationToken) ? CancellationToken.None : cancellationToken;
            encoding = encoding ?? BigModel.DefaultEncoding;

            var request = new MutateRowRequest
            {
                RowKey = key.ToByteString(encoding ?? BigModel.DefaultEncoding),
                TableName = tableName.ToTableId(ClusterId),
                Mutations = { changes.Select(change => change.AsApiObject()) }
            };

            await _client.MutateRowAsync(request, cancellationToken: cancellationToken);
            await Task.Yield();
        }

        public async Task WriteRowAsync(string tableName, byte[] key, IEnumerable<BigChange> changes, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken = cancellationToken == default(CancellationToken) ? CancellationToken.None : cancellationToken;

            var request = new MutateRowRequest
            {
                RowKey = key.ToByteString(),
                TableName = tableName.ToTableId(ClusterId),
                Mutations = { changes.Select(change => change.AsApiObject()) }
            };
            await _client.MutateRowAsync(request, cancellationToken: cancellationToken);
            await Task.Yield();
        }


        #endregion

        #region - Read-Then-Write Signatures -
        // -- // -- Read-then-write Operations -- // -- //



        public async Task<IEnumerable<BigRow>> WriteRowAsync(BigTable table, string key, IEnumerable<BigChange.FromRead> changes)
        {
            return await WriteRowAsync(table, key, CancellationToken.None, changes);
        }

        public async Task<IEnumerable<BigRow>> WriteRowAsync(string tableName, string key, IEnumerable<BigChange.FromRead> changes, Encoding encoding = null)
        {
            return await WriteRowAsync(tableName, key, CancellationToken.None, changes, encoding);
        }

        public async Task<IEnumerable<BigRow>> WriteRowAsync(BigTable table, byte[] key, IEnumerable<BigChange.FromRead> changes)
        {
            return await WriteRowAsync(table, key, CancellationToken.None, changes);
        }

        public async Task<IEnumerable<BigRow>> WriteRowAsync(string tableName, byte[] key, IEnumerable<BigChange.FromRead> changes, Encoding encoding = null)
        {
            return await WriteRowAsync(tableName, key, CancellationToken.None, changes, encoding);
        }

        public async Task<IEnumerable<BigRow>> WriteRowAsync(BigTable table, string key, CancellationToken cancellationToken, IEnumerable<BigChange.FromRead> changes)
        {
            return await WriteRowAsync(table.Name, key, cancellationToken, changes, table.Encoding);
        }

        public async Task<IEnumerable<BigRow>> WriteRowAsync(string tableName, string key, CancellationToken cancellationToken, IEnumerable<BigChange.FromRead> changes, Encoding encoding = null)
        {
            cancellationToken = cancellationToken == default(CancellationToken) ? CancellationToken.None : cancellationToken;
            encoding = encoding ?? BigModel.DefaultEncoding;

            var request = new ReadModifyWriteRowRequest
            {
                RowKey = key.ToByteString(encoding),
                TableName = tableName.ToTableId(ClusterId),
                Rules = { changes.Select(change => change.AsApiObject()) }
            };
            var response = await _client.ReadModifyWriteRowAsync(request, cancellationToken: cancellationToken);
            await Task.Yield();
            return response.Families.Select(row => new BigRow(tableName, encoding, row));
        }


        public async Task<IEnumerable<BigRow>> WriteRowAsync(BigTable table, byte[] key, CancellationToken cancellationToken, IEnumerable<BigChange.FromRead> changes)
        {
            return await WriteRowAsync(table.Name, key, cancellationToken, changes, table.Encoding);
        }
        public async Task<IEnumerable<BigRow>> WriteRowAsync(string tableName, byte[] key, CancellationToken cancellationToken, IEnumerable<BigChange.FromRead> changes, Encoding encoding = null)
        {
            cancellationToken = cancellationToken == default(CancellationToken) ? CancellationToken.None : cancellationToken;
            encoding = encoding ?? BigModel.DefaultEncoding;

            var request = new ReadModifyWriteRowRequest
            {
                RowKey = key.ToByteString(),
                TableName = tableName.ToTableId(ClusterId),
                Rules = {changes.Select(change => change.AsApiObject())}
            };
            var response = await _client.ReadModifyWriteRowAsync(request, cancellationToken: cancellationToken);
            await Task.Yield();
            return response.Families.Select(row => new BigRow(tableName, encoding, row));
        }

        #endregion

        #region - Write-When Signatures -


        public async Task<bool> WriteWhenAsync(BigTable table, string key, RowFilter filter, IEnumerable<Mutation> whenFilterIsTrue, IEnumerable<Mutation> whenFilterIsFalse, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await WriteWhenAsync(table.Name, key, filter, whenFilterIsTrue, whenFilterIsFalse, table.Encoding, cancellationToken);
        }
        public async Task<bool> WriteWhenAsync(string tableName, string key, RowFilter filter, IEnumerable<Mutation> whenFilterIsTrue, IEnumerable<Mutation> whenFilterIsFalse, Encoding encoding = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken = cancellationToken == default(CancellationToken) ? CancellationToken.None : cancellationToken;
            encoding = encoding ?? BigModel.DefaultEncoding;

            var request = new CheckAndMutateRowRequest
            {
                RowKey = key.ToByteString(encoding),
                TableName = tableName.ToTableId(ClusterId),
                PredicateFilter = filter,
                TrueMutations = {whenFilterIsTrue},
                FalseMutations = {whenFilterIsFalse}
            };
            var response = await _client.CheckAndMutateRowAsync(request, cancellationToken: cancellationToken);
            await Task.Yield();
            return response.PredicateMatched;
        }


        public async Task<bool> WriteWhenAsync(BigTable table, byte[] key, RowFilter filter, IEnumerable<Mutation> whenFilterIsTrue, IEnumerable<Mutation> whenFilterIsFalse, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await WriteWhenAsync(table.Name, key, filter, whenFilterIsTrue, whenFilterIsFalse, cancellationToken);

        }
        public async Task<bool> WriteWhenAsync(string tableName, byte[] key, RowFilter filter, IEnumerable<Mutation> whenFilterIsTrue, IEnumerable<Mutation> whenFilterIsFalse, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken = cancellationToken == default(CancellationToken) ? CancellationToken.None : cancellationToken;

            var request = new CheckAndMutateRowRequest
            {
                RowKey = key.ToByteString(),
                TableName = tableName.ToTableId(ClusterId),
                PredicateFilter = filter,
                TrueMutations = { whenFilterIsTrue },
                FalseMutations = { whenFilterIsFalse }
            };
            var response = await _client.CheckAndMutateRowAsync(request, cancellationToken: cancellationToken);
            await Task.Yield();
            return response.PredicateMatched;
        }



        #endregion

        // Functionality

        #region - Private Functionality -


        private IObservable<BigRow> BigRowObservable(BigTable table, ReadRowsRequest request)
        {
            // Return an observable for response
            return new BigtableObservable<ReadRowsResponse, BigRow>(async token => await ReadRows(request, token), row => new BigRow(table, row));
        }

        private IObservable<BigRow.Sample> BigRowSampleObservable(BigTable table, SampleRowKeysRequest request)
        {
            // Return an observable for response
            return new BigtableObservable<SampleRowKeysResponse, BigRow.Sample>(async token =>
            {
                // Send read rows request
                var response = _client.SampleRowKeys(request);

                // Await initial response
                await response.ResponseHeadersAsync;

                return response.ResponseStream;

            }, row => new BigRow.Sample(table, row.RowKey, row.OffsetBytes));
        }

        // Used by single row
        private async Task<BigRow> ConvertRow(string table, Encoding encoding, ReadRowsRequest request, CancellationToken cancellationToken)
        {
            // Chain
            var results = await ConvertRows(table, encoding, request, cancellationToken);

            // Return to caller context
            await Task.Yield();

            // Return first result
            return results.FirstOrDefault();
        }

        // Use by multi-row
        private async Task<IEnumerable<BigRow>> ConvertRows(string tableName, Encoding encoding, ReadRowsRequest request, CancellationToken cancellationToken)
        {
            // Chain
            var response = await ReadRows(request, cancellationToken);

            // Locals
            var results = new List<BigRow>();

            // Iterate results
            while (await response.MoveNext(cancellationToken))
            {
                // Accumulate new row
                results.Add(new BigRow(tableName, encoding, response.Current));
            }

            // Return to caller context
            await Task.Yield();

            // Return results
            return results;
        }

        // Used by all read signatures including observables
        private async Task<IAsyncEnumerator<ReadRowsResponse>> ReadRows(ReadRowsRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Read rows with native client
            var response = _client.ReadRows(request, cancellationToken: cancellationToken);

            // Wait for response to have started
            await response.ResponseHeadersAsync;

            // Return the response stream
            return response.ResponseStream;
        }

        #endregion
    }
}
