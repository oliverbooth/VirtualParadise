namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using API;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>sign</c> command.
    /// </summary>
    [Command("SIGN", "COLOR", "BCOLOR", "MARGIN", "HMARGIN", "VMARGIN", "ALIGN")]
    public class SignCommand : CommandBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SignCommand"/> class.
        /// </summary>
        public SignCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public SignCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, properties)
        {
            string argString = String.Join(" ", args);
            int firstQuote = argString.IndexOf('"');
            int lastQuote = argString.IndexOf('"', firstQuote + 1);
            this.Text = argString.Substring(firstQuote + 1, lastQuote - firstQuote - 1);

            StringBuilder argBuilder = new StringBuilder();
            argBuilder.Append(argString.Substring(0, firstQuote));
            argBuilder.Append(argString.Substring(lastQuote + 1));

            args = Regex.Split(argBuilder.ToString().ToUpperInvariant(), "\\s");
            this.Shadow = args.Contains("SHADOW");
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text alignment.
        /// </summary>
        [Property("ALIGN", TextAlignment.Center)]
        public TextAlignment Alignment { get; set; } = TextAlignment.Center;

        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        [Property("BCOLOR", "0000C0")]
        public Color BackColor { get; set; } = new Color(0x00, 0x00, 0xC0);

        /// <summary>
        /// Gets or sets the foreground color.
        /// </summary>
        [Property("COLOR", "FFFFFF")]
        public Color ForeColor { get; set; } = Color.White;

        /// <summary>
        /// Gets or sets the horizontal margin.
        /// </summary>
        [Property("HMARGIN", 0.0)]
        public double HorizontalMargin { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets the overall margin.
        /// </summary>
        [Property("MARGIN", 0.0)]
        public double Margin { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets a value indicating whether this sound doesn't loop.
        /// </summary>
        [Parameter(1, "SHADOW", typeof(bool),
            DefaultValue = false,
            Optional = true,
            ParameterType = ParameterType.Flag)]
        public bool Shadow { get; set; }

        /// <summary>
        /// Gets or sets the sign text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the vertical margin.
        /// </summary>
        [Property("VMARGIN", 0.0)]
        public double VerticalMargin { get; set; } = 0.0;

        #endregion

        #region Methods

        /// <inheritdoc />
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(base.ToString());
            builder.Append(" \"")
                   .Append(this.Text)
                   .Append('"');

            return builder.ToString();
        }

        #endregion
    }
}
