namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System.ComponentModel;
    using Parsers;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>sound</c> command.
    /// </summary>
    [Command("sound", typeof(CommandDefaultParser<SoundCommand>))]
    public class SoundCommand : Command
    {
        #region Properties

        /// <summary>
        /// Gets or sets the file name or URL of the sound effect.
        /// </summary>
        [DefaultValue("")]
        [Parameter(0, "url")]
        public string FileName { get; set; } = "";

        /// <summary>
        /// Gets or sets the name of the object that plays the left audio channel.
        /// </summary>
        [DefaultValue("")]
        [Property("leftspk")]
        public string LeftSpeaker { get; set; } = "";

        /// <summary>
        /// Gets or sets the radius that this sound can be heard.
        /// </summary>
        [DefaultValue(50.0)]
        [Property("radius")]
        public double Radius { get; set; } = 50.0;

        /// <summary>
        /// Gets or sets the name of the object that plays the right audio channel.
        /// </summary>
        [DefaultValue("")]
        [Property("rightspk")]
        public string RightSpeaker { get; set; } = "";

        /// <summary>
        /// Gets or sets the volume of this sound, between 0.0 and 1.0.
        /// </summary>
        [DefaultValue(1.0)]
        [Property("volume")]
        public double Volume { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets a value indicating whether this sound loops.
        /// </summary>
        public bool IsLooping { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether this sound doesn't loop.
        /// </summary>
        [Flag("noloop")]
        private bool NoLoop
        {
            get => !this.IsLooping;
            set => this.IsLooping = !value;
        }

        #endregion
    }
}
