namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using API;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>light</c> command.
    /// </summary>
    [Command("LIGHT", "COLOR", "RADIUS", "BRIGHTNESS", "TIME", "MAXDIST", "ANGLE", "FX", "TYPE")]
    public class LightCommand : CommandBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LightCommand"/> class.
        /// </summary>
        public LightCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LightCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public LightCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, properties)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the angle of the light.
        /// </summary>
        [Property("ANGLE", 45.0)]
        public double Angle { get; set; } = 45.0;

        /// <summary>
        /// Gets or sets the brightness of the light.
        /// </summary>
        [Property("BRIGHTNESS", 0.5)]
        public double Brightness { get; set; } = 0.5;

        /// <summary>
        /// Gets or sets the color of the light.
        /// </summary>
        [Property("COLOR", "WHITE")]
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Gets or sets the effect of the light.
        /// </summary>
        [Property("FX", "NONE")]
        public LightEffect Effect { get; set; } = LightEffect.None;

        /// <summary>
        /// Gets or sets the maximum distance of the light in meters.
        /// </summary>
        [Property("MAXDIST", 1000.0)]
        public double MaxDistance { get; set; } = 1000.0;

        /// <summary>
        /// Gets or sets the radius of the light in meters.
        /// </summary>
        [Property("RADIUS", 10.0)]
        public double Radius { get; set; } = 10.0;

        /// <summary>
        /// Gets or sets the interval of the light animation.
        /// </summary>
        [Property("TIME", 1.0)]
        public double Time { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the type of the light.
        /// </summary>
        [Property("TYPE", "POINT")]
        public LightType Type { get; set; } = LightType.Point;

        #endregion
    }
}
