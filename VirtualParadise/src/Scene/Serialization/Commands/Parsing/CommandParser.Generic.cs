namespace VirtualParadise.Scene.Serialization.Commands.Parsing
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the base class for command parsers.
    /// </summary>
    public abstract class CommandParser<TCommand> : CommandParser
        where TCommand : Command
    {
        /// <summary>
        /// Parses the command.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Returns a <see cref="TCommand"/>.</returns>
        public abstract Task<TCommand> ParseAsync(string input);

        /// <summary>
        /// Parses the command.
        /// </summary>
        /// <param name="type">The command type.</param>
        /// <param name="input">The input.</param>
        public override async Task<Command> ParseAsync(Type type, string input)
        {
            return await base.ParseAsync(type, input)
                             .ConfigureAwait(false);
        }
    }
}
