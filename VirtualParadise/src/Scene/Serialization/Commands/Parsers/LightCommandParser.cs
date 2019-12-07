namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="LightCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class LightCommandParser : CommandParser<LightCommand>
    {
        /// <summary>
        /// Parses the <c>light</c> command and returns a <see cref="LightCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="LightCommand"/>.</returns>
        public override LightCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(LightCommand), input) as LightCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
