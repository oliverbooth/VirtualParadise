namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="TeleportXyzCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class TeleportXyzCommandParser : CommandParser<TeleportXyzCommand>
    {
        /// <summary>
        /// Parses the <c>teleportxyz</c> command and returns a <see cref="TeleportXyzCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="TeleportXyzCommand"/>.</returns>
        public override TeleportXyzCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(TeleportXyzCommand), input) as TeleportXyzCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
