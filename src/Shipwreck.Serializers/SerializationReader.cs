namespace Shipwreck.Serializers;

public abstract class SerializationReader
{
    public abstract EntryType Type { get; }

    public abstract object? Value { get; }

    public abstract bool Read();
}
