namespace VirtualParadise.Scene.Serialization.Commands.Parsing
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    #endregion

    /// <summary>
    /// Represents an attribute to be attached to <see cref="CommandBase"/> derived type.
    /// This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CommandAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandAttribute"/> class.
        /// </summary>
        /// <param name="name">The command name.</param>
        /// <param name="propertyNames">The property names that this command understands.</param>
        [SuppressMessage("Globalization",
            "CA1308:Normalize strings to uppercase",
            Justification = "Lower case string necessary")]
        public CommandAttribute(string name, params string[] propertyNames)
        {
            this.Name          = name?.ToLowerInvariant() ?? String.Empty;
            this.PropertyNames = propertyNames;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the command name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the valid property names that this command accepts.
        /// </summary>
        public IReadOnlyCollection<string> PropertyNames { get; }

        #endregion
    }
}
