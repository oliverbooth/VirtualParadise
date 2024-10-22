﻿namespace VirtualParadise.Scene.Serialization.Commands
{
    using System.ComponentModel;
    using Parsers;
    using Parsing;

    /// <summary>
    /// Represents a class which serializes the <c>noise</c> command.
    /// </summary>
    [Command("noise", typeof(CommandDefaultParser<NoiseCommand>))]
    public class NoiseCommand : Command
    {
        /// <summary>
        /// Gets or sets the file name or URL of the noise.
        /// </summary>
        [DefaultValue("")]
        [Parameter(0, "url")]
        public string FileName { get; set; } = "";

        /// <summary>
        /// Gets or sets a value indicating whether this noise loops.
        /// </summary>
        [Flag("loop")]
        public bool IsLooping { get; set; } = false;

        /// <summary>
        /// Gets or sets the volume of this sound, between 0.0 and 1.0.
        /// </summary>
        [DefaultValue(1.0)]
        [Property("volume")]
        public double Volume { get; set; } = 1.0;
    }
}
