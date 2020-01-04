namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Text;
    using Parsers;
    using Parsing;
    using VpNet;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>shear</c> command.
    /// </summary>
    [Command("shear", typeof(ShearCommandParser))]
    public class ShearCommand : Command
    {
        #region Properties

        /// <summary>
        /// Gets or sets the positive shear.
        /// </summary>
        public Vector3 Positive { get; set; } = new Vector3(0.0);

        /// <summary>
        /// Gets or sets the negative shear.
        /// </summary>
        public Vector3 Negative { get; set; } = new Vector3(0.0);

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <remarks>The <c>shear</c> command has an enumerable argument set, and so its <see cref="ToString"/>
        /// implementation differs. Results may be inconsistent with other commands.</remarks>
        public override string ToString()
        {
            StringBuilder builder    = new StringBuilder();
            string        properties = this.GetPropertiesString();

            builder.Append(this.CommandName.ToLowerInvariant()).Append(' ')
                   .Append(this.Positive.Z).Append(' ')
                   .Append(this.Positive.X).Append(' ')
                   .Append(this.Positive.Y).Append(' ')
                   .Append(this.Negative.Y).Append(' ')
                   .Append(this.Negative.Z).Append(' ')
                   .Append(this.Negative.X).Append(' ')
                   .Append(properties);

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
