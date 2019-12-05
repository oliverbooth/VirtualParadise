namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using API;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>astart</c> command.
    /// </summary>
    [Command("ASTART", "TAG")]
    public class AstartCommand : CommandBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AstartCommand"/> class.
        /// </summary>
        public AstartCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AstartCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public AstartCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
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

        /// <summary>
        /// Gets or sets a value indicating whether this animation loops.
        /// </summary>
        [Parameter(1, "LOOPING", typeof(bool),
            DefaultValue  = false,
            Optional      = true,
            ParameterType = ParameterType.Flag)]
        public bool Loop { get; set; } = false;

        #endregion
    }
}
