namespace Shipwreck.Serializers.Primitive;

internal sealed class BooleanReader : PrimitiveValueReader
{
    private static readonly object True = true;
    private static readonly object False = false;
    private readonly bool _Value;

    public BooleanReader(bool value)
    {
        _Value = value;
    }

    protected override EntryType ValueType => EntryType.Boolean;
    protected override object InstanceValue => _Value ? True : False;
}
