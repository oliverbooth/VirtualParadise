namespace VirtualParadise.Scene.Serialization.Commands
{
    using Parsers;
    using Parsing;

    /// <summary>
    /// Represents a class which serializes the <c>astart</c> command.
    /// </summary>
    [Command("astart", typeof(CommandDefaultParser<AstartCommand>))]
    public class AstartCommand : Command
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Parameter(0, "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this animation loops.
        /// </summary>
        [Flag("looping")]
        public bool Loop { get; set; } = false;
    }
}
