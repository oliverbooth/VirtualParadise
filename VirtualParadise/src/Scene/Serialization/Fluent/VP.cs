// ReSharper disable InconsistentNaming

namespace VirtualParadise.Scene.Serialization.Fluent
{
    public class VP
    {
        #region Triggers

        public static FluentVP Activate()
        {
            return new FluentVP().Activate();
        }

        public static FluentVP Adone()
        {
            return new FluentVP().Adone();
        }

        public static FluentVP Bump()
        {
            return new FluentVP().Bump();
        }

        public static FluentVP BumpEnd()
        {
            return new FluentVP().BumpEnd();
        }

        public static FluentVP Create()
        {
            return new FluentVP().Create();
        }

        #endregion
    }
}
