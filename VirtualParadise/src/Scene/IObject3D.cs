namespace VirtualParadise.Scene
{
    /// <summary>
    /// Represents a world object.
    /// </summary>
    public interface IObject3D : IObject
    {
        /// <summary>
        /// Gets or sets the object action.
        /// </summary>
        string Action { get; set; }

        /// <summary>
        /// Gets or sets the object angle.
        /// </summary>
        double Angle { get; set; }

        /// <summary>
        /// Gets or sets the object description.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the object model.
        /// </summary>
        string Model { get; set; }
    }
}
