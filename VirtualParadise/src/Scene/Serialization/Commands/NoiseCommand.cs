namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>noise</c> command.
    /// </summary>
    [Command("NOISE", "VOLUME")]
    public class NoiseCommand : CommandBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NoiseCommand"/> class.
        /// </summary>
        public NoiseCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoiseCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public NoiseCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, properties)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the file name or URL of the noise.
        /// </summary>
        [Parameter(0, "URL", typeof(string))]
        public string FileName { get; set; } = "";

        /// <summary>
        /// Gets or sets a value indicating whether this noise loops.
        /// </summary>
        [Parameter(1, "LOOP", typeof(bool),
            DefaultValue  = false,
            Optional      = true,
            ParameterType = ParameterType.Flag)]
        public bool Loop { get; set; } = false;

        /// <summary>
        /// Gets or sets the volume of this sound, between 0.0 and 1.0.
        /// </summary>
        [Property("VOLUME", 1.0)]
        public double Volume { get; set; } = 1.0;

        #endregion
    }
}
