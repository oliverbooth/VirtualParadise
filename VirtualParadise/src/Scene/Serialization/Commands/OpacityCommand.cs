namespace VirtualParadise.Scene.Serialization.Commands
{
    using System.ComponentModel;
    using Parsers;
    using Parsing;

    /// <summary>
    /// Represents a class which serializes the <c>opacity</c> command.
    /// </summary>
    [Command("opacity", typeof(CommandDefaultParser<OpacityCommand>))]
    public class OpacityCommand : Command
    {
        /// <summary>
        /// Gets or sets the opacity value.
        /// </summary>
        [DefaultValue(1.0)]
        [Parameter(0, "opacity")]
        public double Value { get; set; } = 1.0;
    }
}
