namespace VirtualParadise.API
{
    #region Using Directives

    using System;
    using System.Text;

    #endregion

    /// <summary>
    /// Represents a struct which contains Virtual Paradise coordinates.
    /// </summary>
    public struct Coordinates : IEquatable<Coordinates>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        public double Direction { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance represents relative coordinates.
        /// </summary>
        public bool IsRelative { get; set; }

        /// <summary>
        /// Gets or sets the world.
        /// </summary>
        public string World { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the Z coordinate.
        /// </summary>
        public double Z { get; set; }

        #endregion

        #region Operators

        public static bool operator ==(Coordinates left, Coordinates right) =>
            left.Equals(right);

        public static bool operator !=(Coordinates left, Coordinates right) =>
            !(left == right);

        #endregion

        #region Methods

        /// <summary>
        /// Parses a coordinate string.
        /// </summary>
        /// <param name="coordinates">The coordinates to parse.</param>
        /// <returns>Returns an instance of <see cref="Coordinates"/>.</returns>
        public static Coordinates Parse(string coordinates)
        {
            return CoordinateParser.ParseCoordinates(coordinates);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            if (!String.IsNullOrWhiteSpace(this.World))
            {
                builder.Append(this.World)
                       .Append(' ');
            }

            if (this.IsRelative)
            {
                builder.Append(this.Z >= 0.0 ? "+" : "")
                       .Append(this.Z)
                       .Append(' ')
                       .Append(this.X >= 0.0 ? "+" : "")
                       .Append(this.X)
                       .Append(' ')
                       .Append(this.Y >= 0.0 ? "+" : "")
                       .Append(this.Y)
                       .Append('a')
                       .Append(' ')
                       .Append(this.Direction >= 0.0 ? "+" : "")
                       .Append(this.Direction);
            }
            else
            {
                builder.Append(Math.Abs(this.Z))
                       .Append(this.Z >= 0.0 ? 'n' : 's')
                       .Append(' ')
                       .Append(Math.Abs(this.X))
                       .Append(this.X >= 0.0 ? 'w' : 'e')
                       .Append(' ')
                       .Append(this.Y)
                       .Append('a')
                       .Append(' ')
                       .Append(this.Direction);
            }


            return builder.ToString();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is Coordinates other && this.Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.Direction.GetHashCode();
                hashCode = (hashCode * 397) ^ this.X.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Y.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Z.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc />
        public bool Equals(Coordinates other)
        {
            return this.Direction.Equals(other.Direction) &&
                   this.X.Equals(other.X)                 &&
                   this.Y.Equals(other.Y)                 &&
                   this.Z.Equals(other.Z);
        }

        #endregion
    }
}
