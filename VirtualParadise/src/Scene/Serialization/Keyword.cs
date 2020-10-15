namespace VirtualParadise.Scene.Serialization
{
    using System;

    /// <summary>
    /// Action keyword parser
    /// </summary>
    public static class Keyword
    {
        /// <summary>
        /// Tries to parse an action bool keyword to a <see cref="Boolean"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="result">The parsed result.</param>
        /// <returns>Returns <see langword="true"/> if the parse was valid, <see langword="false"/> otherwise.</returns>
        public static bool TryBool(string input, out bool result)
        {
            input  = input?.ToUpperInvariant() ?? String.Empty;
            result = false;

            switch (input)
            {
                case "1":
                case "ON":
                case "YES":
                case "TRUE":
                    result = true;
                    return true;

                case "0":
                case "OFF":
                case "NO":
                case "FALSE":
                    return true;

                default:
                    return false;
            }
        }
    }
}
