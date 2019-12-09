namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="MoveCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class MoveCommandParser : CommandParser<MoveCommand>
    {
        /// <summary>
        /// Parses the <c>move</c> command and returns a <see cref="MoveCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="MoveCommand"/>.</returns>
        public override MoveCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(MoveCommand), input) as MoveCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
