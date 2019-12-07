namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using Parsers;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>name</c> command.
    /// </summary>
    [Command("name", typeof(NameCommandParser))]
    public class NameCommand : CommandBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name value.
        /// </summary>
        [Parameter(0, "name")]
        public string Name { get; set; } = String.Empty;

        #endregion
    }
}
