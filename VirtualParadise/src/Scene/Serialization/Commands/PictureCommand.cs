﻿namespace VirtualParadise.Scene.Serialization.Commands
{
    using System;
    using System.ComponentModel;
    using Parsers;
    using Parsing;

    /// <summary>
    /// Represents a class which serializes the <c>picture</c> command.
    /// </summary>
    [Command("picture", typeof(CommandDefaultParser<PictureCommand>))]
    public class PictureCommand : Command, ITaggedCommand
    {
        /// <summary>
        /// Gets or sets the picture value.
        /// </summary>
        [Parameter(0, "picture")]
        public string Picture { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the object tag.
        /// </summary>
        [DefaultValue("")]
        [Property("tag")]
        public string Tag { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the time, in seconds, before updating the picture.
        /// </summary>
        [DefaultValue(0.0)]
        [Property("update")]
        public double Update { get; set; } = 0.0;
    }
}
