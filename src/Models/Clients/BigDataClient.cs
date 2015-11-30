using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Models.Abstraction;
using BigtableNet.Models.Extensions;
using BigtableNet.Models.Types;
using Google.Bigtable.Admin.Table.V1;
using Google.Bigtable.V1;
using Google.Protobuf;
using Grpc.Core;
using Org.BouncyCastle.Asn1.Cms;

namespace BigtableNet.Models.Clients
{
    public class BigDataClient : BigClient
    {
        public static Encoding Encoding { get; set; }

        static BigDataClient()
        {
            Encoding = Encoding.UTF8;
        }

        private readonly BigtableService.BigtableServiceClient _client;

        internal string ClusterUri { get; private set; }

        public BigDataClient(BigtableCredential credentials, BigtableConnectionConfig config, bool isReadOnly = false ) : base(config)
        {
            // Create
            ClusterUri = config.ToClusterUri();
            _client = new BigtableService.BigtableServiceClient(isReadOnly ? credentials.ToReadOnlyDataChannel() : credentials.ToDataChannel());
        }

        public async Task<BigRow> GetRowAsync(BigTable table, string key, CancellationToken cancellationToken = default(CancellationToken), Action<Metadata.Entry> metadataHandler = null)
        {
            if (cancellationToken == default(CancellationToken))
            {
                cancellationToken = CancellationToken.None;
            }

            var request = new ReadRowsRequest
            {
                TableName = table.Name.ToTableUri(ClusterUri),
                RowKey = key.ToByteString(table.Encoding)
            };

            return await ProcessReadRow(table, cancellationToken, metadataHandler, request);
        }

        public async Task<BigRow> GetRowAsync(BigTable table, byte[] key, CancellationToken cancellationToken = default(CancellationToken), Action<Metadata.Entry> metadataHandler = null)
        {
            if (cancellationToken == default(CancellationToken))
            {
                cancellationToken = CancellationToken.None;
            }

            var request = new ReadRowsRequest
            {
                TableName = table.Name.ToTableUri(ClusterUri),
                RowKey = key.ToByteString()
            };

            return await ProcessReadRow(table, cancellationToken, metadataHandler, request);

        }

        public async Task<IEnumerable<BigRow>> GetRowsAsync(BigTable table, string startKey = "", string endKey = "", long rowLimit = 0, CancellationToken cancellationToken = default(CancellationToken), Action<Metadata.Entry> metadataHandler = null)
        {
            if (cancellationToken == default(CancellationToken))
            {
                cancellationToken = CancellationToken.None;
            }

            var request = new ReadRowsRequest
            {
                TableName = table.Name.ToTableUri(ClusterUri),
                NumRowsLimit = rowLimit,
                RowRange = new RowRange
                {
                    StartKey = startKey.ToByteString(table.Encoding),
                    EndKey = endKey.ToByteString(table.Encoding),
                }
            };

            return await ProcessReadRows(table, cancellationToken, metadataHandler, request);
        }

        public async Task<IEnumerable<BigRow>> GetRowsAsync(BigTable table, byte[] startKey = null, byte[] endKey = null, long rowLimit = 0, CancellationToken cancellationToken = default(CancellationToken), Action<Metadata.Entry> metadataHandler = null)
        {
            if (cancellationToken == default(CancellationToken))
            {
                cancellationToken = CancellationToken.None;
            }

            var request = new ReadRowsRequest
            {
                TableName = table.Name.ToTableUri(ClusterUri),
                NumRowsLimit = rowLimit,
                RowRange = new RowRange
                {
                    StartKey = (startKey ?? new byte[0]).ToByteString(),
                    EndKey = (endKey ?? new byte[0]).ToByteString(),
                }
            };

            return await ProcessReadRows(table, cancellationToken, metadataHandler, request);
        }

