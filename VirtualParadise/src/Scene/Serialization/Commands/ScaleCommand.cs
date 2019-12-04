namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>scale</c> command.
    /// </summary>
    [Command("SCALE")]
    public class ScaleCommand : CommandBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleCommand"/> class.
        /// </summary>
        public ScaleCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public ScaleCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, new Dictionary<string, object>())
        {
            if (args.Count == 1)
            {
                this.Y = this.Z = this.X;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the X-axis scale.
        /// </summary>
        [Parameter(0, "X", typeof(double))]
        public double X { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the Y-axis scale.
        /// </summary>
        [Parameter(1, "Y", typeof(double),
            DefaultValue  = 1.0,
            Optional      = true,
            ParameterType = ParameterType.Literal)]
        public double Y { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the Z-axis scale.
        /// </summary>
        [Parameter(2, "Z", typeof(double),
            DefaultValue  = 1.0,
            Optional      = true,
            ParameterType = ParameterType.Literal)]
        public double Z { get; set; } = 1.0;

        #endregion
    }
}
