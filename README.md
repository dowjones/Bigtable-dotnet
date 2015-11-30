## Google Cloud Bigtable .NET Client

Google Bigtable is Google's latest NoSQL database which offers a fully managed, scaling solution for big data applications.

Initially the product was released with only two native libraries: Java and Go.  The Java implementation is an augmentation of the Apache HBase API.  The Go implementation provides a wrapper around the low-level Bigtable API to take care of some of the eccentricities therein.

This library provides a low-level interface similar to Go's implementation.

## Overview

The Bigtable API is a combination of Google's protobuf serialization framework and the parent project, gRPC, which wraps provides the transport of protobuf serialized messages to well-defined RPC end-points.

This library uses the [grpc](http://grpc.io), compiled for and consumed by a set of classes which manage asynchronous authentication, serialization, and transportation of the protobuf encoding messages.

BigTable.Net -> BigTableSharp -> BigTableClient