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
    /// Represents a class which implements the parser for <see cref="VisibleCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class VisibleCommandParser : CommandParser<VisibleCommand>
    {
        /// <summary>
        /// Parses the <c>visible</c> command and returns a <see cref="VisibleCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="VisibleCommand"/>.</returns>
        public override async Task<VisibleCommand> ParseAsync(string input)
        {
            List<string> words = input.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries)
                                      .ToList();


            string name = String.Empty;

            if (words.Count > 0 && !Keyword.TryBool(words[0], out _)) {
                name = words[0];
                words.RemoveAt(0);
                input = String.Join(" ", words);
            }

            VisibleCommand command = await base.ParseAsync(typeof(VisibleCommand), input)
                                               .ConfigureAwait(false) as VisibleCommand;

            if (!(command is null) && String.IsNullOrWhiteSpace(command.TargetName)) {
                command.TargetName = name;
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
