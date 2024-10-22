﻿namespace VirtualParadise.Scene.Serialization.Triggers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Commands;

    /// <summary>
    /// Represents the base class for all triggers.
    /// </summary>
    public abstract class Trigger
    {
        /// <summary>
        /// Backing field for <see cref="Commands"/>.
        /// </summary>
        private readonly List<Command> commands = new List<Command>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Trigger"/> class.
        /// </summary>
        [SuppressMessage("Globalization",
            "CA1308:Normalize strings to uppercase",
            Justification = "Lower case string necessary")]
        protected Trigger()
        {
            this.TriggerName = this.GetType().GetCustomAttribute<TriggerAttribute>()?.Name ?? String.Empty;
        }

        /// <summary>
        /// Gets the commands in this trigger.
        /// </summary>
        public IEnumerable<Command> Commands => this.commands.AsReadOnly();

        /// <summary>
        /// Gets the trigger name.
        /// </summary>
        public string TriggerName { get; }

        /// <summary>
        /// Adds commands to the trigger.
        /// </summary>
        /// <param name="command">The commands to add.</param>
        public void AddCommand(params Command[] command)
        {
            this.commands.AddRange(command);
        }

        /// <summary>
        /// Gets the first command of the specified type.
        /// </summary>
        /// <typeparam name="T">A <see cref="Command"/> derived type.</typeparam>
        public T GetCommandOfType<T>() where T : Command
        {
            return this.GetCommandsOfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Gets all commands of the specified type.
        /// </summary>
        /// <typeparam name="T">A <see cref="Command"/> derived type.</typeparam>
        public IEnumerable<T> GetCommandsOfType<T>() where T : Command
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

            switch (format) {
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
    }
}
