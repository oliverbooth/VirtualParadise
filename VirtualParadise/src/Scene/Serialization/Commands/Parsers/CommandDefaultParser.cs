namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements a default parser that needs no special implementation.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class CommandDefaultParser<TCommand> : CommandParser
        where TCommand : Command
    {
        /// <summary>
        /// Parses the command and returns a <see cref="TCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="TCommand"/>.</returns>
        public async Task<TCommand> ParseAsync(string input)
        {
            return (await base.ParseAsync(typeof(TCommand), input)
                              .ConfigureAwait(false)) as TCommand;
        }

        /// <inheritdoc />
        public override async Task<Command> ParseAsync(Type type, string input)
        {
            return await this.ParseAsync(input)
                             .ConfigureAwait(false);
        }
    }
}
