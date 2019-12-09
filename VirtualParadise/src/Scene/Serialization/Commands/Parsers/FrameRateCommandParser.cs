namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="FrameRateCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class FrameRateCommandParser : CommandParser<FrameRateCommand>
    {
        /// <summary>
        /// Parses the <c>framerate</c> command and returns a <see cref="FrameRateCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="FrameRateCommand"/>.</returns>
        public override FrameRateCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(FrameRateCommand), input) as FrameRateCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
