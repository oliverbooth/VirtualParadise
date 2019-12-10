namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System.ComponentModel;
    using Parsers;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>visible</c> command.
    /// </summary>
    [Command("visible", typeof(VisibleCommandParser))]
    public class VisibleCommand : CommandBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the radius of influence.
        /// </summary>
        [DefaultValue(-1.0)]
        [Property("radius")]
        public double Radius { get; set; } = -1.0;

        /// <summary>
        /// Gets or sets the visible value.
        /// </summary>
        [DefaultValue(true)]
        [Parameter(0, "visible")]
        public bool IsVisible { get; set; } = true;

        #endregion
    }
}
