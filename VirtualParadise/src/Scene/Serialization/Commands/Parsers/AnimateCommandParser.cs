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
            List<string> args = Regex.Split(input, "\\s").ToList();
            bool         mask = args[0].Equals("mask", StringComparison.InvariantCultureIgnoreCase);
            if (mask)
            {
                args.RemoveAt(0);
            }

            // nothing special needed
            return base.Parse(typeof(AnimateCommand), String.Join(" ", args)) as AnimateCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
