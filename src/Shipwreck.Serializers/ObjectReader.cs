using Shipwreck.Serializers.Primitive;

namespace Shipwreck.Serializers;

public sealed class ObjectReader : SerializationReader
{
    private readonly SerializationReader _InternalReader;

    public ObjectReader(object obj)
    {
        _InternalReader = GetReader(obj);
    }

    public override EntryType Type => _InternalReader.Type;

    public override object Value => _InternalReader.Value;

    public override bool Read() => _InternalReader.Read();

    internal static SerializationReader GetReader(object obj)
    {
        switch (obj)
        {
            case null:
                return NullReader.Default;

            case bool b:
                return new BooleanReader(b);

            case string s:
                return new Primitive.StringReader(s);

            case sbyte i8:
                return new SByteReader(i8);

            case short i16:
                return new Int16Reader(i16);

            case int i32:
                return new Int32Reader(i32);

            case long i64:
                return new Int64Reader(i64);

            case byte u8:
                return new ByteReader(u8);

            case ushort u16:
                return new UInt16Reader(u16);

            case uint u32:
                return new UInt32Reader(u32);

            case ulong u64:
                return new UInt64Reader(u64);

            case float f:
                return new SingleReader(f);

            case double d:
                return new DoubleReader(d);

            case DateTime dt:
                return new DateTimeReader(dt);

            case DateTimeOffset dto:
                return new DateTimeOffsetReader(dto);
        }

        return new ReflectObjectReader(obj);
    }
}
