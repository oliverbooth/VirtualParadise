namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="NormalMapCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class NormalMapCommandParser : CommandParser<NormalMapCommand>
    {
        /// <summary>
        /// Parses the <c>normalmap</c> command and returns a <see cref="NormalMapCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="NormalMapCommand"/>.</returns>
        public override NormalMapCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(NormalMapCommand), input) as NormalMapCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
