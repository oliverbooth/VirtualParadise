namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>framerate</c> command.
    /// </summary>
    [Command("FRAMERATE")]
    public class FrameRateCommand : CommandBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameRateCommand"/> class.
        /// </summary>
        public FrameRateCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameRateCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public FrameRateCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, properties)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the frame rate value.
        /// </summary>
        [Parameter(0, "VALUE", typeof(int),
            DefaultValue = 10)]
        public int Value { get; set; } = 10;

        #endregion
    }
}
