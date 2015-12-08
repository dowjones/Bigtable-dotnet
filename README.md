## Google Cloud Bigtable .NET Client ##

[Google Bigtable](https://cloud.google.com/bigtable/) is a NoSQL database which offers a fully managed, scaling solution for big data applications.  It's the same database that powers many core Google services, including Search, Analytics, Maps, and Gmail.

Initially Bigtable was released with only two native libraries: Java and Go.  The Java implementation is an augmentation of the Apache HBase API to work with Bigtable.  The Go implementation provides a wrapper around the low-level Bigtable API to take care of some of the eccentricities therein.

This library provides the first native client for connecting your .NET applications with Bigtable.  It is cross-platform in architecture, and has been tested on 32-bit and 64-bit Windows environments and well as on Ubuntu 15.04 x64.  In theory, it should work on Mac OSX.  See the [Linux installation notes](doc/Installation.md#deploying-to-linux) for more information.  

You can read more about Google Bigtable's design in their [white paper](http://static.googleusercontent.com/media/research.google.com/en//archive/bigtable-osdi06.pdf).


## Overview ##

The Bigtable API is a combination of Google's [protobuf](https://developers.google.com/protocol-buffers/?hl=en) serialization framework and [gRPC](http://grpc.io), which provides the transport of protobuf serialized messages to well-defined RPC end-points.

This library uses the [Bigtable protobuf library](https://github.com/GoogleCloudPlatform/cloud-bigtable-client/tree/master/bigtable-protos) and pairs that with the [grpc implementation for C#](https://github.com/grpc/grpc/tree/master/src/csharp).  This foundation manages asynchronous authentication, serialization, and transportation of the protobuf encoding messages to the Bigtable API servers.

This library is designed with three tiers of functionality from which to choose: 

1. **[Native](src/Native):** Directly consume the protobuf/grpc layer.

2. **[Models](src/Models):** An abstraction of the Bigtable resources which simplifies most tasks but does not interfere with using lower level functionality (i.e. providing the RowFilter directly).

3. **[Mapped](src/Mapped):** An additional layer of abstraction which provides both CRUD and advanced operations on POCOs.  There are Field and Key abstraction classes as well as attributes which provide similar functionality to [Json.NET](http://www.newtonsoft.com/json), such as changing the storage field name vs the POCO property name, however you are not bound to using them.  Advanced operations such compare-and-swap and atomically increment are  provided.  In addition, you can inject custom serialization for your fields and/or keys, as well as use provided implementations.  One example is hashing your keys transparently (as described in the [schema design](https://cloud.google.com/bigtable/docs/schema-design#types_of_row_keys) document).   

More information about the design of this library can be found [in the documentation](doc/Design.md).


## Examples ##
Show me the code!  There are four example projects included:

**[Low-Level](src/Examples/LowLevel):** Demonstrates [creating your own client](src/Examples/LowLevel/SimpleClient.cs) using the the Native library.

```csharp

var service = new BigtableTableService.BigtableTableServiceClient( _channel );
var request = new ListTablesRequest { Name = /*resource*/ };
var response = await service.ListTablesAsync( request );
return response.Tables.Select( DeconstructTableResource );

```
**[Modeled](src/Examples/Modeled):** Demonstrates [using the BigDataClient and BigAdminClient](src/Examples/Modeled/Example.cs) using the Models library.

```csharp

var row = await dataClient.GetRowAsync( "myTable", "someKey" );
var rows = await dataClient.GetRowsAsync( "myTable", "startKey", "endKey", rowLimit: 20 );

```
**[Mapped](src/Examples/Mapped):** Demonstrates [using POCOs with the Bigtable client](src/Examples/Mapped/Example.cs) using the Mapper library.

```csharp

var start = new MyPoco { IntKey = 1 };
var end = new MyPoco { IntKey = 2 };

var poco = await _bigtable.ScanAsync<MyPoco>( start, end, rowLimit: 10 );

poco.StringValue = "Pig";

await _bigtable.UpdateAsync( poco ); 

await _bigtable.UpdateWhenTrueAsync<MyPoco>(
	row => row.StringValue == "Pig", 
	x => "Chicken" 
);

```

You are not constrained to creating POCOs for read signatures, though, therefore you can specify keys directly as ```string``` or ```byte[]```:

```csharp

var poco = await _bigtable.ScanAsync<MyPoco>( "1", "2", rowLimit: 10 );

```

**[Web](src/Examples/Web):** Demonstrates [using a web-based interface](/src/Examples/Web/Example.cs) to interact with your Bigtable instance.  The web project uses Nancy (because MVC 5 is not portable yet) on top of the Models layer.

```

http://localhost:8913/big/data/sample?table=myTable&maxrows=10

```

<a name="GettingStarted"></a>
## Getting Started ##

This solution can be loaded in Microsoft Visual Studio 2013 or 2015.  It is .NET 4.5, C# 5.

After cloning this repository, you can either follow the directions in the [Installation Guide](doc/Installation.md#installation-guide) to get the gRPC submodule packages setup or work around this step by running downloading the [submodule packages](http://dowjones.github.io/Bigtable-dotnet/) and extracting in the root of the repository directory ("Extract Here").


Before proceeding, *remember that Bigtable is not a free product*, and this sample will add data to your cluster!  While it will not add much data, **by using these examples you accept all liability for the charges you incur while using Bigtable!**  At the time this was written, Google was offering a free 30-day trial to new subscribers.

In addition, the bootstrapper in the project will write to the cluster specified in the next paragraph.  It may be a good idea to setup a cluster specifically for running the examples.

Edit `config\template.json` and specify your project, zone, cluster.  You must set the ```GOOGLE_APPLICATION_CREDENTIALS``` environment variable for the examples to work.  You should download the credentials file from the [developer console](https://console.developers.google.com/permissions/serviceaccounts). 

Save the file as `config\examples.config.json`.

The very first time you run this project, you will need to populate the database with the example data tables and data.  You can do that by running the Examples.Bootstrap project.

From there you can run any of the other examples.



## Further Readering ##

- [Installation Guide](doc/Installation.md)

- [Technical notes](doc/Design.md)

- [Todo List](doc/Todo.md)

 
## Status ##

This library is functional, but under development.

Once it has reached a stable state, we will provide the core libraries as [NuGet packages](https://www.nuget.org/).


## Disclaimer ##

This product is not affiliated with nor endorsed by Google, Microsoft, or the .NET Foundation in any way.

Please read the [LICENSE](LICENSE.md) and [DISCLAIMER](DISCLAIMER.md).
