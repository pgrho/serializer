using Shipwreck.Serializers.Primitive;

namespace Shipwreck.Serializers.Reflect;

internal sealed class ReflectObjectWriter : SerializationWriter
{
    private readonly ReflectTypeInfo _ExpectedType;

    private object? _Object;
    private ReflectPropertyInfo? _Property;
    private SerializationWriter? _PropertyWriter;
    private bool _CanWrite;

    public ReflectObjectWriter(Type expectedType)
    {
        _ExpectedType = ReflectTypeInfo.Get(expectedType);
        _CanWrite = true;
    }

    public object? Object => _Object;

    public override bool CanWrite => _CanWrite;

    public override void WriteStartObject()
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteStartObject();
            ClearPropertyIfNeeded();
            return;
        }
        if (!_CanWrite || _Object != null)
        {
            throw new InvalidOperationException();
        }
        _Object = Activator.CreateInstance(_ExpectedType.Type);
    }

    public override void WriteEndObject()
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteEndObject();
            ClearPropertyIfNeeded();
            return;
        }
        if (!_CanWrite || _Object == null)
        {
            throw new InvalidOperationException();
        }
        _CanWrite = false;
    }

    public override void WriteStartArray()
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteStartArray();
            ClearPropertyIfNeeded();
            return;
        }
        throw new InvalidOperationException();
    }

    public override void WriteEndArray()
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteEndArray();
            ClearPropertyIfNeeded();
            return;
        }
        throw new InvalidOperationException();
    }

    public override void WriteProperty(string propertyName)
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteProperty(propertyName);
            ClearPropertyIfNeeded();
            return;
        }
        if (!_CanWrite || _Object == null || _Property != null)
        {
            throw new InvalidOperationException();
        }

        _Property = _ExpectedType.GetProperty(propertyName, StringComparison.InvariantCultureIgnoreCase);

        if (_Property == null)
        {
            _PropertyWriter = new EmptyWriter();
            return;
        }

        var pt = _Property.PropertyType;
        pt = Nullable.GetUnderlyingType(pt) ?? pt;

        if (pt == typeof(bool)
            || pt == typeof(byte)
            || pt == typeof(short)
            || pt == typeof(int)
            || pt == typeof(long)
            || pt == typeof(sbyte)
            || pt == typeof(ushort)
            || pt == typeof(uint)
            || pt == typeof(ulong)
            || pt == typeof(float)
            || pt == typeof(double)
            || pt == typeof(DateTime)
            || pt == typeof(DateTimeOffset)
            || pt == typeof(string)
            || pt.IsEnum)
        {
        }
        // TODO support list property
        else
        {
            _PropertyWriter = new ReflectObjectWriter(_Property.PropertyType);
        }
    }

    private void ClearPropertyIfNeeded()
    {
        if (_PropertyWriter?.CanWrite == false)
        {
            _PropertyWriter = null;
            _Property = null;
        }
    }

    public override void WriteNull()
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteNull();
            ClearPropertyIfNeeded();
            return;
        }
        WriteCore(null);
    }

    public override void WriteValue(bool value)
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteValue(value);
            ClearPropertyIfNeeded();
            return;
        }
        WriteCore(value);
    }

    public override void WriteValue(string value)
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteValue(value);
            ClearPropertyIfNeeded();
            return;
        }
        WriteCore(value);
    }

    public override void WriteValue(sbyte value)
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteValue(value);
            ClearPropertyIfNeeded();
            return;
        }
        WriteCore(value);
    }

    public override void WriteValue(short value)
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteValue(value);
            ClearPropertyIfNeeded();
            return;
        }
        WriteCore(value);
    }

    public override void WriteValue(int value)
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteValue(value);
            ClearPropertyIfNeeded();
            return;
        }
        WriteCore(value);
    }

    public override void WriteValue(long value)
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteValue(value);
            ClearPropertyIfNeeded();
            return;
        }
        WriteCore(value);
    }

    public override void WriteValue(byte value)
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteValue(value);
            ClearPropertyIfNeeded();
            return;
        }
        WriteCore(value);
    }

    public override void WriteValue(ushort value)
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteValue(value);
            ClearPropertyIfNeeded();
            return;
        }
        WriteCore(value);
    }

    public override void WriteValue(uint value)
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteValue(value);
            ClearPropertyIfNeeded();
            return;
        }
        WriteCore(value);
    }

    public override void WriteValue(ulong value)
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteValue(value);
            ClearPropertyIfNeeded();
            return;
        }
        WriteCore(value);
    }

    public override void WriteValue(float value)
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteValue(value);
            ClearPropertyIfNeeded();
            return;
        }
        WriteCore(value);
    }

    public override void WriteValue(double value)
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteValue(value);
            ClearPropertyIfNeeded();
            return;
        }
        WriteCore(value);
    }

    public override void WriteValue(DateTime value)
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteValue(value);
            ClearPropertyIfNeeded();
            return;
        }
        WriteCore(value);
    }

    public override void WriteValue(DateTimeOffset value)
    {
        if (_PropertyWriter != null)
        {
            _PropertyWriter.WriteValue(value);
            ClearPropertyIfNeeded();
            return;
        }
        WriteCore(value);
    }

    private void WriteCore(object? value)
    {
        if (!_CanWrite)
        {
            throw new InvalidOperationException();
        }
        if (_Property == null)
        {
            throw new InvalidOperationException();
        }

        if (value == null)
        {
            if (!_Property.PropertyType.IsValueType
                || _Property.IsNullableValueType)
            {
                _Property.Property.SetValue(_Object, null);
                _Property = null;
                return;
            }
        }
        else
        {
            var t = value.GetType();

            if (_Property.PropertyType.IsAssignableFrom(t))
            {
                _Property.Property.SetValue(_Object, value);
                _Property = null;
                return;
            }
        }

        _Property.Property.SetValue(_Object, _Property.Converter.ConvertFrom(value));
        _Property = null;
    }
}
