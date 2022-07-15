namespace Shipwreck.Serializers;

public abstract class SerializationWriter : IDisposable
{
    public abstract bool CanWrite { get; }
    public bool IsDisposed { get; protected set; }

    public abstract void WriteStartObject();

    public abstract void WriteEndObject();

    public abstract void WriteStartArray();

    public abstract void WriteEndArray();

    public abstract void WriteProperty(string propertyName);

    public abstract void WriteNull();

    public abstract void WriteValue(bool value);

    public abstract void WriteValue(string value);

    public abstract void WriteValue(sbyte value);

    public abstract void WriteValue(short value);

    public abstract void WriteValue(int value);

    public abstract void WriteValue(long value);

    public abstract void WriteValue(byte value);

    public abstract void WriteValue(ushort value);

    public abstract void WriteValue(uint value);

    public abstract void WriteValue(ulong value);

    public abstract void WriteValue(float value);

    public abstract void WriteValue(double value);

    public abstract void WriteValue(DateTime value);

    public abstract void WriteValue(DateTimeOffset value);

    protected virtual void Dispose(bool disposing)
        => IsDisposed = true;

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
