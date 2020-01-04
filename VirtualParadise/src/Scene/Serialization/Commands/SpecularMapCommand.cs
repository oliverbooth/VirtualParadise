namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using Parsers;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>specularmap</c> command.
    /// </summary>
    [Command("specularmap", typeof(CommandDefaultParser<SpecularMapCommand>))]
    public class SpecularMapCommand : Command, ITaggedCommand
    {
        #region Properties

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
        [DefaultValue("")]
        [Parameter(0, "texture")]
        public string Texture { get; set; } = String.Empty;

        #endregion
    }
}
