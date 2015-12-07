using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Shared = BigtableNet.Common.ProductInfo;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle(Shared.Name + " Models Library")]
[assembly: AssemblyDescription(Shared.Description)]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(Shared.Company)]
[assembly: AssemblyProduct(Shared.Name)]
[assembly: AssemblyCopyright(Shared.Copyright)]
[assembly: AssemblyTrademark(Shared.Trademark)]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion(Shared.Version)]
[assembly: AssemblyFileVersion(Shared.FileVersion)]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("e2b66b7a-f748-470d-bea6-deadee13ae30")]
[assembly: InternalsVisibleTo("BigtableNet.Mapper")]