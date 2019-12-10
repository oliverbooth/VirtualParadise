namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="NameCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class NameCommandParser : CommandParser<NameCommand>
    {
        /// <summary>
        /// Parses the <c>name</c> command and returns a <see cref="NameCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="NameCommand"/>.</returns>
        public override NameCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(NameCommand), input) as NameCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
