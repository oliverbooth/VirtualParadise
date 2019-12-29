namespace VirtualParadise.Scene
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;
    using VpNet;
    using Action = Serialization.Action;

    #endregion

    /// <summary>
    /// Represents a world object.
    /// </summary>
    public class Object3D : IObject3D,
                            IEquatable<Object3D>,
                            IEquatable<IObject3D>,
                            IEquatable<IObject>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Object3D"/> class.
        /// </summary>
        /// <param name="id">The object ID.</param>
        public Object3D(int id)
        {
            this.ID = id;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the object action.
        /// </summary>
        public Action Action { get; set; }

        /// <inheritdoc />
        public double Angle { get; set; }

        /// <inheritdoc />
        public byte[] Data { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public int ID { get; }

        /// <inheritdoc />
        public string Model { get; set; }

        /// <inheritdoc />
        public int Owner { get; set; }

        /// <inheritdoc />
        public Vector3 Position { get; set; }

        /// <inheritdoc />
        public Vector3 Rotation { get; set; }

        /// <inheritdoc />
        public DateTime Time { get; set; }

        /// <inheritdoc />
        string IObject3D.Action { get; set; }

        /// <inheritdoc />
        ObjectType IObject.Type => ObjectType.Object3D;

        #endregion

        #region Methods

        /// <summary>
        /// Converts an instance of <see cref="VpObject"/> to a new instance of <see cref="ParticleEmitter"/>.
        /// </summary>
        /// <param name="vpObj">The <see cref="VpObject"/>.</param>
        /// <returns>Returns a new instance of <see cref="ParticleEmitter"/>.</returns>
        public static async Task<Object3D> FromVpObjectAsync(VpObject vpObj)
        {
            if (vpObj.ObjectType != (int) ObjectType.Object3D) {
                throw new ArgumentException("Object is not a 3D object.", nameof(vpObj));
            }

            Object3D obj = new Object3D(vpObj.Id) {
                Owner       = vpObj.Owner,
                Time        = vpObj.Time,
                Position    = vpObj.Position,
                Rotation    = vpObj.Rotation,
                Angle       = vpObj.Angle,
                Data        = vpObj.Data,
                Model       = vpObj.Model,
                Description = vpObj.Description,
                Action = await Action.ParseAsync(vpObj.Action)
                                     .ConfigureAwait(false)
            };

            ((IObject3D) obj).Action = vpObj.Action;
            return obj;
        }

        /// <inheritdoc />
        public bool Equals(Object3D other)
        {
            return ReferenceEquals(this, other) ||
                   this.ID == other?.ID;
        }

        /// <inheritdoc />
        public bool Equals(IObject3D other)
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
