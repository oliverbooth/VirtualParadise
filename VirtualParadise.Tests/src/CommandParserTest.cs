namespace VirtualParadise.Tests
{
    #region Using Directives

    using System;
    using System.Linq;
    using API;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Scene.Serialization.Commands;
    using Action = Scene.Serialization.Action;

    #endregion

    [TestClass]
    public class CommandParserTest
    {
        #region Methods

        /// <summary>
        /// Tests the <c>animate</c> command.
        /// </summary>
        [TestMethod]
        public void TestAnimate()
        {
            {
                Action         action  = Action.Parse("create animate tag=foo mask me jump 5 9 100 1 2 3 4 5 4 3 2 1;");
                AnimateCommand animate = action.Create.GetCommandOfType<AnimateCommand>();

                Assert.IsNotNull(animate);
                Assert.AreEqual("foo", animate.Tag);
                Assert.IsTrue(animate.IsMask);
                Assert.AreEqual("me",   animate.Name);
                Assert.AreEqual("jump", animate.Animation);
                Assert.AreEqual(5,      animate.ImageCount);
                Assert.AreEqual(9,      animate.FrameCount);
                Assert.AreEqual(100,    animate.FrameDelay);
                CollectionAssert.AreEqual(
                    new[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 }, animate.FrameList.ToArray());
                Assert.IsFalse(animate.IsGlobal);
            }

            {
                Action         action  = Action.Parse("create animate me jump 5 3 100 1 2 1 global;");
                AnimateCommand animate = action.Create.GetCommandOfType<AnimateCommand>();

                Assert.IsNotNull(animate);
                Assert.IsFalse(animate.IsMask);
                Assert.AreEqual("me",   animate.Name);
                Assert.AreEqual("jump", animate.Animation);
                Assert.AreEqual(5,      animate.ImageCount);
                Assert.AreEqual(3,      animate.FrameCount);
                Assert.AreEqual(100,    animate.FrameDelay);
                CollectionAssert.AreEqual(
                    new[] { 1, 2, 1 }, animate.FrameList.ToArray());
                Assert.IsTrue(animate.IsGlobal);
            }
        }

        [TestMethod]
        public void TestAstartLooping()
        {
            Action        action = Action.Parse("create astart foo looping");
            AstartCommand astart = action.Create.GetCommandOfType<AstartCommand>();

            Assert.IsNotNull(astart);
            Assert.IsTrue(astart.Loop);
            Assert.AreEqual("foo", astart.Name);
        }

        [TestMethod]
        public void TestAstartNotLooping()
        {
            Action        action = Action.Parse("create astart bar");
            AstartCommand astart = action.Create.GetCommandOfType<AstartCommand>();

            Assert.IsNotNull(astart);
            Assert.IsFalse(astart.Loop);
            Assert.AreEqual("bar", astart.Name);
        }

        [TestMethod]
        public void TestAstop()
        {
            Action       action = Action.Parse("create astop foo");
            AstopCommand astop  = action.Create.GetCommandOfType<AstopCommand>();

            Assert.IsNotNull(astop);
            Assert.AreEqual("foo", astop.Name);
        }

        [TestMethod]
        public void TestAmbientWithCustomValues()
        {
            Action         action  = Action.Parse("create ambient 0.5 tag=foo");
            AmbientCommand ambient = action.Create.GetCommandOfType<AmbientCommand>();

            Assert.IsNotNull(ambient);
            Assert.AreEqual(0.5,   ambient.Intensity);
            Assert.AreEqual("foo", ambient.Tag);
        }

        [TestMethod]
        public void TestAmbientWithDefaultValues()
        {
            Action         action  = Action.Parse("create ambient");
            AmbientCommand ambient = action.Create.GetCommandOfType<AmbientCommand>();

            Assert.IsNotNull(ambient);
            Assert.AreEqual(1.0,          ambient.Intensity);
            Assert.AreEqual(String.Empty, ambient.Tag);
        }

        [TestMethod]
        public void TestDiffuseWithCustomValues()
        {
            Action         action  = Action.Parse("create diffuse 1.0 tag=foo");
            DiffuseCommand diffuse = action.Create.GetCommandOfType<DiffuseCommand>();

            Assert.IsNotNull(diffuse);
            Assert.AreEqual(1.0,   diffuse.Intensity);
            Assert.AreEqual("foo", diffuse.Tag);
        }

        [TestMethod]
        public void TestDiffuseWithDefaultValues()
        {
            Action         action  = Action.Parse("create diffuse");
            DiffuseCommand diffuse = action.Create.GetCommandOfType<DiffuseCommand>();

            Assert.IsNotNull(diffuse);
            Assert.AreEqual(0.5,          diffuse.Intensity);
            Assert.AreEqual(String.Empty, diffuse.Tag);
        }

        [TestMethod]
        public void TestNameWithCustomValues()
        {
            Action      action = Action.Parse("create name foo name=bar global");
            NameCommand name   = action.Create.GetCommandOfType<NameCommand>();

            Assert.IsNotNull(name);
            Assert.AreEqual("name", name.CommandName, true);
            Assert.AreEqual("foo",  name.Name);
            Assert.AreEqual("bar",  name.TargetName);
            Assert.IsTrue(name.IsGlobal);
        }

        [TestMethod]
        public void TestNameWithDefaultValues()
        {
            Action      action = Action.Parse("create name");
            NameCommand name   = action.Create.GetCommandOfType<NameCommand>();

            Assert.IsNotNull(name);
            Assert.AreEqual("name",       name.CommandName, true);
            Assert.AreEqual(String.Empty, name.Name);
            Assert.AreEqual(String.Empty, name.TargetName);
            Assert.IsFalse(name.IsGlobal);
        }

        [TestMethod]
        public void TestSignWithCustomValues()
        {
            Action action =
                Action.Parse(
                    "create sign color=red \"Hello World\" bcolor=white align=left shadow name=foo global lock");
            SignCommand sign = action.Create.GetCommandOfType<SignCommand>();

            Assert.IsNotNull(sign);
            Assert.AreEqual("sign",             sign.CommandName, true);
            Assert.AreEqual("Hello World",      sign.Text);
            Assert.AreEqual(Color.White,        sign.BackColor);
            Assert.AreEqual(Color.Red,          sign.ForeColor);
            Assert.AreEqual("foo",              sign.TargetName);
            Assert.AreEqual(TextAlignment.Left, sign.Alignment);
            Assert.IsTrue(sign.Shadow);
            Assert.IsTrue(sign.IsGlobal);
            Assert.IsTrue(sign.IsLocked);
        }

        [TestMethod]
        public void TestSignWithDefaultValues()
        {
            Action      action = Action.Parse("create sign");
            SignCommand sign   = action.Create.GetCommandOfType<SignCommand>();

            Assert.IsNotNull(sign);
            Assert.AreEqual("sign",                     sign.CommandName, true);
            Assert.AreEqual(String.Empty,               sign.Text);
            Assert.AreEqual(Color.DefaultSignBackColor, sign.BackColor);
            Assert.AreEqual(Color.White,                sign.ForeColor);
            Assert.AreEqual(String.Empty,               sign.TargetName);
            Assert.AreEqual(TextAlignment.Center,       sign.Alignment);
            Assert.IsFalse(sign.Shadow);
            Assert.IsFalse(sign.IsGlobal);
            Assert.IsFalse(sign.IsLocked);
        }

        [TestMethod]
        public void TestVisibleWithCustomValues()
        {
            Action         action  = Action.Parse("create visible foo off");
            VisibleCommand visible = action.Create.GetCommandOfType<VisibleCommand>();

            Assert.IsNotNull(visible);
            Assert.IsFalse(visible.IsVisible);
            Assert.AreEqual("foo", visible.TargetName);
            Assert.AreEqual(-1.0,  visible.Radius);

            action  = Action.Parse("create visible true");
            visible = action.Create.GetCommandOfType<VisibleCommand>();

            Assert.IsNotNull(visible);
            Assert.IsTrue(visible.IsVisible);
            Assert.AreEqual(String.Empty, visible.TargetName);
            Assert.AreEqual(-1.0,         visible.Radius);

            action  = Action.Parse("create visible no name=bar");
            visible = action.Create.GetCommandOfType<VisibleCommand>();

            Assert.IsNotNull(visible);
            Assert.IsFalse(visible.IsVisible);
            Assert.AreEqual("bar", visible.TargetName);
            Assert.AreEqual(-1.0,  visible.Radius);

            action  = Action.Parse("create visible foobar 1 radius=10");
            visible = action.Create.GetCommandOfType<VisibleCommand>();

            Assert.IsNotNull(visible);
            Assert.IsTrue(visible.IsVisible);
            Assert.AreEqual("foobar", visible.TargetName);
            Assert.AreEqual(10.0,     visible.Radius);
        }

        [TestMethod]
        public void TestVisibleWithDefaultValues()
        {
            Action         action  = Action.Parse("create visible");
            VisibleCommand visible = action.Create.GetCommandOfType<VisibleCommand>();

            Assert.IsNotNull(visible);
            Assert.IsTrue(visible.IsVisible);
            Assert.AreEqual(String.Empty, visible.TargetName);
            Assert.AreEqual(-1.0,         visible.Radius);
        }

        #endregion
    }
}
