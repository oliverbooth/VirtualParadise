namespace VirtualParadise.Scene.Serialization
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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
        /// Represents an empty action.
        /// </summary>
        public static readonly Action Empty = new Action();

        /// <summary>
        /// Backing field for <see cref="Triggers"/>.
        /// </summary>
        private readonly List<Trigger> triggers = new List<Trigger>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Action"/> class.
        /// </summary>
        internal Action() { }

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
                IEnumerable<Command> commands = this.Triggers.OfType<AdoneTrigger>().SelectMany(t => t.Commands);
                foreach (Command command in commands) {
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
                IEnumerable<Command> commands = this.Triggers.OfType<ActivateTrigger>().SelectMany(t => t.Commands);
                foreach (Command command in commands) {
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
                IEnumerable<Command> commands = this.Triggers.OfType<BumpTrigger>().SelectMany(t => t.Commands);
                foreach (Command command in commands) {
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
                IEnumerable<Command> commands = this.Triggers.OfType<BumpEndTrigger>().SelectMany(t => t.Commands);
                foreach (Command command in commands) {
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
                IEnumerable<Command> commands = this.Triggers.OfType<CreateTrigger>().SelectMany(t => t.Commands);
                foreach (Command command in commands) {
                    trigger.AddCommand(command);
                }

                return trigger;
            }
        }

        /// <summary>
        /// Gets the triggers.
        /// </summary>
        public IEnumerable<Trigger> Triggers =>
            this.triggers.AsReadOnly();

        #endregion

        #region Methods

        /// <summary>
        /// Parses an action string.
        /// </summary>
        /// <param name="input">The action string.</param>
        /// <param name="throwOnError">Optional. Whether or not the parser should throw an exception if an operation
        /// failed. Defaults to <see langword="false"/>.</param>
        /// <returns>Returns an instance of <see cref="Action"/>.</returns>
        public static Action Parse(string input, bool throwOnError = false)
        {
            return ActionParser.ParseAsync(input, throwOnError)
                               .ConfigureAwait(false)
                               .GetAwaiter().GetResult();
        }

        /// <summary>
        /// Asynchronously parses an action string.
        /// </summary>
        /// <param name="input">The action string.</param>
        /// <param name="throwOnError">Optional. Whether or not the parser should throw an exception if an operation
        /// failed. Defaults to <see langword="false"/>.</param>
        /// <returns>Returns an instance of <see cref="Action"/>.</returns>
        public static async Task<Action> ParseAsync(string input, bool throwOnError = false)
        {
            return await ActionParser.ParseAsync(input, throwOnError)
                                     .ConfigureAwait(false);
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

            switch (format) {
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
        internal void AddTrigger(Trigger trigger)
        {
            this.triggers.Add(trigger);
        }

        #endregion
    }
}
