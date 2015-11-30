using System.Linq;
using Newtonsoft.Json.Serialization;

namespace BigtableNet.Common.SerializationResolvers
{
    /// <summary>
    /// Dashes should totally auto-map to camelCase.
    /// </summary>
    class DashContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            // Count capital letters
            int upperCount = propertyName.Skip(1).Count(char.IsUpper);

            // Create character array for new name
            char[] newName = new char[propertyName.Length + upperCount];

            // Copy over the first character
            newName[0] = char.ToLowerInvariant(propertyName[0]);

            // Fill the character, and an extra dash for every upper letter
            int index = 1;
            for (int iProperty = 1; iProperty < propertyName.Length; iProperty++)
            {
                if (char.IsUpper(propertyName[iProperty]))
                {
                    // Insert dash and then lower-cased char
                    newName[index++] = '-';
                    newName[index++] = char.ToLowerInvariant(propertyName[iProperty]);
                }
                else
                    newName[index++] = propertyName[iProperty];
            }

            // Return results
            return new string(newName, 0, index);
        }
    }
}
