namespace Shipwreck.Serializers.Primitive;

internal sealed class NullReader : PrimitiveValueReader
{
    public static NullReader Default { get; } = new();

    protected override EntryType ValueType => EntryType.Null;
    protected override object InstanceValue => null;
}
