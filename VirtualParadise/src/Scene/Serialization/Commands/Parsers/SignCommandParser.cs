namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="SignCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class SignCommandParser : CommandParser<SignCommand>
    {
        /// <summary>
        /// Parses the <c>sign</c> command and returns a <see cref="SignCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="SignCommand"/>.</returns>
        public override async Task<SignCommand> ParseAsync(string input)
        {
            // extract quoted string from argument list
            int firstQuote = input.IndexOf('"');
            int lastQuote  = input.IndexOf('"', firstQuote + 1);

            string text = String.Empty;

            StringBuilder args = new StringBuilder();

            if (firstQuote >= 0 && lastQuote > firstQuote) {
                text = input.Substring(firstQuote + 1, lastQuote - firstQuote - 1);

                // trim the quoted string from the input
                args.Append(input.Substring(0, firstQuote));
                args.Append(input.Substring(lastQuote + 1));
            } else {
                // no quoted string in input, use it all.
                args.Append(input);
            }

            SignCommand command = await base.ParseAsync(typeof(SignCommand), args.ToString())
                                            .ConfigureAwait(false) as SignCommand;

            // text is unique. text needs to be assigned manually
            if (!(command is null)) {
                command.Text = text;
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
