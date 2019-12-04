namespace VirtualParadise.Tests
{
    #region Using Directives

    using System.Linq;
    using API;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Scene.Serialization;
    using Scene.Serialization.Commands;
    using Scene.Serialization.Triggers;

    #endregion

    [TestClass]
    public class ActionParserTest
    {
        #region Methods

        [TestMethod]
        public void TestActivateTexture()
        {
            Action      action       = Action.Parse("activate texture stone1 global");
            TriggerBase firstTrigger = action.Triggers.First();
            CommandBase firstCommand = firstTrigger.Commands.First();

            Assert.IsTrue(firstTrigger is ActivateTrigger);
            Assert.IsTrue(firstCommand is TextureCommand);

            TextureCommand textureCommand = firstCommand as TextureCommand;
            Assert.IsNotNull(textureCommand);
            Assert.AreEqual("stone1", textureCommand.Texture);
            Assert.IsTrue(textureCommand.IsGlobal);
        }

        [TestMethod]
        public void TestCreateSign()
        {
            Action      action       = Action.Parse("create sign \"Hello World\" bcolor=blue color=red");
            TriggerBase firstTrigger = action.Triggers.First();
            CommandBase firstCommand = firstTrigger.Commands.First();

            Assert.IsTrue(firstTrigger is CreateTrigger);
            Assert.IsTrue(firstCommand is SignCommand);

            SignCommand signCommand = firstCommand as SignCommand;
            Assert.IsNotNull(signCommand);
            Assert.AreEqual("Hello World", signCommand.Text);
            Assert.AreEqual(Color.Blue,    signCommand.BackColor);
            Assert.AreEqual(Color.Red,     signCommand.ForeColor);
            Assert.IsFalse(signCommand.IsGlobal);
        }

        [TestMethod]
        public void TestMultipleCreateCommands()
        {
            Action      action        = Action.Parse("create name foo, color red");
            TriggerBase firstTrigger  = action.Triggers.First();
            CommandBase firstCommand  = firstTrigger.Commands.First();
            CommandBase secondCommand = firstTrigger.Commands.ElementAt(1);

            Assert.IsTrue(firstTrigger is CreateTrigger);
            Assert.IsTrue(firstCommand is NameCommand);
            Assert.IsTrue(secondCommand is ColorCommand);

            NameCommand  nameCommand  = firstCommand as NameCommand;
            ColorCommand colorCommand = secondCommand as ColorCommand;

            Assert.IsNotNull(nameCommand);
            Assert.AreEqual("foo",     nameCommand.Name);
            Assert.AreEqual(Color.Red, colorCommand.Color);
            Assert.IsFalse(colorCommand.Tint);
        }

        [TestMethod]
        public void TestActionBuilderToString()
        {
            ActionBuilder builder = new ActionBuilder();
            builder.AddTrigger(new CreateTrigger())
                   .AddCommand(new NameCommand {Name = "foo"})
                   .AddTrigger(new ActivateTrigger())
                   .AddCommand(new DiffuseCommand {Intensity = 1.0, TargetName = "foo"});

            Action action = builder.Build();
            Assert.AreEqual(
                "create name foo; activate diffuse 1 name=foo",
                action.ToString(ActionFormat.None),
                false);
        }

        [TestMethod]
        public void TestTeleportCommand()
        {
            Action      action       = Action.Parse("activate teleport asdf 25n 2.6e");
            TriggerBase firstTrigger = action.Triggers.First();
            CommandBase firstCommand = firstTrigger.Commands.First();

            TeleportCommand teleportCommand = firstCommand as TeleportCommand;

            Assert.IsNotNull(teleportCommand);
            Assert.IsTrue(firstTrigger is ActivateTrigger);

            CoordinateParserTest.TestCoordinates(teleportCommand.Coordinates.ToString(),
                -2.6, 0.0, 25.0, 0.0, false, "asdf");
        }

        [TestMethod]
        public void TestScaleCommand()
        {
            Action       action       = Action.Parse("create scale 2 0.5 3");
            TriggerBase  firstTrigger = action.Triggers.First();
            CommandBase  firstCommand = firstTrigger.Commands.First();
            ScaleCommand scaleCommand = firstCommand as ScaleCommand;

            Assert.IsNotNull(scaleCommand);
            Assert.AreEqual(2.0, scaleCommand.X);
            Assert.AreEqual(0.5, scaleCommand.Y);
            Assert.AreEqual(3.0, scaleCommand.Z);

            action       = Action.Parse("create scale 1.5");
            firstTrigger = action.Triggers.First();
            firstCommand = firstTrigger.Commands.First();
            scaleCommand = firstCommand as ScaleCommand;

            Assert.IsNotNull(scaleCommand);
            Assert.AreEqual(1.5, scaleCommand.X);
            Assert.AreEqual(1.5, scaleCommand.Y);
            Assert.AreEqual(1.5, scaleCommand.Z);
        }

        #endregion
    }
}
