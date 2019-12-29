namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="ScaleCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ScaleCommandParser : CommandParser<ScaleCommand>
    {
        /// <summary>
        /// Parses the <c>scale</c> command and returns a <see cref="ScaleCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="ScaleCommand"/>.</returns>
        public override async Task<ScaleCommand> ParseAsync(string input)
        {
            ScaleCommand command = await base.ParseAsync(typeof(ScaleCommand), input)
                                               .ConfigureAwait(false) as ScaleCommand;

            if (command?.Arguments.Count == 1) {
                command.Y = command.Z = command.X;
            }

            return command;
        }

        /// <inheritdoc />
        public override async Task<CommandBase> ParseAsync(Type type, string input)
        {
            return await this.ParseAsync(input).ConfigureAwait(false);
        }
    }
}
