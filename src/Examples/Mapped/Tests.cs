using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Mapper;
using Examples.Mapped.Schema.Tables;

namespace Examples.Mapped
{
    class Tests
    {
        private Bigtable _bigtable;

        public async Task ConnectAsync(string configFile)
        {
            // Setup
            var config = await BigtableConfig.LoadAsync(configFile);
            var credentials = await BigtableCredentials.UseApplicationDefaultCredentialsAsync();

            // Get mapper
            _bigtable = new Bigtable(credentials, config);

        }

        public async Task UpdateValueConditionally()
        {
            // Describe prototype
            var table = new SomeSimpleTable {IntKey = 1, StringKey = "a"};

            // Set value to pig if not specified
            await _bigtable.UpdateWhenFalseAsync(table, row => row.StringValue.IsSpecified, x => "Pig" );

            // Better to be the chicken than the pig
            await _bigtable.UpdateWhenTrueAsync(table, row => row.StringValue == "Pig", x => "Chicken" );
        }

        public async Task<IEnumerable<SomeSimpleTable>> ScanSimpleTablePartialKey()
        {
            var start = new SomeSimpleTable { IntKey = 1 };

            return await _bigtable.ScanAsync(start, rowLimit: 10);
        }

        public async Task<IEnumerable<SomeSimpleTable>> ScanSimpleTable()
        {
            var start = new SomeSimpleTable { IntKey = 1 };
            var end = new SomeSimpleTable { IntKey = 2 };

            return await _bigtable.ScanAsync(start, end, 10);
        }


    }
}
