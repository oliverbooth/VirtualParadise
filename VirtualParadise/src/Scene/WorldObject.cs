namespace VirtualParadise.Scene
{
    #region Using Directives

    using System;

    #endregion

    /// <summary>
    /// Represents a world object.
    /// </summary>
    public class WorldObject : IWorldObject, IEquatable<WorldObject>, IEquatable<IWorldObject>, IEquatable<IObject>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldObject"/> class.
        /// </summary>
        /// <param name="id">The object ID.</param>
        public WorldObject(int id)
        {
            this.ID = id;
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public int ID { get; }

        /// <summary>
        /// Gets or sets the object owner.
        /// </summary>
        public int Owner { get; set; }

        /// <summary>
        /// Gets or sets the object action.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the object description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the object model.
        /// </summary>
        public string Model { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public bool Equals(WorldObject other)
        {
            return ReferenceEquals(this, other) ||
                   this.ID == other?.ID;
        }

        /// <inheritdoc />
        public bool Equals(IWorldObject other)
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
