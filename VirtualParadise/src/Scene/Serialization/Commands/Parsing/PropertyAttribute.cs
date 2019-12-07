namespace VirtualParadise.Scene.Serialization.Commands.Parsing
{
    #region Using Directives

    using System;

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
        public PropertyAttribute(string name)
        {
            this.Name     = name?.ToUpperInvariant() ?? String.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the property name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this property is optional.
        /// </summary>
        public bool Optional { get; set; }

        #endregion
    }
}
