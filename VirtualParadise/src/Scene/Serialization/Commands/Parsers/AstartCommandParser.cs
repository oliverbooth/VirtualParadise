namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="AstartCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class AstartCommandParser : CommandParser<AstartCommand>
    {
        /// <summary>
        /// Parses the <c>astart</c> command and returns a <see cref="AstartCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="AstartCommand"/>.</returns>
        public override AstartCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(AstartCommand), input) as AstartCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
