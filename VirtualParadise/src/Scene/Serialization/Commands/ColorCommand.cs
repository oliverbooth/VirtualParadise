namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.ComponentModel;
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
        public bool Tint { get; set; } = false;

        #endregion
    }
}
