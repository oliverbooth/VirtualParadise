namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using System.Text;
    using Parsers;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>move</c> command.
    /// </summary>
    [Command("move", typeof(MoveCommandParser))]
    public class MoveCommand : CommandBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the file name or URL of the noise.
        /// </summary>
        [Parameter(0, "x")]
        public double X { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets the file name or URL of the noise.
        /// </summary>
        [DefaultValue(0.0)]
        [Parameter(1, "y",
            Optional = true)]
        public double Y { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets the file name or URL of the noise.
        /// </summary>
        [DefaultValue(0.0)]
        [Parameter(2, "z",
            Optional = true)]
        public double Z { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets a value indicating whether this movement is along the object axis (as opposed to world axis).
        /// </summary>
        [Flag("ltm")]
        public bool IsLocalAxis { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this movement loops.
        /// </summary>
        [Flag("loop")]
        public bool IsLooping { get; set; } = false;

        /// <summary>
        /// Gets or sets the offset - in seconds - to apply to universe time when synchronizing.
        /// </summary>
        [DefaultValue(0.0)]
        [Property("offset")]
        public double Offset { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets a value indicating whether this movement resets after completing half a cycle.
        /// </summary>
        [Flag("reset")]
        public bool ShouldReset { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this movement is smooth.
        /// </summary>
        [Flag("smooth")]
        public bool IsSmooth { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this movement syncs.
        /// </summary>
        [Flag("sync")]
        public bool ShouldSync { get; set; } = false;

        /// <summary>
        /// Gets or sets the duration - in seconds - of half of a cycle.
        /// </summary>
        [DefaultValue(1.0)]
        [Property("time")]
        public double Time { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the time - in seconds - before continuing movement after one half of a cycle.
        /// </summary>
        [DefaultValue(0.0)]
        [Property("wait")]
        public double Wait { get; set; } = 0.0;

        #endregion

        #region Methods

        /// <inheritdoc />
        public override string ToString()
        {
            StringBuilder builder    = new StringBuilder();
            string        properties = this.GetPropertiesString();

            builder.Append(this.CommandName.ToLowerInvariant()).Append(' ');

            builder.Append(this.X).Append(' ');

            if (this.Y > 0.0 || this.Z > 0.0)
            {
                builder.Append(this.Y).Append(' ');

                if (this.Z > 0.0)
                {
                    builder.Append(this.Z).Append(' ');
                }
            }

            builder.Append(properties);

            if (!String.IsNullOrWhiteSpace(properties))
            {
                builder.Append(' ');
            }

            builder.Append(this.GetFlagsString());

            return builder.ToString().Trim();
        }

        #endregion
    }
}
