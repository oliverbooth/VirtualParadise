namespace VirtualParadise.API
{
    #region Using Directives

    using System;
    using System.Text.RegularExpressions;

    #endregion

    /// <summary>
    /// Represents a class which can translate a coordinate string to a <see cref="Coordinates"/>
    /// instance.
    /// </summary>
    internal static class CoordinateParser
    {
        /// <summary>
        /// Parses a coordinate string.
        /// </summary>
        /// <param name="coordinates">The coordinates to parse.</param>
        /// <returns>Returns an instance of <see cref="Coordinates"/>.</returns>
        public static Coordinates ParseCoordinates(string coordinates)
        {
            const string pattern =
                @"(?:([a-z]+) *)?(?: *(\+)?(-?\d+(?:\.\d+)?)([ns])? +(\+)?(-?\d+(?:\.\d+)?)([we])?( +(\+)?(-?\d+(?:\.\d+)?)a)?( +(\+)?(-?\d+(?:\.\d+)?))?)?";

            Regex regex    = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match    = regex.Match(coordinates);
            bool  relative = match.Groups[2].Success || match.Groups[5].Success;

            string world     = match.Groups[1].Success ? match.Groups[1].Value : String.Empty;
            double z         = match.Groups[3].Success ? Convert.ToDouble(match.Groups[3].Value) : 0.0;
            double x         = match.Groups[6].Success ? Convert.ToDouble(match.Groups[6].Value) : 0.0;
            double y         = match.Groups[10].Success ? Convert.ToDouble(match.Groups[10].Value) : 0.0;
            double direction = match.Groups[13].Success ? Convert.ToDouble(match.Groups[13].Value) : 0.0;

            if (match.Groups[4].Success &&
                match.Groups[4].Value.Equals("S", StringComparison.InvariantCultureIgnoreCase))
            {
                z = -z;
            }

            if (match.Groups[7].Success &&
                match.Groups[7].Value.Equals("E", StringComparison.InvariantCultureIgnoreCase))
            {
                x = -x;
            }

            return new Coordinates
            {
                Z          = z,
                X          = x,
                Y          = y,
                Direction  = direction,
                World      = world,
                IsRelative = relative
            };
        }
    }
}
