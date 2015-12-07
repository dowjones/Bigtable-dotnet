## Bigtable.NET Models Library ##

This library provides an abstraction on top of (BigtableNET.Native)[../Native] which handles the Bigtable API eccentricities.

All of the Bigtable resources are wrapped, although most methods allow you to use as little or as much of the abstraction as is desired.  For instance, you can request a row using the table name as a ```string```, or by using a ```BigTable``` object received by ```GetTableAsync``` or simply instantiating an instance.  You may request a list of rows, similarly, specifying ```string``` keys, ```byte[]``` keys, or by providing a low-level ```RowFilter```.


## Usage Notes ##

You MUST take a reference to BigtableNet.Native in order for the Grpc library to be included in your build.   The compiler honors a referenced library's targets, but does not honor a reference of a reference's targets.

## Design ##

The clients provide signatures with permutations which allow callers to pass either model objects, strings, or byte arrays, where appropriate, as well as signatures which accept the request parameters or accept the native request object when not all situations are covered by the method group.

For example, get rows:

```
GetRowsAsync(BigTable table, string startKey = "", string endKey = "", ...)
GetRowsAsync(BigTable table, byte[] startKey = null, byte[] endKey = null, ...)
GetRowsAsync(BigTable table, RowFilter filter, ...)

GetRowsAsync(string tableName, string startKey = "", string endKey = "", ...)
GetRowsAsync(string tableName, byte[] startKey = null, byte[] endKey = null, ...)
GetRowsAsync(string tableName, RowFilter filter, ...)
```

All methods are async.  The non-async wrappers are not provided because it would double the number of signatures I'd need to maintain.  Therefore, for consumers that cannot utilize async methods, simply append ```.Result``` for methods that return results or ```.Wait()``` for methods that do not return a result.

Because the grpc layer uses a thread pool, each async method contains a call to Task.Yield to return to the calling context before returning to the consumer.  Observable methods also do this, however the ```Subscribe``` method will place the subscriber on the grpc thread context.  It is important to call ```Dispose()``` on the return value of subscribe at the appropriate time (or use ```using```).



## Development Status

STATE: Functional

TODO: Create autodoc and insert here. 