        public async Task<IEnumerable<BigRow>> GetRowsAsync(BigTable table, RowFilter filter, long rowLimit = 0, CancellationToken cancellationToken = default(CancellationToken), Action<Metadata.Entry> metadataHandler = null)
        {
            if (cancellationToken == default(CancellationToken))
            {
                cancellationToken = CancellationToken.None;
            }

            var request = new ReadRowsRequest
            {
                TableName = table.Name.ToTableUri(ClusterUri),
                NumRowsLimit = rowLimit,
                Filter = filter,
            };

            return await ProcessReadRows(table, cancellationToken, metadataHandler, request);
        }

        public async Task<IObservable<BigRow>> ObserveRowsAsync(BigTable table, string startKey = "", string endKey = "", long rowLimit = 0, CancellationToken cancellationToken = default(CancellationToken), Action<Metadata.Entry> metadataHandler = null)
        {
            if (cancellationToken == default(CancellationToken))
            {
                cancellationToken = CancellationToken.None;
            }

            var request = new ReadRowsRequest
            {
                TableName = table.Name.ToTableUri(ClusterUri),
                NumRowsLimit = rowLimit,
                RowRange = new RowRange
                {
                    StartKey = startKey.ToByteString(table.Encoding),
                    EndKey = endKey.ToByteString(table.Encoding),
                }
            };

            var response = await ReadRows(request, cancellationToken, metadataHandler);
            return new Observable<ReadRowsResponse, BigRow>(response, row => new BigRow(this, table, row));
        }

        public async Task<IObservable<BigRow>> ObserveRowsAsync(BigTable table, byte[] startKey = null, byte[] endKey = null, long rowLimit = 0, CancellationToken cancellationToken = default(CancellationToken), Action<Metadata.Entry> metadataHandler = null)
        {
            if (cancellationToken == default(CancellationToken))
            {
                cancellationToken = CancellationToken.None;
            }

            var request = new ReadRowsRequest
            {
                TableName = table.Name.ToTableUri(ClusterUri),
                NumRowsLimit = rowLimit,
                RowRange = new RowRange
                {
                    StartKey = (startKey ?? new byte[0]).ToByteString(),
                    EndKey = (endKey ?? new byte[0]).ToByteString(),
                }
            };

            var response = await ReadRows(request, cancellationToken, metadataHandler);
            return new Observable<ReadRowsResponse, BigRow>(response, row => new BigRow(this, table, row));
        }

        public async Task<IObservable<BigRow>> ObserveRowsAsync(BigTable table, RowFilter filter, long rowLimit = 0, CancellationToken cancellationToken = default(CancellationToken), Action<Metadata.Entry> metadataHandler = null)
        {
            if (cancellationToken == default(CancellationToken))
            {
                cancellationToken = CancellationToken.None;
            }

            var request = new ReadRowsRequest
            {
                TableName = table.Name.ToTableUri(ClusterUri),
                NumRowsLimit = rowLimit,
                Filter = filter
            };

            var response = await ReadRows(request, cancellationToken, metadataHandler);
            return new Observable<ReadRowsResponse, BigRow>(response, row => new BigRow(this, table, row));
        }

        public async Task<IEnumerable<BigRow>> GetUnsortedRowsAsync(BigTable table, string startKey = "", string endKey = "", CancellationToken cancellationToken = default(CancellationToken), Action<Metadata.Entry> metadataHandler = null)
        {
            if (cancellationToken == default(CancellationToken))
            {
                cancellationToken = CancellationToken.None;
            }

            var request = new ReadRowsRequest
            {
                TableName = table.Name.ToTableUri(ClusterUri),
                AllowRowInterleaving = true,
                RowRange = new RowRange
                {
                    StartKey = startKey.ToByteString(table.Encoding),
                    EndKey = endKey.ToByteString(table.Encoding),
                }
            };

            return await ProcessReadRows(table, cancellationToken, metadataHandler, request);
        }

