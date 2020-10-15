namespace VirtualParadise.Scene.Serialization.Commands
{
    using System;
    using System.ComponentModel;
    using Parsers;
    using Parsing;

    /// <summary>
    /// Represents a class which serializes the <c>ambient</c> command.
    /// </summary>
    [Command("ambient", typeof(CommandDefaultParser<AmbientCommand>))]
    public class AmbientCommand : Command, ITaggedCommand
    {
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
    }
}
