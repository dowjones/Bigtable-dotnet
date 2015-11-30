// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: google/bigtable/admin/table/v1/bigtable_table_service_messages.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Google.Bigtable.Admin.Table.V1 {

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public static partial class BigtableTableServiceMessages {

    #region Descriptor
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static BigtableTableServiceMessages() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CkRnb29nbGUvYmlndGFibGUvYWRtaW4vdGFibGUvdjEvYmlndGFibGVfdGFi", 
            "bGVfc2VydmljZV9tZXNzYWdlcy5wcm90bxIeZ29vZ2xlLmJpZ3RhYmxlLmFk", 
            "bWluLnRhYmxlLnYxGjhnb29nbGUvYmlndGFibGUvYWRtaW4vdGFibGUvdjEv", 
            "YmlndGFibGVfdGFibGVfZGF0YS5wcm90byKGAQoSQ3JlYXRlVGFibGVSZXF1", 
            "ZXN0EgwKBG5hbWUYASABKAkSEAoIdGFibGVfaWQYAiABKAkSNAoFdGFibGUY", 
            "AyABKAsyJS5nb29nbGUuYmlndGFibGUuYWRtaW4udGFibGUudjEuVGFibGUS", 
            "GgoSaW5pdGlhbF9zcGxpdF9rZXlzGAQgAygJIiEKEUxpc3RUYWJsZXNSZXF1", 
            "ZXN0EgwKBG5hbWUYASABKAkiSwoSTGlzdFRhYmxlc1Jlc3BvbnNlEjUKBnRh", 
            "YmxlcxgBIAMoCzIlLmdvb2dsZS5iaWd0YWJsZS5hZG1pbi50YWJsZS52MS5U", 
            "YWJsZSIfCg9HZXRUYWJsZVJlcXVlc3QSDAoEbmFtZRgBIAEoCSIiChJEZWxl", 
            "dGVUYWJsZVJlcXVlc3QSDAoEbmFtZRgBIAEoCSIyChJSZW5hbWVUYWJsZVJl", 
            "cXVlc3QSDAoEbmFtZRgBIAEoCRIOCgZuZXdfaWQYAiABKAkiiAEKGUNyZWF0", 
            "ZUNvbHVtbkZhbWlseVJlcXVlc3QSDAoEbmFtZRgBIAEoCRIYChBjb2x1bW5f", 
            "ZmFtaWx5X2lkGAIgASgJEkMKDWNvbHVtbl9mYW1pbHkYAyABKAsyLC5nb29n", 
            "bGUuYmlndGFibGUuYWRtaW4udGFibGUudjEuQ29sdW1uRmFtaWx5IikKGURl", 
            "bGV0ZUNvbHVtbkZhbWlseVJlcXVlc3QSDAoEbmFtZRgBIAEoCUJJCiJjb20u", 
            "Z29vZ2xlLmJpZ3RhYmxlLmFkbWluLnRhYmxlLnYxQiFCaWd0YWJsZVRhYmxl", 
            "U2VydmljZU1lc3NhZ2VzUHJvdG9QAWIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.InternalBuildGeneratedFileFrom(descriptorData,
          new pbr::FileDescriptor[] { global::Google.Bigtable.Admin.Table.V1.BigtableTableData.Descriptor, },
          new pbr::GeneratedCodeInfo(null, new pbr::GeneratedCodeInfo[] {
            new pbr::GeneratedCodeInfo(typeof(global::Google.Bigtable.Admin.Table.V1.CreateTableRequest), new[]{ "Name", "TableId", "Table", "InitialSplitKeys" }, null, null, null),
            new pbr::GeneratedCodeInfo(typeof(global::Google.Bigtable.Admin.Table.V1.ListTablesRequest), new[]{ "Name" }, null, null, null),
            new pbr::GeneratedCodeInfo(typeof(global::Google.Bigtable.Admin.Table.V1.ListTablesResponse), new[]{ "Tables" }, null, null, null),
            new pbr::GeneratedCodeInfo(typeof(global::Google.Bigtable.Admin.Table.V1.GetTableRequest), new[]{ "Name" }, null, null, null),
            new pbr::GeneratedCodeInfo(typeof(global::Google.Bigtable.Admin.Table.V1.DeleteTableRequest), new[]{ "Name" }, null, null, null),
            new pbr::GeneratedCodeInfo(typeof(global::Google.Bigtable.Admin.Table.V1.RenameTableRequest), new[]{ "Name", "NewId" }, null, null, null),
            new pbr::GeneratedCodeInfo(typeof(global::Google.Bigtable.Admin.Table.V1.CreateColumnFamilyRequest), new[]{ "Name", "ColumnFamilyId", "ColumnFamily" }, null, null, null),
            new pbr::GeneratedCodeInfo(typeof(global::Google.Bigtable.Admin.Table.V1.DeleteColumnFamilyRequest), new[]{ "Name" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class CreateTableRequest : pb::IMessage<CreateTableRequest> {
    private static readonly pb::MessageParser<CreateTableRequest> _parser = new pb::MessageParser<CreateTableRequest>(() => new CreateTableRequest());
    public static pb::MessageParser<CreateTableRequest> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Bigtable.Admin.Table.V1.BigtableTableServiceMessages.Descriptor.MessageTypes[0]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public CreateTableRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    public CreateTableRequest(CreateTableRequest other) : this() {
      name_ = other.name_;
      tableId_ = other.tableId_;
      Table = other.table_ != null ? other.Table.Clone() : null;
      initialSplitKeys_ = other.initialSplitKeys_.Clone();
    }

    public CreateTableRequest Clone() {
      return new CreateTableRequest(this);
    }

    public const int NameFieldNumber = 1;
    private string name_ = "";
    public string Name {
      get { return name_; }
      set {
        name_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public const int TableIdFieldNumber = 2;
    private string tableId_ = "";
    public string TableId {
      get { return tableId_; }
      set {
        tableId_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public const int TableFieldNumber = 3;
    private global::Google.Bigtable.Admin.Table.V1.Table table_;
    public global::Google.Bigtable.Admin.Table.V1.Table Table {
      get { return table_; }
      set {
        table_ = value;
      }
    }

    public const int InitialSplitKeysFieldNumber = 4;
    private static readonly pb::FieldCodec<string> _repeated_initialSplitKeys_codec
        = pb::FieldCodec.ForString(34);
    private readonly pbc::RepeatedField<string> initialSplitKeys_ = new pbc::RepeatedField<string>();
    public pbc::RepeatedField<string> InitialSplitKeys {
      get { return initialSplitKeys_; }
    }

    public override bool Equals(object other) {
      return Equals(other as CreateTableRequest);
    }

    public bool Equals(CreateTableRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if (TableId != other.TableId) return false;
      if (!object.Equals(Table, other.Table)) return false;
      if(!initialSplitKeys_.Equals(other.initialSplitKeys_)) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (TableId.Length != 0) hash ^= TableId.GetHashCode();
      if (table_ != null) hash ^= Table.GetHashCode();
      hash ^= initialSplitKeys_.GetHashCode();
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
      if (TableId.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(TableId);
      }
      if (table_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Table);
      }
      initialSplitKeys_.WriteTo(output, _repeated_initialSplitKeys_codec);
    }

    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (TableId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(TableId);
      }
      if (table_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Table);
      }
      size += initialSplitKeys_.CalculateSize(_repeated_initialSplitKeys_codec);
      return size;
    }

    public void MergeFrom(CreateTableRequest other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.TableId.Length != 0) {
        TableId = other.TableId;
      }
      if (other.table_ != null) {
        if (table_ == null) {
          table_ = new global::Google.Bigtable.Admin.Table.V1.Table();
        }
        Table.MergeFrom(other.Table);
      }
      initialSplitKeys_.Add(other.initialSplitKeys_);
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
            TableId = input.ReadString();
            break;
          }
          case 26: {
            if (table_ == null) {
              table_ = new global::Google.Bigtable.Admin.Table.V1.Table();
            }
            input.ReadMessage(table_);
            break;
          }
          case 34: {
            initialSplitKeys_.AddEntriesFrom(input, _repeated_initialSplitKeys_codec);
            break;
          }
        }
      }
    }

  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class ListTablesRequest : pb::IMessage<ListTablesRequest> {
    private static readonly pb::MessageParser<ListTablesRequest> _parser = new pb::MessageParser<ListTablesRequest>(() => new ListTablesRequest());
    public static pb::MessageParser<ListTablesRequest> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Bigtable.Admin.Table.V1.BigtableTableServiceMessages.Descriptor.MessageTypes[1]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public ListTablesRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    public ListTablesRequest(ListTablesRequest other) : this() {
      name_ = other.name_;
    }

    public ListTablesRequest Clone() {
      return new ListTablesRequest(this);
    }

    public const int NameFieldNumber = 1;
    private string name_ = "";
    public string Name {
      get { return name_; }
      set {
        name_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public override bool Equals(object other) {
      return Equals(other as ListTablesRequest);
    }

    public bool Equals(ListTablesRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
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
    }

    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      return size;
    }

    public void MergeFrom(ListTablesRequest other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
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
        }
      }
    }

  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class ListTablesResponse : pb::IMessage<ListTablesResponse> {
    private static readonly pb::MessageParser<ListTablesResponse> _parser = new pb::MessageParser<ListTablesResponse>(() => new ListTablesResponse());
    public static pb::MessageParser<ListTablesResponse> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Bigtable.Admin.Table.V1.BigtableTableServiceMessages.Descriptor.MessageTypes[2]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public ListTablesResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    public ListTablesResponse(ListTablesResponse other) : this() {
      tables_ = other.tables_.Clone();
    }

    public ListTablesResponse Clone() {
      return new ListTablesResponse(this);
    }

    public const int TablesFieldNumber = 1;
    private static readonly pb::FieldCodec<global::Google.Bigtable.Admin.Table.V1.Table> _repeated_tables_codec
        = pb::FieldCodec.ForMessage(10, global::Google.Bigtable.Admin.Table.V1.Table.Parser);
    private readonly pbc::RepeatedField<global::Google.Bigtable.Admin.Table.V1.Table> tables_ = new pbc::RepeatedField<global::Google.Bigtable.Admin.Table.V1.Table>();
    public pbc::RepeatedField<global::Google.Bigtable.Admin.Table.V1.Table> Tables {
      get { return tables_; }
    }

    public override bool Equals(object other) {
      return Equals(other as ListTablesResponse);
    }

    public bool Equals(ListTablesResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!tables_.Equals(other.tables_)) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      hash ^= tables_.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.Default.Format(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      tables_.WriteTo(output, _repeated_tables_codec);
    }

    public int CalculateSize() {
      int size = 0;
      size += tables_.CalculateSize(_repeated_tables_codec);
      return size;
    }

    public void MergeFrom(ListTablesResponse other) {
      if (other == null) {
        return;
      }
      tables_.Add(other.tables_);
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            tables_.AddEntriesFrom(input, _repeated_tables_codec);
            break;
          }
        }
      }
    }

  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class GetTableRequest : pb::IMessage<GetTableRequest> {
    private static readonly pb::MessageParser<GetTableRequest> _parser = new pb::MessageParser<GetTableRequest>(() => new GetTableRequest());
    public static pb::MessageParser<GetTableRequest> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Bigtable.Admin.Table.V1.BigtableTableServiceMessages.Descriptor.MessageTypes[3]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public GetTableRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    public GetTableRequest(GetTableRequest other) : this() {
      name_ = other.name_;
    }

    public GetTableRequest Clone() {
      return new GetTableRequest(this);
    }

    public const int NameFieldNumber = 1;
    private string name_ = "";
    public string Name {
      get { return name_; }
      set {
        name_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public override bool Equals(object other) {
      return Equals(other as GetTableRequest);
    }

    public bool Equals(GetTableRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
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
    }

    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      return size;
    }

    public void MergeFrom(GetTableRequest other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
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
        }
      }
    }

  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class DeleteTableRequest : pb::IMessage<DeleteTableRequest> {
    private static readonly pb::MessageParser<DeleteTableRequest> _parser = new pb::MessageParser<DeleteTableRequest>(() => new DeleteTableRequest());
    public static pb::MessageParser<DeleteTableRequest> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Bigtable.Admin.Table.V1.BigtableTableServiceMessages.Descriptor.MessageTypes[4]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public DeleteTableRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    public DeleteTableRequest(DeleteTableRequest other) : this() {
      name_ = other.name_;
    }

    public DeleteTableRequest Clone() {
      return new DeleteTableRequest(this);
    }

    public const int NameFieldNumber = 1;
    private string name_ = "";
    public string Name {
      get { return name_; }
      set {
        name_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public override bool Equals(object other) {
      return Equals(other as DeleteTableRequest);
    }

    public bool Equals(DeleteTableRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
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
    }

    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      return size;
    }

    public void MergeFrom(DeleteTableRequest other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
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
        }
      }
    }

  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class RenameTableRequest : pb::IMessage<RenameTableRequest> {
    private static readonly pb::MessageParser<RenameTableRequest> _parser = new pb::MessageParser<RenameTableRequest>(() => new RenameTableRequest());
    public static pb::MessageParser<RenameTableRequest> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Bigtable.Admin.Table.V1.BigtableTableServiceMessages.Descriptor.MessageTypes[5]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public RenameTableRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    public RenameTableRequest(RenameTableRequest other) : this() {
      name_ = other.name_;
      newId_ = other.newId_;
    }

    public RenameTableRequest Clone() {
      return new RenameTableRequest(this);
    }

    public const int NameFieldNumber = 1;
    private string name_ = "";
    public string Name {
      get { return name_; }
      set {
        name_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public const int NewIdFieldNumber = 2;
    private string newId_ = "";
    public string NewId {
      get { return newId_; }
      set {
        newId_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public override bool Equals(object other) {
      return Equals(other as RenameTableRequest);
    }

    public bool Equals(RenameTableRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if (NewId != other.NewId) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (NewId.Length != 0) hash ^= NewId.GetHashCode();
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
      if (NewId.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(NewId);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (NewId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(NewId);
      }
      return size;
    }

    public void MergeFrom(RenameTableRequest other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.NewId.Length != 0) {
        NewId = other.NewId;
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
            NewId = input.ReadString();
            break;
          }
        }
      }
    }

  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class CreateColumnFamilyRequest : pb::IMessage<CreateColumnFamilyRequest> {
    private static readonly pb::MessageParser<CreateColumnFamilyRequest> _parser = new pb::MessageParser<CreateColumnFamilyRequest>(() => new CreateColumnFamilyRequest());
    public static pb::MessageParser<CreateColumnFamilyRequest> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Bigtable.Admin.Table.V1.BigtableTableServiceMessages.Descriptor.MessageTypes[6]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public CreateColumnFamilyRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    public CreateColumnFamilyRequest(CreateColumnFamilyRequest other) : this() {
      name_ = other.name_;
      columnFamilyId_ = other.columnFamilyId_;
      ColumnFamily = other.columnFamily_ != null ? other.ColumnFamily.Clone() : null;
    }

    public CreateColumnFamilyRequest Clone() {
      return new CreateColumnFamilyRequest(this);
    }

    public const int NameFieldNumber = 1;
    private string name_ = "";
    public string Name {
      get { return name_; }
      set {
        name_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public const int ColumnFamilyIdFieldNumber = 2;
    private string columnFamilyId_ = "";
    public string ColumnFamilyId {
      get { return columnFamilyId_; }
      set {
        columnFamilyId_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public const int ColumnFamilyFieldNumber = 3;
    private global::Google.Bigtable.Admin.Table.V1.ColumnFamily columnFamily_;
    public global::Google.Bigtable.Admin.Table.V1.ColumnFamily ColumnFamily {
      get { return columnFamily_; }
      set {
        columnFamily_ = value;
      }
    }

    public override bool Equals(object other) {
      return Equals(other as CreateColumnFamilyRequest);
    }

    public bool Equals(CreateColumnFamilyRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if (ColumnFamilyId != other.ColumnFamilyId) return false;
      if (!object.Equals(ColumnFamily, other.ColumnFamily)) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (ColumnFamilyId.Length != 0) hash ^= ColumnFamilyId.GetHashCode();
      if (columnFamily_ != null) hash ^= ColumnFamily.GetHashCode();
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
      if (ColumnFamilyId.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(ColumnFamilyId);
      }
      if (columnFamily_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(ColumnFamily);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (ColumnFamilyId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ColumnFamilyId);
      }
      if (columnFamily_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ColumnFamily);
      }
      return size;
    }

    public void MergeFrom(CreateColumnFamilyRequest other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.ColumnFamilyId.Length != 0) {
        ColumnFamilyId = other.ColumnFamilyId;
      }
      if (other.columnFamily_ != null) {
        if (columnFamily_ == null) {
          columnFamily_ = new global::Google.Bigtable.Admin.Table.V1.ColumnFamily();
        }
        ColumnFamily.MergeFrom(other.ColumnFamily);
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
            ColumnFamilyId = input.ReadString();
            break;
          }
          case 26: {
            if (columnFamily_ == null) {
              columnFamily_ = new global::Google.Bigtable.Admin.Table.V1.ColumnFamily();
            }
            input.ReadMessage(columnFamily_);
            break;
          }
        }
      }
    }

  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class DeleteColumnFamilyRequest : pb::IMessage<DeleteColumnFamilyRequest> {
    private static readonly pb::MessageParser<DeleteColumnFamilyRequest> _parser = new pb::MessageParser<DeleteColumnFamilyRequest>(() => new DeleteColumnFamilyRequest());
    public static pb::MessageParser<DeleteColumnFamilyRequest> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Bigtable.Admin.Table.V1.BigtableTableServiceMessages.Descriptor.MessageTypes[7]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public DeleteColumnFamilyRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    public DeleteColumnFamilyRequest(DeleteColumnFamilyRequest other) : this() {
      name_ = other.name_;
    }

    public DeleteColumnFamilyRequest Clone() {
      return new DeleteColumnFamilyRequest(this);
    }

    public const int NameFieldNumber = 1;
    private string name_ = "";
    public string Name {
      get { return name_; }
      set {
        name_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public override bool Equals(object other) {
      return Equals(other as DeleteColumnFamilyRequest);
    }

    public bool Equals(DeleteColumnFamilyRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
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
    }

    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      return size;
    }

    public void MergeFrom(DeleteColumnFamilyRequest other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
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
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
