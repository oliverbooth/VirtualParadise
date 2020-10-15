// ReSharper disable StringLiteralTypo

namespace VirtualParadise.Examples
{
    using System;
    using Scene.Sequencing;
    using Scene.Serialization.Fluent;
    using Action = Scene.Serialization.Action;

    public static class SequencingExample
    {
        public static void RunExample()
        {
            SequenceBuilder builder = new SequenceBuilder("textureChanger");

            builder.Append(VP.Adone().Texture("stone1.jpg", targetName: "myPp01"), TimeSpan.FromSeconds(1));
            builder.Append(VP.Adone().Texture("stone2.jpg", targetName: "myPp01"), TimeSpan.FromSeconds(1));
            builder.Append(VP.Adone().Texture("stone3.jpg", targetName: "myPp01"), TimeSpan.FromSeconds(1));

            foreach (Action action in builder.Build().ToActions()) {
                Console.WriteLine(action);
            }
        }
    }
}
