namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using API;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>color</c> command.
    /// </summary>
    [Command("COLOR", "TAG")]
    public class ColorCommand : CommandBase, ITaggedCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorCommand"/> class.
        /// </summary>
        public ColorCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public ColorCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, properties)
        {
            //            this.Tint = args.ElementAtOrDefault(0, String.Empty)
            //                            .Equals("TINT", StringComparison.InvariantCultureIgnoreCase);

            //            this.Color = args.ElementAtOrDefault(this.Tint.ToInt32(), String.Empty);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name value.
        /// </summary>
        [Parameter(1, "COLOR", typeof(Color))]
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Gets or sets the object tag.
        /// </summary>
        [Property("TAG", "")]
        public string Tag { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this color is a tint.
        /// </summary>
        [Parameter(0, "TINT", typeof(bool),
            DefaultValue  = false,
            Optional      = true,
            ParameterType = ParameterType.Flag)]
        public bool Tint { get; set; } = false;

        #endregion
    }
}
