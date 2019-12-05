namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>solid</c> command.
    /// </summary>
    [Command("SOLID")]
    public class SolidCommand : CommandBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SolidCommand"/> class.
        /// </summary>
        public SolidCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolidCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public SolidCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
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
        /// Gets or sets the solid value.
        /// </summary>
        [Parameter(0, "SOLID", typeof(bool),
            DefaultValue = true)]
        public bool Value { get; set; } = true;

        #endregion
    }
}
