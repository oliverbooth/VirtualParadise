namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System.ComponentModel;
    using Parsers;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>teleportxyz</c> command.
    /// </summary>
    [Command("teleportxyz", typeof(CommandDefaultParser<TeleportXyzCommand>))]
    public class TeleportXyzCommand : CommandBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the facing direction.
        /// </summary>
        [DefaultValue(0.0)]
        [Parameter(3, "yaw", Optional = true)]
        public double Direction { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        [Parameter(0, "x")]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        [Parameter(1, "y")]
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the Z coordinate.
        /// </summary>
        [Parameter(2, "z")]
        public double Z { get; set; }

        #endregion
    }
}
