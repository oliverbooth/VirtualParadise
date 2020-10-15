namespace VirtualParadise.Scene.Serialization.Commands
{
    using System;
    using Parsers;
    using Parsing;

    /// <summary>
    /// Represents a class which serializes the <c>name</c> command.
    /// </summary>
    [Command("name", typeof(CommandDefaultParser<NameCommand>))]
    public class NameCommand : Command
    {
        /// <summary>
        /// Gets or sets the name value.
        /// </summary>
        [Parameter(0, "name")]
        public string Name { get; set; } = String.Empty;
    }
}
