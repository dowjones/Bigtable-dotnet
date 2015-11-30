// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: google/protobuf/field_mask.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Google.Protobuf {

  namespace Proto {

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public static partial class FieldMask {

      #region Descriptor
      public static pbr::FileDescriptor Descriptor {
        get { return descriptor; }
      }
      private static pbr::FileDescriptor descriptor;

      static FieldMask() {
        byte[] descriptorData = global::System.Convert.FromBase64String(
            string.Concat(
              "CiBnb29nbGUvcHJvdG9idWYvZmllbGRfbWFzay5wcm90bxIPZ29vZ2xlLnBy", 
              "b3RvYnVmIhoKCUZpZWxkTWFzaxINCgVwYXRocxgBIAMoCUInChNjb20uZ29v", 
              "Z2xlLnByb3RvYnVmQg5GaWVsZE1hc2tQcm90b1ABYgZwcm90bzM="));
        descriptor = pbr::FileDescriptor.InternalBuildGeneratedFileFrom(descriptorData,
            new pbr::FileDescriptor[] { },
            new pbr::GeneratedCodeInfo(null, new pbr::GeneratedCodeInfo[] {
              new pbr::GeneratedCodeInfo(typeof(global::Google.Protobuf.FieldMask), new[]{ "Paths" }, null, null, null)
            }));
      }
      #endregion

    }
  }
  #region Messages
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class FieldMask : pb::IMessage<FieldMask> {
    private static readonly pb::MessageParser<FieldMask> _parser = new pb::MessageParser<FieldMask>(() => new FieldMask());
    public static pb::MessageParser<FieldMask> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Protobuf.Proto.FieldMask.Descriptor.MessageTypes[0]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public FieldMask() {
      OnConstruction();
    }

    partial void OnConstruction();

    public FieldMask(FieldMask other) : this() {
      paths_ = other.paths_.Clone();
    }

    public FieldMask Clone() {
      return new FieldMask(this);
    }

    public const int PathsFieldNumber = 1;
    private static readonly pb::FieldCodec<string> _repeated_paths_codec
        = pb::FieldCodec.ForString(10);
    private readonly pbc::RepeatedField<string> paths_ = new pbc::RepeatedField<string>();
    public pbc::RepeatedField<string> Paths {
      get { return paths_; }
    }

    public override bool Equals(object other) {
      return Equals(other as FieldMask);
    }

    public bool Equals(FieldMask other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!paths_.Equals(other.paths_)) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      hash ^= paths_.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.Default.Format(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      paths_.WriteTo(output, _repeated_paths_codec);
    }

    public int CalculateSize() {
      int size = 0;
      size += paths_.CalculateSize(_repeated_paths_codec);
      return size;
    }

    public void MergeFrom(FieldMask other) {
      if (other == null) {
        return;
      }
      paths_.Add(other.paths_);
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            paths_.AddEntriesFrom(input, _repeated_paths_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
