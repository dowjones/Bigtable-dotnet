using System.Runtime.Hosting;
using BigtableNet.Common;
using BigtableNet.Models.Clients;
using NUnit.Framework;

namespace BigtableNet.Tests
{
    [TestFixture]
    public class Temp
    {
        private BigAdminClient _admin;
        private BigDataClient _data;

        [OneTimeSetUp]
        public void Setup()
        {
            var test = "test";
            var config = new BigtableConfig(test, test, test);
            var creds = new BigtableCredentials();
            _admin = new BigAdminClient(creds, config);
            _data = new BigDataClient(creds, config);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _admin.Dispose();
            _data.Dispose();
        }

        [Test]
        public void Todo()
        {
            Assert.That(_admin != null, "AdminClient created");
            Assert.That(_data != null, "DataClient created");
        }
    }
}
