using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Mapper;
using Examples.Bootstrap;
using Examples.Mapped.Schema.Tables;
using Grpc.Core;

namespace Examples.Mapped
{
    public static class Example
    {
        public static async Task Run()
        {
            // Disable Grpc logging
            GrpcEnvironment.DisableLogging();

            try
            {
                // Create config
                var config = Utilities.GetConfig();

                // Create credentials
                var credentials = await BigtableCredentials.UseApplicationDefaultCredentialsAsync();

                // Mapper client
                using (var bigtable = new Bigtable(credentials, config))
                {
                    var tableExists = await bigtable.TableExists<Pricing>();

                    // Ensure pricing table exists
                    if (!tableExists)
                    {
                        // Inform user
                        CommandLine.InformUser("Setup", "Missing example table.  Please run the Examples.Bootstrap project.");

                        // Hard stop
                        return;
                    }

                    //// -----------------------------------------------------------------------------------------------------------------///

                    //// Inform User
                    //CommandLine.InformUser("Start", "Getting first pricing poco record");

                    //// Seek one record
                    //var firstPricing = await bigtable.GetFirstRowAsync<Pricing>();

                    //// Show user
                    //DisplayPricing(firstPricing);

                    //// -----------------------------------------------------------------------------------------------------------------///

                    //// Inform User
                    //CommandLine.WaitForUserAndThen("get keyed pricing poco");
                    
                    //// Seek one record
                    //var pricing = await bigtable.GetAsync(new Pricing { Id = Constants.SeekId, Date = Constants.SeekDate });

                    //// Show user
                    //DisplayPricing(pricing);

                    //// -----------------------------------------------------------------------------------------------------------------///

                    //// Inform User
                    //CommandLine.WaitForUserAndThen("get alternate-schema pricing poco");

                    //// Seek one record using alternate schema
                    //var simplePricing = await bigtable.GetAsync(new SimplePricing {Id = Constants.SeekId, Date = Constants.SeekDate});

                    //// Show user
                    //DisplayPricing(FromSimplePricing(simplePricing));

                    //// -----------------------------------------------------------------------------------------------------------------///

                    //// Inform User
                    //CommandLine.WaitForUserAndThen("scan partially-keyed pricing poco");

                    //// Seek one record
                    //var pricings = await bigtable.ScanAsync(new Pricing { Id = Constants.SeekId }, new Pricing { Id = Constants.SeekId + 1 });

                    //// Show user
                    //DisplayPricing(pricings.ToArray());


                    // -----------------------------------------------------------------------------------------------------------------///

                    // Inform User
                    CommandLine.WaitForUserAndThen("read write read");

                    // Seek one record
                    var rewrite = await bigtable.GetAsync(new Pricing { Id = Constants.SeekId, Date = Constants.SeekDate + 1 });

                    DisplayPricing(rewrite);

                    var tempHigh = rewrite.High;
                    var tempLow = rewrite.Low;

                    rewrite.High = 142;
                    rewrite.Low = null;

                    await bigtable.UpdateAsync(rewrite);

                    var rewriteRead = await bigtable.GetAsync(new Pricing { Id = Constants.SeekId, Date = Constants.SeekDate + 1 });
                    DisplayPricing(rewrite,rewriteRead);
                    rewrite.High = tempHigh;
                    rewrite.Low = tempLow;
                    await bigtable.UpdateAsync(rewrite);
                }
            }
            catch (Exception exception)
            {
                CommandLine.InformUser("Oops", "Example didn't work out as planned");
                CommandLine.RenderException(exception);
            }
            finally
            {
                // All done
                CommandLine.WaitForUserAndThen("exit");
            }

        }


        private const string Format = "|{0,8}|{1,-11}|{2,9}|{3,9}|{4,9}|{5,9}|";
        private const string Dividers = "|--------|-----------|---------|---------|---------|---------|";

        private static Pricing FromSimplePricing(SimplePricing pricing)
        {
            return new Pricing
            {
                Id = pricing.Id,
                Date = pricing.Date,
                Open = pricing.Open,
                Close = pricing.Close,
                High = pricing.High,
                Low = pricing.Low
            };
        }
        private static void DisplayPricing(params Pricing[] records)
        {

            Console.WriteLine(Dividers);
            Console.WriteLine(Format, "Id", "Date", "Open", "High", "Low", "Close");
            Console.WriteLine(Dividers);

            foreach (var record in records)
                DisplayPricingRecord(record);

            Console.WriteLine();
        }

        private static void DisplayPricingRecord(Pricing data)
        {
            Console.WriteLine(
                Format,
                data.Id,
                data.Date,
                FormatPrice(data.Open),
                FormatPrice(data.High),
                FormatPrice(data.Low),
                FormatPrice(data.Close)
            );
        }

        private static string FormatDouble(double? val, string format)
        {
            return val.HasValue ? val.Value.ToString(format) : String.Empty;
        }

        private static string FormatPrice(double? price)
        {
            return FormatDouble(price, "0.000");
        }
    }
}
