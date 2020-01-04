namespace VirtualParadise.Scene.Serialization
{
    #region Using Directives

    using System;
    using System.Runtime.Serialization;
    using System.Security;
    using Commands;
    using Triggers;

    #endregion

    /// <summary>
    /// Represents a mutable action.
    /// This class cannot be inherited.
    /// </summary>
    [Serializable]
    public sealed class ActionBuilder : ISerializable
    {
        #region Fields

        private readonly Action action = new Action();
        private TriggerBase currentTrigger;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionBuilder"/> class.
        /// </summary>
        public ActionBuilder()
        {
        }

        private ActionBuilder(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : this()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a command to the builder by adding it to the current trigger.
        /// </summary>
        /// <typeparam name="T">A <see cref="Command"/> derived type.</typeparam>
        /// <param name="command">The command to add.</param>
        /// <returns>Returns the current instance of <see cref="Command"/>.</returns>
        public ActionBuilder AddCommand<T>(T command) where T : Command
        {
            this.currentTrigger?.AddCommand(command);
            return this;
        }

        /// <summary>
        /// Adds a trigger to the builder by adding it to the current action.
        /// </summary>
        /// <typeparam name="T">A <see cref="TriggerBase"/> derived type.</typeparam>
        /// <param name="trigger">The trigger to add.</param>
        /// <returns>Returns the current instance of <see cref="ActionBuilder"/>.</returns>
        public ActionBuilder AddTrigger<T>(T trigger) where T : TriggerBase
        {
            this.action.AddTrigger(trigger);
            this.currentTrigger = trigger;
            return this;
        }

        /// <summary>
        /// Returns the serialized action.
        /// </summary>
        public Action Build()
        {
            return this.action;
        }

        /// <inheritdoc />
        [SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue("m_StringValue", this.action);
        }

        #endregion
    }
}
