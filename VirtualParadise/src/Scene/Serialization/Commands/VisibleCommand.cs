namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>visible</c> command.
    /// </summary>
    [Command("VISIBLE", "RADIUS")]
    public class VisibleCommand : CommandBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VisibleCommand"/> class.
        /// </summary>
        public VisibleCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VisibleCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public VisibleCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, properties)
        {
            if (args is null || args.Count == 0)
            {
                return;
            }

            if (!Keyword.TryBool(args.ElementAt(0), out bool value))
            {
                this.TargetName = args.ElementAt(0);

                if (Keyword.TryBool(args.ElementAt(1), out value))
                {
                    this.Value = value;
                }
            }
            else
            {
                this.Value = value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the radius of influence.
        /// </summary>
        [Property("RADIUS", -1.0)]
        public double Radius { get; set; } = -1.0;

        /// <summary>
        /// Gets or sets the solid value.
        /// </summary>
        [Parameter(0, "SOLID", typeof(bool),
            DefaultValue = true)]
        public bool Value { get; set; } = true;

        #endregion
    }
}
