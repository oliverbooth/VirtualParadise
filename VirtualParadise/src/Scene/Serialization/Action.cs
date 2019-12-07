namespace VirtualParadise.Scene.Serialization
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Commands;
    using Triggers;

    #endregion

    /// <summary>
    /// Represents a serialized action.
    /// </summary>
    public sealed class Action
    {
        #region Fields

        /// <summary>
        /// Backing field for <see cref="Triggers"/>.
        /// </summary>
        private readonly List<TriggerBase> triggers = new List<TriggerBase>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Action"/> class.
        /// </summary>
        internal Action()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets all commands within the <c>adone</c> trigger.
        /// </summary>
        public AdoneTrigger Adone
        {
            get
            {
                AdoneTrigger             trigger  = new AdoneTrigger();
                IEnumerable<CommandBase> commands = this.Triggers.OfType<AdoneTrigger>().SelectMany(t => t.Commands);
                foreach (CommandBase command in commands)
                {
                    trigger.AddCommand(command);
                }

                return trigger;
            }
        }

        /// <summary>
        /// Gets all commands within the <c>activate</c> trigger.
        /// </summary>
        public ActivateTrigger Activate
        {
            get
            {
                ActivateTrigger          trigger  = new ActivateTrigger();
                IEnumerable<CommandBase> commands = this.Triggers.OfType<ActivateTrigger>().SelectMany(t => t.Commands);
                foreach (CommandBase command in commands)
                {
                    trigger.AddCommand(command);
                }

                return trigger;
            }
        }

        /// <summary>
        /// Gets all commands within the <c>bump</c> trigger.
        /// </summary>
        public BumpTrigger Bump
        {
            get
            {
                BumpTrigger              trigger  = new BumpTrigger();
                IEnumerable<CommandBase> commands = this.Triggers.OfType<BumpTrigger>().SelectMany(t => t.Commands);
                foreach (CommandBase command in commands)
                {
                    trigger.AddCommand(command);
                }

                return trigger;
            }
        }

        /// <summary>
        /// Gets all commands within the <c>bumpend</c> trigger.
        /// </summary>
        public BumpEndTrigger BumpEnd
        {
            get
            {
                BumpEndTrigger           trigger  = new BumpEndTrigger();
                IEnumerable<CommandBase> commands = this.Triggers.OfType<BumpEndTrigger>().SelectMany(t => t.Commands);
                foreach (CommandBase command in commands)
                {
                    trigger.AddCommand(command);
                }

                return trigger;
            }
        }

        /// <summary>
        /// Gets all commands within the <c>create</c> trigger.
        /// </summary>
        public CreateTrigger Create
        {
            get
            {
                CreateTrigger            trigger  = new CreateTrigger();
                IEnumerable<CommandBase> commands = this.Triggers.OfType<CreateTrigger>().SelectMany(t => t.Commands);
                foreach (CommandBase command in commands)
                {
                    trigger.AddCommand(command);
                }

                return trigger;
            }
        }

        /// <summary>
        /// Gets the triggers.
        /// </summary>
        public IEnumerable<TriggerBase> Triggers =>
            this.triggers.AsReadOnly();

        #endregion

        #region Methods

        public static Action Parse(string input)
        {
            return ActionParser.Parse(input);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.ToString(ActionFormat.None);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public string ToString(ActionFormat format)
        {
            string join = ";";

            switch (format)
            {
                case ActionFormat.Compressed:
                    break;
                case ActionFormat.None:
                    join += " ";
                    break;
                case ActionFormat.Indented:
                    join = ";\n\n";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, null);
            }

            return String.Join(join, this.Triggers.Select(t => t.ToString(format).Trim()));
        }

        /// <summary>
        /// Adds a trigger to this action.
        /// </summary>
        /// <param name="trigger">The trigger to add.</param>
        internal void AddTrigger(TriggerBase trigger)
        {
            this.triggers.Add(trigger);
        }

        #endregion
    }
}
