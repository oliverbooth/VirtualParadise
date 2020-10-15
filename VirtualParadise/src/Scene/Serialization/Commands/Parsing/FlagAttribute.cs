namespace VirtualParadise.Scene.Serialization.Commands.Parsing
{
    using System;

    /// <summary>
    /// Represents an attribute to be attached to properties in a <see cref="Command"/> derived type
    /// which represent command flags.
    /// This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FlagAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlagAttribute"/> class.
        /// </summary>
        /// <param name="name">The flag name.</param>
        public FlagAttribute(string name)
        {
            this.Name = name?.ToUpperInvariant() ?? String.Empty;
        }

        /// <summary>
        /// Gets or sets the default value of this flag.
        /// </summary>
        public bool DefaultValue { get; set; } = false;

        /// <summary>
        /// Gets the flag name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this flag is optional.
        /// </summary>
        public bool Optional { get; set; } = true;
    }
}
