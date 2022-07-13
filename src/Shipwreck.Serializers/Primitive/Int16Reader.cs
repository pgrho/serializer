namespace Shipwreck.Serializers.Primitive;

internal sealed class Int16Reader : PrimitiveValueReader
{
    private readonly short _Value;

    public Int16Reader(short value)
    {
        _Value = value;
    }

    protected override EntryType ValueType => EntryType.Int16;
    protected override object InstanceValue => _Value;
}
