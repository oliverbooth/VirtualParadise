namespace VirtualParadise.Examples
{
    using System;
    using Scene.Serialization.Fluent;
    using Action = Scene.Serialization.Action;

    public static class FluentExample
    {
        public static void RunExample()
        {
            // build an action using a fluent API
            Action action = VP.Create()
                              .Texture("stone1.jpg")
                              .Bump()
                              .Texture("stone2.jpg", global: true)
                              .Activate()
                              .Texture("stone3.jpg", global: true);

            // output the action as a string
            Console.WriteLine(action);
        }
    }
}
