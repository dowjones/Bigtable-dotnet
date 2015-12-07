using System;
using System.Collections.Generic;
using System.Linq;
using BigtableNet.Models.Types;

namespace Examples.Bootstrap
{
    public static class CommandLine
    {
        public static bool ValidateIntent(string message)
        {
            Console.WriteLine(message);
            Console.Write("Are you sure? [y/N] ");
            var key = Console.ReadKey(true);
            var yes = key.KeyChar == 'y' || key.KeyChar == 'Y';
            Console.WriteLine(yes ? "Yes" : "No");
            return yes;
        }

        public static void WaitForUserAndThen(string nextThing)
        {
            Console.WriteLine();
            InformUser("User", "Press any key to " + nextThing + "...");
            Console.ReadKey(true);
            Console.WriteLine();
        }

        public static void DisplayRows(IEnumerable<BigRow> rows)
        {
            foreach (var row in rows)
            {
                Console.WriteLine(new String('-', 20));
                Console.WriteLine("Key: " + row.KeyString);
                Console.WriteLine(new String('-', 20));
                foreach (var field in row.FieldsByFamily)
                {
                    Console.WriteLine(String.Join(Environment.NewLine, field.Value.Values.SelectMany(v => v).Select(v => String.Concat(field.Key, ":", v.ColumnName, " = ", v.StringValue))));
                }
                Console.WriteLine(new String('-', 20));
                Console.WriteLine();
            }
        }

        public static void DisplayTables(IEnumerable<BigTable> tables)
        {
            Console.WriteLine();
            Console.WriteLine("Available Tables: " + String.Join(", ", tables.Select(table => table.Name)));
        }

        public static void InformUser(string state, string format, params object[] args )
        {
            Console.WriteLine(String.Concat("[", state, "] ", String.Format(format, args)));
        }

        public static void RenderException(Exception exception)
        {
            // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull // But doesn't need to be
            if (exception is AggregateException)
            {
                var exceptions = ((AggregateException)exception).InnerExceptions;
                foreach (var inner in exceptions)
                {
                    Console.WriteLine("Reason: " + inner);
                }
            }
            else
            {
                Console.WriteLine("Reason: " + exception);
            }
        }
    }
}
