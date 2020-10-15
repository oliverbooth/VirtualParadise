namespace VirtualParadise.Scene.Serialization.Commands
{
    using Parsers;
    using Parsing;

    /// <summary>
    /// Represents a class which serializes the <c>astop</c> command.
    /// </summary>
    [Command("astop", typeof(CommandDefaultParser<AstopCommand>))]
    public class AstopCommand : Command
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Parameter(0, "name")]
        public string Name { get; set; }
    }
}
