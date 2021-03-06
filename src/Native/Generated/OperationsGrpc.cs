// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: google/longrunning/operations.proto
#region Designer generated code

using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;

namespace Google.Longrunning {
  public static class Operations
  {
    static readonly string __ServiceName = "google.longrunning.Operations";

    static readonly Marshaller<global::Google.Longrunning.GetOperationRequest> __Marshaller_GetOperationRequest = Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Google.Longrunning.GetOperationRequest.Parser.ParseFrom);
    static readonly Marshaller<global::Google.Longrunning.Operation> __Marshaller_Operation = Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Google.Longrunning.Operation.Parser.ParseFrom);
    static readonly Marshaller<global::Google.Longrunning.ListOperationsRequest> __Marshaller_ListOperationsRequest = Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Google.Longrunning.ListOperationsRequest.Parser.ParseFrom);
    static readonly Marshaller<global::Google.Longrunning.ListOperationsResponse> __Marshaller_ListOperationsResponse = Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Google.Longrunning.ListOperationsResponse.Parser.ParseFrom);
    static readonly Marshaller<global::Google.Longrunning.CancelOperationRequest> __Marshaller_CancelOperationRequest = Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Google.Longrunning.CancelOperationRequest.Parser.ParseFrom);
    static readonly Marshaller<global::Google.Protobuf.Empty> __Marshaller_Empty = Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Google.Protobuf.Empty.Parser.ParseFrom);
    static readonly Marshaller<global::Google.Longrunning.DeleteOperationRequest> __Marshaller_DeleteOperationRequest = Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Google.Longrunning.DeleteOperationRequest.Parser.ParseFrom);

    static readonly Method<global::Google.Longrunning.GetOperationRequest, global::Google.Longrunning.Operation> __Method_GetOperation = new Method<global::Google.Longrunning.GetOperationRequest, global::Google.Longrunning.Operation>(
        MethodType.Unary,
        __ServiceName,
        "GetOperation",
        __Marshaller_GetOperationRequest,
        __Marshaller_Operation);

    static readonly Method<global::Google.Longrunning.ListOperationsRequest, global::Google.Longrunning.ListOperationsResponse> __Method_ListOperations = new Method<global::Google.Longrunning.ListOperationsRequest, global::Google.Longrunning.ListOperationsResponse>(
        MethodType.Unary,
        __ServiceName,
        "ListOperations",
        __Marshaller_ListOperationsRequest,
        __Marshaller_ListOperationsResponse);

    static readonly Method<global::Google.Longrunning.CancelOperationRequest, global::Google.Protobuf.Empty> __Method_CancelOperation = new Method<global::Google.Longrunning.CancelOperationRequest, global::Google.Protobuf.Empty>(
        MethodType.Unary,
        __ServiceName,
        "CancelOperation",
        __Marshaller_CancelOperationRequest,
        __Marshaller_Empty);

    static readonly Method<global::Google.Longrunning.DeleteOperationRequest, global::Google.Protobuf.Empty> __Method_DeleteOperation = new Method<global::Google.Longrunning.DeleteOperationRequest, global::Google.Protobuf.Empty>(
        MethodType.Unary,
        __ServiceName,
        "DeleteOperation",
        __Marshaller_DeleteOperationRequest,
        __Marshaller_Empty);

    // service descriptor
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Google.Longrunning.Proto.Operations.Descriptor.Services[0]; }
    }

    // client interface
    public interface IOperationsClient
    {
      global::Google.Longrunning.Operation GetOperation(global::Google.Longrunning.GetOperationRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken));
      global::Google.Longrunning.Operation GetOperation(global::Google.Longrunning.GetOperationRequest request, CallOptions options);
      AsyncUnaryCall<global::Google.Longrunning.Operation> GetOperationAsync(global::Google.Longrunning.GetOperationRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken));
      AsyncUnaryCall<global::Google.Longrunning.Operation> GetOperationAsync(global::Google.Longrunning.GetOperationRequest request, CallOptions options);
      global::Google.Longrunning.ListOperationsResponse ListOperations(global::Google.Longrunning.ListOperationsRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken));
      global::Google.Longrunning.ListOperationsResponse ListOperations(global::Google.Longrunning.ListOperationsRequest request, CallOptions options);
      AsyncUnaryCall<global::Google.Longrunning.ListOperationsResponse> ListOperationsAsync(global::Google.Longrunning.ListOperationsRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken));
      AsyncUnaryCall<global::Google.Longrunning.ListOperationsResponse> ListOperationsAsync(global::Google.Longrunning.ListOperationsRequest request, CallOptions options);
      global::Google.Protobuf.Empty CancelOperation(global::Google.Longrunning.CancelOperationRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken));
      global::Google.Protobuf.Empty CancelOperation(global::Google.Longrunning.CancelOperationRequest request, CallOptions options);
      AsyncUnaryCall<global::Google.Protobuf.Empty> CancelOperationAsync(global::Google.Longrunning.CancelOperationRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken));
      AsyncUnaryCall<global::Google.Protobuf.Empty> CancelOperationAsync(global::Google.Longrunning.CancelOperationRequest request, CallOptions options);
      global::Google.Protobuf.Empty DeleteOperation(global::Google.Longrunning.DeleteOperationRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken));
      global::Google.Protobuf.Empty DeleteOperation(global::Google.Longrunning.DeleteOperationRequest request, CallOptions options);
      AsyncUnaryCall<global::Google.Protobuf.Empty> DeleteOperationAsync(global::Google.Longrunning.DeleteOperationRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken));
      AsyncUnaryCall<global::Google.Protobuf.Empty> DeleteOperationAsync(global::Google.Longrunning.DeleteOperationRequest request, CallOptions options);
    }

    // server-side interface
    public interface IOperations
    {
      Task<global::Google.Longrunning.Operation> GetOperation(global::Google.Longrunning.GetOperationRequest request, ServerCallContext context);
      Task<global::Google.Longrunning.ListOperationsResponse> ListOperations(global::Google.Longrunning.ListOperationsRequest request, ServerCallContext context);
      Task<global::Google.Protobuf.Empty> CancelOperation(global::Google.Longrunning.CancelOperationRequest request, ServerCallContext context);
      Task<global::Google.Protobuf.Empty> DeleteOperation(global::Google.Longrunning.DeleteOperationRequest request, ServerCallContext context);
    }

    // client stub
    public class OperationsClient : ClientBase, IOperationsClient
    {
      public OperationsClient(Channel channel) : base(channel)
      {
      }
      public global::Google.Longrunning.Operation GetOperation(global::Google.Longrunning.GetOperationRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        var call = CreateCall(__Method_GetOperation, new CallOptions(headers, deadline, cancellationToken));
        return Calls.BlockingUnaryCall(call, request);
      }
      public global::Google.Longrunning.Operation GetOperation(global::Google.Longrunning.GetOperationRequest request, CallOptions options)
      {
        var call = CreateCall(__Method_GetOperation, options);
        return Calls.BlockingUnaryCall(call, request);
      }
      public AsyncUnaryCall<global::Google.Longrunning.Operation> GetOperationAsync(global::Google.Longrunning.GetOperationRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        var call = CreateCall(__Method_GetOperation, new CallOptions(headers, deadline, cancellationToken));
        return Calls.AsyncUnaryCall(call, request);
      }
      public AsyncUnaryCall<global::Google.Longrunning.Operation> GetOperationAsync(global::Google.Longrunning.GetOperationRequest request, CallOptions options)
      {
        var call = CreateCall(__Method_GetOperation, options);
        return Calls.AsyncUnaryCall(call, request);
      }
      public global::Google.Longrunning.ListOperationsResponse ListOperations(global::Google.Longrunning.ListOperationsRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        var call = CreateCall(__Method_ListOperations, new CallOptions(headers, deadline, cancellationToken));
        return Calls.BlockingUnaryCall(call, request);
      }
      public global::Google.Longrunning.ListOperationsResponse ListOperations(global::Google.Longrunning.ListOperationsRequest request, CallOptions options)
      {
        var call = CreateCall(__Method_ListOperations, options);
        return Calls.BlockingUnaryCall(call, request);
      }
      public AsyncUnaryCall<global::Google.Longrunning.ListOperationsResponse> ListOperationsAsync(global::Google.Longrunning.ListOperationsRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        var call = CreateCall(__Method_ListOperations, new CallOptions(headers, deadline, cancellationToken));
        return Calls.AsyncUnaryCall(call, request);
      }
      public AsyncUnaryCall<global::Google.Longrunning.ListOperationsResponse> ListOperationsAsync(global::Google.Longrunning.ListOperationsRequest request, CallOptions options)
      {
        var call = CreateCall(__Method_ListOperations, options);
        return Calls.AsyncUnaryCall(call, request);
      }
      public global::Google.Protobuf.Empty CancelOperation(global::Google.Longrunning.CancelOperationRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        var call = CreateCall(__Method_CancelOperation, new CallOptions(headers, deadline, cancellationToken));
        return Calls.BlockingUnaryCall(call, request);
      }
      public global::Google.Protobuf.Empty CancelOperation(global::Google.Longrunning.CancelOperationRequest request, CallOptions options)
      {
        var call = CreateCall(__Method_CancelOperation, options);
        return Calls.BlockingUnaryCall(call, request);
      }
      public AsyncUnaryCall<global::Google.Protobuf.Empty> CancelOperationAsync(global::Google.Longrunning.CancelOperationRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        var call = CreateCall(__Method_CancelOperation, new CallOptions(headers, deadline, cancellationToken));
        return Calls.AsyncUnaryCall(call, request);
      }
      public AsyncUnaryCall<global::Google.Protobuf.Empty> CancelOperationAsync(global::Google.Longrunning.CancelOperationRequest request, CallOptions options)
      {
        var call = CreateCall(__Method_CancelOperation, options);
        return Calls.AsyncUnaryCall(call, request);
      }
      public global::Google.Protobuf.Empty DeleteOperation(global::Google.Longrunning.DeleteOperationRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        var call = CreateCall(__Method_DeleteOperation, new CallOptions(headers, deadline, cancellationToken));
        return Calls.BlockingUnaryCall(call, request);
      }
      public global::Google.Protobuf.Empty DeleteOperation(global::Google.Longrunning.DeleteOperationRequest request, CallOptions options)
      {
        var call = CreateCall(__Method_DeleteOperation, options);
        return Calls.BlockingUnaryCall(call, request);
      }
      public AsyncUnaryCall<global::Google.Protobuf.Empty> DeleteOperationAsync(global::Google.Longrunning.DeleteOperationRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        var call = CreateCall(__Method_DeleteOperation, new CallOptions(headers, deadline, cancellationToken));
        return Calls.AsyncUnaryCall(call, request);
      }
      public AsyncUnaryCall<global::Google.Protobuf.Empty> DeleteOperationAsync(global::Google.Longrunning.DeleteOperationRequest request, CallOptions options)
      {
        var call = CreateCall(__Method_DeleteOperation, options);
        return Calls.AsyncUnaryCall(call, request);
      }
    }

    // creates service definition that can be registered with a server
    public static ServerServiceDefinition BindService(IOperations serviceImpl)
    {
      return ServerServiceDefinition.CreateBuilder(__ServiceName)
          .AddMethod(__Method_GetOperation, serviceImpl.GetOperation)
          .AddMethod(__Method_ListOperations, serviceImpl.ListOperations)
          .AddMethod(__Method_CancelOperation, serviceImpl.CancelOperation)
          .AddMethod(__Method_DeleteOperation, serviceImpl.DeleteOperation).Build();
    }

    // creates a new client
    public static OperationsClient NewClient(Channel channel)
    {
      return new OperationsClient(channel);
    }

  }
}
#endregion
