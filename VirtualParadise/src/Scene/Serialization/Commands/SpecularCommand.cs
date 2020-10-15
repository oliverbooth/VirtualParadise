namespace VirtualParadise.Scene.Serialization.Commands
{
    using System;
    using System.ComponentModel;
    using Parsers;
    using Parsing;

    /// <summary>
    /// Represents a class which serializes the <c>specular</c> command.
    /// </summary>
    [Command("specular", typeof(CommandDefaultParser<SpecularCommand>))]
    public class SpecularCommand : Command, ITaggedCommand
    {
        /// <summary>
        /// Gets or sets a value indicating whether to apply specular highlights onto alpha transparent objects.
        /// </summary>
        [Flag("alpha")]
        public bool Alpha { get; set; } = false;

        /// <summary>
        /// Gets or sets the object tag.
        /// </summary>
        [DefaultValue("")]
        [Property("tag")]
        public string Tag { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the intensity.
        /// </summary>
        [DefaultValue(1.0)]
        [Parameter(0, "intensity")]
        public double Intensity { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the shininess.
        /// </summary>
        [DefaultValue(30.0)]
        [Parameter(1, "shininess", Optional = true)]
        public double Shininess { get; set; } = 30.0;
    }
}
