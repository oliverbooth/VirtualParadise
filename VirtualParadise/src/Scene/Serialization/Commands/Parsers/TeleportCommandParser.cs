namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;
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
        public override async Task<TeleportCommand> ParseAsync(string input)
        {
            TeleportCommand command = await base.ParseAsync(typeof(TeleportCommand), input)
                                                .ConfigureAwait(false) as TeleportCommand;

            if (!(command is null)) {
                command.Coordinates = Coordinates.Parse(input);
            }

            return command;
        }

        /// <inheritdoc />
        public override async Task<Command> ParseAsync(Type type, string input)
        {
            return await this.ParseAsync(input).ConfigureAwait(false);
        }
    }
}
