namespace VirtualParadise.Scene.Serialization.Commands.Parsing
{
    #region Using Directives

    using System;

    #endregion

    /// <summary>
    /// Represents the base class for command parsers.
    /// </summary>
    public abstract class CommandParser
    {
        #region Methods

        /// <summary>
        /// Parses the command.
        /// </summary>
        /// <param name="input">The input.</param>
        public virtual CommandBase Parse(string input)
        {
            CommandBase command = Activator.CreateInstance(typeof(CommandBase)) as CommandBase;
            return command;
        }

        #endregion
    }
}
