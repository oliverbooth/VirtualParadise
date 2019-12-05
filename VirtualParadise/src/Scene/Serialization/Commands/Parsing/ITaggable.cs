namespace VirtualParadise.Scene.Serialization.Commands.Parsing
{
    /// <summary>
    /// Represents a command that supports the <c>tag</c> property.
    /// </summary>
    public interface ITaggedCommand
    {
        /// <summary>
        /// Gets the tag.
        /// </summary>
        string Tag { get; }
    }
}
