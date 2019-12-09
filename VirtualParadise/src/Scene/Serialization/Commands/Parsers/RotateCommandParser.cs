namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="RotateCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class RotateCommandParser : CommandParser<RotateCommand>
    {
        /// <summary>
        /// Parses the <c>rotate</c> command and returns a <see cref="RotateCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="RotateCommand"/>.</returns>
        public override RotateCommand Parse(string input)
        {
            if (!(base.Parse(typeof(RotateCommand), input) is RotateCommand command))
            {
                return null;
            }

            IReadOnlyCollection<string> args = command.Arguments;
            double                      a    = 0.0;

            if (args.Count >= 1 && Double.TryParse(args.ElementAt(0), out a))
            {
                command.Y = a;
                command.X = 0.0;
                command.Z = 0.0;
            }

            if (args.Count >= 2 && Double.TryParse(args.ElementAt(1), out double b))
            {
                command.X = a;
                command.Y = b;
                command.Z = 0.0;
            }

            if (args.Count >= 3 && Double.TryParse(args.ElementAt(2), out double c))
            {
                command.Z = c;
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
