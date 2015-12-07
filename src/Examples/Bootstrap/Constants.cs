using System.Dynamic;
using System.IO;

namespace Examples.Bootstrap
{
    public static class Constants
    {
        public static readonly string[] ExamplesTables = { PricingTable };
        public static readonly string ColumnFamilyName = "day";

        // Loader 
        public const string PricingTable = "ExamplesPricing";
        public const int PricingIdsToCreate = 10;
        public const int PricingRowsPerId = 60;

        // Examples
        public const long ScanLimit = 100;
        public const string SeekKey = "1234:20150506";
        public const string ScanKeyStart = "1234:";
        public const string ScanKeyEnd = "1234::";

        // Mapped 
        public const long SeekId = 1234;
        public const int SeekDate = 20150506;

        public static string ConfigFileLocation { get; private set; }

        static Constants()
        {
            const string exampleConfigFile = "examples.config.json";

            // Convert to full path just for user-friendliness
            var basePath = Path.GetFullPath(Path.Combine("..", "..", "..", "..", "..", "config"));
            ConfigFileLocation = Path.Combine(basePath, exampleConfigFile);
        }
    }
}
