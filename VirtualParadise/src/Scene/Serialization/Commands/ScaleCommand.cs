namespace VirtualParadise.Scene.Serialization.Commands
{
    using System;
    using System.ComponentModel;
    using System.Text;
    using Parsers;
    using Parsing;

    /// <summary>
    /// Represents a class which serializes the <c>scale</c> command.
    /// </summary>
    [Command("scale", typeof(ScaleCommandParser))]
    public class ScaleCommand : Command
    {
        /// <summary>
        /// Gets or sets the X-axis scale.
        /// </summary>
        [DefaultValue(1.0)]
        [Parameter(0, "x")]
        public double X { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the Y-axis scale.
        /// </summary>
        [DefaultValue(1.0)]
        [Parameter(1, "y",
            Optional = true)]
        public double Y { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the Z-axis scale.
        /// </summary>
        [DefaultValue(1.0)]
        [Parameter(2, "z",
            Optional = true)]
        public double Z { get; set; } = 1.0;

        /// <inheritdoc />
        public override string ToString()
        {
            StringBuilder builder    = new StringBuilder();
            string        properties = this.GetPropertiesString();

            builder.Append(this.CommandName.ToLowerInvariant()).Append(' ');

            if (Math.Abs(this.X - this.Y) < Double.Epsilon && Math.Abs(this.X - this.Z) < Double.Epsilon)
            {
                builder.Append(this.X);
            }
            else
            {
                builder.Append(this.X).Append(' ')
                       .Append(this.Y).Append(' ')
                       .Append(this.Z);
            }

            builder.Append(' ').Append(properties);

            if (!String.IsNullOrWhiteSpace(properties))
            {
                builder.Append(' ');
            }

            builder.Append(this.GetFlagsString());

            return builder.ToString().Trim();
        }
    }
}
