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
    /// Represents a class which serializes the <c>sign</c> command.
    /// </summary>
    [Command("sign", typeof(SignCommandParser))]
    public class SignCommand : CommandBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the text alignment.
        /// </summary>
        [DefaultValue(TextAlignment.Center)]
        [Property("align")]
        public TextAlignment Alignment { get; set; } = TextAlignment.Center;

        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        [DefaultValue("0000C0")]
        [Property("bcolor")]
        public Color BackColor { get; set; } = new Color(0x00, 0x00, 0xC0);

        /// <summary>
        /// Gets or sets the foreground color.
        /// </summary>
        [DefaultValue("FFFFFF")]
        [Property("color")]
        public Color ForeColor { get; set; } = Color.White;

        /// <summary>
        /// Gets or sets the horizontal margin.
        /// </summary>
        [DefaultValue(0.0)]
        [Property("hmargin")]
        public double HorizontalMargin { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets the overall margin.
        /// </summary>
        [DefaultValue(0.0)]
        [Property("margin")]
        public double Margin { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets a value indicating whether this sound doesn't loop.
        /// </summary>
        [Flag("shadow")]
        public bool Shadow { get; set; }

        /// <summary>
        /// Gets or sets the sign text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the vertical margin.
        /// </summary>
        [DefaultValue(0.0)]
        [Property("vmargin")]
        public double VerticalMargin { get; set; } = 0.0;

        #endregion

        #region Methods

        /// <inheritdoc />
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(base.ToString());

            if (!String.IsNullOrWhiteSpace(this.Text))
            {
                builder.Append(" \"")
                       .Append(this.Text)
                       .Append('"');
            }

            return builder.ToString();
        }

        #endregion
    }
}
