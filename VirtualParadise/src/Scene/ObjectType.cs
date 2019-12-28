namespace VirtualParadise.Scene
{
    #region Using Directives

    using System;
    using System.ComponentModel;

    #endregion

    /// <summary>
    /// An enumeration of object types.
    /// </summary>
    public enum ObjectType
    {
        /// <summary>
        /// 3D object.
        /// </summary>
        [Description("3D object")]
        Object3D,

        /// <summary>
        /// Cloth object.
        /// </summary>
        [Description("Cloth object")]
        [Obsolete]
        Cloth,

        /// <summary>
        /// Particle emitter.
        /// </summary>
        [Description("Particle emitter")]
        Particle,

        /// <summary>
        /// Path.
        /// </summary>
        [Description("Path")]
        Path
    }
}
