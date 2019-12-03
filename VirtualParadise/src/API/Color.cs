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
    public struct Color : IEquatable<Color>
    {
        #region Fields

        /// <summary>
        /// Dictionary for known colors.
        /// </summary>
        private static readonly Dictionary<string, Color> KnownColors = new Dictionary<string, Color>();

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
                KnownColors.Add(member.Name.ToUpperInvariant(), (Color) member.GetValue(null));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct by taking the component values.
        /// </summary>
        /// <param name="r">The red component value.</param>
        /// <param name="g">The green component value.</param>
        /// <param name="b">The blue component value.</param>
        public Color(byte r, byte g, byte b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        #endregion

        #region Predefined Values

        public static Color Aquamarine => new Color(0x70, 0xDB, 0x93);

        public static Color Black => new Color(0x00, 0x00, 0x00);

        public static Color Blue => new Color(0x00, 0x00, 0xFF);

        public static Color Brass => new Color(0xB5, 0xA6, 0x42);

        public static Color Bronze => new Color(0x8C, 0x78, 0x53);

        public static Color Copper => new Color(0xB8, 0x73, 0x33);

        public static Color Cyan => new Color(0x00, 0xFF, 0xFF);

        public static Color DarkGrey => new Color(0x30, 0x30, 0x30);

        public static Color ForestGreen => new Color(0x23, 0x8E, 0x23);

        public static Color Gold => new Color(0xCD, 0x7F, 0x32);

        public static Color Green => new Color(0x00, 0xFF, 0x00);

        public static Color Grey => new Color(0x70, 0x70, 0x70);

        public static Color LightGrey => new Color(0xC0, 0xC0, 0xC0);

        public static Color Magenta => new Color(0xFF, 0x00, 0xFF);

        public static Color Maroon => new Color(0x8E, 0x23, 0x6B);

        public static Color NavyBlue => new Color(0x23, 0x23, 0x8E);

        public static Color Orange => new Color(0xFF, 0x7F, 0x00);

        public static Color OrangeRed => new Color(0xFF, 0x24, 0x00);

        public static Color Orchid => new Color(0xDB, 0x70, 0xDB);

        public static Color Pink => new Color(0xFF, 0x6E, 0xC7);

        public static Color Red => new Color(0xFF, 0x00, 0x00);

        public static Color Salmon => new Color(0x6F, 0x42, 0x42);

        public static Color Scarlet => new Color(0x8C, 0x17, 0x17);

        public static Color Silver => new Color(0xE6, 0xE8, 0xFA);

        public static Color SkyBlue => new Color(0x32, 0x99, 0xCC);

        public static Color Tan => new Color(0xDB, 0x93, 0x70);

        public static Color Teal => new Color(0x00, 0x70, 0x70);

        public static Color Turquoise => new Color(0xAD, 0xEA, 0xEA);

        public static Color Violet => new Color(0x4F, 0x2F, 0x4F);

        public static Color White => new Color(0xFF, 0xFF, 0xFF);

        public static Color Yellow => new Color(0xFF, 0xFF, 0x00);

        #endregion

        #region Properties

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

            if (KnownColors.ContainsKey(str.ToUpperInvariant()))
            {
                return KnownColors.FirstOrDefault(c => c.Key.Equals(str, StringComparison.InvariantCultureIgnoreCase))
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

            try
            {
                int  rgb = Convert.ToInt32(str, 16);
                byte r   = (byte) ((rgb >> 16) & 0xff);
                byte g   = (byte) ((rgb >> 8)  & 0xff);
                byte b   = (byte) (rgb         & 0xff);
                return new Color(r, g, b);
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
                   this.B == other.B;
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
                int hashCode = this.B.GetHashCode();
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
                    return $"{this.R:X2}{this.G:X2}{this.B:X2}";
                }

                Color  that = this;
                string key;
                if (!String.IsNullOrWhiteSpace(key = KnownColors
                                                    .FirstOrDefault(c => that.R == c.Value.R &&
                                                                         that.G == c.Value.G &&
                                                                         that.B == c.Value.B)
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
