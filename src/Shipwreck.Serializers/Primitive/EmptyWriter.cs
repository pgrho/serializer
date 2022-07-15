namespace Shipwreck.Serializers.Primitive;

internal sealed class EmptyWriter : SerializationWriter
{
    private bool _CanWrite = true;

    private int _Level;

    public override bool CanWrite => _CanWrite;

    public override void WriteStartObject()
        => WriteStart();

    public override void WriteStartArray()
        => WriteStart();

    private void WriteStart()
    {
        if (!_CanWrite)
        {
            throw new InvalidOperationException();
        }
        _Level++;
    }

    public override void WriteEndObject()
        => WriteEnd();

    public override void WriteEndArray()
        => WriteEnd();

    private void WriteEnd()
    {
        if (!_CanWrite || _Level <= 0)
        {
            throw new InvalidOperationException();
        }
        _CanWrite = --_Level > 0;
    }

    public override void WriteProperty(string propertyName) 
        => Ignore();

    public override void WriteNull()
        => Ignore();
    public override void WriteValue(bool value)
        => Ignore();

    public override void WriteValue(string value)
        => Ignore();

    public override void WriteValue(sbyte value)
        => Ignore();

    public override void WriteValue(short value)
        => Ignore();

    public override void WriteValue(int value)
        => Ignore();

    public override void WriteValue(long value)
        => Ignore();

    public override void WriteValue(byte value)
        => Ignore();

    public override void WriteValue(ushort value)
        => Ignore();

    public override void WriteValue(uint value)
        => Ignore();

    public override void WriteValue(ulong value)
        => Ignore();

    public override void WriteValue(float value)
        => Ignore();

    public override void WriteValue(double value)
        => Ignore();

    public override void WriteValue(DateTime value)
        => Ignore();

    public override void WriteValue(DateTimeOffset value)
        => Ignore();

    private void Ignore()
    {
        if (!_CanWrite)
        {
            throw new InvalidOperationException();
        }
    }
}