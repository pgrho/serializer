using System.ComponentModel;
using System.Reflection;

namespace Shipwreck.Serializers;

internal sealed class ReflectPropertyInfo
{
    private static readonly Dictionary<Type, ReflectPropertyInfo[]> _Properties = new();

    private ReflectPropertyInfo(PropertyInfo property)
    {
        Property = property;
        var dva = Property.GetCustomAttribute<DefaultValueAttribute>();
        HasDefaultValue = dva != null;
        DefaultValue = dva?.Value;

        ShouldSerializeMethod = property.ReflectedType.GetMethod("ShouldSerialize" + Property.Name, BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null);
        if (ShouldSerializeMethod?.ReturnType != typeof(bool))
        {
            ShouldSerializeMethod = null;
        }
    }

    public PropertyInfo Property { get; }

    public MethodInfo? ShouldSerializeMethod { get; }

    public string Name => Property.Name;
    public Type PropertyType => Property.PropertyType;

    public bool HasDefaultValue { get; }

    public object? DefaultValue { get; }

    public bool ShouldSerialize(object obj)
        => (bool?)ShouldSerializeMethod?.Invoke(obj, Array.Empty<object>())
        ?? (!HasDefaultValue || Property.GetValue(obj) is var v && !Equals(v, DefaultValue));

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