        public async Task<IEnumerable<BigRow>> GetUnsortedRowsAsync(BigTable table, byte[] startKey = null, byte[] endKey = null, CancellationToken cancellationToken = default(CancellationToken), Action<Metadata.Entry> metadataHandler = null)
        {
            if (cancellationToken == default(CancellationToken))
            {
                cancellationToken = CancellationToken.None;
            }

            var request = new ReadRowsRequest
            {
                TableName = table.Name.ToTableUri(ClusterUri),
                AllowRowInterleaving = true,
                RowRange = new RowRange
                {
                    StartKey = (startKey ?? new byte[0]).ToByteString(),
                    EndKey = (endKey ?? new byte[0]).ToByteString(),
                }
            };

            return await ProcessReadRows(table, cancellationToken, metadataHandler, request);
        }

        public async Task<IEnumerable<BigRow>> GetUnsortedRowsAsync(BigTable table, RowFilter filter, CancellationToken cancellationToken = default(CancellationToken), Action<Metadata.Entry> metadataHandler = null)
        {
            if (cancellationToken == default(CancellationToken))
            {
                cancellationToken = CancellationToken.None;
            }

            var request = new ReadRowsRequest
            {
                TableName = table.Name.ToTableUri(ClusterUri),
                AllowRowInterleaving = true,
                Filter = filter,
            };

            return await ProcessReadRows(table, cancellationToken, metadataHandler, request);
        }

        public async Task<IObservable<BigRow>> ObserveUnsortedRowsAsync(BigTable table, string startKey = "", string endKey = "", CancellationToken cancellationToken = default(CancellationToken), Action<Metadata.Entry> metadataHandler = null)
        {
            if (cancellationToken == default(CancellationToken))
            {
                cancellationToken = CancellationToken.None;
            }

            var request = new ReadRowsRequest
            {
                TableName = table.Name.ToTableUri(ClusterUri),
                AllowRowInterleaving = true,
                RowRange = new RowRange
                {
                    StartKey = startKey.ToByteString(table.Encoding),
                    EndKey = endKey.ToByteString(table.Encoding),
                }
            };

            var response = await ReadRows(request, cancellationToken, metadataHandler);
            return new Observable<ReadRowsResponse, BigRow>(response, row => new BigRow(this, table, row));
        }

        public async Task<IObservable<BigRow>> ObserveUnsortedRowsAsync(BigTable table, byte[] startKey = null, byte[] endKey = null, CancellationToken cancellationToken = default(CancellationToken), Action<Metadata.Entry> metadataHandler = null)
        {
            if (cancellationToken == default(CancellationToken))
            {
                cancellationToken = CancellationToken.None;
            }

            var request = new ReadRowsRequest
            {
                TableName = table.Name.ToTableUri(ClusterUri),
                AllowRowInterleaving = true,
                RowRange = new RowRange
                {
                    StartKey = (startKey ?? new byte[0]).ToByteString(),
                    EndKey = (endKey ?? new byte[0]).ToByteString(),
                }
            };

            var response = await ReadRows(request, cancellationToken, metadataHandler);
            return new Observable<ReadRowsResponse, BigRow>(response, row => new BigRow(this, table, row));
        }

        public async Task<IObservable<BigRow>> ObserveUnsortedRowsAsync(BigTable table, RowFilter filter, CancellationToken cancellationToken = default(CancellationToken), Action<Metadata.Entry> metadataHandler = null)
        {
            if (cancellationToken == default(CancellationToken))
            {
                cancellationToken = CancellationToken.None;
            }

            var request = new ReadRowsRequest
            {
                TableName = table.Name.ToTableUri(ClusterUri),
                AllowRowInterleaving = true,
                Filter = filter
            };

            var response = await ReadRows(request, cancellationToken, metadataHandler);
            return new Observable<ReadRowsResponse, BigRow>(response, row => new BigRow(this, table, row));
        }

        public async Task UpdateRowAsync(BigTable table, string key, params Mutation[] mutations)
        {
            await UpdateRowAsync(table, key, CancellationToken.None, mutations);
        }

