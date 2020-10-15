// ReSharper disable InconsistentNaming

namespace VirtualParadise.Scene.Serialization.Fluent
{
    /// <summary>
    /// Represents a class which implements a Fluent-esque API.
    /// </summary>
    public static class VP
    {
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
    }
}
