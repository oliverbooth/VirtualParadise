namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using Parsers;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>camera</c> command.
    /// </summary>
    [Command("camera", typeof(CommandDefaultParser<CameraCommand>))]
    public class CameraCommand : Command
    {
        #region Properties

        /// <summary>
        /// Gets or sets the camera location.
        /// </summary>
        [DefaultValue("")]
        [Property("location")]
        public string Location { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the camera target.
        /// </summary>
        [DefaultValue("")]
        [Property("target")]
        public string Target { get; set; } = String.Empty;

        #endregion
    }
}
