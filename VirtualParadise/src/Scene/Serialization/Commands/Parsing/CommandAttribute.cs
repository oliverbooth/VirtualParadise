namespace VirtualParadise.Scene.Serialization.Commands.Parsing
{
    #region Using Directives

    using System;

    #endregion

    /// <summary>
    /// Represents an attribute to be attached to <see cref="Command"/> derived type.
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
        /// <param name="parser">The command parser responsible for this command.</param>
        public CommandAttribute(string name, Type parser)
        {
            this.Name = name?.ToUpperInvariant() ?? String.Empty;

            if (parser.IsSubclassOf(typeof(CommandParser)) || typeof(CommandParser).IsAssignableFrom(parser))
            {
                this.Parser = parser;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the command name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the command parser type.
        /// </summary>
        public Type Parser { get; }

        #endregion
    }
}
