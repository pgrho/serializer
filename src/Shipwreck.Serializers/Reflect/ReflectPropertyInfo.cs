using System.ComponentModel;
using System.Reflection;

namespace Shipwreck.Serializers.Reflect;

internal sealed class ReflectPropertyInfo
{
    private static readonly Dictionary<Type, ReflectPropertyInfo[]> _Properties = new();

    public ReflectPropertyInfo(PropertyInfo property)
    {
        Property = property;
        var dva = Property.GetCustomAttribute<DefaultValueAttribute>();
        HasDefaultValue = dva != null;
        DefaultValue = dva?.Value;

        IsNullableValueType = property.PropertyType.IsValueType && Nullable.GetUnderlyingType(property.PropertyType) != null;

        ShouldSerializeMethod = property.ReflectedType.GetMethod("ShouldSerialize" + Property.Name, BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null);
        if (ShouldSerializeMethod?.ReturnType != typeof(bool))
        {
            ShouldSerializeMethod = null;
        }

        Converter = property.GetCustomAttribute<TypeConverterAttribute>()?.ConverterTypeName is string ctn
                    && Activator.CreateInstance(Type.GetType(ctn)) is TypeConverter c
                    ? c : TypeDescriptor.GetConverter(Property.PropertyType);
    }

    public PropertyInfo Property { get; }

    public bool IsNullableValueType { get; }

    public MethodInfo? ShouldSerializeMethod { get; }

    public string Name => Property.Name;
    public Type PropertyType => Property.PropertyType;

    public bool HasDefaultValue { get; }

    public object? DefaultValue { get; }

    public TypeConverter Converter { get; }

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
