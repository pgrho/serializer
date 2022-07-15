namespace Shipwreck.Serializers.Primitive;

internal class PrimitiveValueWriter<T> : SerializationWriter
{
    private static readonly Type _Type = typeof(T);
    private T? _Value;
    private object? _Object;
    private bool _CanWrite = true;


    public T? Value => _Value;

    public object? Object => _Object ??= _Value;


    public override bool CanWrite => _CanWrite;

    protected void SetValue(T? value)
    {
        _Value = value;
        _Object = null;
        _CanWrite = false;
    }

    public override void WriteStartObject()
        => throw new NotSupportedException();

    public override void WriteEndObject()
        => throw new NotSupportedException();

    public override void WriteStartArray()
        => throw new NotSupportedException();

    public override void WriteEndArray()
        => throw new NotSupportedException();

    public override void WriteProperty(string propertyName)
        => throw new NotSupportedException();

    public override void WriteNull()
        => WriteCore(null);

    public override void WriteValue(bool value)
        => WriteCore(value);

    public override void WriteValue(string value)
        => WriteCore(value);

    public override void WriteValue(sbyte value)
        => WriteCore(value);

    public override void WriteValue(short value)
        => WriteCore(value);

    public override void WriteValue(int value)
        => WriteCore(value);

    public override void WriteValue(long value)
        => WriteCore(value);

    public override void WriteValue(byte value)
        => WriteCore(value);

    public override void WriteValue(ushort value)
        => WriteCore(value);

    public override void WriteValue(uint value)
        => WriteCore(value);

    public override void WriteValue(ulong value)
        => WriteCore(value);

    public override void WriteValue(float value)
        => WriteCore(value);

    public override void WriteValue(double value)
        => WriteCore(value);

    public override void WriteValue(DateTime value)
        => WriteCore(value);

    public override void WriteValue(DateTimeOffset value)
        => WriteCore(value);

    private void ThrowIfNeeded()
    {
        if (_CanWrite)
        {
            throw new InvalidOperationException();
        }
    }

    private void WriteCore(object? value)
    {
        ThrowIfNeeded();
        _Value = ConvertFrom(null);
        _CanWrite = false;
    }

    private T? ConvertFrom(object? value)
    {
        // TODO ConvertFrom

        //if (value == null)
        //{
        //    if (!_Type.IsValueType
        //        || _Property.IsNullableValueType)
        //    {
        //        return null;
        //    }
        //}
        //else
        //{
        //    var t = value.GetType();

        //    if (_Type.IsAssignableFrom(t))
        //    {
        return (T?)value;
        //            }
        //        }

        //return   _Property.Converter.ConvertFrom(value) ;
    }
}
