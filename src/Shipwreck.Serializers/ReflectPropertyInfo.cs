using System.Reflection;

namespace Shipwreck.Serializers;

internal sealed class ReflectPropertyInfo
{
    private static readonly Dictionary<Type, ReflectPropertyInfo[]> _Properties = new();

    private ReflectPropertyInfo(PropertyInfo property)
    {
        Property = property;
    }

    public PropertyInfo Property { get; }

    public string Name => Property.Name;
    public Type PropertyType => Property.PropertyType;

    public static ReflectPropertyInfo[] GetProperties(Type type)
    {
        lock (_Properties)
        {
            if (_Properties.TryGetValue(type, out var d))
            {
                return d;
            }
        }
        var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
        var entries = new List<ReflectPropertyInfo>(props.Length);
        foreach (var p in props)
        {
            var e = new ReflectPropertyInfo(p);

            entries.Add(e);
        }
        var ary = entries.ToArray();
        lock (_Properties)
        {
            _Properties[type] = ary;
        }
        return ary;
    }
}
