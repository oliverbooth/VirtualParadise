namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="SolidCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class SolidCommandParser : CommandParser<SolidCommand>
    {
        /// <summary>
        /// Parses the <c>solid</c> command and returns a <see cref="SolidCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="SolidCommand"/>.</returns>
        public override async Task<SolidCommand> ParseAsync(string input)
        {
            List<string> words = input.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries)
                                      .ToList();

            string name = String.Empty;

            if (!Keyword.TryBool(words[0], out _)) {
                name = words[0];
                words.RemoveAt(0);
                input = String.Join(" ", words);
            }


            if (!(await base.ParseAsync(typeof(SolidCommand), input)
                            .ConfigureAwait(false) is SolidCommand command)) {
                return null;
            }

            if (String.IsNullOrWhiteSpace(command.TargetName)) {
                command.TargetName = name;
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
