using System.Collections;

namespace Shipwreck.Serializers;

internal sealed class ReflectListReader : SerializationReader
{
    private readonly IList _Values;
    private int _Index;
    private SerializationReader? _ElementReader;

    public ReflectListReader(IList values)
    {
        _Values = values;
        _Index = -2;
    }

    public override EntryType Type
    {
        get
        {
            if (_Index == -1)
            {
                return EntryType.StartArray;
            }
            else if (_Index < _Values.Count)
            {
                return _ElementReader!.Type;
            }
            else if (_Index == _Values.Count)
            {
                return EntryType.EndArray;
            }
            return EntryType.Unknown;
        }
    }

    public override object? Value
        => _ElementReader?.Value;

    public override bool Read()
    {
        if (_Index == -2)
        {
            _Index = -1;
            return true;
        }

        if (_ElementReader?.Read() == true)
        {
            return true;
        }

        if (++_Index < _Values.Count)
        {
            var v = _Values[_Index];
            _ElementReader = ObjectReader.GetReader(v);
            _ElementReader.Read();
            return true;
        }
        _ElementReader = null;
        return _Index == _Values.Count;
    }
}
