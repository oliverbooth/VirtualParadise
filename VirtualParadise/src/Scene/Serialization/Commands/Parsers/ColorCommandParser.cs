namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using API;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="ColorCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ColorCommandParser : CommandParser<ColorCommand>
    {
        /// <summary>
        /// Parses the <c>color</c> command and returns a <see cref="ColorCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="ColorCommand"/>.</returns>
        public override async Task<ColorCommand> ParseAsync(string input)
        {
            if (!(await base.ParseAsync(typeof(ColorCommand), input)
                            .ConfigureAwait(false) is ColorCommand command)) {
                return null;
            }

            List<string> args = input.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries)
                                     .ToList();

            if (command.IsTint) {
                args.RemoveAt(0);
            }

            command.Color = Color.FromString(args[0]);

            return command;
        }

        /// <inheritdoc />
        public override async Task<CommandBase> ParseAsync(Type type, string input)
        {
            return await this.ParseAsync(input)
                             .ConfigureAwait(false);
        }
    }
}
