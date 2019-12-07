namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="DiffuseCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class DiffuseCommandParser : CommandParser<DiffuseCommand>
    {
        /// <summary>
        /// Parses the <c>diffuse</c> command and returns a <see cref="DiffuseCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="DiffuseCommand"/>.</returns>
        public override DiffuseCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(DiffuseCommand), input) as DiffuseCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
