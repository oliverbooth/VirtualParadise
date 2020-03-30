namespace VirtualParadise.Scene.Serialization
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    public static class PropertiesParser
    {
        /// <summary>
        /// Parses a multi-line set of properties.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns an <see cref="IDictionary{TKey,TValue}"/>.</returns>
        public static async Task<IDictionary<string, string>> ParseAsync(string input)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();

            string[]   lines = input.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            List<Task> tasks = lines.Select(line => ParseLineAsync(line, ref properties)).ToList();

            await Task.WhenAll(tasks).ConfigureAwait(false);
            return properties;
        }

        /// <summary>
        /// Parses a line and adds the key/value to the provided dictionary.
        /// </summary>
        /// <param name="line">The line to parse.</param>
        /// <param name="properties">The properties.</param>
        private static Task ParseLineAsync(string line, ref Dictionary<string, string> properties)
        {
            int eq = line.IndexOf('=');

            if (eq < 0 || line.Length < eq + 1) {
                return Task.CompletedTask;
            }

            string key   = line.Substring(0, eq);
            string value = line.Substring(eq + 1);

            properties.Add(key, value);
            return Task.CompletedTask;
        }
    }
}
