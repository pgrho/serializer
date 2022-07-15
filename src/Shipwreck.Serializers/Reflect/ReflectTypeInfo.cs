using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Shipwreck.Serializers.Reflect;

internal sealed class ReflectTypeInfo
{
    private static readonly Dictionary<Type, ReflectTypeInfo> _Instances = new();

    private ReflectTypeInfo(Type type)
    {
        Type = type;

        var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
        var entries = new List<ReflectPropertyInfo>(props.Length);
        foreach (var p in props)
        {
            var e = new ReflectPropertyInfo(p);

            entries.Add(e);
        }

        Properties = Array.AsReadOnly(entries.ToArray());
    }

    public Type Type { get; }

    public ReadOnlyCollection<ReflectPropertyInfo> Properties { get; }

    #region ListElementType

    private Type? _ListElementType;

    public Type? ListElementType
    {
        get
        {
            if (_ListElementType == null)
            {
                _ListElementType = (typeof(IList).IsAssignableFrom(Type)
                        ? Type.GetInterfaces().Concat(new[] { Type }).FirstOrDefault(e => e.IsConstructedGenericType && e.GetGenericTypeDefinition() == typeof(IEnumerable<>))?.GenericTypeArguments[0]
                        : null)
                    ?? typeof(void);
            }
            return _ListElementType == typeof(void) ? null : _ListElementType;
        }
    }

    #endregion ListElementType

    // TODO
    public ReflectPropertyInfo? GetProperty(string propertyName, StringComparison comparisonType = StringComparison.Ordinal)
        => Properties.FirstOrDefault(e => e.Name.Equals(propertyName, comparisonType));

    public static ReflectTypeInfo Get(Type type)
    {
        lock (_Instances)
        {
            if (_Instances.TryGetValue(type, out var d))
            {
                return d;
            }
        }
        var r = new ReflectTypeInfo(type);
        lock (_Instances)
        {
            _Instances[type] = r;
        }
        return r;
    }
}
