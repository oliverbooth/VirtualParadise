namespace VirtualParadise.Scene.Sequencing
{
    using System;
    using System.Collections.Generic;
    using Serialization.Commands;
    using Serialization.Fluent;

    public class SequenceBuilder
    {
        /// <summary>
        /// The current <see cref="Sequence"/> instance.
        /// </summary>
        private readonly Sequence sequence;

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceBuilder"/> class.
        /// </summary>
        /// <param name="name">The sequence name.</param>
        public SequenceBuilder(string name, bool loop = false)
        {
            this.sequence = new Sequence(name) {IsLooping = loop};
        }

        public SequenceBuilder Append(FluentVP vp, double delay)
        {
            return this.Append(vp, TimeSpan.FromMilliseconds(delay));
        }

        public SequenceBuilder Append(FluentVP vp, TimeSpan delay)
        {
            IEnumerable<Command> commands = vp.actionBuilder.Build()?.Adone?.Commands;
            if (commands is null) {
                return this;
            }

            Frame frame = new Frame(this.sequence) {Delay = delay};

            foreach (Command command in commands) {
                frame.Commands.Add(command);
            }

            this.sequence.Frames.Add(frame);
            return this;
        }

        public SequenceBuilder Append(Frame frame)
        {
            this.sequence.Frames.Add(frame);
            return this;
        }

        public Sequence Build()
        {
            return this.sequence;
        }
    }
}
