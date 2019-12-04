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
        public void TestMultipleCreateCommands()
        {
            Action      action        = Action.Parse("create name foo,   color  red;activate specular;");
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
                action.ToString(ActionFormat.None));
        }

        #endregion
    }
}
