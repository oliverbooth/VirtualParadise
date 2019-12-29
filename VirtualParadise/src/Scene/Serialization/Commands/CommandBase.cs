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
    using API;
    using Extensions;
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
            if (String.IsNullOrWhiteSpace(this.CommandName)) {
                this.CommandName = (this.GetType().GetCustomAttribute<CommandAttribute>()?.Name ?? String.Empty)
                   .ToUpperInvariant();
            }
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
        public bool IsGlobal { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this command is locked.
        /// </summary>
        [Flag("lock")]
        public bool IsLocked { get; set; } = false;

        /// <summary>
        /// Gets or sets the name in this command.
        /// </summary>
        [DefaultValue("")]
        [Property("name")]
        public string TargetName { get; set; } = String.Empty;

        /// <summary>
        /// Gets the arguments passed to this command.
        /// </summary>
        public IReadOnlyCollection<string> Arguments { get; internal set; } = new List<string>();

        /// <summary>
        /// Gets the flags passed to this command.
        /// </summary>
        public IReadOnlyCollection<string> Flags { get; internal set; } = new List<string>();

        /// <summary>
        /// Gets the properties passed to this command.
        /// </summary>
        public IDictionary<string, object> Properties { get; internal set; } = new Dictionary<string, object>();

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
                builder.Append(this.CommandName?.ToLowerInvariant() ??
                               this.GetType().GetCustomAttribute<CommandAttribute>().Name.ToLowerInvariant())
                       .Append(' ');

                // Add necessary arguments
                PropertyInfo[] members = this.GetType().GetProperties(BindingFlags.Instance |
                                                                      BindingFlags.Public   |
                                                                      BindingFlags.NonPublic);

                foreach (PropertyInfo member in members.Where(t => !(t.ToVpParameter() is null))
                                                       .OrderBy(p => p.ToVpParameter().Index)) {
                    ParameterAttribute parameter = member.ToVpParameter();
                    object             value     = member.GetValue(this, null);

                    if (value is string str) {
                        if (str.Any(Char.IsWhiteSpace)) {
                            value = '"' + str + '"';
                        }
                    }

                    if (member.PropertyType.IsEnum) {
                        value = value.ToString().ToLowerInvariant();
                    }

                    switch (value) {
                        /*case Enum _:
                            builder.Append(value.ToString().ToLowerInvariant())
                                   .Append(' ');
    
                            break;*/

                        case ColorEnum ce:
                            if (!parameter.Optional || ce != member.GetDefaultValue<ColorEnum>()) {
                                builder.Append(ColorEnumExtensions.ToString(ce))
                                       .Append(' ');
                            }

                            break;

                        case Color c:
                            if (!parameter.Optional || c != member.GetDefaultValue<Color>()) {
                                builder.Append(c.ToString())
                                       .Append(' ');
                            }

                            break;

                        case bool b:
                            if (!parameter.Optional || b != member.GetDefaultValue<bool>()) {
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
                                !value.ToString().Equals(member.GetDefaultValue().ToString(),
                                    StringComparison.InvariantCultureIgnoreCase)) {
                                // Just append the value
                                builder.Append(value)
                                       .Append(' ');
                            }

                            break;
                    }
                }

                string properties = this.GetPropertiesString().Trim();

                builder.Append(properties);
                if (!String.IsNullOrWhiteSpace(properties)) {
                    builder.Append(' ');
                }

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

            foreach (PropertyInfo member in members.Where(t => !(t.GetCustomAttribute<FlagAttribute>() is null))) {
                FlagAttribute flag  = member.GetCustomAttribute<FlagAttribute>();
                object        value = member.GetValue(this, null);
                if (value is bool b && b && !ignore.Contains(flag.Name, StringComparer.InvariantCultureIgnoreCase)) {
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

            foreach (PropertyInfo member in members.Where(t => !(t.ToVpProperty() is null))) {
                PropertyAttribute property     = member.ToVpProperty();
                object            value        = member.GetValue(this, null);
                string            stringValue  = value.ToString();
                string            defaultValue = member.GetDefaultValue().ToString();

                if (value is Color) {
                    defaultValue = ((Color) member.GetDefaultValue<ColorEnum>()).ToString();
                }

                if (!property.Optional ||
                    !stringValue.Equals(defaultValue, StringComparison.InvariantCultureIgnoreCase)) {
                    if (value is Enum) {
                        value = stringValue.ToLowerInvariant();
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

            List<string> args = this.Arguments.Where(a => a.IndexOf('=') < 0).ToList();
            args.RemoveAll(String.IsNullOrWhiteSpace);

            // get arguments without flags
            foreach (PropertyInfo member in members.Where(prop => !(prop.GetCustomAttribute<FlagAttribute>() is null))
                                                   .Where(prop => prop.PropertyType == typeof(bool))) {
                FlagAttribute flag = member.GetCustomAttribute<FlagAttribute>();
                if (args.Select(a => a.ToUpperInvariant()).Contains(flag.Name.ToUpperInvariant())) {
                    args.RemoveAll(a => a.Equals(flag.Name, StringComparison.InvariantCultureIgnoreCase));
                }
            }

            this.Arguments = args.AsReadOnly();

            foreach (PropertyInfo member in members.Where(prop => !(prop.ToVpParameter() is null))
                                                   .OrderBy(prop => prop.ToVpParameter().Index)) {
                ParameterAttribute attribute = member.ToVpParameter();
                object             value     = member.GetDefaultValue();

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

            List<string> args = this.Arguments.Where(a => a.IndexOf('=') < 0).ToList();

            foreach (PropertyInfo memberProperty in
                memberProperties.Where(prop => !(prop.GetCustomAttribute<FlagAttribute>() is null))
                                .Where(prop => prop.PropertyType == typeof(bool))) {
                FlagAttribute flag = memberProperty.GetCustomAttribute<FlagAttribute>();
                if (args.Any(a => a.Equals(flag.Name, StringComparison.InvariantCultureIgnoreCase))) {
                    memberProperty.SetValue(this, true, null);

                    int index = args.FindIndex(s => s.Equals(flag.Name, StringComparison.InvariantCultureIgnoreCase));
                    args.RemoveAt(index);
                }
            }

            this.Arguments = args.AsReadOnly();
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

            foreach (PropertyInfo member in members) {
                PropertyAttribute property = member.ToVpProperty();
                if (property is null) {
                    continue;
                }

                // fetch default value for property, or parse it if it exists
                object value = this.Properties.ContainsKey(property.Name.ToUpperInvariant())
                    ? this.Properties[property.Name.ToUpperInvariant()]
                    : member.GetDefaultValue();

                value = SanitizeValue(member.PropertyType, value);
                member.SetValue(this, value, null);
            }
        }

        private static object SanitizeValue(Type type, object value)
        {
            if (type == typeof(string) && value == default) {
                value = String.Empty;
            } else if (value is null) {
                return null;
            } else if (type == typeof(ColorEnum)) {
                value = Color.FromEnum((ColorEnum) value);
            } else if (type == typeof(Color)) {
                value = Color.FromString(value.ToString());
            } else if (type == typeof(bool)) {
                value = Keyword.TryBool(value.ToString(), out bool b) && b;
            } else if (type.IsEnum) {
                try {
                    value = Enum.Parse(type, value.ToString(), true);
                } catch {
                    // ignored
                }
            } else {
                value = Convert.ChangeType(value, type);
            }

            return value;
        }

        #endregion
    }
}
