namespace VirtualParadise.Scene.Serialization.Commands.Parsers
{
    #region Using Directives

    using System;
    using Parsing;

    #endregion

    /// <summary>
    /// Represents a class which implements the parser for <see cref="CameraCommand"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class CameraCommandParser : CommandParser<CameraCommand>
    {
        /// <summary>
        /// Parses the <c>camera</c> command and returns a <see cref="CameraCommand"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Returns a new instance of <see cref="CameraCommand"/>.</returns>
        public override CameraCommand Parse(string input)
        {
            // nothing special needed
            return base.Parse(typeof(CameraCommand), input) as CameraCommand;
        }

        /// <inheritdoc />
        public override CommandBase Parse(Type type, string input)
        {
            return this.Parse(input);
        }
    }
}
