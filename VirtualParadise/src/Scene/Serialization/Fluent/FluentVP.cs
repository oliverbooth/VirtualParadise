// ReSharper disable InconsistentNaming

namespace VirtualParadise.Scene.Serialization.Fluent
{
    #region Using Directives

    using System;
    using API;
    using Commands;
    using Triggers;
    using Action = Action;

    #endregion

    public class FluentVP
    {
        #region Fields

        /// <summary>
        /// The underlying <see cref="ActionBuilder"/> instance.
        /// </summary>
        internal readonly ActionBuilder actionBuilder = new ActionBuilder();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentVP"/> class.
        /// </summary>
        internal FluentVP() { }

        #endregion

        #region Conversions

        public static implicit operator Action(FluentVP vp) => vp.actionBuilder.Build();

        public static implicit operator string(FluentVP vp) => vp.ToString();

        #endregion

        #region Triggers

        public FluentVP Activate()
        {
            this.actionBuilder.AddTrigger(new ActivateTrigger());
            return this;
        }

        public FluentVP Adone()
        {
            this.actionBuilder.AddTrigger(new AdoneTrigger());
            return this;
        }

        public FluentVP Bump()
        {
            this.actionBuilder.AddTrigger(new BumpTrigger());
            return this;
        }

        public FluentVP BumpEnd()
        {
            this.actionBuilder.AddTrigger(new BumpEndTrigger());
            return this;
        }

        public FluentVP Create()
        {
            this.actionBuilder.AddTrigger(new CreateTrigger());
            return this;
        }

        #endregion

        #region Commands

        public FluentVP Animate(string animation  = ".", string name = "me", string tag = "",
                                int    imageCount = 1,
                                int    frameCount = 1, int frameDelay = 0, int[] frames = null,
                                bool   mask       = false,
                                bool   global     = false, bool locked = false, string targetName = "")
        {
            frames??=Array.Empty<int>();

            this.actionBuilder.AddCommand(new AnimateCommand {
                Animation  = animation, Name       = name, ImageCount   = imageCount, FrameCount = frameCount,
                FrameDelay = frameDelay, FrameList = frames, IsMask     = mask, Tag              = tag,
                IsGlobal   = global, IsLocked      = locked, TargetName = targetName
            });
            return this;
        }

        public FluentVP Astart(string name, bool loop = false, bool global = false, bool locked = false,
                               string targetName = "")
        {
            this.actionBuilder.AddCommand(new AstartCommand
                {Name = name, Loop = loop, IsGlobal = global, IsLocked = locked, TargetName = targetName});
            return this;
        }

        public FluentVP Astop(string name, bool global = false, bool locked = false,
                              string targetName = "")
        {
            this.actionBuilder.AddCommand(new AstopCommand
                {Name = name, IsGlobal = global, IsLocked = locked, TargetName = targetName});
            return this;
        }

        public FluentVP Color(Color color,          bool tint   = false, string tag        = "",
                              bool  global = false, bool locked = false, string targetName = "")
        {
            this.actionBuilder.AddCommand(new ColorCommand {
                Color    = color, IsTint    = tint, Tag          = tag,
                IsGlobal = global, IsLocked = locked, TargetName = targetName
            });
            return this;
        }

        public FluentVP FrameRate(int    frameRate, bool global = false, bool locked = false,
                                  string targetName = "")
        {
            this.actionBuilder.AddCommand(new FrameRateCommand
                {Value = frameRate, IsGlobal = global, IsLocked = locked, TargetName = targetName});
            return this;
        }

        public FluentVP Move(double x          = 0.0,   double y      = 0.0,   double z      = 0.0,
                             bool   smooth     = false, bool   reset  = false, bool   sync   = false,
                             bool   localAxis  = false, bool   loop   = false, double time   = 1.0,
                             double wait       = 0.0,   bool   global = false, bool   locked = false,
                             string targetName = "")
        {
            this.actionBuilder.AddCommand(new MoveCommand {
                X           = x, Y                  = y, Z            = z, Time = time, Wait = wait,
                IsSmooth    = smooth, ShouldReset   = reset, IsGlobal = global,
                IsLocalAxis = localAxis, ShouldSync = sync, IsLooping = loop,
                IsLocked    = locked,
                TargetName  = targetName
            });
            return this;
        }

