using System;
using System.Globalization;
using System.IO;
using BigtableNet.Common;

namespace Examples.Bootstrap
{
    public static class Utilities
    {
        public static BigtableConfig GetConfig()
        {
            try
            {
                // Notify user
                Console.WriteLine("Loading config from: " + Constants.ConfigFileLocation);

                // Load config
                return BigtableConfig.Load(Constants.ConfigFileLocation);
            }
            catch (Exception exception)
            {
                // Notify user
                CommandLine.InformUser("Fatal", "Could not load config file: " + Constants.ConfigFileLocation);
                CommandLine.RenderException(exception);
                CommandLine.WaitForUserAndThen("exit");

                // Giveup
                return null;
            }
        }

        public static int DateToInt(DateTime date)
        {
            return Convert.ToInt32(date.ToString("yyyyMMdd"));
        }

        public static DateTime? IntToDate(int dateInt)
        {
            DateTime date;

            if (DateTime.TryParseExact(dateInt.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return date;
            }

            return null;
        }
    }
}
