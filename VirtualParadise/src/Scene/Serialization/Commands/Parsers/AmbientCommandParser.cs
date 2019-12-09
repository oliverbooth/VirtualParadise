namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="AmbientCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class AmbientCommandParser : CommandParser<AmbientCommand>
    {
        /// <summary>
        /// Parses the <c>ambient</c> command and returns a <see cref="AmbientCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="AmbientCommand"/>.</returns>
        public override AmbientCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(AmbientCommand), input) as AmbientCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
