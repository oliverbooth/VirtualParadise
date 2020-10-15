namespace VirtualParadise.API
{
    using System.ComponentModel;

    /// <summary>
    /// An enumeration of light types to use on the <c>light</c> command.
    /// </summary>
    public enum LightType
    {
        /// <summary>
        /// Point light.
        /// </summary>
        [Description("Point light.")]
        Point,

        /// <summary>
        /// Spot light.
        /// </summary>
        [Description("Spot light.")]
        Spot
    }
}
