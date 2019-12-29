namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using Parsers;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>astart</c> command.
    /// </summary>
    [Command("astart", typeof(CommandDefaultParser<AstartCommand>))]
    public class AstartCommand : CommandBase
    {
        #region Properties

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

        #endregion
    }
}
