using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Models.Abstraction;
using BigtableNet.Models.Clients;
using BigtableNet.Models.Extensions;
using Google.Bigtable.Admin.Table.V1;

namespace BigtableNet.Models.Types
{
    /// <summary>
    /// Name: [-_.a-zA-Z0-9]{1,64}
    /// </summary>
    public class BigFamily : BigModel
    {
        public RetentionPolicy RetentionPolicy { get; private set; }

        internal BigFamily(ColumnFamily family, string tableId )
        {
            var prefix = tableId + BigtableConstants.Templates.FamilyAdjunct;
            Name = family.Name.Replace(prefix, "");
            RetentionPolicy = new RetentionPolicy(family.GcRule, family.GcExpression);
        }

        internal BigFamily(string familyName)
        {
            // Used for table creation
            Name = familyName;
        }

        public BigFamily(string familyName, RetentionPolicy retentionPolicy = default(RetentionPolicy))
        {
            Name = familyName;
            RetentionPolicy = retentionPolicy;
        }

        public ColumnFamily AsApiObject()
        {
            return RetentionPolicy.ToColumnFamilyPrototype();
        }
    }
}
