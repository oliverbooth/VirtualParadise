namespace VirtualParadise.API
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using X10D;

    #endregion

    /// <summary>
    /// Represents a color.
    /// </summary>
    public partial struct Color : IEquatable<Color>
    {
        #region Fields

        /// <summary>
        /// Dictionary for known colors.
        /// </summary>
        private static readonly Dictionary<string, Color> knownColors = new Dictionary<string, Color>();

        #endregion

        #region Constructors

        /// <summary>
        /// Static constructor for <see cref="Color"/>.
        /// </summary>
        static Color()
        {
            foreach (PropertyInfo member in typeof(Color).GetMembers(BindingFlags.Public | BindingFlags.Static)
                                                         .OfType<PropertyInfo>())
            {
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
        public Color(byte r, byte g, byte b) : this(r, g, b, 255)
        {
        }

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

        #endregion

        #region Properties

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

        #endregion

        #region Operators

        public static implicit operator Color(string str) => FromString(str);

        public static implicit operator string(Color c) => c.ToString(false);

        public static bool operator ==(Color a, Color b) => a.Equals(b);

        public static bool operator !=(Color a, Color b) => !(a == b);

        #endregion

        #region Methods

        /// <summary>
        /// Converts a hex string or known color name to a <see cref="Color"/>.
        /// </summary>
        /// <param name="str">The input string.</param>
        /// <returns>Returns an instance of <see cref="Color"/>.</returns>
        public static Color FromString(string str)
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                str = "white";
            }

            if (knownColors.ContainsKey(str.ToUpperInvariant()))
            {
                return knownColors.FirstOrDefault(c => c.Key.Equals(str, StringComparison.InvariantCultureIgnoreCase))
                                  .Value;
            }

            str = str.TrimStart('#');

            if (str.Length == 3)
            {
                StringBuilder builder = new StringBuilder(6);
                for (int i = 0; i < 3; i++)
                {
                    builder.Append(str[i].Repeat(2));
                }

                str = builder.ToString();
            }

            if (str.Length == 4)
            {
                str += "FF";
            }

            try
            {
                long rgb = Convert.ToInt64(str, 16);
                byte r   = (byte) ((rgb >> 32) & 0xff);
                byte g   = (byte) ((rgb >> 16) & 0xff);
                byte b   = (byte) ((rgb >> 8)  & 0xff);
                byte a   = (byte) (rgb         & 0xff);
                return new Color(r, g, b, a);
            }
            catch
            {
                return Black;
            }
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
            unchecked
            {
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
            return this.ToString(false);
        }

        /// <summary>
        /// Returns the color as a hex color string.
        /// </summary>
        /// <param name="asKnownColor">Optional. Whether or not the return value should be a known color name. Defaults
        /// to <see langword="false"/>.</param>
        public string ToString(bool asKnownColor)
        {
            while (true)
            {
                if (!asKnownColor)
                {
                    string retVal = $"{this.R:X2}{this.G:X2}{this.B:X2}";

                    if (this.A < 255)
                    {
                        retVal += $"{this.A:X2}";
                    }

                    return retVal;
                }

                Color  that = this;
                string key;
                if (!String.IsNullOrWhiteSpace(key = knownColors
                                                    .FirstOrDefault(c => that.R == c.Value.R &&
                                                                         that.G == c.Value.G &&
                                                                         that.B == c.Value.B &&
                                                                         that.B == c.Value.A)
                                                    .Key))

                {
                    return key;
                }

                asKnownColor = false;
            }
        }

        #endregion
    }
}
