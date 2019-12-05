namespace VirtualParadise.Scene.Serialization.Commands.Parsing
{
    #region Using Directives

    using System;

    #endregion

    /// <summary>
    /// Represents the base class for command parsers.
    /// </summary>
    public abstract class CommandParser<TCommand> : CommandParser
        where TCommand : CommandBase
    {
        #region Methods

        /// <summary>
        /// Parses the command.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Returns a <see cref="TCommand"/>.</returns>
        public new virtual TCommand Parse(string input)
        {
            TCommand command = Activator.CreateInstance<TCommand>();
            return command;
        }

        #endregion
    }
}
