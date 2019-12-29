namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Parsing;
    using X10D;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="AnimateCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class AnimateCommandParser : CommandParser<AnimateCommand>
    {
        /// <summary>
        /// Parses the <c>animate</c> command and returns a <see cref="AnimateCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="AnimateCommand"/>.</returns>
        public override async Task<AnimateCommand> ParseAsync(string input)
        {
            if (!(await base.ParseAsync(typeof(AnimateCommand), input)
                            .ConfigureAwait(false) is AnimateCommand command)) {
                return null;
            }

            List<string> args = input.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries)
                                     .ToList();

            args.RemoveAll(m => m.Equals("mask", StringComparison.InvariantCultureIgnoreCase));

            command.FrameList = args.Skip(command.IsMask ? 6 : 5)
                                    .Take(command.FrameCount)
                                    .Select(s => s.To<int>())
                                    .ToArray();

            return command;
        }

        /// <inheritdoc />
        public override async Task<CommandBase> ParseAsync(Type type, string input)
        {
            return await this.ParseAsync(input).ConfigureAwait(false);
        }
    }
}
