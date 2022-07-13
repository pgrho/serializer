namespace Shipwreck.Serializers.Primitive;

internal sealed class StringReader : PrimitiveValueReader
{
    private readonly string _Value;

    public StringReader(string value)
    {
        _Value = value;
    }

    protected override EntryType ValueType => EntryType.String;
    protected override object InstanceValue => _Value;
}
