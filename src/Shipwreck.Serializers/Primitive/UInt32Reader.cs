namespace Shipwreck.Serializers.Primitive;

internal sealed class UInt32Reader : PrimitiveValueReader
{
    private readonly uint _Value;

    public UInt32Reader(uint value)
    {
        _Value = value;
    }

    protected override EntryType ValueType => EntryType.UInt32;
    protected override object InstanceValue => _Value;
}
