namespace VirtualParadise.Scene
{
    using System.ComponentModel;
    using API;
    using Serialization.Commands.Parsing;
    using VpNet;
    using Color = API.Color;

    /// <summary>
    /// Represents a particle emitter object.
    /// </summary>
    public interface IParticleEmitter : IObject
    {
        /// <summary>
        /// Gets or sets the blend mode.
        /// </summary>
        [DefaultValue(BlendMode.Normal)]
        [Property("blend", Optional = false)]
        BlendMode BlendMode { get; set; }

        /// <summary>
        /// Gets or sets the starting color value.
        /// </summary>
        [DefaultValue(ColorEnum.White)]
        [Property("color_min", Optional = false)]
        Color ColorFrom { get; set; }

        /// <summary>
        /// Gets or sets the ending color value.
        /// </summary>
        [DefaultValue(ColorEnum.White)]
        [Property("color_max", Optional = false)]
        Color ColorTo { get; set; }

        /// <summary>
        /// Gets or sets the opacity.
        /// </summary>
        [DefaultValue(1.0)]
        [Property("opacity", Optional = false)]
        double Opacity { get; set; }

        /// <summary>
        /// Gets or sets the starting volume vector.
        /// </summary>
        [Property("volume_min", Optional = false)]
        Vector3 VolumeFrom { get; set; }

        /// <summary>
        /// Gets or sets the ending volume vector.
        /// </summary>
        [Property("volume_max", Optional = false)]
        Vector3 VolumeTo { get; set; }
    }
}
