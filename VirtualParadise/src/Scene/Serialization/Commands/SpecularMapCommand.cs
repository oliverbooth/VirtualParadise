namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>specularmap</c> command.
    /// </summary>
    [Command("SPECULARMAP", "MASK", "TAG")]
    public class SpecularMapCommand : CommandBase, ITaggedCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecularMapCommand"/> class.
        /// </summary>
        public SpecularMapCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecularMapCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public SpecularMapCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
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
