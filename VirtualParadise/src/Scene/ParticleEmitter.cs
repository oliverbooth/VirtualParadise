namespace VirtualParadise.Scene
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using API;
    using Serialization;
    using Serialization.Commands.Parsing;
    using Serialization.Internal;
    using VpNet;
    using X10D;
    using Color = API.Color;

    #endregion

    /// <summary>
    /// Represents a particle emitter.
    /// </summary>
    public class ParticleEmitter : IParticleEmitter,
                                   IEquatable<ParticleEmitter>,
                                   IEquatable<IParticleEmitter>,
                                   IEquatable<IObject>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleEmitter"/> class.
        /// </summary>
        /// <param name="id">The object ID.</param>
        public ParticleEmitter(int id)
        {
            this.ID = id;
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public BlendMode BlendMode { get; set; }

        /// <inheritdoc />
        public Color ColorFrom { get; set; }

        /// <inheritdoc />
        public Color ColorTo { get; set; }

        /// <inheritdoc />
        public byte[] Data { get; set; }

        /// <inheritdoc />
        public int ID { get; }

        /// <inheritdoc />
        public double Opacity { get; set; }

        /// <inheritdoc />
        public int Owner { get; set; }

        /// <inheritdoc />
        public Vector3 Position { get; set; }

        /// <inheritdoc />
        public Vector3 Rotation { get; set; }

        /// <inheritdoc />
        public DateTime Time { get; set; }

        /// <inheritdoc />
        public Vector3 VolumeFrom { get; set; }

        /// <inheritdoc />
        public Vector3 VolumeTo { get; set; }

        /// <inheritdoc />
        ObjectType IObject.Type => ObjectType.Particle;

        #endregion

        #region Methods

        /// <summary>
        /// Converts an instance of <see cref="VpObject"/> to a new instance of <see cref="ParticleEmitter"/>.
        /// </summary>
        /// <param name="obj">The <see cref="VpObject"/>.</param>
        /// <returns>Returns a new instance of <see cref="ParticleEmitter"/>.</returns>
        public static async Task<ParticleEmitter> FromVpObjectAsync(VpObject obj)
        {
            throw new NotImplementedException("This method is still a work in progress.");

            if (obj.ObjectType != (int) ObjectType.Particle) {
                throw new ArgumentException("Object is not a particle emitter.", nameof(obj));
            }

            PropertyInfo[]  members  = typeof(ParticleEmitter).GetProperties();
            ParticleEmitter particle = new ParticleEmitter(obj.Id);
            string          data     = obj.Data.GetString();
            IDictionary<string, string> properties = await PropertiesParser.ParseAsync(data)
                                                                           .ConfigureAwait(false);

            foreach (PropertyInfo member in members) {
                PropertyAttribute property = member.ToVpProperty();
                if (properties.ContainsKey(property.Name)) {
                    member.SetValue(particle, properties[property.Name], null);
                }
            }

            return particle;
        }

        /// <inheritdoc />
        public bool Equals(ParticleEmitter other)
        {
            return ReferenceEquals(this, other) ||
                   this.ID == other?.ID;
        }

        /// <inheritdoc />
        public bool Equals(IParticleEmitter other)
        {
            return ReferenceEquals(this, other) ||
                   this.ID == other?.ID;
        }

        /// <inheritdoc />
        public bool Equals(IObject other)
        {
            return ReferenceEquals(this, other) ||
                   this.ID == other?.ID;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is IObject other &&
                   this.Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.ID;
        }

        #endregion
    }
}

/*
 * acceleration_max=0 -0.05 0
acceleration_min=0 0 0
blend=add
color_max=ffffff
color_min=ffffff
emitter_lifespan=0
fade_in_time=500
fade_out_time=2000
interpolate=0
opacity=1
particle_lifespan=5000
particle_type=sprite
release_count=12
release_time_max=1000
release_time_min=25
size_max=0.02 0.02 0.02
size_min=0.02 0.02 0.02
speed_max=0 -0.05 0
speed_min=0 -0.05 0
spin_max=0 0 0
spin_min=0 0 0
start_angle_max=0 0 0
start_angle_min=0 0 0
tag=
texture=p_snow.jpg
volume_max=-2 0.5 -2
volume_min=2 2 2
*/
