namespace VirtualParadise.Scene.Serialization.Triggers
{
    #region Using Directives

    using System;
    using System.Diagnostics.CodeAnalysis;

    #endregion

    /// <summary>
    /// Represents an attribute to be attached to <see cref="Trigger"/> derived type.
    /// This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TriggerAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerAttribute"/> class.
        /// </summary>
        /// <param name="name">The trigger name.</param>
        [SuppressMessage("Globalization",
            "CA1308:Normalize strings to uppercase",
            Justification = "Lower case string necessary")]
        public TriggerAttribute(string name)
        {
            this.Name = name?.ToLowerInvariant() ?? String.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the trigger name.
        /// </summary>
        public string Name { get; }

        #endregion
    }
}
