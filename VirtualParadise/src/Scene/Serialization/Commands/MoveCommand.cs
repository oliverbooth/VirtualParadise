namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>move</c> command.
    /// </summary>
    [Command("MOVE", "WAIT", "TIME", "OFFSET")]
    public class MoveCommand : CommandBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveCommand"/> class.
        /// </summary>
        public MoveCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public MoveCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, properties)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the file name or URL of the noise.
        /// </summary>
        [Parameter(0, "URL", typeof(string))]
        public double X { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets the file name or URL of the noise.
        /// </summary>
        [Parameter(1, "URL", typeof(string),
            DefaultValue = 0.0,
            Optional     = true)]
        public double Y { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets the file name or URL of the noise.
        /// </summary>
        [Parameter(2, "URL", typeof(string),
            DefaultValue = 0.0,
            Optional     = true)]
        public double Z { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets a value indicating whether this movement is along the object axis (as opposed to world axis).
        /// </summary>
        [Parameter(7, "LTM", typeof(bool),
            DefaultValue  = false,
            Optional      = true,
            ParameterType = ParameterType.Flag)]
        public bool LocalAxis { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this movement loops.
        /// </summary>
        [Parameter(3, "LOOP", typeof(bool),
            DefaultValue  = false,
            Optional      = true,
            ParameterType = ParameterType.Flag)]
        public bool Loop { get; set; } = false;

        /// <summary>
        /// Gets or sets the offset - in seconds - to apply to universe time when synchronizing.
        /// </summary>
        [Property("OFFSET", 0.0)]
        public double Offset { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets a value indicating whether this movement resets after completing half a cycle.
        /// </summary>
        [Parameter(6, "RESET", typeof(bool),
            DefaultValue  = false,
            Optional      = true,
            ParameterType = ParameterType.Flag)]
        public bool Reset { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this movement is smooth.
        /// </summary>
        [Parameter(5, "SMOOTH", typeof(bool),
            DefaultValue  = false,
            Optional      = true,
            ParameterType = ParameterType.Flag)]
        public bool Smooth { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this movement syncs.
        /// </summary>
        [Parameter(4, "SYNC", typeof(bool),
            DefaultValue  = false,
            Optional      = true,
            ParameterType = ParameterType.Flag)]
        public bool Sync { get; set; } = false;

        /// <summary>
        /// Gets or sets the duration - in seconds - of half of a cycle.
        /// </summary>
        [Property("TIME", 1.0)]
        public double Time { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the time - in seconds - before continuing movement after one half of a cycle.
        /// </summary>
        [Property("WAIT", 0.0)]
        public double Wait { get; set; } = 0.0;

        #endregion
    }
}
