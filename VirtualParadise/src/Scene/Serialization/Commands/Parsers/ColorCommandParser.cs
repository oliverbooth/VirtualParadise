namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="ColorCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ColorCommandParser : CommandParser<ColorCommand>
    {
        /// <summary>
        /// Parses the <c>color</c> command and returns a <see cref="ColorCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="ColorCommand"/>.</returns>
        public override ColorCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(ColorCommand), input) as ColorCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
