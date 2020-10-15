namespace VirtualParadise.Scene.Sequencing
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Serialization;
    using Serialization.Commands;
    using Serialization.Triggers;

    /// <summary>
    /// Represents an Astart, Animate, Adone sequence.
    /// </summary>
    public class Sequence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sequence"/> class.
        /// </summary>
        /// <param name="name">The sequence name.</param>
        public Sequence(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the name of this sequence.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of frames in this sequence.
        /// </summary>
        public List<Frame> Frames { get; set; } = new List<Frame>();

        /// <summary>
        /// Gets or sets a value whether this sequence loops.
        /// </summary>
        public bool IsLooping { get; set; }

        public IEnumerable<Action> ToActions()
        {
            List<Action> actions = new List<Action>();

            foreach (Frame frame in this.Frames) {
                int           index         = this.Frames.IndexOf(frame);
                bool          loopNow       = this.IsLooping && index == this.Frames.Count - 1;
                ActionBuilder actionBuilder = new ActionBuilder();
                CreateTrigger create        = new CreateTrigger();
                AdoneTrigger  adone         = new AdoneTrigger();

                double frameDelay = frame.Delay.TotalMilliseconds;
                double delay      = frameDelay + this.Frames.Take(index).Sum(f => f.Delay.TotalMilliseconds);

                create.AddCommand(new NameCommand {Name = this.Name});
                create.AddCommand(new AnimateCommand {
                    Name       = "me",
                    Animation  = ".",
                    FrameCount = 1,
                    ImageCount = 1,
                    FrameDelay = (int) (delay)
                });

                adone.AddCommand(frame.Commands.ToArray());

                if (loopNow) {
                    // re-fire first frame on final frame completion
                    adone.AddCommand(new AstartCommand {Name = this.Name});
                }

                actionBuilder.AddTrigger(create).AddTrigger(adone);
                actions.Add(actionBuilder.Build());
            }

            return actions;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Action action in this.ToActions()) {
                stringBuilder.Append($"{action}\n\n");
            }

            return stringBuilder.ToString();
        }
    }
}
