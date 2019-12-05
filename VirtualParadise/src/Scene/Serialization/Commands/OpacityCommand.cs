namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>opacity</c> command.
    /// </summary>
    [Command("OPACITY")]
    public class OpacityCommand : CommandBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpacityCommand"/> class.
        /// </summary>
        public OpacityCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpacityCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public OpacityCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, properties)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the opacity value.
        /// </summary>
        [Parameter(0, "OPACITY", typeof(int),
            DefaultValue = 1.0)]
        public double Value { get; set; } = 1.0;

        #endregion
    }
}
