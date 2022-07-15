namespace Shipwreck.Serializers.Reflect;

public class ReflectObjectWriterTest
{
    private sealed class Hoge
    {
        public bool Boolean { get; set; }

        public int Int32 { get; set; }
        public double Double { get; set; }
        public string? String { get; set; }
    }

    [Fact]
    public void Test()
    {
        var expected = new Hoge
        {
            Boolean = true,
            Int32 = 2,
            Double = 3.3,
            String = "4"
        };

        var w = new ReflectObjectWriter(typeof(Hoge));

        w.WriteStartObject();

        w.WriteProperty("invalid");
        w.WriteStartObject();
        w.WriteEndObject();

        w.WriteProperty(nameof(expected.Boolean));
        w.WriteValue(expected.Boolean);

        w.WriteProperty(nameof(expected.Int32));
        w.WriteValue(expected.Int32);

        w.WriteProperty(nameof(expected.Double));
        w.WriteValue(expected.Double);

        w.WriteProperty(nameof(expected.String));
        w.WriteValue(expected.String);

        w.WriteEndObject();

        Assert.False(w.CanWrite);

        var actual = Assert.IsType<Hoge>(w.Object);

        Assert.Equal(expected.Boolean, actual.Boolean);
        Assert.Equal(expected.Int32, actual.Int32);
        Assert.Equal(expected.Double, actual.Double);
        Assert.Equal(expected.String, actual.String);
    }
}
