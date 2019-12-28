namespace VirtualParadise.Scene
{
    #region Using Directives

    using System;
    using VpNet;

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
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the object angle.
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// Gets or sets the object data.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Gets or sets the object description.
        /// </summary>
        public string Description { get; set; }

        /// <inheritdoc />
        public int ID { get; }

        /// <summary>
        /// Gets or sets the object model.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the object owner.
        /// </summary>
        public int Owner { get; set; }

        /// <summary>
        /// Gets or sets the object position.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets the object world data.
        /// </summary>
        public Vector3 Rotation { get; set; }

        /// <summary>
        /// Gets or sets the date and time this object was built.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the object type.
        /// </summary>
        ObjectType IObject.Type => ObjectType.Object3D;

        #endregion

        #region Methods

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
