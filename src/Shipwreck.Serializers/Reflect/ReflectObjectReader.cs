namespace Shipwreck.Serializers.Reflect;

internal sealed class ReflectObjectReader : SerializationReader
{
    private readonly ReflectTypeInfo _Type;
    private readonly object _Object;
    private int _PropertyIndex;
    private bool _ReadingPropertyValue;
    private SerializationReader? _ValueReader;

    public ReflectObjectReader(object obj)
    {
        _Object = obj;
        _Type = ReflectTypeInfo.Get(obj.GetType());
        _PropertyIndex = -2;
    }

    public override EntryType Type
    {
        get
        {
            if (_PropertyIndex == -1)
            {
                return EntryType.StartObject;
            }
            else if (_PropertyIndex < _Type.Properties.Count)
            {
                if (!_ReadingPropertyValue)
                {
                    return EntryType.Property;
                }
                return _ValueReader!.Type;
            }
            else if (_PropertyIndex == _Type.Properties.Count)
            {
                return EntryType.EndObject;
            }
            return EntryType.Unknown;
        }
    }

    public override object? Value
    {
        get
        {
            if (0 <= _PropertyIndex && _PropertyIndex < _Type.Properties.Count)
            {
                if (!_ReadingPropertyValue)
                {
                    return _Type.Properties[_PropertyIndex].Name;
                }
                return _ValueReader!.Value;
            }
            return null;
        }
    }

    public override bool Read()
    {
        if (_PropertyIndex == -2)
        {
            _PropertyIndex = -1;
            return true;
        }

        if (_ValueReader?.Read() == true)
        {
            _ReadingPropertyValue = true;
            return true;
        }

        for (; ; )
        {
            var p = _Type.Properties.ElementAtOrDefault(++_PropertyIndex);

            if (p == null)
            {
                _ValueReader = null;
                return _PropertyIndex == _Type.Properties.Count;
            }

            if (p.ShouldSerialize(_Object))
            {
                _ReadingPropertyValue = false;
                _ValueReader = ObjectReader.GetReader(p.Property.GetValue(_Object));

                return true;
            }
        }
    }
}