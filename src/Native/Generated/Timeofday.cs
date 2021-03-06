// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: google/type/timeofday.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Google.Type {

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public static partial class Timeofday {

    #region Descriptor
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static Timeofday() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Chtnb29nbGUvdHlwZS90aW1lb2ZkYXkucHJvdG8SC2dvb2dsZS50eXBlIksK", 
            "CVRpbWVPZkRheRINCgVob3VycxgBIAEoBRIPCgdtaW51dGVzGAIgASgFEg8K", 
            "B3NlY29uZHMYAyABKAUSDQoFbmFub3MYBCABKAVCJgoPY29tLmdvb2dsZS50", 
            "eXBlQg5UaW1lT2ZEYXlQcm90b1ABoAEBYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.InternalBuildGeneratedFileFrom(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedCodeInfo(null, new pbr::GeneratedCodeInfo[] {
            new pbr::GeneratedCodeInfo(typeof(global::Google.Type.TimeOfDay), new[]{ "Hours", "Minutes", "Seconds", "Nanos" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class TimeOfDay : pb::IMessage<TimeOfDay> {
    private static readonly pb::MessageParser<TimeOfDay> _parser = new pb::MessageParser<TimeOfDay>(() => new TimeOfDay());
    public static pb::MessageParser<TimeOfDay> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Type.Timeofday.Descriptor.MessageTypes[0]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public TimeOfDay() {
      OnConstruction();
    }

    partial void OnConstruction();

    public TimeOfDay(TimeOfDay other) : this() {
      hours_ = other.hours_;
      minutes_ = other.minutes_;
      seconds_ = other.seconds_;
      nanos_ = other.nanos_;
    }

    public TimeOfDay Clone() {
      return new TimeOfDay(this);
    }

    public const int HoursFieldNumber = 1;
    private int hours_;
    public int Hours {
      get { return hours_; }
      set {
        hours_ = value;
      }
    }

    public const int MinutesFieldNumber = 2;
    private int minutes_;
    public int Minutes {
      get { return minutes_; }
      set {
        minutes_ = value;
      }
    }

    public const int SecondsFieldNumber = 3;
    private int seconds_;
    public int Seconds {
      get { return seconds_; }
      set {
        seconds_ = value;
      }
    }

    public const int NanosFieldNumber = 4;
    private int nanos_;
    public int Nanos {
      get { return nanos_; }
      set {
        nanos_ = value;
      }
    }

    public override bool Equals(object other) {
      return Equals(other as TimeOfDay);
    }

    public bool Equals(TimeOfDay other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Hours != other.Hours) return false;
      if (Minutes != other.Minutes) return false;
      if (Seconds != other.Seconds) return false;
      if (Nanos != other.Nanos) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (Hours != 0) hash ^= Hours.GetHashCode();
      if (Minutes != 0) hash ^= Minutes.GetHashCode();
      if (Seconds != 0) hash ^= Seconds.GetHashCode();
      if (Nanos != 0) hash ^= Nanos.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.Default.Format(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Hours != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Hours);
      }
      if (Minutes != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Minutes);
      }
      if (Seconds != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Seconds);
      }
      if (Nanos != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(Nanos);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (Hours != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Hours);
      }
      if (Minutes != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Minutes);
      }
      if (Seconds != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Seconds);
      }
      if (Nanos != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Nanos);
      }
      return size;
    }

    public void MergeFrom(TimeOfDay other) {
      if (other == null) {
        return;
      }
      if (other.Hours != 0) {
        Hours = other.Hours;
      }
      if (other.Minutes != 0) {
        Minutes = other.Minutes;
      }
      if (other.Seconds != 0) {
        Seconds = other.Seconds;
      }
      if (other.Nanos != 0) {
        Nanos = other.Nanos;
      }
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Hours = input.ReadInt32();
            break;
          }
          case 16: {
            Minutes = input.ReadInt32();
            break;
          }
          case 24: {
            Seconds = input.ReadInt32();
            break;
          }
          case 32: {
            Nanos = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
