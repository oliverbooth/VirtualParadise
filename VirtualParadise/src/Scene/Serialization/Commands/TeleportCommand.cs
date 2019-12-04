namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Text;
    using API;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>teleport</c> command.
    /// </summary>
    [Command("TELEPORT")]
    public class TeleportCommand : CommandBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TeleportCommand"/> class.
        /// </summary>
        public TeleportCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TeleportCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties. For <c>teleport</c>, this is ignored.</param>
        public TeleportCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, new Dictionary<string, object>())
        {
            string input = String.Join(" ", args);
            this.Coordinates = Coordinates.Parse(input);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the coordinates.
        /// </summary>
        public Coordinates Coordinates { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(base.ToString());

            builder.Append(" ")
                   .Append(this.Coordinates.ToString());

            return builder.ToString();
        }

        #endregion
    }
}