        public async Task UpdateRowAsync(BigTable table, string key, CancellationToken cancellationToken, params Mutation[] mutations)
        {
            var request = new MutateRowRequest
            {
                RowKey = key.ToByteString(table.Encoding),
                TableName = table.Name.ToTableUri(ClusterUri),
                Mutations = { mutations }
            };
            await _client.MutateRowAsync(request, cancellationToken: cancellationToken);
        }

        public async Task UpdateRowAsync(BigTable table, byte[] key, params Mutation[] mutations)
        {
            await UpdateRowAsync(table, key, CancellationToken.None, mutations);
        }

        public async Task UpdateRowAsync(BigTable table, byte[] key, CancellationToken cancellationToken, params Mutation[] mutations)
        {
            var request = new MutateRowRequest
            {
                RowKey = key.ToByteString(),
                TableName = table.Name.ToTableUri(ClusterUri),
                Mutations = { mutations }
            };
            await _client.MutateRowAsync(request, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<BigRow>> UpdateRowAsync(BigTable table, string key, params ReadModifyWriteRule[] rules)
        {
            return await UpdateRowAsync(table, key, CancellationToken.None, rules);
        }

        public async Task<IEnumerable<BigRow>> UpdateRowAsync(BigTable table, string key, CancellationToken cancellationToken, params ReadModifyWriteRule[] rules)
        {
            var request = new ReadModifyWriteRowRequest
            {
                RowKey = key.ToByteString(table.Encoding),
                TableName = table.Name.ToTableUri(ClusterUri),
                Rules = { rules }
            };
            var response = await _client.ReadModifyWriteRowAsync(request, cancellationToken: cancellationToken);
            return response.Families.Select(row => new BigRow(this, table, row));
        }

        public async Task<IEnumerable<BigRow>> UpdateRowAsync(BigTable table, byte[] key, params ReadModifyWriteRule[] rules)
        {
            return await UpdateRowAsync(table, key, CancellationToken.None, rules);
        }

        public async Task<IEnumerable<BigRow>> UpdateRowAsync(BigTable table, byte[] key, CancellationToken cancellationToken, params ReadModifyWriteRule[] rules)
        {
            var request = new ReadModifyWriteRowRequest
            {
                RowKey = key.ToByteString(),
                TableName = table.Name.ToTableUri(ClusterUri),
                Rules = { rules }
            };
            var response = await _client.ReadModifyWriteRowAsync(request, cancellationToken: cancellationToken);
            return response.Families.Select(row => new BigRow(this, table, row));
        }

        public async Task<bool> UpdateRowWhenAsync(BigTable table, string key, RowFilter filter, IEnumerable<Mutation> whenFilterIsTrue, IEnumerable<Mutation> whenFilterIsFalse, CancellationToken cancellationToken = default(CancellationToken), Action<Metadata.Entry> metadataHandler = null)
        {
            if (cancellationToken == default(CancellationToken))
            {
                cancellationToken = CancellationToken.None;
            }

            var request = new CheckAndMutateRowRequest
            {
                RowKey = key.ToByteString(table.Encoding),
                TableName = table.Name.ToTableUri(ClusterUri),
                PredicateFilter = filter,
                TrueMutations = {whenFilterIsTrue},
                FalseMutations = {whenFilterIsFalse}
            };
            var response = _client.CheckAndMutateRowAsync(request, cancellationToken: cancellationToken);
            if (metadataHandler != null)
            {
                var headers = await response.ResponseHeadersAsync;
                foreach (var header in headers)
                    metadataHandler(header);
            }
            var result = await response.ResponseAsync;
            return result.PredicateMatched;
        }


        public async Task<bool> UpdateRowWhenAsync(BigTable table, byte[] key, RowFilter filter, IEnumerable<Mutation> whenFilterIsTrue, IEnumerable<Mutation> whenFilterIsFalse, CancellationToken cancellationToken = default(CancellationToken), Action<Metadata.Entry> metadataHandler = null)
        {
            if (cancellationToken == default(CancellationToken))
            {
                cancellationToken = CancellationToken.None;
            }

            var request = new CheckAndMutateRowRequest
            {
                RowKey = key.ToByteString(),
                TableName = table.Name.ToTableUri(ClusterUri),
                PredicateFilter = filter,
                TrueMutations = { whenFilterIsTrue },
                FalseMutations = { whenFilterIsFalse }
            };
            var response = _client.CheckAndMutateRowAsync(request, cancellationToken: cancellationToken);
            if (metadataHandler != null)
            {
                var headers = await response.ResponseHeadersAsync;
                foreach (var header in headers)
                    metadataHandler(header);
            }
            var result = await response.ResponseAsync;
            return result.PredicateMatched;
        }

        public async Task<IEnumerable<BigRow.Sample>> SampleRowKeysAsync(BigTable table, Action<Metadata.Entry> metadataHandler = null)
        {
            return await SampleRowKeysAsync(table, metadataHandler);
        }
        public async Task<IEnumerable<BigRow.Sample>> SampleRowKeysAsync(BigTable table, CancellationToken cancellationToken, Action<Metadata.Entry> metadataHandler = null)
        {
            var request = new SampleRowKeysRequest
            {
                TableName = table.Name.ToTableUri(ClusterUri)
            };
            var response = _client.SampleRowKeys(request, cancellationToken: cancellationToken);
            if (metadataHandler != null)
            {
                var headers = await response.ResponseHeadersAsync;
                foreach (var header in headers)
                    metadataHandler(header);
            }
            var stream = response.ResponseStream;
            var results = new List<BigRow.Sample>();

            while (await stream.MoveNext(cancellationToken))
            {
                var item = stream.Current;
                results.Add(new BigRow.Sample(table, item.RowKey, item.OffsetBytes));
            }

            return results;
        }

        public async Task<IObservable<BigRow.Sample>> ObserveSampleRowKeysAsync(BigTable table, CancellationToken cancellationToken, Action<Metadata.Entry> metadataHandler = null)
        {
            var request = new SampleRowKeysRequest
            {
                TableName = table.Name.ToTableUri(ClusterUri)
            };
            var response = _client.SampleRowKeys(request, cancellationToken: cancellationToken);
            if (metadataHandler != null)
            {
                var headers = await response.ResponseHeadersAsync;
                foreach (var header in headers)
                    metadataHandler(header);
            }
            var stream = response.ResponseStream;
            var results = new List<BigRow>();

            while (await stream.MoveNext(cancellationToken))
            {
                var item = stream.Current;
            }
            return new Observable<SampleRowKeysResponse, BigRow.Sample>(stream, row => new BigRow.Sample(table, row.RowKey, row.OffsetBytes));
        }


        private async Task<IAsyncEnumerator<ReadRowsResponse>> ReadRows(ReadRowsRequest request, CancellationToken cancellationToken = default(CancellationToken), Action<Metadata.Entry> metadataHandler = null)
        {
            var response = _client.ReadRows(request, cancellationToken: cancellationToken);

            if (metadataHandler != null)
            {
                var headers = await response.ResponseHeadersAsync;
                foreach (var header in headers)
                    metadataHandler(header);
            }

            return response.ResponseStream;
        }


        private async Task<BigRow> ProcessReadRow(BigTable table, CancellationToken cancellationToken, Action<Metadata.Entry> metadataHandler, ReadRowsRequest request)
        {
            var response = await ReadRows(request, cancellationToken, metadataHandler);

            if (!await response.MoveNext(cancellationToken))
            {
                return null;
            }

            return new BigRow(this, table, response.Current);
        }

        private async Task<IEnumerable<BigRow>> ProcessReadRows(BigTable table, CancellationToken cancellationToken, Action<Metadata.Entry> metadataHandler, ReadRowsRequest request)
        {
            var response = await ReadRows(request, cancellationToken, metadataHandler);
            var results = new List<BigRow>();

            while (await response.MoveNext(cancellationToken))
            {
                results.Add(new BigRow(this, table, response.Current));
            }

            return results;
        }






    }
}
