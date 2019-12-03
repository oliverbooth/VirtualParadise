namespace VirtualParadise.Scene.Serialization
{
    #region Using Directives

    using System.ComponentModel;

    #endregion

    /// <summary>
    /// An enumeration of action formats.
    /// </summary>
    public enum ActionFormat
    {
        /// <summary>
        /// Indicates no specific formatting should be applied.
        /// </summary>
        [Description("Indicates no specific formatting should be applied.")]
        None,

        /// <summary>
        /// Indicates any redundant whitespace should be trimmed.
        /// </summary>
        [Description("Indicates any redundant whitespace should be trimmed.")]
        Compressed,

        /// <summary>
        /// Indicates that formatting should be indented.
        /// </summary>
        [Description("Indicates that formatting should be indented.")]
        Indented
    }
}
