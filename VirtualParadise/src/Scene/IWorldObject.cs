﻿namespace VirtualParadise.Scene
{
    /// <summary>
    /// Represents a world object.
    /// </summary>
    public interface IWorldObject : IObject
    {
        /// <summary>
        /// Gets the object action.
        /// </summary>
        string Action { get; }

        /// <summary>
        /// Gets the object description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the object model.
        /// </summary>
        string Model { get; }
    }
}