namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>specular</c> command.
    /// </summary>
    [Command("SPECULAR", "TAG")]
    public class SpecularCommand : CommandBase, ITaggedCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecularCommand"/> class.
        /// </summary>
        public SpecularCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecularCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public SpecularCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, properties)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether to apply specular highlights onto alpha transparent objects.
        /// </summary>
        [Parameter(2, "ALPHA", typeof(bool),
            Optional = true,
            DefaultValue = false,
            ParameterType = ParameterType.Flag)]
        public bool Alpha { get; set; } = false;

        /// <summary>
        /// Gets or sets the object tag.
        /// </summary>
        [Property("TAG", "")]
        public string Tag { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the intensity.
        /// </summary>
        [Parameter(0, "INTENSITY", typeof(double),
            DefaultValue = 1.0)]
        public double Intensity { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the shininess.
        /// </summary>
        [Parameter(1, "SHININESS", typeof(double),
            Optional = true,
            DefaultValue = 30.0)]
        public double Shininess { get; set; } = 30.0;

        #endregion
    }
}
