namespace VirtualParadise.Scene.Extensions
{
    using System;
    using Serialization.Commands;

    /// <summary>
    /// Extension methods for <see cref="Object3D"/>.
    /// </summary>
    public static class Object3DExtensions
    {
        /// <summary>
        /// Gets the name of this object, if it has one.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>Returns the name of this object, or <see cref="String.Empty"/>.</returns>
        public static string GetName(this Object3D obj)
        {
            return obj.Action?.Create?.GetCommandOfType<NameCommand>()?.Name ?? String.Empty;
        }
    }
}
