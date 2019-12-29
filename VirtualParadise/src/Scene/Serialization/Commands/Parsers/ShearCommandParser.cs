namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Parsing;
    using VpNet;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="ShearCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ShearCommandParser : CommandParser<ShearCommand>
    {
        /// <summary>
        /// Parses the <c>scale</c> command and returns a <see cref="ShearCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="ShearCommand"/>.</returns>
        public override async Task<ShearCommand> ParseAsync(string input)
        {
            ShearCommand command = await base.ParseAsync(typeof(ShearCommand), input)
                                             .ConfigureAwait(false) as ShearCommand;

            Vector3 positive = new Vector3(0.0);
            Vector3 negative = new Vector3(0.0);

            if (!(command is null)) {
                if (command.Arguments.Count >= 1) {
                    positive.Z = Convert.ToDouble(command.Arguments.ElementAt(0));
                }

                if (command.Arguments.Count >= 2) {
                    positive.X = Convert.ToDouble(command.Arguments.ElementAt(1));
                }

                if (command.Arguments.Count >= 3) {
                    positive.Y = Convert.ToDouble(command.Arguments.ElementAt(2));
                }

                if (command.Arguments.Count >= 4) {
                    negative.Y = Convert.ToDouble(command.Arguments.ElementAt(3));
                }

                if (command.Arguments.Count >= 5) {
                    negative.Z = Convert.ToDouble(command.Arguments.ElementAt(4));
                }

                if (command.Arguments.Count >= 6) {
                    negative.X = Convert.ToDouble(command.Arguments.ElementAt(5));
                }

                command.Positive = positive;
                command.Negative = negative;
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
;
