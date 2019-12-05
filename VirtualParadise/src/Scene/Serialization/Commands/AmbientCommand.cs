namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using API;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>ambient</c> command.
    /// </summary>
    [Command("AMBIENT", "TAG")]
    public class AmbientCommand : CommandBase, ITaggedCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AmbientCommand"/> class.
        /// </summary>
        public AmbientCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AmbientCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public AmbientCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, properties)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the intensity.
        /// </summary>
        [Parameter(0, "INTENSITY", typeof(double),
            DefaultValue = 1.0)]
        public double Intensity { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the object tag.
        /// </summary>
        [Property("TAG", "")]
        public string Tag { get; set; } = String.Empty;

        #endregion
    }
}
