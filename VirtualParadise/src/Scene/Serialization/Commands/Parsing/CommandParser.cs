namespace VirtualParadise.Scene.Serialization.Commands.Parsing
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

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
        public virtual CommandBase Parse(Type type, string input)
        {
            if (!(Activator.CreateInstance(type) is CommandBase command))
            {
                return null;
            }

            command.CommandName = type.GetCustomAttribute<CommandAttribute>()?.Name ?? String.Empty;
            command.Properties  = this.ExtractProperties(input, out input);
            command.Arguments   = Regex.Split(input, "\\s");

            command.UpdateProperties();
            command.UpdateFlags();
            command.UpdateArguments();

            return command;
        }

        /// <summary>
        /// Extracts the key/value properties pairs from an argument set.
        /// </summary>
        /// <param name="input">The command arguments.</param>
        /// <param name="remainder">The args that remain after extracting properties.</param>
        /// <returns></returns>
        protected Dictionary<string, object> ExtractProperties(string input, out string remainder)
        {
            IEnumerable<string> words = Regex.Split(input, "\\s")
                                             .Where(s => !String.IsNullOrWhiteSpace(s))
                                             .ToArray();

            // convert key=value pairs to a dictionary we can assign as the command properties
            Dictionary<string, object> pairs = words.Where(w => Regex.Match(w, "\\S+=\\S+").Success)
                                                    .Select(p =>
                                                     {
                                                         Match match = Regex.Match(p, "(\\S+)=(\\S+)");
                                                         return new[]
                                                         {
                                                             match.Groups[1].Value.ToUpperInvariant(),
                                                             match.Groups[2].Value
                                                         };
                                                     })
                                                    .ToDictionary(s => s.ElementAt(0), s => (object) s.ElementAt(1));

            // pass back the input arguments without key=value pairs
            List<string> remainderList = words.ToList();
            remainderList.RemoveAll(w => Regex.Match(w, "\\S+=\\S+").Success);
            remainder = String.Join(" ", remainderList);

            return pairs;
        }

        #endregion
    }
}
