namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="SpecularMapCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class SpecularMapCommandParser : CommandParser<SpecularMapCommand>
    {
        /// <summary>
        /// Parses the <c>specularmap</c> command and returns a <see cref="SpecularMapCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="SpecularMapCommand"/>.</returns>
        public override SpecularMapCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(SpecularMapCommand), input) as SpecularMapCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