        public FluentVP Name(string name, bool global = false, bool locked = false, string targetName = "")
        {
            this.actionBuilder.AddCommand(new NameCommand
                {Name = name, IsGlobal = global, IsLocked = locked, TargetName = targetName});
            return this;
        }

        public FluentVP NormalMap(string texture,        string mask       = "", string tag = "", bool global = false,
                                  bool   locked = false, string targetName = "")
        {
            this.actionBuilder.AddCommand(new NormalMapCommand {
                Texture    = texture,
                Mask       = mask,
                IsGlobal   = global,
                Tag        = tag,
                IsLocked   = locked,
                TargetName = targetName
            });
            return this;
        }

        public FluentVP Rotate(double x          = 0.0,   double y      = 0.0,   double z      = 0.0,
                               bool   smooth     = false, bool   reset  = false, bool   sync   = false,
                               bool   localAxis  = false, bool   loop   = false, double time   = 1.0,
                               double wait       = 0.0,   bool   global = false, bool   locked = false,
                               string targetName = "")
        {
            this.actionBuilder.AddCommand(new RotateCommand {
                X           = x,
                Y           = y,
                Z           = z,
                Time        = time,
                Wait        = wait,
                IsSmooth    = smooth,
                ShouldReset = reset,
                IsGlobal    = global,
                IsLocalAxis = localAxis,
                ShouldSync  = sync,
                IsLooping   = loop,
                IsLocked    = locked,
                TargetName  = targetName
            });
            return this;
        }

        public FluentVP Scale(double scale  = 1.0,   double x      = 1.0,   double y          = 1.0, double z = 1.0,
                              bool   global = false, bool   locked = false, string targetName = "")
        {
            if (Math.Abs(scale - 1.0) > Double.Epsilon &&
                (Math.Abs(scale - x) > Double.Epsilon ||
                 Math.Abs(scale - y) > Double.Epsilon ||
                 Math.Abs(scale - z) > Double.Epsilon)) {
                x = y = z = scale;
            }

            this.actionBuilder.AddCommand(new ScaleCommand
                {X = x, Y = y, Z = z, IsGlobal = global, IsLocked = locked, TargetName = targetName});
            return this;
        }

        public FluentVP Solid(bool solid, bool global = false, bool locked = false, string targetName = "")
        {
            this.actionBuilder.AddCommand(new SolidCommand
                {IsSolid = solid, IsGlobal = global, IsLocked = locked, TargetName = targetName});
            return this;
        }

        public FluentVP SpecularMap(string texture,        string mask       = "", string tag = "", bool global = false,
                                    bool   locked = false, string targetName = "")
        {
            this.actionBuilder.AddCommand(new SpecularMapCommand {
                Texture    = texture,
                Mask       = mask,
                IsGlobal   = global,
                Tag        = tag,
                IsLocked   = locked,
                TargetName = targetName
            });
            return this;
        }

        public FluentVP Teleport(Coordinates coordinates, bool global = false, bool locked = false,
                                 string      targetName = "")
        {
            this.actionBuilder.AddCommand(new TeleportCommand
                {Coordinates = coordinates, IsGlobal = global, IsLocked = locked, TargetName = targetName});
            return this;
        }

        public FluentVP TeleportXyz(double x,              double y, double z, double yaw = 0.0, bool global = false,
                                    bool   locked = false, string targetName = "")
        {
            this.actionBuilder.AddCommand(new TeleportXyzCommand {
                X        = x, Y             = y, Z               = z, Direction = yaw,
                IsGlobal = global, IsLocked = locked, TargetName = targetName
            });
            return this;
        }

        public FluentVP Texture(string texture,        string mask       = "", string tag = "", bool global = false,
                                bool   locked = false, string targetName = "")
        {
            this.actionBuilder.AddCommand(new TextureCommand {
                Texture  = texture, Mask      = mask, IsGlobal = global, Tag = tag,
                IsLocked = locked, TargetName = targetName
            });
            return this;
        }

        public FluentVP Visible(bool visible, bool global = false, bool locked = false, string targetName = "")
        {
            this.actionBuilder.AddCommand(new VisibleCommand
                {IsVisible = visible, IsGlobal = global, IsLocked = locked, TargetName = targetName});
            return this;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override string ToString()
        {
            return this.actionBuilder.Build().ToString();
        }

        #endregion
    }
}
