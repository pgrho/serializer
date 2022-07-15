namespace Shipwreck.Serializers.Reflect;

public class ReflectListReaderTest
{
    private sealed class Hoge
    {
        public int Int32 { get; set; }
    }

    [Fact]
    public void Test()
    {
        var ary = new[]
        {
            new Hoge()
            {
                Int32 = 1,
            },
            null,
            new Hoge()
            {
                Int32 = 3,
            },
        };

        using var r = new ReflectListReader(ary);

        Assert.True(r.Read());
        Assert.Equal(EntryType.StartArray, r.Type);

        Assert.True(r.Read());
        Assert.Equal(EntryType.StartObject, r.Type);
        Assert.True(r.Read());
        Assert.Equal(EntryType.Property, r.Type);
        Assert.True(r.Read());
        Assert.Equal(EntryType.Int32, r.Type);
        Assert.True(r.Read());
        Assert.Equal(EntryType.EndObject, r.Type);

        Assert.True(r.Read());
        Assert.Equal(EntryType.Null, r.Type);

        Assert.True(r.Read());
        Assert.Equal(EntryType.StartObject, r.Type);
        Assert.True(r.Read());
        Assert.Equal(EntryType.Property, r.Type);
        Assert.True(r.Read());
        Assert.Equal(EntryType.Int32, r.Type);
        Assert.True(r.Read());
        Assert.Equal(EntryType.EndObject, r.Type);

        Assert.True(r.Read());
        Assert.Equal(EntryType.EndArray, r.Type);

        Assert.False(r.Read());
    }
}
