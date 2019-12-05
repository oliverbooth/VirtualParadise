namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>texture</c> command.
    /// </summary>
    [Command("TEXTURE", "MASK", "TAG")]
    public class TextureCommand : CommandBase, ITaggedCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureCommand"/> class.
        /// </summary>
        public TextureCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public TextureCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, properties)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the texture mask.
        /// </summary>
        [Property("MASK", "")]
        public string Mask { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the object tag.
        /// </summary>
        [Property("TAG", "")]
        public string Tag { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the texture value.
        /// </summary>
        [Parameter(0, "TEXTURE", typeof(string))]
        public string Texture { get; set; } = String.Empty;

        #endregion
    }
}
