namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using System.Text;
    using API;
    using Parsers;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>color</c> command.
    /// </summary>
    [Command("color", typeof(ColorCommandParser))]
    public class ColorCommand : CommandBase, ITaggedCommand
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name value.
        /// </summary>
        [DefaultValue("")]
        [Parameter(1, "color")]
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Gets or sets the object tag.
        /// </summary>
        [DefaultValue("")]
        [Property("tag")]
        public string Tag { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this color is a tint.
        /// </summary>
        [Flag("tint")]
        public bool IsTint { get; set; } = false;

        #endregion

        #region Methods

        /// <inheritdoc />
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(this.CommandName.ToLowerInvariant()).Append(' ');
            if (this.IsTint)
            {
                builder.Append("tint ");
            }

            builder.Append(this.Color).Append(' ');
            builder.Append(this.GetPropertiesString());
            builder.Append(this.GetFlagsString("tint"));

            return builder.ToString().Trim();
        }

        #endregion
    }
}
