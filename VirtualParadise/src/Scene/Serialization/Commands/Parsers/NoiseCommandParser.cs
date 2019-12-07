namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="NoiseCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class NoiseCommandParser : CommandParser<NoiseCommand>
    {
        /// <summary>
        /// Parses the <c>noise</c> command and returns a <see cref="NoiseCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="NoiseCommand"/>.</returns>
        public override NoiseCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(NoiseCommand), input) as NoiseCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
