// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: google/protobuf/type.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Google.Protobuf {

  namespace Proto {

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public static partial class Type {

      #region Descriptor
      public static pbr::FileDescriptor Descriptor {
        get { return descriptor; }
      }
      private static pbr::FileDescriptor descriptor;

      static Type() {
        byte[] descriptorData = global::System.Convert.FromBase64String(
            string.Concat(
              "Chpnb29nbGUvcHJvdG9idWYvdHlwZS5wcm90bxIPZ29vZ2xlLnByb3RvYnVm", 
              "Ghlnb29nbGUvcHJvdG9idWYvYW55LnByb3RvGiRnb29nbGUvcHJvdG9idWYv", 
              "c291cmNlX2NvbnRleHQucHJvdG8irgEKBFR5cGUSDAoEbmFtZRgBIAEoCRIm", 
              "CgZmaWVsZHMYAiADKAsyFi5nb29nbGUucHJvdG9idWYuRmllbGQSDgoGb25l", 
              "b2ZzGAMgAygJEigKB29wdGlvbnMYBCADKAsyFy5nb29nbGUucHJvdG9idWYu", 
              "T3B0aW9uEjYKDnNvdXJjZV9jb250ZXh0GAUgASgLMh4uZ29vZ2xlLnByb3Rv", 
              "YnVmLlNvdXJjZUNvbnRleHQimwUKBUZpZWxkEikKBGtpbmQYASABKA4yGy5n", 
              "b29nbGUucHJvdG9idWYuRmllbGQuS2luZBI3CgtjYXJkaW5hbGl0eRgCIAEo", 
              "DjIiLmdvb2dsZS5wcm90b2J1Zi5GaWVsZC5DYXJkaW5hbGl0eRIOCgZudW1i", 
              "ZXIYAyABKAUSDAoEbmFtZRgEIAEoCRIQCgh0eXBlX3VybBgGIAEoCRITCgtv", 
              "bmVvZl9pbmRleBgHIAEoBRIOCgZwYWNrZWQYCCABKAgSKAoHb3B0aW9ucxgJ", 
              "IAMoCzIXLmdvb2dsZS5wcm90b2J1Zi5PcHRpb24iuAIKBEtpbmQSEAoMVFlQ", 
              "RV9VTktOT1dOEAASDwoLVFlQRV9ET1VCTEUQARIOCgpUWVBFX0ZMT0FUEAIS", 
              "DgoKVFlQRV9JTlQ2NBADEg8KC1RZUEVfVUlOVDY0EAQSDgoKVFlQRV9JTlQz", 
              "MhAFEhAKDFRZUEVfRklYRUQ2NBAGEhAKDFRZUEVfRklYRUQzMhAHEg0KCVRZ", 
              "UEVfQk9PTBAIEg8KC1RZUEVfU1RSSU5HEAkSEAoMVFlQRV9NRVNTQUdFEAsS", 
              "DgoKVFlQRV9CWVRFUxAMEg8KC1RZUEVfVUlOVDMyEA0SDQoJVFlQRV9FTlVN", 
              "EA4SEQoNVFlQRV9TRklYRUQzMhAPEhEKDVRZUEVfU0ZJWEVENjQQEBIPCgtU", 
              "WVBFX1NJTlQzMhAREg8KC1RZUEVfU0lOVDY0EBIidAoLQ2FyZGluYWxpdHkS", 
              "FwoTQ0FSRElOQUxJVFlfVU5LTk9XThAAEhgKFENBUkRJTkFMSVRZX09QVElP", 
              "TkFMEAESGAoUQ0FSRElOQUxJVFlfUkVRVUlSRUQQAhIYChRDQVJESU5BTElU", 
              "WV9SRVBFQVRFRBADIqUBCgRFbnVtEgwKBG5hbWUYASABKAkSLQoJZW51bXZh", 
              "bHVlGAIgAygLMhouZ29vZ2xlLnByb3RvYnVmLkVudW1WYWx1ZRIoCgdvcHRp", 
              "b25zGAMgAygLMhcuZ29vZ2xlLnByb3RvYnVmLk9wdGlvbhI2Cg5zb3VyY2Vf", 
              "Y29udGV4dBgEIAEoCzIeLmdvb2dsZS5wcm90b2J1Zi5Tb3VyY2VDb250ZXh0", 
              "IlMKCUVudW1WYWx1ZRIMCgRuYW1lGAEgASgJEg4KBm51bWJlchgCIAEoBRIo", 
              "CgdvcHRpb25zGAMgAygLMhcuZ29vZ2xlLnByb3RvYnVmLk9wdGlvbiI7CgZP", 
              "cHRpb24SDAoEbmFtZRgBIAEoCRIjCgV2YWx1ZRgCIAEoCzIULmdvb2dsZS5w", 
              "cm90b2J1Zi5BbnlCIgoTY29tLmdvb2dsZS5wcm90b2J1ZkIJVHlwZVByb3Rv", 
              "UAFiBnByb3RvMw=="));
        descriptor = pbr::FileDescriptor.InternalBuildGeneratedFileFrom(descriptorData,
            new pbr::FileDescriptor[] { global::Google.Protobuf.Proto.Any.Descriptor, global::Google.Protobuf.Proto.SourceContext.Descriptor, },
            new pbr::GeneratedCodeInfo(null, new pbr::GeneratedCodeInfo[] {
              new pbr::GeneratedCodeInfo(typeof(global::Google.Protobuf.Type), new[]{ "Name", "Fields", "Oneofs", "Options", "SourceContext" }, null, null, null),
              new pbr::GeneratedCodeInfo(typeof(global::Google.Protobuf.Field), new[]{ "Kind", "Cardinality", "Number", "Name", "TypeUrl", "OneofIndex", "Packed", "Options" }, null, new[]{ typeof(global::Google.Protobuf.Field.Types.Kind), typeof(global::Google.Protobuf.Field.Types.Cardinality) }, null),
              new pbr::GeneratedCodeInfo(typeof(global::Google.Protobuf.Enum), new[]{ "Name", "Enumvalue", "Options", "SourceContext" }, null, null, null),
              new pbr::GeneratedCodeInfo(typeof(global::Google.Protobuf.EnumValue), new[]{ "Name", "Number", "Options" }, null, null, null),
              new pbr::GeneratedCodeInfo(typeof(global::Google.Protobuf.Option), new[]{ "Name", "Value" }, null, null, null)
            }));
      }
      #endregion

    }
  }
  #region Messages
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class Type : pb::IMessage<Type> {
    private static readonly pb::MessageParser<Type> _parser = new pb::MessageParser<Type>(() => new Type());
    public static pb::MessageParser<Type> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Protobuf.Proto.Type.Descriptor.MessageTypes[0]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public Type() {
      OnConstruction();
    }

    partial void OnConstruction();

    public Type(Type other) : this() {
      name_ = other.name_;
      fields_ = other.fields_.Clone();
      oneofs_ = other.oneofs_.Clone();
      options_ = other.options_.Clone();
      SourceContext = other.sourceContext_ != null ? other.SourceContext.Clone() : null;
    }

    public Type Clone() {
      return new Type(this);
    }

    public const int NameFieldNumber = 1;
    private string name_ = "";
    public string Name {
      get { return name_; }
      set {
        name_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public const int FieldsFieldNumber = 2;
    private static readonly pb::FieldCodec<global::Google.Protobuf.Field> _repeated_fields_codec
        = pb::FieldCodec.ForMessage(18, global::Google.Protobuf.Field.Parser);
    private readonly pbc::RepeatedField<global::Google.Protobuf.Field> fields_ = new pbc::RepeatedField<global::Google.Protobuf.Field>();
    public pbc::RepeatedField<global::Google.Protobuf.Field> Fields {
      get { return fields_; }
    }

    public const int OneofsFieldNumber = 3;
    private static readonly pb::FieldCodec<string> _repeated_oneofs_codec
        = pb::FieldCodec.ForString(26);
    private readonly pbc::RepeatedField<string> oneofs_ = new pbc::RepeatedField<string>();
    public pbc::RepeatedField<string> Oneofs {
      get { return oneofs_; }
    }

    public const int OptionsFieldNumber = 4;
    private static readonly pb::FieldCodec<global::Google.Protobuf.Option> _repeated_options_codec
        = pb::FieldCodec.ForMessage(34, global::Google.Protobuf.Option.Parser);
    private readonly pbc::RepeatedField<global::Google.Protobuf.Option> options_ = new pbc::RepeatedField<global::Google.Protobuf.Option>();
    public pbc::RepeatedField<global::Google.Protobuf.Option> Options {
      get { return options_; }
    }

    public const int SourceContextFieldNumber = 5;
    private global::Google.Protobuf.SourceContext sourceContext_;
    public global::Google.Protobuf.SourceContext SourceContext {
      get { return sourceContext_; }
      set {
        sourceContext_ = value;
      }
    }

    public override bool Equals(object other) {
      return Equals(other as Type);
    }

    public bool Equals(Type other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if(!fields_.Equals(other.fields_)) return false;
      if(!oneofs_.Equals(other.oneofs_)) return false;
      if(!options_.Equals(other.options_)) return false;
      if (!object.Equals(SourceContext, other.SourceContext)) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      hash ^= fields_.GetHashCode();
      hash ^= oneofs_.GetHashCode();
      hash ^= options_.GetHashCode();
      if (sourceContext_ != null) hash ^= SourceContext.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.Default.Format(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      fields_.WriteTo(output, _repeated_fields_codec);
      oneofs_.WriteTo(output, _repeated_oneofs_codec);
      options_.WriteTo(output, _repeated_options_codec);
      if (sourceContext_ != null) {
        output.WriteRawTag(42);
        output.WriteMessage(SourceContext);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      size += fields_.CalculateSize(_repeated_fields_codec);
      size += oneofs_.CalculateSize(_repeated_oneofs_codec);
      size += options_.CalculateSize(_repeated_options_codec);
      if (sourceContext_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(SourceContext);
      }
      return size;
    }

    public void MergeFrom(Type other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      fields_.Add(other.fields_);
      oneofs_.Add(other.oneofs_);
      options_.Add(other.options_);
      if (other.sourceContext_ != null) {
        if (sourceContext_ == null) {
          sourceContext_ = new global::Google.Protobuf.SourceContext();
        }
        SourceContext.MergeFrom(other.SourceContext);
      }
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
          case 18: {
            fields_.AddEntriesFrom(input, _repeated_fields_codec);
            break;
          }
          case 26: {
            oneofs_.AddEntriesFrom(input, _repeated_oneofs_codec);
            break;
          }
          case 34: {
            options_.AddEntriesFrom(input, _repeated_options_codec);
            break;
          }
          case 42: {
            if (sourceContext_ == null) {
              sourceContext_ = new global::Google.Protobuf.SourceContext();
            }
            input.ReadMessage(sourceContext_);
            break;
          }
        }
      }
    }

  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class Field : pb::IMessage<Field> {
    private static readonly pb::MessageParser<Field> _parser = new pb::MessageParser<Field>(() => new Field());
    public static pb::MessageParser<Field> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Protobuf.Proto.Type.Descriptor.MessageTypes[1]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public Field() {
      OnConstruction();
    }

    partial void OnConstruction();

    public Field(Field other) : this() {
      kind_ = other.kind_;
      cardinality_ = other.cardinality_;
      number_ = other.number_;
      name_ = other.name_;
      typeUrl_ = other.typeUrl_;
      oneofIndex_ = other.oneofIndex_;
      packed_ = other.packed_;
      options_ = other.options_.Clone();
    }

    public Field Clone() {
      return new Field(this);
    }

    public const int KindFieldNumber = 1;
    private global::Google.Protobuf.Field.Types.Kind kind_ = global::Google.Protobuf.Field.Types.Kind.TYPE_UNKNOWN;
    public global::Google.Protobuf.Field.Types.Kind Kind {
      get { return kind_; }
      set {
        kind_ = value;
      }
    }

    public const int CardinalityFieldNumber = 2;
    private global::Google.Protobuf.Field.Types.Cardinality cardinality_ = global::Google.Protobuf.Field.Types.Cardinality.CARDINALITY_UNKNOWN;
    public global::Google.Protobuf.Field.Types.Cardinality Cardinality {
      get { return cardinality_; }
      set {
        cardinality_ = value;
      }
    }

    public const int NumberFieldNumber = 3;
    private int number_;
    public int Number {
      get { return number_; }
      set {
        number_ = value;
      }
    }

    public const int NameFieldNumber = 4;
    private string name_ = "";
    public string Name {
      get { return name_; }
      set {
        name_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public const int TypeUrlFieldNumber = 6;
    private string typeUrl_ = "";
    public string TypeUrl {
      get { return typeUrl_; }
      set {
        typeUrl_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public const int OneofIndexFieldNumber = 7;
    private int oneofIndex_;
    public int OneofIndex {
      get { return oneofIndex_; }
      set {
        oneofIndex_ = value;
      }
    }

    public const int PackedFieldNumber = 8;
    private bool packed_;
    public bool Packed {
      get { return packed_; }
      set {
        packed_ = value;
      }
    }

    public const int OptionsFieldNumber = 9;
    private static readonly pb::FieldCodec<global::Google.Protobuf.Option> _repeated_options_codec
        = pb::FieldCodec.ForMessage(74, global::Google.Protobuf.Option.Parser);
    private readonly pbc::RepeatedField<global::Google.Protobuf.Option> options_ = new pbc::RepeatedField<global::Google.Protobuf.Option>();
    public pbc::RepeatedField<global::Google.Protobuf.Option> Options {
      get { return options_; }
    }

    public override bool Equals(object other) {
      return Equals(other as Field);
    }

    public bool Equals(Field other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Kind != other.Kind) return false;
      if (Cardinality != other.Cardinality) return false;
      if (Number != other.Number) return false;
      if (Name != other.Name) return false;
      if (TypeUrl != other.TypeUrl) return false;
      if (OneofIndex != other.OneofIndex) return false;
      if (Packed != other.Packed) return false;
      if(!options_.Equals(other.options_)) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (Kind != global::Google.Protobuf.Field.Types.Kind.TYPE_UNKNOWN) hash ^= Kind.GetHashCode();
      if (Cardinality != global::Google.Protobuf.Field.Types.Cardinality.CARDINALITY_UNKNOWN) hash ^= Cardinality.GetHashCode();
      if (Number != 0) hash ^= Number.GetHashCode();
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (TypeUrl.Length != 0) hash ^= TypeUrl.GetHashCode();
      if (OneofIndex != 0) hash ^= OneofIndex.GetHashCode();
      if (Packed != false) hash ^= Packed.GetHashCode();
      hash ^= options_.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.Default.Format(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Kind != global::Google.Protobuf.Field.Types.Kind.TYPE_UNKNOWN) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Kind);
      }
      if (Cardinality != global::Google.Protobuf.Field.Types.Cardinality.CARDINALITY_UNKNOWN) {
        output.WriteRawTag(16);
        output.WriteEnum((int) Cardinality);
      }
      if (Number != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Number);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Name);
      }
      if (TypeUrl.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(TypeUrl);
      }
      if (OneofIndex != 0) {
        output.WriteRawTag(56);
        output.WriteInt32(OneofIndex);
      }
      if (Packed != false) {
        output.WriteRawTag(64);
        output.WriteBool(Packed);
      }
      options_.WriteTo(output, _repeated_options_codec);
    }

    public int CalculateSize() {
      int size = 0;
      if (Kind != global::Google.Protobuf.Field.Types.Kind.TYPE_UNKNOWN) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Kind);
      }
      if (Cardinality != global::Google.Protobuf.Field.Types.Cardinality.CARDINALITY_UNKNOWN) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Cardinality);
      }
      if (Number != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Number);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (TypeUrl.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(TypeUrl);
      }
      if (OneofIndex != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(OneofIndex);
      }
      if (Packed != false) {
        size += 1 + 1;
      }
      size += options_.CalculateSize(_repeated_options_codec);
      return size;
    }

    public void MergeFrom(Field other) {
      if (other == null) {
        return;
      }
      if (other.Kind != global::Google.Protobuf.Field.Types.Kind.TYPE_UNKNOWN) {
        Kind = other.Kind;
      }
      if (other.Cardinality != global::Google.Protobuf.Field.Types.Cardinality.CARDINALITY_UNKNOWN) {
        Cardinality = other.Cardinality;
      }
      if (other.Number != 0) {
        Number = other.Number;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.TypeUrl.Length != 0) {
        TypeUrl = other.TypeUrl;
      }
      if (other.OneofIndex != 0) {
        OneofIndex = other.OneofIndex;
      }
      if (other.Packed != false) {
        Packed = other.Packed;
      }
      options_.Add(other.options_);
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            kind_ = (global::Google.Protobuf.Field.Types.Kind) input.ReadEnum();
            break;
          }
          case 16: {
            cardinality_ = (global::Google.Protobuf.Field.Types.Cardinality) input.ReadEnum();
            break;
          }
          case 24: {
            Number = input.ReadInt32();
            break;
          }
          case 34: {
            Name = input.ReadString();
            break;
          }
          case 50: {
            TypeUrl = input.ReadString();
            break;
          }
          case 56: {
            OneofIndex = input.ReadInt32();
            break;
          }
          case 64: {
            Packed = input.ReadBool();
            break;
          }
          case 74: {
            options_.AddEntriesFrom(input, _repeated_options_codec);
            break;
          }
        }
      }
    }

    #region Nested types
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public static partial class Types {
      public enum Kind {
        TYPE_UNKNOWN = 0,
        TYPE_DOUBLE = 1,
        TYPE_FLOAT = 2,
        TYPE_INT64 = 3,
        TYPE_UINT64 = 4,
        TYPE_INT32 = 5,
        TYPE_FIXED64 = 6,
        TYPE_FIXED32 = 7,
        TYPE_BOOL = 8,
        TYPE_STRING = 9,
        TYPE_MESSAGE = 11,
        TYPE_BYTES = 12,
        TYPE_UINT32 = 13,
        TYPE_ENUM = 14,
        TYPE_SFIXED32 = 15,
        TYPE_SFIXED64 = 16,
        TYPE_SINT32 = 17,
        TYPE_SINT64 = 18,
      }

      public enum Cardinality {
        CARDINALITY_UNKNOWN = 0,
        CARDINALITY_OPTIONAL = 1,
        CARDINALITY_REQUIRED = 2,
        CARDINALITY_REPEATED = 3,
      }

    }
    #endregion

  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class Enum : pb::IMessage<Enum> {
    private static readonly pb::MessageParser<Enum> _parser = new pb::MessageParser<Enum>(() => new Enum());
    public static pb::MessageParser<Enum> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Protobuf.Proto.Type.Descriptor.MessageTypes[2]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public Enum() {
      OnConstruction();
    }

    partial void OnConstruction();

    public Enum(Enum other) : this() {
      name_ = other.name_;
      enumvalue_ = other.enumvalue_.Clone();
      options_ = other.options_.Clone();
      SourceContext = other.sourceContext_ != null ? other.SourceContext.Clone() : null;
    }

    public Enum Clone() {
      return new Enum(this);
    }

    public const int NameFieldNumber = 1;
    private string name_ = "";
    public string Name {
      get { return name_; }
      set {
        name_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public const int EnumvalueFieldNumber = 2;
    private static readonly pb::FieldCodec<global::Google.Protobuf.EnumValue> _repeated_enumvalue_codec
        = pb::FieldCodec.ForMessage(18, global::Google.Protobuf.EnumValue.Parser);
    private readonly pbc::RepeatedField<global::Google.Protobuf.EnumValue> enumvalue_ = new pbc::RepeatedField<global::Google.Protobuf.EnumValue>();
    public pbc::RepeatedField<global::Google.Protobuf.EnumValue> Enumvalue {
      get { return enumvalue_; }
    }

    public const int OptionsFieldNumber = 3;
    private static readonly pb::FieldCodec<global::Google.Protobuf.Option> _repeated_options_codec
        = pb::FieldCodec.ForMessage(26, global::Google.Protobuf.Option.Parser);
    private readonly pbc::RepeatedField<global::Google.Protobuf.Option> options_ = new pbc::RepeatedField<global::Google.Protobuf.Option>();
    public pbc::RepeatedField<global::Google.Protobuf.Option> Options {
      get { return options_; }
    }

    public const int SourceContextFieldNumber = 4;
    private global::Google.Protobuf.SourceContext sourceContext_;
    public global::Google.Protobuf.SourceContext SourceContext {
      get { return sourceContext_; }
      set {
        sourceContext_ = value;
      }
    }

    public override bool Equals(object other) {
      return Equals(other as Enum);
    }

    public bool Equals(Enum other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if(!enumvalue_.Equals(other.enumvalue_)) return false;
      if(!options_.Equals(other.options_)) return false;
      if (!object.Equals(SourceContext, other.SourceContext)) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      hash ^= enumvalue_.GetHashCode();
      hash ^= options_.GetHashCode();
      if (sourceContext_ != null) hash ^= SourceContext.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.Default.Format(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      enumvalue_.WriteTo(output, _repeated_enumvalue_codec);
      options_.WriteTo(output, _repeated_options_codec);
      if (sourceContext_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(SourceContext);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      size += enumvalue_.CalculateSize(_repeated_enumvalue_codec);
      size += options_.CalculateSize(_repeated_options_codec);
      if (sourceContext_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(SourceContext);
      }
      return size;
    }

    public void MergeFrom(Enum other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      enumvalue_.Add(other.enumvalue_);
      options_.Add(other.options_);
      if (other.sourceContext_ != null) {
        if (sourceContext_ == null) {
          sourceContext_ = new global::Google.Protobuf.SourceContext();
        }
        SourceContext.MergeFrom(other.SourceContext);
      }
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
          case 18: {
            enumvalue_.AddEntriesFrom(input, _repeated_enumvalue_codec);
            break;
          }
          case 26: {
            options_.AddEntriesFrom(input, _repeated_options_codec);
            break;
          }
          case 34: {
            if (sourceContext_ == null) {
              sourceContext_ = new global::Google.Protobuf.SourceContext();
            }
            input.ReadMessage(sourceContext_);
            break;
          }
        }
      }
    }

  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class EnumValue : pb::IMessage<EnumValue> {
    private static readonly pb::MessageParser<EnumValue> _parser = new pb::MessageParser<EnumValue>(() => new EnumValue());
    public static pb::MessageParser<EnumValue> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Protobuf.Proto.Type.Descriptor.MessageTypes[3]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public EnumValue() {
      OnConstruction();
    }

    partial void OnConstruction();

    public EnumValue(EnumValue other) : this() {
      name_ = other.name_;
      number_ = other.number_;
      options_ = other.options_.Clone();
    }

    public EnumValue Clone() {
      return new EnumValue(this);
    }

    public const int NameFieldNumber = 1;
    private string name_ = "";
    public string Name {
      get { return name_; }
      set {
        name_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public const int NumberFieldNumber = 2;
    private int number_;
    public int Number {
      get { return number_; }
      set {
        number_ = value;
      }
    }

    public const int OptionsFieldNumber = 3;
    private static readonly pb::FieldCodec<global::Google.Protobuf.Option> _repeated_options_codec
        = pb::FieldCodec.ForMessage(26, global::Google.Protobuf.Option.Parser);
    private readonly pbc::RepeatedField<global::Google.Protobuf.Option> options_ = new pbc::RepeatedField<global::Google.Protobuf.Option>();
    public pbc::RepeatedField<global::Google.Protobuf.Option> Options {
      get { return options_; }
    }

    public override bool Equals(object other) {
      return Equals(other as EnumValue);
    }

    public bool Equals(EnumValue other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if (Number != other.Number) return false;
      if(!options_.Equals(other.options_)) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (Number != 0) hash ^= Number.GetHashCode();
      hash ^= options_.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.Default.Format(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (Number != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Number);
      }
      options_.WriteTo(output, _repeated_options_codec);
    }

    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Number != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Number);
      }
      size += options_.CalculateSize(_repeated_options_codec);
      return size;
    }

    public void MergeFrom(EnumValue other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.Number != 0) {
        Number = other.Number;
      }
      options_.Add(other.options_);
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
          case 16: {
            Number = input.ReadInt32();
            break;
          }
          case 26: {
            options_.AddEntriesFrom(input, _repeated_options_codec);
            break;
          }
        }
      }
    }

  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class Option : pb::IMessage<Option> {
    private static readonly pb::MessageParser<Option> _parser = new pb::MessageParser<Option>(() => new Option());
    public static pb::MessageParser<Option> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Protobuf.Proto.Type.Descriptor.MessageTypes[4]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public Option() {
      OnConstruction();
    }

    partial void OnConstruction();

    public Option(Option other) : this() {
      name_ = other.name_;
      Value = other.value_ != null ? other.Value.Clone() : null;
    }

    public Option Clone() {
      return new Option(this);
    }

    public const int NameFieldNumber = 1;
    private string name_ = "";
    public string Name {
      get { return name_; }
      set {
        name_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public const int ValueFieldNumber = 2;
    private global::Google.Protobuf.Any value_;
    public global::Google.Protobuf.Any Value {
      get { return value_; }
      set {
        value_ = value;
      }
    }

    public override bool Equals(object other) {
      return Equals(other as Option);
    }

    public bool Equals(Option other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if (!object.Equals(Value, other.Value)) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (value_ != null) hash ^= Value.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.Default.Format(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (value_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Value);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (value_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Value);
      }
      return size;
    }

    public void MergeFrom(Option other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.value_ != null) {
        if (value_ == null) {
          value_ = new global::Google.Protobuf.Any();
        }
        Value.MergeFrom(other.Value);
      }
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
          case 18: {
            if (value_ == null) {
              value_ = new global::Google.Protobuf.Any();
            }
            input.ReadMessage(value_);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
