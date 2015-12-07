using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Common.Implementation;
using BigtableNet.Mapper.Implementation;
using BigtableNet.Mapper.Interfaces;
using BigtableNet.Models;
using BigtableNet.Models.Clients;
using BigtableNet.Models.Types;
using Google.Apis.Auth.OAuth2;
using Google.Bigtable.V1;

namespace BigtableNet.Mapper
{
    public class BigtableReader : IDisposable
    {
        internal static ConcurrentBag<IBigtableFieldSerializer> FieldSerializers = new ConcurrentBag<IBigtableFieldSerializer>();

        protected Lazy<BigDataClient> DataClient;
        protected Lazy<BigAdminClient> AdminClient;

        protected BigtableReader() { }

        public BigtableReader(BigtableCredentials credentials, string project, string zone, string cluster) : this(credentials, new BigtableConfig(project, zone, cluster))
        {

        }

        public BigtableReader(BigtableCredentials credentials, BigtableConfig config)
        {
            AdminClient = new Lazy<BigAdminClient>(() => new BigAdminClient(credentials, config));
            DataClient = new Lazy<BigDataClient>(() => new BigDataClient(credentials, config));
        }


        public static void RegisterFieldSerializer(IBigtableFieldSerializer serializer)
        {
            FieldSerializers.Add(serializer);
        }

        public async Task<bool> TableExists<T>()
        {
            var cache = ReflectionCache.For<T>();
            var table = await AdminClient.Value.GetTableAsync(cache.TableName);
            if (table != null)
            {
                cache.Adjunct(() => table);
                return true;
            }

            return false;
        }

        public async Task<T> GetAsync<T>(T prototype, CancellationToken cancellationToken = default(CancellationToken))
        {
            var cache = ReflectionCache.For<T>();
            var table = LocateTable<T>(cache);
            var key = ExtractKey(cache, prototype);
            var row = await DataClient.Value.GetRowAsync(table, key, cancellationToken);
            return Inflate(row, prototype);
        }

        public async Task<IEnumerable<T>> SampleAsync<T>(CancellationToken cancellationToken = default(CancellationToken))
        {
            var cache = ReflectionCache.For<T>();
            var table = LocateTable<T>(cache);
            var rows = await DataClient.Value.SampleRowKeysAsync(table, cancellationToken);
            return rows.AsParallel().Select(Inflate<T>).ToArray();
        }

        public async Task<IEnumerable<T>> ScanAsync<T>(T start = default(T), T end = default(T), int rowLimit = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            var cache = ReflectionCache.For<T>();
            var table = LocateTable<T>(cache);
            var startKey = ExtractKey(cache,start);
            var endKey = ExtractKey(cache,end);
            var rows = await DataClient.Value.GetRowsAsync(table, startKey, endKey, rowLimit, cancellationToken);
            return rows.AsParallel().Select(Inflate<T>).ToArray();
        }
        public async Task<IObservable<T>> ObservableScanAsync<T>(T start = default(T), T end = default(T), int rowLimit = 0, CancellationToken cancellationToken = default(CancellationToken))
            where T : class
        {
            var cache = ReflectionCache.For<T>();
            var table = LocateTable<T>(cache);
            var startKey = ExtractKey(cache,start);
            var endKey = ExtractKey(cache,end);
            var observable = await DataClient.Value.ObserveRowsAsync(table, startKey, endKey, rowLimit, cancellationToken);
            return new ChainedObservable<ReadRowsResponse, BigRow, T>((Observable<ReadRowsResponse, BigRow>)observable, Inflate<T>);
        }

        public async Task<IEnumerable<T>> UnsortedScanAsync<T>(T start = default(T), T end = default(T), CancellationToken cancellationToken = default(CancellationToken))
        {
            var cache = ReflectionCache.For<T>();
            var table = LocateTable<T>(cache);
            var startKey = ExtractKey(cache,start);
            var endKey = ExtractKey(cache,end);
            var rows = await DataClient.Value.GetUnsortedRowsAsync(table, startKey, endKey, cancellationToken);
            return rows.AsParallel().Select(Inflate<T>).ToArray();
        }
        public async Task<IObservable<T>> UnsortedObservableScanAsync<T>(T start = default(T), T end = default(T), CancellationToken cancellationToken = default(CancellationToken))
            where T : class
        {
            var cache = ReflectionCache.For<T>();
            var table = LocateTable<T>(cache);
            var startKey = ExtractKey(cache,start);
            var endKey = ExtractKey(cache,end);
            var observable = await DataClient.Value.ObserveUnsortedRowsAsync(table, startKey, endKey, cancellationToken);
            return new ChainedObservable<ReadRowsResponse, BigRow, T>((Observable<ReadRowsResponse, BigRow>)observable, Inflate<T>);
        }


        protected T Inflate<T>(BigRow.Sample r)
        {
            var cache = ReflectionCache.For<T>();
            var row = DataClient.Value.GetRowsAsync(cache.TableName, r.Key, rowLimit: 1, encoding: cache.TableEncoding).Result.ToArray();
            return row.Any() ? Inflate<T>(row.First()) : default(T);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected T Inflate<T>(BigRow row, T prototype)
        {
            return (T)ReflectionCache.For<T>().PopulateInstance(row.Key, row.GetValues, prototype);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected T Inflate<T>(BigRow row)
        {
            return (T)ReflectionCache.For<T>().CreateInstance(row.Key, row.GetValues);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected BigTable LocateTable<T>(ReflectionCache cache)
        {
            return cache.Adjunct( () => new BigTable(cache.TableName, cache.TableEncoding) );
        }

        protected byte[] ExtractKey<T>(ReflectionCache cache, T instance)
        {
            return cache.KeyGetter(instance);
        }

        public void Dispose()
        {
            if (AdminClient.IsValueCreated)
                AdminClient.Value.Dispose();

            if (DataClient.IsValueCreated)
                DataClient.Value.Dispose();
        }
    }
}
