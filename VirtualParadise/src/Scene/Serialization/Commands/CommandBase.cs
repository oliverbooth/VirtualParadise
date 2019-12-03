namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using API;

    #endregion

    /// <summary>
    /// Represents the base class for all commands.
    /// </summary>
    public abstract class CommandBase
    {
        #region Constructors

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
            Type type = this.GetType();

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
            this.IsGlobal   = this.Arguments.Select(s => s.ToUpperInvariant()).Contains("GLOBAL");
            this.IsLocked   = this.Arguments.Select(s => s.ToUpperInvariant()).Contains("LOCK");
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
                            switch (arg.ToUpperInvariant())
                            {
                                case "OFF":
                                case "NO":
                                case "0":
                                case "FALSE":
                                    value = false;
                                    break;

                                case "ON":
                                case "YES":
                                case "1":
                                case "TRUE":
                                    value = true;
                                    break;

                                default:
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
                        switch (value.ToString().ToUpperInvariant())
                        {
                            case "OFF":
                            case "NO":
                            case "0":
                            case "FALSE":
                                value = false;
                                break;

                            case "ON":
                            case "YES":
                            case "1":
                            case "TRUE":
                                value = true;
                                break;
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
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the command name.
        /// </summary>
        public string CommandName { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this command is global.
        /// </summary>
        [Parameter(101, "global", typeof(bool),
            DefaultValue  = false,
            Optional      = true,
            ParameterType = ParameterType.Flag)]
        public bool IsGlobal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this command is locked.
        /// </summary>
        [Parameter(100, "lock", typeof(bool),
            DefaultValue  = false,
            Optional      = true,
            ParameterType = ParameterType.Flag)]
        public bool IsLocked { get; set; }

        /// <summary>
        /// Gets or sets the name in this command.
        /// </summary>
        [Property("name", "")]
        public string TargetName { get; set; }

        /// <summary>
        /// Gets the arguments passed to this command.
        /// </summary>
        protected IReadOnlyCollection<string> Arguments { get; }

        /// <summary>
        /// Gets the properties passed to this command.
        /// </summary>
        protected IDictionary<string, object> Properties { get; }

        #endregion

        #region Methods

        /// <inheritdoc />
        [SuppressMessage("Globalization",
            "CA1308:Normalize strings to uppercase",
            Justification = "Lower case string necessary")]
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.CommandName.ToLowerInvariant())
                   .Append(' ');

            // Add necessary arguments
            PropertyInfo[] typeProperties = this.GetType()
                                                .GetProperties(
                                                     BindingFlags.Instance | BindingFlags.Public |
                                                     BindingFlags.NonPublic);

            foreach (PropertyInfo typeProperty in typeProperties
                                                 .Where(t => !(t.GetCustomAttribute<ParameterAttribute>() is null))
                                                 .OrderBy(p => p.GetCustomAttribute<ParameterAttribute>().Index))
            {
                ParameterAttribute parameter = typeProperty.GetCustomAttribute<ParameterAttribute>();
                object             value     = typeProperty.GetValue(this, null);

                if (value is string str && Regex.Match(str, "\\s").Success)
                {
                    value = $"\"{str}\"";
                }

                if (typeProperty.PropertyType.IsEnum)
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
                        if (!parameter.Optional || b != (bool) parameter.DefaultValue)
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
                        if (!parameter.Optional || !value.Equals(parameter.DefaultValue))
                        {
                            // Just append the value
                            builder.Append(value)
                                   .Append(' ');
                        }

                        break;
                }
            }

            foreach (PropertyInfo typeProperty in typeProperties.Where(
                t => !(t.GetCustomAttribute<PropertyAttribute>() is null)))
            {
                PropertyAttribute property = typeProperty.GetCustomAttribute<PropertyAttribute>();
                object            value    = typeProperty.GetValue(this, null);

                if (!property.Optional || !value.Equals(property.DefaultValue))
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

        #endregion
    }
}
