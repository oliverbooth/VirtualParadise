namespace VirtualParadise.Scene.Serialization.Commands
{
    using System;
    using System.ComponentModel;
    using Parsers;
    using Parsing;

    /// <summary>
    /// Represents a class which serializes the <c>texture</c> command.
    /// </summary>
    [Command("texture", typeof(CommandDefaultParser<TextureCommand>))]
    public class TextureCommand : Command, ITaggedCommand
    {
        /// <summary>
        /// Gets or sets the texture mask.
        /// </summary>
        [DefaultValue("")]
        [Property("mask")]
        public string Mask { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the object tag.
        /// </summary>
        [DefaultValue("")]
        [Property("tag")]
        public string Tag { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the texture value.
        /// </summary>
        [Parameter(0, "texture")]
        public string Texture { get; set; } = String.Empty;
    }
}
