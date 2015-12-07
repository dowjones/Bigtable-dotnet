using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;

namespace BigtableNet.Common.Extensions
{
    public static class TypeExtensions
    {
        public static string SimpleName(this Type type)
        {
            using (var csCode = new CSharpCodeProvider())
            {
                var typeRef = new CodeTypeReference(type);
                return csCode.GetTypeOutput(typeRef);
            }
        }
    }
}
