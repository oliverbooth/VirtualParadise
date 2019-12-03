namespace VirtualParadise.Scene
{
    /// <summary>
    /// Represents an object.
    /// </summary>
    public interface IObject
    {
        /// <summary>
        /// Gets the object ID.
        /// </summary>
        int ID { get; }

        /// <summary>
        /// Gets the object owner.
        /// </summary>
        int Owner { get; }
    }
}
