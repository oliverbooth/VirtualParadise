namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>diffuse</c> command.
    /// </summary>
    [Command("DIFFUSE", "TAG")]
    public class DiffuseCommand : CommandBase, ITaggedCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DiffuseCommand"/> class.
        /// </summary>
        public DiffuseCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiffuseCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public DiffuseCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, properties)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the object tag.
        /// </summary>
        [Property("TAG", "")]
        public string Tag { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the intensity.
        /// </summary>
        [Parameter(0, "INTENSITY", typeof(double),
            DefaultValue = 0.5)]
        public double Intensity { get; set; } = 0.5;

        #endregion
    }
}
