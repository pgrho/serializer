namespace Shipwreck.Serializers.Primitive;

internal abstract class PrimitiveValueReader : SerializationReader
{
    public enum ReaderState : byte
    {
        Initial,
        Reading,
        Read
    }

    public ReaderState State { get; private set; }

    public override object Value
        => State == ReaderState.Reading ? InstanceValue : null;

    public override EntryType Type
        => State == ReaderState.Reading ? ValueType : EntryType.Unknown;

    protected abstract object InstanceValue { get; }

    protected abstract EntryType ValueType { get; }

    public override bool Read()
    {
        switch (State)
        {
            case ReaderState.Initial:
                State = ReaderState.Reading;
                return true;

            default:
                State = ReaderState.Read;
                return false;
        }
    }
}
