using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Mapper.Interfaces;
using BigtableNet.Mapper.Types;
using BigtableNet.Models;
using BigtableNet.Models.Clients;
using BigtableNet.Models.Extensions;
using BigtableNet.Models.Types;
using Google.Bigtable.V1;
using Google.Protobuf;

namespace BigtableNet.Mapper
{
    public class Bigtable : BigtableReadonly
    {

        public Bigtable(BigtableCredential credentials, BigtableConnectionConfig config)
        {
            AdminClient = new Lazy<BigAdminClient>(() => new BigAdminClient(credentials, config));
            DataClient = new Lazy<BigDataClient>(() => new BigDataClient(credentials, config));
        }



        public async Task UpdateAsync<T>(T instance, CancellationToken cancellationToken = default(CancellationToken))
        {
            var table = LocateTable<T>();
            var key = ExtractKey(instance);
            var mutations = ExtractMutations(instance);
            await DataClient.Value.UpdateRowAsync(table, key, cancellationToken, mutations.ToArray());
        }

        public async Task UpdateWhenTrueAsync<T>(T instance, Func<T, bool> predicate, Func<T, object> whenTrue, CancellationToken cancellationToken = default(CancellationToken))
        {
            await UpdateWhenAsync(instance, predicate, whenTrue, null, cancellationToken);
        }
        public async Task UpdateWhenFalseAsync<T>(T instance, Func<T, bool> predicate, Func<T, object> whenFalse, CancellationToken cancellationToken = default(CancellationToken))
        {
            await UpdateWhenAsync(instance, predicate, null, whenFalse, cancellationToken);
        }
        public async Task UpdateWhenAsync<T>(T instance, Func<T,bool> predicate, Func<T,object> whenTrue = null, Func<T,object> whenFalse = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if( whenTrue == null && whenFalse == null )
                throw new ArgumentNullException("whenTrue", "Must set whenTrue or whenFalse to perform and UpdateWhen");
            var table = LocateTable<T>();
            var key = ExtractKey(instance);
            var filter = ExtractFilter(predicate);
            var trueCase = ExtractMutateWhen(whenTrue);
            var falseCase = ExtractMutateWhen(whenFalse);
            await DataClient.Value.UpdateRowWhenAsync(table, key, filter, trueCase, falseCase, cancellationToken);
        }

        private RowFilter ExtractFilter<T>(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Mutation> ExtractMutateWhen<T>(Func<T, object> whenTrue)
        {
            throw new NotImplementedException();
        }


        public async Task IncrementFieldAsync<T, TParameter>(T instance, Func<T, TParameter> field, long value = 1, CancellationToken cancellationToken = default(CancellationToken))
            where TParameter : IBigTableProperty
        {
            var table = LocateTable<T>();
            var key = ExtractKey(instance);
            var rule = CreateIncrementRule(field, value);
            await DataClient.Value.UpdateRowAsync(table, key, cancellationToken, rule);
        }
        public async Task AppendFieldAsync<T, TParameter>(T instance, Func<T, TParameter> field, string value, CancellationToken cancellationToken = default(CancellationToken))
            where TParameter : IBigTableProperty
        {
            var table = LocateTable<T>();
            var key = ExtractKey(instance);
            var rule = CreateAppendRule(field, value.ToByteString(table.Encoding));
            await DataClient.Value.UpdateRowAsync(table, key, cancellationToken, rule);
        }

        public async Task AppendFieldAsync<T, TParameter>(T instance, Func<T, TParameter> field, byte[] value, CancellationToken cancellationToken = default(CancellationToken))
            where TParameter : IBigTableProperty
        {
            var table = LocateTable<T>();
            var key = ExtractKey(instance);
            var rule = CreateAppendRule(field, value.ToByteString());
            await DataClient.Value.UpdateRowAsync(table, key, cancellationToken, rule);
        }

        private ReadModifyWriteRule CreateIncrementRule<T, TParameter>(Func<T, TParameter> field, long value)
            where TParameter : IBigTableProperty
        {
            return new ReadModifyWriteRule
            {
                IncrementAmount = value,
                ColumnQualifier = ExtractColumnName<T, TParameter>(field),
                FamilyName = ExtractFamilyName<T, TParameter>(field)
            };
        }
        private ReadModifyWriteRule CreateAppendRule<T, TParameter>(Func<T, TParameter> field, ByteString value)
            where TParameter : IBigTableProperty
        {
            return new ReadModifyWriteRule
            {
                AppendValue = value,
                ColumnQualifier = ExtractColumnName<T, TParameter>(field),
                FamilyName = ExtractFamilyName<T, TParameter>(field)
            };
        }

        private string ExtractFamilyName<T, TParameter>(Func<T, TParameter> field) 
            where TParameter : IBigTableProperty
        {
            throw new NotImplementedException();
        }

        private ByteString ExtractColumnName<T, TParameter>(Func<T, TParameter> field)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Mutation> ExtractMutations<T>(T instance)
        {
            var result = new Mutation();
            //result.SetCell = new Mutation.Types.SetCell
            //result.DeleteFromRow
            //result.DeleteFromColumn
            //result.DeleteFromFamily
            return new [] { result };
        }


    }
}
