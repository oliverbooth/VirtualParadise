namespace VirtualParadise.Scene.Serialization
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
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
