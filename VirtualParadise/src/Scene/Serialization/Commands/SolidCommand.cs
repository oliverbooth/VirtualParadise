namespace VirtualParadise.Scene.Serialization.Commands
{
    using System.ComponentModel;
    using Parsers;
    using Parsing;

    /// <summary>
    /// Represents a class which serializes the <c>solid</c> command.
    /// </summary>
    [Command("solid", typeof(SolidCommandParser))]
    public class SolidCommand : Command
    {
        /// <summary>
        /// Gets or sets the solid value.
        /// </summary>
        [DefaultValue(true)]
        [Parameter(0, "solid")]
        public bool IsSolid { get; set; } = true;
    }
}
