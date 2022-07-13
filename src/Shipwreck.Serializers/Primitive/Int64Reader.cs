namespace Shipwreck.Serializers.Primitive;

internal sealed class Int64Reader : PrimitiveValueReader
{
    private readonly long _Value;

    public Int64Reader(long value)
    {
        _Value = value;
    }

    protected override EntryType ValueType => EntryType.Int64;
    protected override object InstanceValue => _Value;
}
