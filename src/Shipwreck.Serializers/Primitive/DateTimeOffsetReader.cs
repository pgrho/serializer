namespace Shipwreck.Serializers.Primitive;

internal sealed class DateTimeOffsetReader : PrimitiveValueReader
{
    private readonly DateTimeOffset _Value;

    public DateTimeOffsetReader(DateTimeOffset value)
    {
        _Value = value;
    }

    protected override EntryType ValueType => EntryType.DateTimeOffset;
    protected override object InstanceValue => _Value;
}
