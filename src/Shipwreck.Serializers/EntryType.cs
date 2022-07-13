namespace Shipwreck.Serializers;

public enum EntryType
{
    Unknown,

    StartObject,
    EndObject,
    StartArray,
    EndArray,

    Property,

    Null,
    Boolean,
    String,

    SByte,
    Int16,
    Int32,
    Int64,
    Byte,
    UInt16,
    UInt32,
    UInt64,

    Single,
    Double,

    DateTime,
    DateTimeOffset,
}
