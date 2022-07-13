namespace Shipwreck.Serializers.Primitive;

internal sealed class SByteReader : PrimitiveValueReader
{
    private readonly sbyte _Value;

    public SByteReader(sbyte value)
    {
        _Value = value;
    }

    protected override EntryType ValueType => EntryType.SByte;
    protected override object InstanceValue => _Value;
}
