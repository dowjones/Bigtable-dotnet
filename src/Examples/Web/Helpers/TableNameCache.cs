using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Examples.Web.Helpers
{
    public interface ITableNameCache
    {
        IEnumerable<string> Names { get; }
    }


    public class TableNameCache : ITableNameCache
    {
        private readonly IClientFactory _clientFactory;
        private readonly object _mutex = new object();

        private List<string> _tableNames;
        private bool _initialized;
        private DateTime _expirationMoment = DateTime.MinValue;


        public TableNameCache(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }



        public IEnumerable<string> Names
        {
            get
            {
                DateTime now = DateTime.UtcNow;

                // TODO - if expired, fire off background worker to do the update; for now, just be lazy
                lock (_mutex)
                {
                    // Fetch inside the lock so multiple clients do not pile up requests
                    if (!_initialized || (now > _expirationMoment))
                    {
                        FetchNamesAsync().Wait();
                    }

                    return _tableNames;
                }
            }
        }



        private async Task FetchNamesAsync()
        {
            // NOTE: assumes the caller has locked the mutex
            using (var adminClient = _clientFactory.GetAdminClient())
            {
                var tables = await adminClient.ListTablesAsync();

                var names = tables.Select(x => x.Name).ToList();

                _tableNames = names;
                _initialized = true;
                _expirationMoment = DateTime.UtcNow.AddMinutes(5);
            }
        }
    }
}
