namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System.ComponentModel;
    using API;
    using Parsers;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>light</c> command.
    /// </summary>
    [Command("light", typeof(CommandDefaultParser<LightCommand>))]
    public class LightCommand : Command
    {
        #region Properties

        /// <summary>
        /// Gets or sets the angle of the light.
        /// </summary>
        [DefaultValue(45.0)]
        [Property("angle")]
        public double Angle { get; set; } = 45.0;

        /// <summary>
        /// Gets or sets the brightness of the light.
        /// </summary>
        [DefaultValue(0.5)]
        [Property("brightness")]
        public double Brightness { get; set; } = 0.5;

        /// <summary>
        /// Gets or sets the color of the light.
        /// </summary>
        [DefaultValue("white")]
        [Property("color")]
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Gets or sets the effect of the light.
        /// </summary>
        [DefaultValue("none")]
        [Property("fx")]
        public LightEffect Effect { get; set; } = LightEffect.None;

        /// <summary>
        /// Gets or sets the maximum distance of the light in meters.
        /// </summary>
        [DefaultValue(1000.0)]
        [Property("maxdist")]
        public double MaxDistance { get; set; } = 1000.0;

        /// <summary>
        /// Gets or sets the radius of the light in meters.
        /// </summary>
        [DefaultValue(10.0)]
        [Property("radius")]
        public double Radius { get; set; } = 10.0;

        /// <summary>
        /// Gets or sets the interval of the light animation.
        /// </summary>
        [DefaultValue(1.0)]
        [Property("time")]
        public double Time { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the type of the light.
        /// </summary>
        [DefaultValue("point")]
        [Property("type")]
        public LightType Type { get; set; } = LightType.Point;

        #endregion
    }
}
