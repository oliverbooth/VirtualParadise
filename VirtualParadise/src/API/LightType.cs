namespace VirtualParadise.API
{
    #region Using Directives

    using System.ComponentModel;

    #endregion

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
