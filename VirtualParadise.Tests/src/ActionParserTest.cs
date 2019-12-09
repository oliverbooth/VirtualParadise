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
            Assert.IsFalse(colorCommand.IsTint);
        }

        [TestMethod]
        public void TestActionBuilderToString()
        {
            ActionBuilder builder = new ActionBuilder();
            builder.AddTrigger(new CreateTrigger())
                   .AddCommand(new NameCommand {Name = "foo"})
                   .AddCommand(new ShearCommand {Z   = new[] {2.0}})
                   .AddTrigger(new ActivateTrigger())
                   .AddCommand(new DiffuseCommand {Intensity = 1.0, TargetName = "foo"})
                   .AddCommand(new ShearCommand
                        {X = new[] {1.0, -1.0}, Y = new[] {2.0, -2.0}, Z = new[] {3.0, -3.0}});

            Action action = builder.Build();

            Assert.AreEqual(
                "create name foo, shear 2; activate diffuse 1 name=foo, shear 3 1 2 -2 -3 -1",
                action.ToString(ActionFormat.None));
        }

        #endregion
    }
}
