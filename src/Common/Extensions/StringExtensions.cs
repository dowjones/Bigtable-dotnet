using System.Linq;

namespace BigtableNet.Common.Extensions
{
    public static class StringExtensions
    {
        public static string UnCamelCase(this string value, char separator = '-')
        {
            // Count capital letters
            int upperCount = value.Skip(1).Count(char.IsUpper);

            // Create character array for new name with enough room for dashes
            var length = value.Length + upperCount;
            var results = new char[length];

            // Copy over the first character
            results[0] = char.ToLowerInvariant(value[0]);

            // Iterate the string, skipping the first character
            var index = 1;
            for (int x = 1; x < value.Length; x++)
            {
                if (char.IsUpper(value[x]))
                {
                    // Insert dash and then lower-cased char
                    results[index++] = separator;
                    results[index++] = char.ToLowerInvariant(value[x]);
                }
                else
                    results[index++] = value[x];
            }

            // Return results
            return new string(results, 0, length);

        }

        public static bool IsValidBigtableQualifier(this string value)
        {
            // TODO: Regex [A-Za-z_$][A-Za-z_$0-9]*
            return true;
        }
    }
}
