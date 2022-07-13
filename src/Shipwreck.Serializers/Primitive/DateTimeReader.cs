namespace Shipwreck.Serializers.Primitive;

internal sealed class DateTimeReader : PrimitiveValueReader
{
    private readonly DateTime _Value;

    public DateTimeReader(DateTime value)
    {
        _Value = value;
    }

    protected override EntryType ValueType => EntryType.DateTime;
    protected override object InstanceValue => _Value;
}
