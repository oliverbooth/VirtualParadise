namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>camera</c> command.
    /// </summary>
    [Command("CAMERA", "LOCATION", "TARGET")]
    public class CameraCommand : CommandBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraCommand"/> class.
        /// </summary>
        public CameraCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public CameraCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, properties)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the camera location.
        /// </summary>
        [Property("LOCATION", "")]
        public string Location { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the camera target.
        /// </summary>
        [Property("TARGET", "")]
        public string Target { get; set; } = String.Empty;

        #endregion
    }
}
