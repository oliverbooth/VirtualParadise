namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="OpacityCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class OpacityCommandParser : CommandParser<OpacityCommand>
    {
        /// <summary>
        /// Parses the <c>opacity</c> command and returns a <see cref="OpacityCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="OpacityCommand"/>.</returns>
        public override OpacityCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(OpacityCommand), input) as OpacityCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
