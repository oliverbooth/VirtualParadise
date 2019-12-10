namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="AstopCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class AstopCommandParser : CommandParser<AstopCommand>
    {
        /// <summary>
        /// Parses the <c>astop</c> command and returns a <see cref="AstopCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="AstopCommand"/>.</returns>
        public override AstopCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(AstopCommand), input) as AstopCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
