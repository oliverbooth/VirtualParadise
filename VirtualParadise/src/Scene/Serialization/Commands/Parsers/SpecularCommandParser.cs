namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="SpecularCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class SpecularCommandParser : CommandParser<SpecularCommand>
    {
        /// <summary>
        /// Parses the <c>specular</c> command and returns a <see cref="SpecularCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="SpecularCommand"/>.</returns>
        public override SpecularCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(SpecularCommand), input) as SpecularCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
