using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Models;
using BigtableNet.Models.Clients;
using BigtableNet.Models.Types;
using Google.Bigtable.V1;

namespace BigtableNet.Mapper
{
    public class BigtableReadonly
    {
        protected Lazy<BigDataClient> DataClient;
        protected Lazy<BigAdminClient> AdminClient;

        protected BigtableReadonly()
        {
            
        }

        public BigtableReadonly(BigtableCredential credentials, BigtableConnectionConfig config)
        {
            AdminClient = new Lazy<BigAdminClient>(() => new BigAdminClient(credentials, config));
            DataClient = new Lazy<BigDataClient>(() => new BigDataClient(credentials, config, true));
        }

        public async Task<IEnumerable<T>> SampleAsync<T>(CancellationToken cancellationToken = default(CancellationToken))
        {
            var table = LocateTable<T>();
            var rows = await DataClient.Value.SampleRowKeysAsync(table, cancellationToken);
            return rows.Select(Inflate<T>);
        }

        public async Task<IEnumerable<T>> ScanAsync<T>(T start = default(T), T end = default(T), int rowLimit = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            var table = LocateTable<T>();
            var startKey = ExtractKey(start);
            var endKey = ExtractKey(end);
            var rows = await DataClient.Value.GetRowsAsync(table, startKey, endKey, rowLimit, cancellationToken);
            return rows.Select(Inflate<T>);
        }
        public async Task<IObservable<T>> ObservableScanAsync<T>(T start = default(T), T end = default(T), int rowLimit = 0, CancellationToken cancellationToken = default(CancellationToken))
            where T : class
        {
            var table = LocateTable<T>();
            var startKey = ExtractKey(start);
            var endKey = ExtractKey(end);
            var observable = await DataClient.Value.ObserveRowsAsync(table, startKey, endKey, rowLimit, cancellationToken);
            return new ChainedObservable<ReadRowsResponse, BigRow, T>((Observable<ReadRowsResponse, BigRow>)observable, Inflate<T>);
        }

        public async Task<IEnumerable<T>> UnsortedScanAsync<T>(T start = default(T), T end = default(T), CancellationToken cancellationToken = default(CancellationToken))
        {
            var table = LocateTable<T>();
            var startKey = ExtractKey(start);
            var endKey = ExtractKey(end);
            var rows = await DataClient.Value.GetUnsortedRowsAsync(table, startKey, endKey, cancellationToken);
            return rows.Select(Inflate<T>);
        }
        public async Task<IObservable<T>> UnsortedObservableScanAsync<T>(T start = default(T), T end = default(T), CancellationToken cancellationToken = default(CancellationToken))
            where T : class
        {
            var table = LocateTable<T>();
            var startKey = ExtractKey(start);
            var endKey = ExtractKey(end);
            var observable = await DataClient.Value.ObserveUnsortedRowsAsync(table, startKey, endKey, cancellationToken);
            return new ChainedObservable<ReadRowsResponse, BigRow, T>((Observable<ReadRowsResponse, BigRow>)observable, Inflate<T>);
        }

        public async Task<T> GetAsync<T>(T prototype, CancellationToken cancellationToken = default(CancellationToken))
        {
            var table = LocateTable<T>();
            var key = ExtractKey(prototype);
            var row = await DataClient.Value.GetRowAsync(table, key, cancellationToken);
            return Inflate<T>(row);
        }



        protected T Inflate<T>(BigRow row)
        {
            throw new NotImplementedException();
        }

        protected T Inflate<T>(BigRow.Sample r)
        {
            throw new NotImplementedException();
        }

        protected BigTable LocateTable<T>()
        {
            throw new NotImplementedException();
        }

        protected byte[] ExtractKey<T>(T instance)
        {
            if (instance.Equals(default(T)))
            {
                return new byte[0];
            }

            throw new NotImplementedException();
        }
    }
}
