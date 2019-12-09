namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using Parsers;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>astop</c> command.
    /// </summary>
    [Command("astop", typeof(AstopCommandParser))]
    public class AstopCommand : CommandBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Parameter(0, "name")]
        public string Name { get; set; }

        #endregion
    }
}
