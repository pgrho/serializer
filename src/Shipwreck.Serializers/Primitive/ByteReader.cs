namespace Shipwreck.Serializers.Primitive;

internal sealed class ByteReader : PrimitiveValueReader
{
    private readonly byte _Value;

    public ByteReader(byte value)
    {
        _Value = value;
    }

    protected override EntryType ValueType => EntryType.Byte;
    protected override object InstanceValue => _Value;
}
