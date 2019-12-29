namespace VirtualParadise.Scene
{
    #region Using Directives

    using System;
    using VpNet;

    #endregion

    /// <summary>
    /// Represents an object.
    /// </summary>
    public interface IObject
    {
        /// <summary>
        /// Gets or sets the object data.
        /// </summary>
        byte[] Data { get; set; }

        /// <summary>
        /// Gets the object ID.
        /// </summary>
        int ID { get; }

        /// <summary>
        /// Gets or sets the object owner.
        /// </summary>
        int Owner { get; set; }

        /// <summary>
        /// Gets or sets the object position.
        /// </summary>
        Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets the object rotation.
        /// </summary>
        Vector3 Rotation { get; set; }

        /// <summary>
        /// Gets or sets the date and time this object was built.
        /// </summary>
        DateTime Time { get; set; }

        /// <summary>
        /// Gets the object type.
        /// </summary>
        ObjectType Type { get; }
    }
}
