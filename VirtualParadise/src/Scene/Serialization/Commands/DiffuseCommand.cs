namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using Parsers;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>diffuse</c> command.
    /// </summary>
    [Command("diffuse", typeof(DiffuseCommandParser))]
    public class DiffuseCommand : CommandBase, ITaggedCommand
    {
        #region Properties

        /// <summary>
        /// Gets or sets the object tag.
        /// </summary>
        [DefaultValue("")]
        [Property("tag")]
        public string Tag { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the intensity.
        /// </summary>
        [DefaultValue(0.5)]
        [Parameter(0, "intensity")]
        public double Intensity { get; set; } = 0.5;

        #endregion
    }
}
