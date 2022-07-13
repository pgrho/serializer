namespace Shipwreck.Serializers.Primitive;

internal sealed class NullReader : PrimitiveValueReader
{
    protected override EntryType ValueType => EntryType.Null;
    protected override object? InstanceValue => null;
}
