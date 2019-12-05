namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using API;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>astop</c> command.
    /// </summary>
    [Command("ASTOP", "TAG")]
    public class AstopCommand : CommandBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AstopCommand"/> class.
        /// </summary>
        public AstopCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AstopCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public AstopCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, properties)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Parameter(0, "NAME", typeof(string))]
        public string Name { get; set; }

        #endregion
    }
}
