namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="SoundCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class SoundCommandParser : CommandParser<SoundCommand>
    {
        /// <summary>
        /// Parses the <c>sound</c> command and returns a <see cref="SoundCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="SoundCommand"/>.</returns>
        public override SoundCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(SoundCommand), input) as SoundCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
