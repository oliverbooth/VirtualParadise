﻿namespace VirtualParadise.Scene.Serialization.Commands
{
    using System;
    using System.ComponentModel;
    using Parsers;
    using Parsing;

    /// <summary>
    /// Represents a class which serializes the <c>normalmap</c> command.
    /// </summary>
    [Command("normalmap", typeof(CommandDefaultParser<NormalMapCommand>))]
    public class NormalMapCommand : Command, ITaggedCommand
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
        [DefaultValue("")]
        [Parameter(0, "texture")]
        public string Texture { get; set; } = String.Empty;
    }
}
