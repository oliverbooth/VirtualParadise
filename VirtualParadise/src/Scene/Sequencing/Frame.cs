namespace VirtualParadise.Scene.Sequencing
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using Serialization.Commands;

    #endregion

    /// <summary>
    /// Represents a sequence frame.
    /// </summary>
    public class Frame
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Frame"/> class.
        /// </summary>
        /// <param name="sequence">The container <see cref="Sequencing.Sequence"/>.</param>
        internal Frame(Sequence sequence)
        {
            this.Sequence = sequence;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the commands in this frame.
        /// </summary>
        public List<Command> Commands { get; set; } = new List<Command>();

        /// <summary>
        /// Gets or sets the execution delay since the previous frame.
        /// </summary>
        public TimeSpan Delay { get; set; }

        /// <summary>
        /// Gets the sequence that this frame is in.
        /// </summary>
        public Sequence Sequence { get; }

        #endregion
    }
}
