namespace VirtualParadise.Scene.Serialization.Commands.Parsing
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// Represents the base class for command parsers.
    /// </summary>
    public abstract class CommandParser
    {
        #region Methods

        /// <summary>
        /// Parses the command.
        /// </summary>
        /// <param name="type">The command type.</param>
        /// <param name="input">The input.</param>
        public virtual async Task<Command> ParseAsync(Type type, string input)
        {
            if (!(Activator.CreateInstance(type) is Command command)) {
                return null;
            }

            if (!(type.GetCustomAttribute<CommandAttribute>() is { } attribute)) {
                return null;
            }

            command.CommandName = (attribute.Name ?? String.Empty).ToUpperInvariant();

            (Dictionary<string, object> properties, string remainder) =
                await this.ExtractPropertiesAsync(input).ConfigureAwait(false);

            string[] args = remainder.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);

            command.Properties = properties;
            command.Arguments  = args.ToList();
            command.UpdateProperties();
            command.UpdateFlags();
            command.UpdateArguments();

            return command;
        }

        /// <summary>
        /// Extracts the key/value properties pairs from an argument set.
        /// </summary>
        /// <param name="input">The command arguments.</param>
        /// <returns></returns>
        protected async Task<(Dictionary<string, object>, string)> ExtractPropertiesAsync(string input)
        {
            string[] words =
                input.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);
            string sanitized = String.Join("\n", words);
            IDictionary<string, string> dict = await PropertiesParser.ParseAsync(sanitized)
                                                                     .ConfigureAwait(false);

            List<string> remainderList = words.ToList();
            remainderList.RemoveAll(s => s.Contains('='));

            string remainder = String.Join(" ", remainderList);
            return (dict.ToDictionary(s => s.Key.ToUpperInvariant(), s => (object) s.Value), remainder);
        }

        #endregion
    }
}
