namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="TextureCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class TextureCommandParser : CommandParser<TextureCommand>
    {
        /// <summary>
        /// Parses the <c>texture</c> command and returns a <see cref="TextureCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="TextureCommand"/>.</returns>
        public override TextureCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(TextureCommand), input) as TextureCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
