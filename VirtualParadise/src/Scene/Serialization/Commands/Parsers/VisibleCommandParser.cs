namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
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
        public override VisibleCommand Parse(string input)
        {
            List<string> words = Regex.Split(input, "\\s").ToList();
            string       name  = String.Empty;

            if (!Keyword.TryBool(words[0], out _))
            {
                name = words[0];
                words.RemoveAt(0);
                input = String.Join(" ", words);
            }

            VisibleCommand command = base.Parse(typeof(VisibleCommand), input) as VisibleCommand;

            if (command != null && String.IsNullOrWhiteSpace(command.TargetName))
            {
                command.TargetName = name;
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
