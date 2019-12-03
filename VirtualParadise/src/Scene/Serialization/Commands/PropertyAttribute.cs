namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Diagnostics.CodeAnalysis;

    #endregion

    /// <summary>
    /// Represents an attribute to be attached to properties in a <see cref="CommandBase"/> derived type
    /// which represent command properties.
    /// This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PropertyAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyAttribute"/> class.
        /// </summary>
        /// <param name="name">The name / key of the property.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="optional">Optional. Whether or not the property itself is optional. Defaults to
        /// <see langword="false"/>.</param>
        [SuppressMessage("Globalization",
            "CA1308:Normalize strings to uppercase",
            Justification = "Lower case string necessary")]
        public PropertyAttribute(string name,
                                 object defaultValue = default,
                                 bool   optional     = true)
        {
            this.Name         = name?.ToLowerInvariant() ?? String.Empty;
            this.DefaultValue = defaultValue;
            this.Optional     = optional;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the default value of this property.
        /// </summary>
        public object DefaultValue { get; }

        /// <summary>
        /// Gets the property name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets a value indicating whether this property is optional.
        /// </summary>
        public bool Optional { get; }

        #endregion
    }
}
