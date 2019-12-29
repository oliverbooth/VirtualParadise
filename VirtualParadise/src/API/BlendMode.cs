namespace VirtualParadise.API
{
    #region Using Directives

    using System.ComponentModel;
    using Scene;

    #endregion

    /// <summary>
    /// An enumeration of blend modes for an <see cref="IParticleEmitter"/>.
    /// </summary>
    public enum BlendMode
    {
        /// <summary>
        /// Normal.
        /// </summary>
        [Description("Normal")]
        Normal,

        /// <summary>
        /// Additive.
        /// </summary>
        [Description("Additive")]
        Add
    }
}
