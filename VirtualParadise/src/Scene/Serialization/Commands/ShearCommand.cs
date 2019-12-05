namespace VirtualParadise.Scene.Serialization.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #endregion

    /// <summary>
    /// Represents a class which serializes the <c>shear</c> command.
    /// </summary>
    [Command("SHEAR")]
    public class ShearCommand : CommandBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ShearCommand"/> class.
        /// </summary>
        public ShearCommand()
            : this(Array.Empty<string>(), new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShearCommand"/> class.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="properties">The command properties.</param>
        public ShearCommand(IReadOnlyCollection<string> args, Dictionary<string, object> properties)
            : base(args, new Dictionary<string, object>())
        {
            double[] x = {0.0, 0.0};
            double[] y = {0.0, 0.0};
            double[] z = {0.0, 0.0};

            if (!(args is null) && args.Count != 0)
            {
                z[0] = Double.TryParse(args.ElementAt(0), out double zPos) ? zPos : 0.0;
                if (args.Count > 1)
                {
                    x[0] = Double.TryParse(args.ElementAt(1), out double xPos) ? xPos : 0.0;
                }

                if (args.Count > 2)
                {
                    y[0] = Double.TryParse(args.ElementAt(2), out double yPos) ? yPos : 0.0;
                }

                if (args.Count > 3)
                {
                    y[1] = Double.TryParse(args.ElementAt(3), out double yNeg) ? yNeg : 0.0;
                }

                if (args.Count > 4)
                {
                    z[1] = Double.TryParse(args.ElementAt(4), out double zNeg) ? zNeg : 0.0;
                }

                if (args.Count > 5)
                {
                    x[1] = Double.TryParse(args.ElementAt(5), out double xNeg) ? xNeg : 0.0;
                }
            }

            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the X shear.
        /// </summary>
        public IEnumerable<double> X { get; set; }

        /// <summary>
        /// Gets or sets the Y shear.
        /// </summary>
        public IEnumerable<double> Y { get; set; }

        /// <summary>
        /// Gets or sets the Z shear.
        /// </summary>
        public IEnumerable<double> Z { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <remarks>The <c>shear</c> command has an enumerable argument set, and so its <see cref="ToString"/>
        /// implementation differs. Results may be inconsistent with other commands.</remarks>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            bool addOthers = Math.Abs(this.X.ElementAt(0)) > Double.Epsilon &&
                             Math.Abs(this.Y.ElementAt(0)) > Double.Epsilon &&
                             Math.Abs(this.X.ElementAt(1)) > Double.Epsilon &&
                             Math.Abs(this.Y.ElementAt(1)) > Double.Epsilon &&
                             Math.Abs(this.Z.ElementAt(1)) > Double.Epsilon;

            string parameters = addOthers
                ? $" {this.X.ElementAt(0)} {this.Y.ElementAt(0)} {this.Y.ElementAt(1)} {this.Z.ElementAt(1)} {this.X.ElementAt(1)}"
                : String.Empty;

            builder.Append(this.CommandName)
                   .Append(' ')
                   .Append(this.Z.ElementAt(0))
                   .Append(parameters)
                   .Append(this.IsGlobal ? " global" : String.Empty)
                   .Append(this.IsLocked ? " lock" : String.Empty);

            return builder.ToString();
        }

        #endregion
    }
}
