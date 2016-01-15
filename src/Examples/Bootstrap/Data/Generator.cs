using System;
using System.Collections.Generic;

namespace Examples.Bootstrap.Data
{
    using DataFields = Dictionary<string, object>;
    using DataRows = List<Dictionary<string, object>>;
    static class Generator
    {
        public static DataRows GeneratePricing(int idCount, int rowsPerId)
        {
            // Constants
            const double openStart = 42.0d;
            const double closeStart = 44.5d;
            const double lowStart = 41.7d;
            const double highStart = 46.3;
            const double variance = 0.3d;
            const int idStart = (int) Constants.SeekId;
            const int idSep = 1000;
            const int idInc = 11;
            DateTime dateStart = DateTime.Parse("2015-05-06");

            // Locals
            var results = new DataRows();
            var rand = new Random();

            int id = idStart;
            for (int idIndex = 0; idIndex < idCount; idIndex++)
            {
                // Locals
                double open = openStart, close = closeStart;
                double high = highStart, low = lowStart;
                DateTime date = dateStart;

                for (int index = 0; index < rowsPerId; index++)
                {
                    // Store fields in easily consumable format
                    results.Add(new DataFields
                    {
                        {"Key", String.Concat(id, ":", Utilities.DateToInt(date))},
                        {"Open", ((int) (open*100))/100d},
                        {"Close", ((int) (close*100))/100d},
                        {"High", ((int) (high*100))/100d},
                        {"Low", ((int) (low*100))/100d},
                        {"Note", "StringValue" },
                    });

                    // Adjust values
                    var direction = rand.NextDouble() > 0.5d ? 1d : -1d;
                    open += direction*variance*rand.NextDouble();
                    close += direction*variance*rand.NextDouble();
                    high += direction*variance*rand.NextDouble();
                    low += direction*variance*rand.NextDouble();
                    date = date.AddDays(1);

                    // Have them make some sense
                    if (open > high) high = open;
                    if (close > high) high = close;
                    if (open < low) low = open;
                    if (close < low) low = close;

                }
                int idPlus = idIndex + 1;
                id += idSep * idPlus + idInc * idPlus + rand.Next(idInc, idSep);

            }
            return results;
        }
    }
}
