namespace Shipwreck.Serializers;

public abstract class SerializationReader : IDisposable
{
    public bool IsDisposed { get; protected set; }

    public abstract EntryType Type { get; }

    public abstract object? Value { get; }

    public abstract bool Read();

    protected virtual void Dispose(bool disposing)
        => IsDisposed = true;

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
