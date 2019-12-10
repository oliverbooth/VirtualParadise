namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using API;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="TeleportCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class TeleportCommandParser : CommandParser<TeleportCommand>
    {
        /// <summary>
        /// Parses the <c>teleport</c> command and returns a <see cref="TeleportCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="TeleportCommand"/>.</returns>
        public override TeleportCommand Parse(string input)
        {
            if (!(base.Parse(typeof(TeleportCommand), String.Empty) is TeleportCommand command))
            {
                return null;
            }

            command.Coordinates = Coordinates.Parse(input);
            return command;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
