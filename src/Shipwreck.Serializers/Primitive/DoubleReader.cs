namespace Shipwreck.Serializers.Primitive;

internal sealed class DoubleReader : PrimitiveValueReader
{
    private readonly double _Value;

    public DoubleReader(double value)
    {
        _Value = value;
    }

    protected override EntryType ValueType => EntryType.Double;
    protected override object InstanceValue => _Value;
}
