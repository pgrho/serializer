namespace Shipwreck.Serializers;

public class ReflectObjectReaderTest
{
    public sealed class Hoge
    {
        public bool Boolean { get; set; }
        public int Int32 { get; set; }
        public double Double { get; set; }
        public string? String { get; set; }
    }

    public sealed class Fuga
    {
        public Hoge? Hoge { get; set; }
    }

    [Fact]
    public void HogeTest()
    {
        var obj = CreateHoge();

        var r = new ObjectReader(obj);

        AssertHoge(r, obj);

        Assert.False(r.Read());
    }

    [Fact]
    public void FugaNullTest()
    {
        var obj = new Fuga();

        var r = new ObjectReader(obj);

        Assert.True(r.Read());
        Assert.Equal(EntryType.StartObject, r.Type);

        Assert.True(r.Read());
        Assert.Equal(EntryType.Property, r.Type);
        Assert.Equal(nameof(obj.Hoge), r.Value);
        Assert.True(r.Read());
        Assert.Equal(EntryType.Null, r.Type);
        Assert.Null(r.Value);

        Assert.True(r.Read());
        Assert.Equal(EntryType.EndObject, r.Type);

        Assert.False(r.Read());
    }

    [Fact]
    public void FugaNonNullTest()
    {
        var obj = new Fuga()
        {
            Hoge = CreateHoge()
        };

        var r = new ObjectReader(obj);

        Assert.True(r.Read());
        Assert.Equal(EntryType.StartObject, r.Type);

        Assert.True(r.Read());
        Assert.Equal(EntryType.Property, r.Type);
        Assert.Equal(nameof(obj.Hoge), r.Value);
        AssertHoge(r, obj.Hoge);

        Assert.True(r.Read());
        Assert.Equal(EntryType.EndObject, r.Type);

        Assert.False(r.Read());
    }

    private static Hoge CreateHoge()
    {
        return new Hoge
        {
            Boolean = true,
            Int32 = 1,
            Double = 1.5,
            String = "hoge"
        };
    }

    private static void AssertHoge(ObjectReader r, Hoge obj)
    {
        Assert.True(r.Read());
        Assert.Equal(EntryType.StartObject, r.Type);

        Assert.True(r.Read());
        Assert.Equal(EntryType.Property, r.Type);
        Assert.Equal(nameof(obj.Boolean), r.Value);
        Assert.True(r.Read());
        Assert.Equal(EntryType.Boolean, r.Type);
        Assert.Equal(obj.Boolean, r.Value);

        Assert.True(r.Read());
        Assert.Equal(EntryType.Property, r.Type);
        Assert.Equal(nameof(obj.Int32), r.Value);
        Assert.True(r.Read());
        Assert.Equal(EntryType.Int32, r.Type);
        Assert.Equal(obj.Int32, r.Value);

        Assert.True(r.Read());
        Assert.Equal(EntryType.Property, r.Type);
        Assert.Equal(nameof(obj.Double), r.Value);
        Assert.True(r.Read());
        Assert.Equal(EntryType.Double, r.Type);
        Assert.Equal(obj.Double, r.Value);

        Assert.True(r.Read());
        Assert.Equal(EntryType.Property, r.Type);
        Assert.Equal(nameof(obj.String), r.Value);
        Assert.True(r.Read());
        Assert.Equal(EntryType.String, r.Type);
        Assert.Equal(obj.String, r.Value);

        Assert.True(r.Read());
        Assert.Equal(EntryType.EndObject, r.Type);
    }
}
