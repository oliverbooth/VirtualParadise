namespace VirtualParadise.Scene.Serialization.Commands
{
    using System.Text;
    using API;
    using Parsers;
    using Parsing;

    /// <summary>
    /// Represents a class which serializes the <c>teleport</c> command.
    /// </summary>
    [Command("teleport", typeof(TeleportCommandParser))]
    public class TeleportCommand : Command
    {
        /// <summary>
        /// Gets or sets the coordinates.
        /// </summary>
        public Coordinates Coordinates { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(base.ToString());

            builder.Append(" ")
                   .Append(this.Coordinates.ToString());

            return builder.ToString();
        }
    }
}
