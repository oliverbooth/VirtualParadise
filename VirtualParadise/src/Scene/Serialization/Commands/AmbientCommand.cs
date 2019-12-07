namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using Parsers;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>ambient</c> command.
    /// </summary>
    [Command("ambient", typeof(AmbientCommandParser))]
    public class AmbientCommand : CommandBase, ITaggedCommand
    {
        #region Properties

        /// <summary>
        /// Gets or sets the intensity.
        /// </summary>
        [DefaultValue(1.0)]
        [Parameter(0, "intensity")]
        public double Intensity { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the object tag.
        /// </summary>
        [DefaultValue("")]
        [Property("tag")]
        public string Tag { get; set; } = String.Empty;

        #endregion
    }
}
