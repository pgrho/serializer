namespace Shipwreck.Serializers.Primitive;

internal sealed class SingleReader : PrimitiveValueReader
{
    private readonly float _Value;

    public SingleReader(float value)
    {
        _Value = value;
    }

    protected override EntryType ValueType => EntryType.Single;
    protected override object InstanceValue => _Value;
}
