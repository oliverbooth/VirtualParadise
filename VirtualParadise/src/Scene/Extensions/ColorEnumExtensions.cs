namespace VirtualParadise.Scene.Extensions
{
    using API;

    /// <summary>
    /// Extension methods for <see cref="ColorEnum"/>.
    /// </summary>
    public static class ColorEnumExtensions
    {
        /// <summary>
        /// Overrides <see cref="ColorEnum.ToString()"/> to call <see cref="Color.ToString()"/>.
        /// </summary>
        /// <param name="c">The color.</param>
        /// <returns>Returns a string representation of the color.</returns>
        public static string ToString(this ColorEnum c)
        {
            return ((Color) c).ToString();
        }
    }
}
