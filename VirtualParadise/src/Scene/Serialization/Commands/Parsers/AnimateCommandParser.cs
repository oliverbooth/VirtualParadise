namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
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
        public override AnimateCommand Parse(string input)
        {
            AnimateCommand command = base.Parse(typeof(AnimateCommand), input) as AnimateCommand;

            List<string> args = Regex.Split(input, "\\s").ToList();
            args.RemoveAll(m => m.Equals("mask", StringComparison.InvariantCultureIgnoreCase));

            if (command != null)
            {
                command.FrameList = args.Skip(6)
                                        .Take(command.FrameCount)
                                        .Select(s => s.To<int>())
                                        .ToArray();
            }

            return command;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
