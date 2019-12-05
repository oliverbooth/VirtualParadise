namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>name</c> command.
    /// </summary>
    [Command("NAME")]
    public class NameCommand : CommandBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NameCommand"/> class.
        /// </summary>
        public NameCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NameCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public NameCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, properties)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name value.
        /// </summary>
        [Parameter(0, "NAME", typeof(string))]
        public string Name { get; set; } = "";

        #endregion
    }
}
