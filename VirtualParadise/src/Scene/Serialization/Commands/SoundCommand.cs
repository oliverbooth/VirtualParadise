namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>sound</c> command.
    /// </summary>
    [Command("SOUND", "RADIUS", "VOLUME", "LEFTSPK", "RIGHTSPK")]
    public class SoundCommand : CommandBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundCommand"/> class.
        /// </summary>
        public SoundCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public SoundCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, properties)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the file name or URL of the sound effect.
        /// </summary>
        [Parameter(0, "URL", typeof(string))]
        public string FileName { get; set; } = "";

        /// <summary>
        /// Gets or sets the name of the object that plays the left audio channel.
        /// </summary>
        [Property("LEFTSPK", "")]
        public string LeftSpeaker { get; set; } = "";

        /// <summary>
        /// Gets or sets the radius that this sound can be heard.
        /// </summary>
        [Property("RADIUS", 50.0)]
        public double Radius { get; set; } = 50.0;

        /// <summary>
        /// Gets or sets the name of the object that plays the right audio channel.
        /// </summary>
        [Property("RIGHTSPK", "")]
        public string RightSpeaker { get; set; } = "";

        /// <summary>
        /// Gets or sets the volume of this sound, between 0.0 and 1.0.
        /// </summary>
        [Property("VOLUME", 1.0)]
        public double Volume { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets a value indicating whether this sound loops.
        /// </summary>
        public bool Loop { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether this sound doesn't loop.
        /// </summary>
        [Parameter(1, "NOLOOP", typeof(bool),
            DefaultValue = false,
            Optional = true,
            ParameterType = ParameterType.Flag)]
        private bool NoLoop
        {
            get => !this.Loop;
            set => this.Loop = !value;
        }

        #endregion
    }
}
