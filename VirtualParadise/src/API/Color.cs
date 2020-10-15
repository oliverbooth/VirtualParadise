namespace VirtualParadise.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using X10D;

    /// <summary>
    /// Represents a color.
    /// </summary>
    public partial struct Color : IEquatable<Color>,
                                  IEquatable<VpNet.Color>,
                                  IEquatable<System.Drawing.Color>
    {
        /// <summary>
        /// Dictionary for known colors.
        /// </summary>
        private static readonly Dictionary<string, Color> knownColors = new Dictionary<string, Color>();

        /// <summary>
        /// Static constructor for <see cref="Color"/>.
        /// </summary>
        static Color()
        {
            foreach (PropertyInfo member in typeof(Color).GetMembers(BindingFlags.Public | BindingFlags.Static)
                                                         .OfType<PropertyInfo>()) {
                knownColors.Add(member.Name.ToUpperInvariant(), (Color) member.GetValue(null));
            }

            knownColors.Remove("TRANSPARENT");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct by taking the component values.
        /// </summary>
        /// <param name="r">The red component value.</param>
        /// <param name="g">The green component value.</param>
        /// <param name="b">The blue component value.</param>
        public Color(byte r, byte g, byte b) : this(r, g, b, 255) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct by taking the component values.
        /// </summary>
        /// <param name="r">The red component value.</param>
        /// <param name="g">The green component value.</param>
        /// <param name="b">The blue component value.</param>
        /// <param name="a">The alpha component value.</param>
        public Color(byte r, byte g, byte b, byte a)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        /// <summary>
        /// Gets or sets the alpha component of this color.
        /// </summary>
        public byte A { get; set; }

        /// <summary>
        /// Gets or sets the blue component of this color.
        /// </summary>
        public byte B { get; set; }

        /// <summary>
        /// Gets or sets the green component of this color.
        /// </summary>
        public byte G { get; set; }

        /// <summary>
        /// Gets or sets the red component of this color.
        /// </summary>
        public byte R { get; set; }

        /// <summary>
        /// Implicit converts a <see cref="Color"/> to a <see cref="System.Drawing.Color"/>.
        /// </summary>
        /// <param name="c">The <see cref="Color"/> value.</param>
        public static implicit operator System.Drawing.Color(Color c) =>
            System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);

        /// <summary>
        /// Implicit converts a <see cref="System.Drawing.Color"/> to a <see cref="Color"/>.
        /// </summary>
        /// <param name="c">The <see cref="System.Drawing.Color"/> value.</param>
        public static implicit operator Color(System.Drawing.Color c) =>
            new Color(c.R, c.G, c.B, c.A);

        /// <summary>
        /// Implicit converts a <see cref="Color"/> to a <see cref="VpNet.Color"/>.
        /// </summary>
        /// <param name="c">The <see cref="Color"/> value.</param>
        public static implicit operator VpNet.Color(Color c) =>
            new VpNet.Color(c.R, c.G, c.B);

        /// <summary>
        /// Implicit converts a <see cref="VpNet.Color"/> to a <see cref="Color"/>.
        /// </summary>
        /// <param name="c">The <see cref="VpNet.Color"/> value.</param>
        public static implicit operator Color(VpNet.Color c) =>
            new Color(c.R, c.G, c.B);

        /// <summary>
        /// Implicit converts a <see cref="ColorEnum"/> to a <see cref="Color"/> by calling <see cref="FromEnum"/>.
        /// </summary>
        /// <param name="c">The <see cref="ColorEnum"/> value.</param>
        public static implicit operator Color(ColorEnum c) => FromEnum(c);

        /// <summary>
        /// Attempts to implicit converts a <see cref="Color"/> to a <see cref="ColorEnum"/>.
        /// </summary>
        /// <param name="c">The <see cref="Color"/> value.</param>
        public static implicit operator ColorEnum(Color c)
        {
            string str = c.ToString();
            if (str.Length < 8) {
                str += "FF";
            }

            int rgba = Convert.ToInt32(str, 16);
            return (ColorEnum) rgba;
        }

        /// <summary>
        /// Implicit converts a <see cref="String"/> to a <see cref="Color"/> by calling <see cref="FromString"/>.
        /// </summary>
        /// <param name="str">The <see cref="String"/> value.</param>
        public static implicit operator Color(string str) => FromString(str);

        /// <summary>
        /// Implicit converts a <see cref="Color"/> to a <see cref="String"/> by calling <see cref="ToString()"/>.
        /// </summary>
        /// <param name="c">The <see cref="Color"/> value.</param>
        public static implicit operator string(Color c) => c.ToString(false);

        public static bool operator ==(Color a, System.Drawing.Color b) => a.Equals(b);

        public static bool operator !=(Color a, System.Drawing.Color b) => !(a == b);

        public static bool operator ==(System.Drawing.Color a, Color b) => b.Equals(a);

        public static bool operator !=(System.Drawing.Color a, Color b) => !(a == b);

        public static bool operator ==(Color a, VpNet.Color b) => a.Equals(b);

        public static bool operator !=(Color a, VpNet.Color b) => !(a == b);

        public static bool operator ==(VpNet.Color a, Color b) => b.Equals(a);

        public static bool operator !=(VpNet.Color a, Color b) => !(a == b);

        public static bool operator ==(Color a, Color b) => a.Equals(b);

        public static bool operator !=(Color a, Color b) => !(a == b);

        /// <summary>
        /// Converts a known numeric color value to a <see cref="Color"/>.
        /// </summary>
        /// <param name="color">The <see cref="ColorEnum"/> value.</param>
        /// <returns>Returns an instance of <see cref="Color"/>.</returns>
        public static Color FromEnum(ColorEnum color)
        {
            return FromString($"{(long) color:X2}");
        }

        /// <summary>
        /// Converts a hex string or known color name to a <see cref="Color"/>.
        /// </summary>
        /// <param name="str">The input string.</param>
        /// <returns>Returns an instance of <see cref="Color"/>.</returns>
        public static Color FromString(string str)
        {
            if (String.IsNullOrWhiteSpace(str)) {
                str = "white";
            }

            if (knownColors.ContainsKey(str.ToUpperInvariant())) {
                return knownColors.FirstOrDefault(c => c.Key.Equals(str, StringComparison.InvariantCultureIgnoreCase))
                                  .Value;
            }

            str = str.TrimStart('#');

            if (str.Length == 3) {
                StringBuilder builder = new StringBuilder(6);
                for (int i = 0; i < 3; i++) {
                    builder.Append(str[i].Repeat(2));
                }

                str = builder.ToString();
            }

            if (str.Length == 6) {
                str += "FF";
            }

            try {
                long rgba = Convert.ToInt64(str, 16);
                byte r    = (byte) ((rgba >> 24) & 0xff);
                byte g    = (byte) ((rgba >> 16) & 0xff);
                byte b    = (byte) ((rgba >> 8)  & 0xff);
                byte a    = (byte) (rgba         & 0xff);
                return new Color(r, g, b, a);
            } catch {
                return Black;
            }
        }

        /// <inheritdoc />
        public bool Equals(System.Drawing.Color other)
        {
            return this.R == other.R &&
                   this.G == other.G &&
                   this.B == other.B &&
                   this.A == other.A;
        }

        /// <inheritdoc />
        public bool Equals(VpNet.Color other)
        {
            return !(other is null)  &&
                   this.R == other.R &&
                   this.G == other.G &&
                   this.B == other.B;
        }

        /// <inheritdoc />
        public bool Equals(Color other)
        {
            return this.R == other.R &&
                   this.G == other.G &&
                   this.B == other.B &&
                   this.A == other.A;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is Color other && this.Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked {
                int hashCode = this.A.GetHashCode();
                hashCode = (hashCode * 397) ^ this.B.GetHashCode();
                hashCode = (hashCode * 397) ^ this.G.GetHashCode();
                hashCode = (hashCode * 397) ^ this.R.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Returns the color as a hex color string.
        /// </summary>
        public override string ToString()
        {
            string retVal = $"{this.R:X2}{this.G:X2}{this.B:X2}";

            if (this.A < 255) {
                retVal += $"{this.A:X2}";
            }

            return retVal;
        }

        /// <summary>
        /// Returns the color as a hex color string.
        /// </summary>
        /// <param name="asKnownColor">Optional. Whether or not the return value should be a known color name. Defaults
        /// to <see langword="false"/>.</param>
        public string ToString(bool asKnownColor)
        {
            return this.ToString();
        }
    }
}
