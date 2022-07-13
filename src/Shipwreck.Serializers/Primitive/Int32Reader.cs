namespace Shipwreck.Serializers.Primitive;

internal sealed class Int32Reader : PrimitiveValueReader
{
    private readonly int _Value;

    public Int32Reader(int value)
    {
        _Value = value;
    }

    protected override EntryType ValueType => EntryType.Int32;
    protected override object InstanceValue => _Value;
}
