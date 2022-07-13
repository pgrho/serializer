namespace Shipwreck.Serializers.Primitive;

internal sealed class UInt16Reader : PrimitiveValueReader
{
    private readonly ushort _Value;

    public UInt16Reader(ushort value)
    {
        _Value = value;
    }

    protected override EntryType ValueType => EntryType.UInt16;
    protected override object InstanceValue => _Value;
}
