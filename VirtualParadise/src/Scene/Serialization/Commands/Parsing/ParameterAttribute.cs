namespace VirtualParadise.Scene.Serialization.Commands.Parsing
{
    #region Using Directives

    using System;
    using System.Diagnostics.CodeAnalysis;
    using X10D;

    #endregion

    /// <summary>
    /// Represents an attribute to be attached to properties in a <see cref="CommandBase"/> derived type
    /// which represent command parameters.
    /// This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ParameterAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterAttribute"/> class.
        /// </summary>
        /// <param name="index">The parameter index.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The parameter type.</param>
        [SuppressMessage("Globalization",
            "CA1308:Normalize strings to uppercase",
            Justification = "Lower case string necessary")]
        public ParameterAttribute(int index, string name, Type type)
        {
            this.Index    = index.Clamp(0, Int32.MaxValue);
            this.Name     = name?.ToLowerInvariant() ?? String.Empty;
            this.Type     = type;
            this.Sanitize = o => o;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the default value of this parameter.
        /// </summary>
        public object DefaultValue { get; set; } = default;

        /// <summary>
        /// Gets or sets a value indicating how the parameter should be parsed or written in the event it's a boolean.
        /// </summary>
        public ParameterType ParameterType { get; set; }

        /// <summary>
        /// Gets the parameter index.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Gets the parameter name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this parameter is optional.
        /// </summary>
        public bool Optional { get; set; } = false;

        /// <summary>
        /// Gets the data type of the parameter.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets or sets the sanitize function
        /// </summary>
        public Func<object, object> Sanitize { get; set; }

        #endregion
    }
}
