namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using API;
    using Parsing;
    using X10D;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>animate</c> command.
    /// </summary>
    [Command("ANIMATE", "TAG")]
    public class AnimateCommand : CommandBase, ITaggedCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimateCommand"/> class.
        /// </summary>
        public AnimateCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimateCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public AnimateCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, properties)
        {
            List<string> list = args.ToList();
            if (list[0].Equals("MASK", StringComparison.InvariantCultureIgnoreCase))
            {
                list.RemoveAt(0);
            }

            this.FrameList = list.Skip(5).Take(this.FrameCount).Select(s => s.To<int>()).ToArray();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the animation name.
        /// </summary>
        [Parameter(2, "ANIMATION", typeof(string))]
        public string Animation { get; set; }

        /// <summary>
        /// Gets or sets the frame count.
        /// </summary>
        [Parameter(4, "FRAMECOUNT", typeof(int))]
        public int FrameCount { get; set; }

        /// <summary>
        /// Gets or sets the delay, in milliseconds, between frames.
        /// </summary>
        [Parameter(5, "FRAMEDELAY", typeof(int))]
        public int FrameDelay { get; set; }

        /// <summary>
        /// Gets or sets the frame list.
        /// </summary>
        public IEnumerable<int> FrameList { get; set; }

        /// <summary>
        /// Gets or sets the image count.
        /// </summary>
        [Parameter(3, "IMAGECOUNT", typeof(int))]
        public int ImageCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this animation is masked.
        /// </summary>
        [Parameter(0, "MASK", typeof(bool),
            DefaultValue  = false,
            Optional      = true,
            ParameterType = ParameterType.Flag)]
        public bool Mask { get; set; } = false;

        /// <summary>
        /// Gets or sets the target object to be animated.
        /// </summary>
        [Parameter(1, "NAME", typeof(string))]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the object tag.
        /// </summary>
        [Property("TAG", "")]
        public string Tag { get; set; } = String.Empty;

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <remarks>The <c>animate</c> command has a variable argument count (thanks to <see cref="FrameList"/>), and
        /// so its <see cref="ToString"/> implementation differs. Results may be inconsistent with other
        /// commands.</remarks>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            string frameList = String.Join(" ", this.FrameList);

            builder.Append(this.CommandName)
                   .Append(String.IsNullOrWhiteSpace(this.Tag) ? String.Empty : $" tag={this.Tag} ")
                   .Append(this.Mask ? " mask" : String.Empty)
                   .Append(' ').Append(this.Name)
                   .Append(' ').Append(this.Animation)
                   .Append(' ').Append(this.ImageCount)
                   .Append(' ').Append(this.FrameCount)
                   .Append(' ').Append(this.FrameDelay)
                   .Append(String.IsNullOrWhiteSpace(frameList) ? String.Empty : $" {frameList}")
                   .Append(this.IsGlobal ? " global" : String.Empty);

            return builder.ToString();
        }

        #endregion
    }
}
