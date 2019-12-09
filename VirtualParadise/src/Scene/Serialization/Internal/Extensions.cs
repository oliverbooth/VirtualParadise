namespace VirtualParadise.Scene.Serialization.Internal
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using Commands.Parsing;

    internal static class Extensions
    {
        public static ParameterAttribute ToVpParameter(this PropertyInfo memberInfo)
        {
            return memberInfo.GetCustomAttribute<ParameterAttribute>() is { } attribute ? attribute : null;
        }

        public static PropertyAttribute ToVpProperty(this PropertyInfo memberInfo)
        {
            return memberInfo.GetCustomAttribute<PropertyAttribute>() is { } attribute ? attribute : null;
        }

        public static object GetDefaultValue(this PropertyInfo memberInfo, Type type)
        {
            if (!(memberInfo.GetCustomAttribute<DefaultValueAttribute>() is { } attribute))
            {
                return default;
            }

            object value = attribute.Value;
            try
            {
                value = Convert.ChangeType(attribute.Value, type);
            }
            catch
            {
                // ignored
            }

            return value;
        }

        public static T GetDefaultValue<T>(this PropertyInfo memberInfo)
        {
            return (T) memberInfo.GetDefaultValue(typeof(T));
        }
    }
}
