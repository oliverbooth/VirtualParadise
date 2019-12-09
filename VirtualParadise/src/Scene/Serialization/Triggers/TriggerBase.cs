namespace VirtualParadise.Scene.Serialization.Triggers
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Commands;

    #endregion

    /// <summary>
    /// Represents the base class for all triggers.
    /// </summary>
    public abstract class TriggerBase
    {
        #region Fields

        /// <summary>
        /// Backing field for <see cref="Commands"/>.
        /// </summary>
        private readonly List<CommandBase> commands = new List<CommandBase>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerBase"/> class.
        /// </summary>
        [SuppressMessage("Globalization",
            "CA1308:Normalize strings to uppercase",
            Justification = "Lower case string necessary")]
        protected TriggerBase()
        {
            this.TriggerName = this.GetType().GetCustomAttribute<TriggerAttribute>()?.Name ?? String.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the commands in this trigger.
        /// </summary>
        public IEnumerable<CommandBase> Commands => this.commands.AsReadOnly();

        /// <summary>
        /// Gets the trigger name.
        /// </summary>
        public string TriggerName { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the first command of the specified type.
        /// </summary>
        /// <typeparam name="T">A <see cref="CommandBase"/> derived type.</typeparam>
        public T GetCommandOfType<T>() where T : CommandBase
        {
            return this.GetCommandsOfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Gets all commands of the specified type.
        /// </summary>
        /// <typeparam name="T">A <see cref="CommandBase"/> derived type.</typeparam>
        public IEnumerable<T> GetCommandsOfType<T>() where T : CommandBase
        {
            return this.commands.OfType<T>();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.ToString(ActionFormat.None);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        [SuppressMessage("Globalization",
            "CA1308:Normalize strings to uppercase",
            Justification = "String literal wanted")]
        public string ToString(ActionFormat format)
        {
            string join   = ",";
            bool   indent = false;

            switch (format)
            {
                case ActionFormat.Compressed:
                    break;
                case ActionFormat.None:
                    join += " ";
                    break;
                case ActionFormat.Indented:
                    join   = ",\n  ";
                    indent = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, null);
            }

            return
                this.TriggerName.ToLowerInvariant() +
                (indent ? "\n  " : " ")             +
                String.Join(join, this.Commands.Select(c => c.ToString().Trim()));
        }

        /// <summary>
        /// Adds a commands to the trigger.
        /// </summary>
        /// <param name="command">The commands to add.</param>
        internal void AddCommand(params CommandBase[] command)
        {
            this.commands.AddRange(command);
        }

        #endregion
    }
}
