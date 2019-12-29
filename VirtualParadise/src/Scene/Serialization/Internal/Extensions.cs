namespace VirtualParadise.Scene.Serialization.Internal
{
    using System.ComponentModel;
    using System.Reflection;
    using Commands.Parsing;

    internal static class Extensions
    {
        public static ParameterAttribute ToVpParameter(this PropertyInfo memberInfo)
        {
            return memberInfo.GetCustomAttribute<ParameterAttribute>();
        }

        public static PropertyAttribute ToVpProperty(this PropertyInfo memberInfo)
        {
            return memberInfo.GetCustomAttribute<PropertyAttribute>();
        }

        public static object GetDefaultValue(this PropertyInfo memberInfo)
        {
            return memberInfo.GetCustomAttribute<DefaultValueAttribute>() is { } attribute ? attribute.Value : default;
        }

        public static T GetDefaultValue<T>(this PropertyInfo memberInfo)
        {
            return (T) memberInfo.GetDefaultValue();
        }
    }
}
