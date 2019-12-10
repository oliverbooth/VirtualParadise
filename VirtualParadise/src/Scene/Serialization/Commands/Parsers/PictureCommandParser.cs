namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="PictureCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class PictureCommandParser : CommandParser<PictureCommand>
    {
        /// <summary>
        /// Parses the <c>texture</c> command and returns a <see cref="PictureCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="PictureCommand"/>.</returns>
        public override PictureCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(PictureCommand), input) as PictureCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
