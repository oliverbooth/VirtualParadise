namespace VirtualParadise.Scene.Serialization.Commands
{
    using System.ComponentModel;
    using Parsers;
    using Parsing;

    /// <summary>
    /// Represents a class which serializes the <c>framerate</c> command.
    /// </summary>
    [Command("framerate", typeof(CommandDefaultParser<FrameRateCommand>))]
    public class FrameRateCommand : Command
    {
        /// <summary>
        /// Gets or sets the frame rate value.
        /// </summary>
        [DefaultValue(10)]
        [Parameter(0, "value")]
        public int Value { get; set; } = 10;
    }
}
