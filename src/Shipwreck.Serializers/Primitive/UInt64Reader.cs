namespace Shipwreck.Serializers.Primitive;

internal sealed class UInt64Reader : PrimitiveValueReader
{
    private readonly ulong _Value;

    public UInt64Reader(ulong value)
    {
        _Value = value;
    }

    protected override EntryType ValueType => EntryType.UInt64;
    protected override object InstanceValue => _Value;
}