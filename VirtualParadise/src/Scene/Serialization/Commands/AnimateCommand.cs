﻿namespace VirtualParadise.Scene.Serialization.Commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using Parsers;
    using Parsing;

    /// <summary>
    /// Represents a class which serializes the <c>animate</c> command.
    /// </summary>
    [Command("animate", typeof(AnimateCommandParser))]
    public class AnimateCommand : Command, ITaggedCommand
    {
        /// <summary>
        /// Gets or sets the animation name.
        /// </summary>
        [Parameter(1, "animation")]
        public string Animation { get; set; }

        /// <summary>
        /// Gets or sets the frame count.
        /// </summary>
        [Parameter(3, "framecount")]
        public int FrameCount { get; set; }

        /// <summary>
        /// Gets or sets the delay, in milliseconds, between frames.
        /// </summary>
        [Parameter(4, "framedelay")]
        public int FrameDelay { get; set; }

        /// <summary>
        /// Gets or sets the frame list.
        /// </summary>
        public IEnumerable<int> FrameList { get; set; }

        /// <summary>
        /// Gets or sets the image count.
        /// </summary>
        [Parameter(2, "imagecount")]
        public int ImageCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this animation is masked.
        /// </summary>
        [Flag("mask")]
        public bool IsMask { get; set; } = false;

        /// <summary>
        /// Gets or sets the target object to be animated.
        /// </summary>
        [Parameter(0, "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the object tag.
        /// </summary>
        [DefaultValue("")]
        [Property("tag")]
        public string Tag { get; set; } = String.Empty;

        /// <inheritdoc />
        /// <remarks>The <c>animate</c> command has a variable argument count (thanks to <see cref="FrameList"/>), and
        /// so its <see cref="ToString"/> implementation differs. Results may be inconsistent with other
        /// commands.</remarks>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            string frameList = String.Join(" ", this.FrameList ?? Array.Empty<int>());

            builder.Append(this.CommandName.ToLowerInvariant())
                   .Append(String.IsNullOrWhiteSpace(this.Tag) ? String.Empty : $" tag={this.Tag}")
                   .Append(this.IsMask ? " mask" : String.Empty)
                   .Append(' ').Append(this.Name)
                   .Append(' ').Append(this.Animation)
                   .Append(' ').Append(this.ImageCount)
                   .Append(' ').Append(this.FrameCount)
                   .Append(' ').Append(this.FrameDelay)
                   .Append(String.IsNullOrWhiteSpace(frameList) ? String.Empty : $" {frameList}")
                   .Append(this.GetFlagsString("mask"));

            return builder.ToString();
        }
    }
}
