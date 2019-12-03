namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>teleportxyz</c> command.
    /// </summary>
    [Command("TELEPORTXYZ")]
    public class TeleportXyzCommand : CommandBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TeleportXyzCommand"/> class.
        /// </summary>
        public TeleportXyzCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TeleportXyzCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties. For <c>teleportxyz</c>, this are ignored.</param>
        public TeleportXyzCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, new Dictionary<string, object>())
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the facing direction.
        /// </summary>
        [Parameter(3, "YAW", typeof(double),
            DefaultValue = 0.0,
            Optional = true)]
        public double Direction { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        [Parameter(0, "X", typeof(double))]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        [Parameter(1, "Y", typeof(double))]
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the Z coordinate.
        /// </summary>
        [Parameter(2, "Z", typeof(double))]
        public double Z { get; set; }

        #endregion
    }
}
