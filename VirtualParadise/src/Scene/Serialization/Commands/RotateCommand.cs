namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using Parsers;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>rotate</c> command.
    /// </summary>
    [Command("rotate", typeof(RotateCommandParser))]
    public class RotateCommand : CommandBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the X axis rotation.
        /// </summary>
        [DefaultValue(0.0)]
        [Parameter(0, "x",
            Optional     = true)]
        public double X { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets the Y axis rotation.
        /// </summary>
        [DefaultValue(0.0)]
        [Parameter(1, "y",
            Optional     = false)]
        public double Y { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets the Z axis rotation.
        /// </summary>
        [DefaultValue(0.0)]
        [Parameter(2, "z",
            Optional     = true)]
        public double Z { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets a value indicating whether this rotation is along the object axis (as opposed to world axis).
        /// </summary>
        [Flag("ltm")]
        public bool LocalAxis { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this rotation loops.
        /// </summary>
        [Flag("loop")]
        public bool Loop { get; set; } = false;

        /// <summary>
        /// Gets or sets the offset - in seconds - to apply to universe time when synchronizing.
        /// </summary>
        [DefaultValue(0.0)]
        [Property("offset")]
        public double Offset { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets a value indicating whether this rotation resets after completing half a cycle.
        /// </summary>
        [Flag("reset")]
        public bool Reset { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this rotation is smooth.
        /// </summary>
        [Flag("smooth")]
        public bool Smooth { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this rotation syncs.
        /// </summary>
        [Flag("sync")]
        public bool Sync { get; set; } = false;

        /// <summary>
        /// Gets or sets the duration - in seconds - of half of a cycle.
        /// </summary>
        [DefaultValue(1.0)]
        [Property("time")]
        public double Time { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the time - in seconds - before continuing rotation after one half of a cycle.
        /// </summary>
        [DefaultValue(0.0)]
        [Property("wait")]
        public double Wait { get; set; } = 0.0;

        #endregion
    }
}
