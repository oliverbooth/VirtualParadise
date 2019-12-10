namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using API;
    using Internal;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents the base class for all commands.
    /// </summary>
    public abstract class CommandBase
    {
        #region Constructors

        protected CommandBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBase"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        [SuppressMessage("Globalization",
            "CA1308:Normalize strings to uppercase",
            Justification = "Lower case string necessary")]
        protected CommandBase(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
        {
            /*Type type = this.GetType();

            // Set the command name
            this.CommandName = type.GetCustomAttribute<CommandAttribute>()?.Name ?? String.Empty;

            // Get valid property names and add the default ones
            IList<string> propertyNames = (type.GetCustomAttribute<CommandAttribute>()?
                                               .PropertyNames
                                               .Select(p => p.ToUpperInvariant()) ??
                                           Array.Empty<string>()).ToList();

            propertyNames.Add("NAME");

            // Only add property values that are accepted
            this.Properties = (properties ?? new Dictionary<string, object>())
                             .Where(pair => propertyNames.Contains(pair.Key.ToUpperInvariant()))
                             .ToDictionary(pair => pair.Key.ToUpperInvariant(), pair => pair.Value);

            this.Arguments = args is null || args.Count == 0
                ? Array.Empty<string>()
                : args;

            // Set globally expected values
//            this.IsGlobal   = this.Arguments.Select(s => s.ToUpperInvariant()).Contains("GLOBAL");
//            this.IsLocked   = this.Arguments.Select(s => s.ToUpperInvariant()).Contains("LOCK");
            this.TargetName = this.Properties.ContainsKey("NAME") ? this.Properties["NAME"].ToString() : String.Empty;

            PropertyInfo[] typeProperties = type.GetProperties(BindingFlags.Instance |
                                                               BindingFlags.Public   |
                                                               BindingFlags.NonPublic);

            PropertyInfo[] orderedTypeProperties
                = typeProperties
                 .Where(t => !(t.GetCustomAttribute<ParameterAttribute>() is null))
                 .OrderBy(p => p.GetCustomAttribute<ParameterAttribute>().Index)
                 .ToArray();

            for (int i = 0, j = 0; i < this.Arguments.Count; i++, j++)
            {
                if (j >= orderedTypeProperties.Length)
                {
                    break;
                }

                PropertyInfo       typeProperty = orderedTypeProperties.ElementAt(j);
                ParameterAttribute parameter    = typeProperty.GetCustomAttribute<ParameterAttribute>();

                if (this.Arguments.Count <= i && !parameter.Optional)
                {
                    throw new ArgumentOutOfRangeException(parameter.Name,
                        $"{type.Name} expects more than {this.Arguments.Count} arguments");
                }

                object value = this.Arguments.ElementAtOrDefault(i);
                try
                {
                    value = Convert.ChangeType(value,
                        typeProperty.PropertyType,
                        CultureInfo.InvariantCulture);
                }
                catch (Exception ex) when (ex is FormatException || ex is InvalidCastException)
                {
                    string arg = value?.ToString() ?? String.Empty;

                    if (typeProperty.PropertyType == typeof(bool))
                    {
                        if (parameter.ParameterType == ParameterType.Literal)
                        {
                            if (Keyword.TryBool(arg, out bool b))
                            {
                                value = b;
                            }
                            else
                            {
                                i--;
                                continue;
                            }
                        }
                        else if (parameter.ParameterType == ParameterType.Flag)
                        {
                            if (value?.ToString().Equals(parameter.Name, StringComparison.InvariantCultureIgnoreCase) ??
                                false)
                            {
                                value = true;
                            }
                            else
                            {
                                i--;
                                continue;
                            }
                        }
                    }
                    else if (typeProperty.PropertyType == typeof(Color))
                    {
                        value = Color.FromString(arg);
                    }
                    else if (typeProperty.PropertyType == typeof(int))
                    {
                        if (Int32.TryParse(value?.ToString() ?? String.Empty, out int intValue))
                        {
                            value = intValue;
                        }
                        else
                        {
                            i--;
                            continue;
                        }
                    }
                    else if (typeProperty.PropertyType == typeof(double))
                    {
                        if (Double.TryParse(value?.ToString() ?? String.Empty, out double doubleValue))
                        {
                            value = doubleValue;
                        }
                        else
                        {
                            i--;
                            continue;
                        }
                    }
                    else
                    {
                        i--;
                        continue;
                    }
                }

                typeProperty.SetValue(this, value, null);
            }

            foreach (PropertyInfo typeProperty in typeProperties
               .Where(t => !(t.GetCustomAttribute<PropertyAttribute>() is null)))
            {
                PropertyAttribute property = typeProperty.GetCustomAttribute<PropertyAttribute>();

                if (this.Properties.ContainsKey(property.Name.ToUpperInvariant()))
                {
                    object value = this.Properties[property.Name.ToUpperInvariant()];

                    if (typeProperty.PropertyType == typeof(bool))
                    {
                        if (Keyword.TryBool(value.ToString(), out bool b))
                        {
                            value = b;
                        }
                    }
                    else if (typeProperty.PropertyType == typeof(Color))
                    {
                        value = Color.FromString(value.ToString());
                    }
                    else if (typeProperty.PropertyType == typeof(int))
                    {
                        if (Int32.TryParse(value?.ToString() ?? String.Empty, out int intValue))
                        {
                            value = intValue;
                        }
                    }
                    else if (typeProperty.PropertyType == typeof(double))
                    {
                        if (Double.TryParse(value?.ToString() ?? String.Empty, out double doubleValue))
                        {
                            value = doubleValue;
                        }
                    }
                    else if (typeProperty.PropertyType.IsEnum)
                    {
                        try
                        {
                            value = Enum.Parse(typeProperty.PropertyType,
                                value?.ToString() ?? String.Empty,
                                true);
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    typeProperty.SetValue(this, value, null);
                }
            }*/
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the command name.
        /// </summary>
        public string CommandName { get; internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether this command is global.
        /// </summary>
        [Flag("global")]
        public bool IsGlobal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this command is locked.
        /// </summary>
        [Flag("lock")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// Gets or sets the name in this command.
        /// </summary>
        [DefaultValue("")]
        [Property("name")]
        public string TargetName { get; set; }

        /// <summary>
        /// Gets the arguments passed to this command.
        /// </summary>
        public IReadOnlyCollection<string> Arguments { get; internal set; }

        /// <summary>
        /// Gets the flags passed to this command.
        /// </summary>
        public IReadOnlyCollection<string> Flags { get; internal set; }

        /// <summary>
        /// Gets the properties passed to this command.
        /// </summary>
        public IDictionary<string, object> Properties { get; internal set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        [SuppressMessage("Globalization",
            "CA1308:Normalize strings to uppercase",
            Justification = "Lower case string necessary")]
        public override string ToString()
        {
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(this.CommandName.ToLowerInvariant())
                       .Append(' ');

                // Add necessary arguments
                PropertyInfo[] members = this.GetType().GetProperties(BindingFlags.Instance |
                                                                      BindingFlags.Public   |
                                                                      BindingFlags.NonPublic);

                foreach (PropertyInfo member in members.Where(t => !(t.ToVpParameter() is null))
                                                       .OrderBy(p => p.ToVpParameter().Index))
                {
                    ParameterAttribute parameter = member.ToVpParameter();
                    object             value     = member.GetValue(this, null);

                    if (value is string str && Regex.Match(str, "\\s").Success)
                    {
                        value = $"\"{str}\"";
                    }

                    if (member.PropertyType.IsEnum)
                    {
                        value = value.ToString().ToLowerInvariant();
                    }

                    switch (value)
                    {
                        /*case Enum _:
                            builder.Append(value.ToString().ToLowerInvariant())
                                   .Append(' ');
    
                            break;*/

                        case bool b:
                            if (!parameter.Optional || b != member.GetDefaultValue<bool>())
                            {
                                // Append ON or OFF for literals, or the parameter name for flags
                                builder.Append(parameter.ParameterType == ParameterType.Flag
                                    ? parameter.Name.ToLowerInvariant()
                                    : b
                                        ? "on"
                                        : "off").Append(' ');
                            }

                            break;

                        default:
                            if (!parameter.Optional ||
                                !value.ToString().Equals(member.GetDefaultValue(member.PropertyType).ToString(),
                                    StringComparison.InvariantCultureIgnoreCase))
                            {
                                // Just append the value
                                builder.Append(value)
                                       .Append(' ');
                            }

                            break;
                    }
                }

                builder.Append(this.GetPropertiesString()).Append(' ');
                builder.Append(this.GetFlagsString()).Append(' ');

                return builder.ToString().Trim();
            }
        }

        /// <summary>
        /// Gets the flags in this command.
        /// </summary>
        protected virtual string GetFlagsString(params string[] ignore)
        {
            StringBuilder builder = new StringBuilder();
            PropertyInfo[] members = this.GetType().GetProperties(BindingFlags.Instance |
                                                                  BindingFlags.Public   |
                                                                  BindingFlags.NonPublic);

            foreach (PropertyInfo member in members.Where(t => !(t.GetCustomAttribute<FlagAttribute>() is null)))
            {
                FlagAttribute flag  = member.GetCustomAttribute<FlagAttribute>();
                object        value = member.GetValue(this, null);
                if (value is bool b && b && !ignore.Contains(flag.Name, StringComparer.InvariantCultureIgnoreCase))
                {
                    builder.Append(flag.Name.ToLowerInvariant())
                           .Append(' ');
                }
            }

            return builder.ToString().Trim();
        }

        /// <summary>
        /// Gets the properties in this command.
        /// </summary>
        protected virtual string GetPropertiesString()
        {
            StringBuilder builder = new StringBuilder();
            PropertyInfo[] members = this.GetType().GetProperties(BindingFlags.Instance |
                                                                  BindingFlags.Public   |
                                                                  BindingFlags.NonPublic);

            foreach (PropertyInfo member in members.Where(t => !(t.ToVpProperty() is null)))
            {
                PropertyAttribute property = member.ToVpProperty();
                object            value    = member.GetValue(this, null);

                if (!property.Optional ||
                    !value.ToString().Equals(member.GetDefaultValue(member.PropertyType).ToString(),
                        StringComparison.InvariantCultureIgnoreCase))
                {
                    if (value is Enum)
                    {
                        value = value.ToString().ToLowerInvariant();
                    }

                    builder.Append(property.Name.ToLowerInvariant().Trim())
                           .Append('=')
                           .Append(value)
                           .Append(' ');
                }
            }

            return builder.ToString().Trim();
        }

        /// <summary>
        /// Updates the command's property members by referencing the <see cref="Arguments"/> collection.
        /// </summary>
        internal void UpdateArguments()
        {
            Type type = this.GetType();
            PropertyInfo[] members = type.GetProperties(BindingFlags.Instance |
                                                        BindingFlags.Public   |
                                                        BindingFlags.NonPublic);

            List<string> args = this.Arguments
                                    .Where(a => !Regex.Match(a, "\\S+=\\S").Success)
                                    .ToList();
            args.RemoveAll(String.IsNullOrWhiteSpace);

            // get arguments without flags
            foreach (PropertyInfo member in members.Where(prop => !(prop.GetCustomAttribute<FlagAttribute>() is null))
                                                   .Where(prop => prop.PropertyType == typeof(bool)))
            {
                FlagAttribute flag = member.GetCustomAttribute<FlagAttribute>();
                if (args.Select(a => a.ToUpperInvariant()).Contains(flag.Name.ToUpperInvariant()))
                {
                    args.RemoveAll(a => a.Equals(flag.Name, StringComparison.InvariantCultureIgnoreCase));
                }
            }

            this.Arguments = args.AsReadOnly();

            foreach (PropertyInfo member in members.Where(prop => !(prop.ToVpParameter() is null))
                                                   .OrderBy(prop => prop.ToVpParameter().Index))
            {
                ParameterAttribute attribute = member.ToVpParameter();
                object             value     = member.GetDefaultValue(member.PropertyType);

                value = SanitizeValue(member.PropertyType, args.Count > attribute.Index
                    ? args[attribute.Index]
                    : value);

                member.SetValue(this, value, null);
            }
        }

        /// <summary>
        /// Updates the command's property members by referencing the <see cref="Flags"/> collection.
        /// </summary>
        internal void UpdateFlags()
        {
            Type type = this.GetType();
            PropertyInfo[] memberProperties = type.GetProperties(BindingFlags.Instance |
                                                                 BindingFlags.Public   |
                                                                 BindingFlags.NonPublic);

            List<string> args = this.Arguments
                                    .Where(a => !Regex.Match(a, "\\S+=\\S").Success)
                                    .ToList();

            foreach (PropertyInfo memberProperty in
                memberProperties.Where(prop => !(prop.GetCustomAttribute<FlagAttribute>() is null))
                                .Where(prop => prop.PropertyType == typeof(bool)))
            {
                FlagAttribute flag = memberProperty.GetCustomAttribute<FlagAttribute>();
                if (args.Select(a => a.ToUpperInvariant()).Contains(flag.Name.ToUpperInvariant()))
                {
                    memberProperty.SetValue(this, true, null);
                }
            }
        }

        /// <summary>
        /// Updates the command's property members by referencing the <see cref="Properties"/> dictionary.
        /// </summary>
        internal void UpdateProperties()
        {
            Type type = this.GetType();
            PropertyInfo[] members = type.GetProperties(BindingFlags.Instance |
                                                        BindingFlags.Public   |
                                                        BindingFlags.NonPublic);

            foreach (PropertyInfo member in members.Where(prop => !(prop.ToVpProperty() is null)))
            {
                PropertyAttribute attribute = member.ToVpProperty();

                // fetch default value for property
                object value = member.GetDefaultValue(member.PropertyType);

                if (this.Properties.ContainsKey(attribute.Name.ToUpperInvariant()))
                {
                    // assign parsed value if it exists
                    value = this.Properties[attribute.Name.ToUpperInvariant()];
                }

                value = SanitizeValue(member.PropertyType, value);
                member.SetValue(this, value, null);
            }
        }

        private static object SanitizeValue(Type type, object value)
        {
            if (type == typeof(Color))
            {
                value = Color.FromString(value.ToString());
            }
            else if (type == typeof(bool))
            {
                value = Keyword.TryBool(value.ToString(), out bool b) && b;
            }
            else if (type.IsEnum)
            {
                try
                {
                    value = Enum.Parse(type, value?.ToString() ?? String.Empty, true);
                }
                catch
                {
                    // ignored
                }
            }
            else if (type == typeof(string) && value == default)
            {
                value = String.Empty;
            }
            else
            {
                value = Convert.ChangeType(value, type);
            }

            return value;
        }

        #endregion
    }
}
