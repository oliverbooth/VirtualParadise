namespace VirtualParadise.Scene.Serialization.Commands.Parsing
{
    #region Using Directives

    using System;
    using X10D;

    #endregion

    /// <summary>
    /// Represents an attribute to be attached to properties in a <see cref="Command"/> derived type
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
        public ParameterAttribute(int index, string name)
        {
            this.Index    = index.Clamp(0, Int32.MaxValue);
            this.Name     = name?.ToUpperInvariant() ?? String.Empty;
            this.Sanitize = o => o;
        }

        #endregion

        #region Properties

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
        /// Gets or sets the sanitize function
        /// </summary>
        public Func<object, object> Sanitize { get; set; }

        #endregion
    }
}
