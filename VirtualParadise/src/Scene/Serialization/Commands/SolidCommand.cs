namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System.ComponentModel;
    using Parsers;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>solid</c> command.
    /// </summary>
    [Command("solid", typeof(SolidCommandParser))]
    public class SolidCommand : CommandBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the solid value.
        /// </summary>
        [DefaultValue(true)]
        [Parameter(0, "solid")]
        public bool IsSolid { get; set; } = true;

        #endregion
    }
}
