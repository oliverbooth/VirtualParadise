namespace VirtualParadise.Tests.SerializationTests
{
    #region Using Directives

    using System;
    using System.Linq;
    using API;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Scene.Serialization.Commands;
    using Action = Scene.Serialization.Action;

    #endregion

    /// <summary>
    /// Tests for command serialization.
    /// </summary>
    [TestClass]
    public class CommandTests
    {
        #region Methods

        /// <summary>
        /// Tests the <c>ambient</c> command.
        /// </summary>
        [TestMethod]
        public void TestAmbient()
        {
            {
                Action         action  = Action.Parse("create ambient;");
                CommandBase    command = action.Triggers.First().Commands.First();
                AmbientCommand ambient = command as AmbientCommand;

                Assert.IsNotNull(ambient);
                Assert.AreEqual(1.0, ambient.Intensity);
            }

            {
                Action         action  = Action.Parse("create ambient 0.8 tag=foo;");
                CommandBase    command = action.Triggers.First().Commands.First();
                AmbientCommand ambient = command as AmbientCommand;

                Assert.IsNotNull(ambient);
                Assert.AreEqual(0.8,   ambient.Intensity);
                Assert.AreEqual("foo", ambient.Tag);
            }
        }

        /// <summary>
        /// Tests the <c>animate</c> command.
        /// </summary>
        [TestMethod]
        public void TestAnimate()
        {
            {
                Action         action  = Action.Parse("create animate tag=foo mask me jump 5 9 100 1 2 3 4 5 4 3 2 1;");
                CommandBase    command = action.Triggers.First().Commands.First();
                AnimateCommand animate = command as AnimateCommand;

                Assert.IsNotNull(animate);
                Assert.AreEqual("foo", animate.Tag);
                Assert.IsTrue(animate.Mask);
                Assert.AreEqual("me",   animate.Name);
                Assert.AreEqual("jump", animate.Animation);
                Assert.AreEqual(5,      animate.ImageCount);
                Assert.AreEqual(9,      animate.FrameCount);
                Assert.AreEqual(100,    animate.FrameDelay);
                CollectionAssert.AreEqual(
                    new[] {1, 2, 3, 4, 5, 4, 3, 2, 1}, animate.FrameList.ToArray());
                Assert.IsFalse(animate.IsGlobal);
            }

            {
                Action         action  = Action.Parse("create animate me jump 5 9 100 1 2 3 4 5 4 3 2 1 global;");
                CommandBase    command = action.Triggers.First().Commands.First();
                AnimateCommand animate = command as AnimateCommand;

                Assert.IsNotNull(animate);
                Assert.IsFalse(animate.Mask);
                Assert.AreEqual("me",   animate.Name);
                Assert.AreEqual("jump", animate.Animation);
                Assert.AreEqual(5,      animate.ImageCount);
                Assert.AreEqual(9,      animate.FrameCount);
                Assert.AreEqual(100,    animate.FrameDelay);
                CollectionAssert.AreEqual(
                    new[] {1, 2, 3, 4, 5, 4, 3, 2, 1}, animate.FrameList.ToArray());
                Assert.IsTrue(animate.IsGlobal);
            }
        }

        /// <summary>
        /// Tests the <c>astart</c> command.
        /// </summary>
        [TestMethod]
        public void TestAstart()
        {
            {
                Action        action  = Action.Parse("create astart anim1;");
                CommandBase   command = action.Triggers.First().Commands.First();
                AstartCommand astart  = command as AstartCommand;

                Assert.IsNotNull(astart);
                Assert.AreEqual("anim1", astart.Name);
                Assert.IsFalse(astart.Loop);
            }

            {
                Action        action  = Action.Parse("create astart anim1 looping;");
                CommandBase   command = action.Triggers.First().Commands.First();
                AstartCommand astart  = command as AstartCommand;

                Assert.IsNotNull(astart);
                Assert.AreEqual("anim1", astart.Name);
                Assert.IsTrue(astart.Loop);
            }
        }

        /// <summary>
        /// Tests the <c>astop</c> command.
        /// </summary>
        [TestMethod]
        public void TestAstop()
        {
            Action       action  = Action.Parse("create astop anim1;");
            CommandBase  command = action.Triggers.First().Commands.First();
            AstopCommand astop   = command as AstopCommand;

            Assert.IsNotNull(astop);
            Assert.AreEqual("anim1", astop.Name);
        }

        /// <summary>
        /// Tests the <c>color</c> command.
        /// </summary>
        [TestMethod]
        public void TestColor()
        {
            {
                Action       action  = Action.Parse("activate color blue global;");
                CommandBase  command = action.Triggers.First().Commands.First();
                ColorCommand color   = command as ColorCommand;

                Assert.IsNotNull(color);
                Assert.AreEqual(Color.Blue, color.Color);
                Assert.IsTrue(color.IsGlobal);
            }

            {
                Action       action  = Action.Parse("create color tint 238e23 tag=foo;");
                CommandBase  command = action.Triggers.First().Commands.First();
                ColorCommand color   = command as ColorCommand;

                Assert.IsNotNull(color);
                Assert.IsTrue(color.Tint);
                Assert.AreEqual("foo",             color.Tag);
                Assert.AreEqual(Color.ForestGreen, color.Color);
            }
        }

        /// <summary>
        /// Tests the <c>diffuse</c> command.
        /// </summary>
        [TestMethod]
        public void TestDiffuse()
        {
            {
                Action         action  = Action.Parse("create  diffuse;");
                CommandBase    command = action.Triggers.First().Commands.First();
                DiffuseCommand diffuse = command as DiffuseCommand;

                Assert.IsNotNull(diffuse);
                Assert.AreEqual(0.5, diffuse.Intensity);
            }

            {
                Action         action  = Action.Parse("bump diffuse 0.12345;");
                CommandBase    command = action.Triggers.First().Commands.First();
                DiffuseCommand diffuse = command as DiffuseCommand;

                Assert.IsNotNull(diffuse);
                Assert.AreEqual(0.12345, diffuse.Intensity);
            }

            {
                Action         action  = Action.Parse(" activate diffuse 1.0 tag=foo lock;");
                CommandBase    command = action.Triggers.First().Commands.First();
                DiffuseCommand diffuse = command as DiffuseCommand;

                Assert.IsNotNull(diffuse);
                Assert.AreEqual("foo", diffuse.Tag);
                Assert.AreEqual(1.0,   diffuse.Intensity);
                Assert.IsTrue(diffuse.IsLocked);
            }
        }

        /// <summary>
        /// Tests the <c>name</c> command.
        /// </summary>
        [TestMethod]
        public void TestName()
        {
            {
                Action      action  = Action.Parse("create name foo;");
                CommandBase command = action.Triggers.First().Commands.First();
                NameCommand name    = command as NameCommand;

                Assert.IsNotNull(name);
                Assert.AreEqual("foo", name.Name);
            }

            {
                Action      action  = Action.Parse("activate   name bar name=foo;");
                CommandBase command = action.Triggers.First().Commands.First();
                NameCommand name    = command as NameCommand;

                Assert.IsNotNull(name);
                Assert.AreEqual("bar", name.Name);
                Assert.AreEqual("foo", name.TargetName);
            }
        }

        /// <summary>
        /// Tests the <c>noise</c> command.
        /// </summary>
        [TestMethod]
        public void TestNoise()
        {
            {
                Action       action  = Action.Parse("create noise ambient1 loop volume=0.8");
                CommandBase  command = action.Triggers.First().Commands.First();
                NoiseCommand noise   = command as NoiseCommand;

                Assert.IsNotNull(noise);
                Assert.IsTrue(noise.Loop);
                Assert.AreEqual("ambient1", noise.FileName);
                Assert.AreEqual(0.8,        noise.Volume);
            }
        }

        /// <summary>
        /// Tests the <c>normalmap</c> command.
        /// </summary>
        [TestMethod]
        public void TestNormalMap()
        {
            {
                Action           action    = Action.Parse("create normalmap stone1 mask=stone1m");
                CommandBase      command   = action.Triggers.First().Commands.First();
                NormalMapCommand normalMap = command as NormalMapCommand;

                Assert.IsNotNull(normalMap);
                Assert.AreEqual("stone1",  normalMap.Texture);
                Assert.AreEqual("stone1m", normalMap.Mask);
            }
        }

        /// <summary>
        /// Tests the <c>normalmap</c> command.
        /// </summary>
        [TestMethod]
        public void TestScale()
        {
            {
                Action       action  = Action.Parse("create scale 2 0.5 3");
                CommandBase  command = action.Triggers.First().Commands.First();
                ScaleCommand scale   = command as ScaleCommand;

                Assert.IsNotNull(scale);
                Assert.AreEqual(2.0, scale.X);
                Assert.AreEqual(0.5, scale.Y);
                Assert.AreEqual(3.0, scale.Z);
            }

            {
                Action       action  = Action.Parse("create scale 1.5");
                CommandBase  command = action.Triggers.First().Commands.First();
                ScaleCommand scale   = command as ScaleCommand;

                Assert.IsNotNull(scale);
                Assert.AreEqual(1.5, scale.X);
                Assert.AreEqual(1.5, scale.Y);
                Assert.AreEqual(1.5, scale.Z);
            }
        }

        /// <summary>
        /// Tests the <c>sign</c> command.
        /// </summary>
        [TestMethod]
        public void TestSign()
        {
            {
                Action action = Action.Parse("create sign color=red bcolor=000000 \"Hello World\" " +
                                             "margin=1.0 hmargin=1.5 vmargin=2.0 align=right");
                CommandBase command = action.Triggers.First().Commands.First();
                SignCommand sign    = command as SignCommand;

                Assert.IsNotNull(sign);
                Assert.AreEqual(Color.Red,           sign.ForeColor);
                Assert.AreEqual(Color.Black,         sign.BackColor);
                Assert.AreEqual("Hello World",       sign.Text);
                Assert.AreEqual(1.0,                 sign.Margin);
                Assert.AreEqual(1.5,                 sign.HorizontalMargin);
                Assert.AreEqual(2.0,                 sign.VerticalMargin);
                Assert.AreEqual(TextAlignment.Right, sign.Alignment);
                Assert.IsFalse(sign.Shadow);
            }

            {
                Action      action  = Action.Parse("create sign shadow \"Shadow Unit Test\"");
                CommandBase command = action.Triggers.First().Commands.First();
                SignCommand sign    = command as SignCommand;

                Assert.IsNotNull(sign);
                Assert.AreEqual(Color.White,                sign.ForeColor);
                Assert.AreEqual(Color.FromString("0000c0"), sign.BackColor);
                Assert.AreEqual("Shadow Unit Test",         sign.Text);
                Assert.IsTrue(sign.Shadow);
            }
        }

        /// <summary>
        /// Tests the <c>sound</c> command.
        /// </summary>
        [TestMethod]
        public void TestSound()
        {
            {
                Action       action  = Action.Parse("create sound ambient1");
                CommandBase  command = action.Triggers.First().Commands.First();
                SoundCommand sound   = command as SoundCommand;

                Assert.IsNotNull(sound);
                Assert.IsTrue(sound.Loop);
                Assert.AreEqual("ambient1",   sound.FileName);
                Assert.AreEqual(String.Empty, sound.LeftSpeaker);
                Assert.AreEqual(String.Empty, sound.RightSpeaker);
                Assert.AreEqual(1.0,          sound.Volume);
                Assert.AreEqual(50.0,         sound.Radius);
            }

            {
                Action action = Action.Parse("create sound ambient1 noloop " +
                                             "leftspk=foo rightspk=bar radius=10 volume=0.5");
                CommandBase  command = action.Triggers.First().Commands.First();
                SoundCommand sound   = command as SoundCommand;

                Assert.IsNotNull(sound);
                Assert.IsFalse(sound.Loop);
                Assert.AreEqual("ambient1", sound.FileName);
                Assert.AreEqual("foo",      sound.LeftSpeaker);
                Assert.AreEqual("bar",      sound.RightSpeaker);
                Assert.AreEqual(0.5,        sound.Volume);
                Assert.AreEqual(10.0,       sound.Radius);
            }
        }

        /// <summary>
        /// Tests the <c>specular</c> command.
        /// </summary>
        [TestMethod]
        public void TestSpecular()
        {
            {
                Action          action   = Action.Parse("create specular;");
                CommandBase     command  = action.Triggers.First().Commands.First();
                SpecularCommand specular = command as SpecularCommand;

                Assert.IsNotNull(specular);
                Assert.AreEqual(1.0,  specular.Intensity);
                Assert.AreEqual(30.0, specular.Shininess);
                Assert.IsFalse(specular.Alpha);
            }

            {
                Action          action   = Action.Parse("create specular 0.4 22.3 alpha;");
                CommandBase     command  = action.Triggers.First().Commands.First();
                SpecularCommand specular = command as SpecularCommand;

                Assert.IsNotNull(specular);
                Assert.AreEqual(0.4,  specular.Intensity);
                Assert.AreEqual(22.3, specular.Shininess);
                Assert.IsTrue(specular.Alpha);
            }
        }

        /// <summary>
        /// Tests the <c>specularmap</c> command.
        /// </summary>
        [TestMethod]
        public void TestSpecularMap()
        {
            {
                Action             action      = Action.Parse("create specularmap specular123 mask=stone1m");
                CommandBase        command     = action.Triggers.First().Commands.First();
                SpecularMapCommand specularMap = command as SpecularMapCommand;

                Assert.IsNotNull(specularMap);
                Assert.AreEqual("specular123", specularMap.Texture);
                Assert.AreEqual("stone1m",     specularMap.Mask);
            }
        }

        /// <summary>
        /// Tests the <c>teleport</c> command.
        /// </summary>
        [TestMethod]
        public void TestTeleport()
        {
            {
                Action          action   = Action.Parse("activate teleport asdf 25n 2.6e");
                CommandBase     command  = action.Triggers.First().Commands.First();
                TeleportCommand teleport = command as TeleportCommand;

                Assert.IsNotNull(teleport);

                CoordinateParserTest.TestCoordinates(teleport.Coordinates.ToString(),
                    -2.6, 0.0, 25.0, 0.0, false, "asdf");
            }
        }

        /// <summary>
        /// Tests the <c>teleportxyz</c> command.
        /// </summary>
        [TestMethod]
        public void TestTeleportXyz()
        {
            {
                Action             action   = Action.Parse("activate teleportxyz 1 -3.14 12");
                CommandBase        command  = action.Triggers.First().Commands.First();
                TeleportXyzCommand teleport = command as TeleportXyzCommand;

                Assert.IsNotNull(teleport);
                Assert.AreEqual(1.0,   teleport.X);
                Assert.AreEqual(-3.14, teleport.Y);
                Assert.AreEqual(12.0,  teleport.Z);
                Assert.AreEqual(0.0,   teleport.Direction);
            }

            {
                Action             action   = Action.Parse("bump teleportxyz 0 0 0 180");
                CommandBase        command  = action.Triggers.First().Commands.First();
                TeleportXyzCommand teleport = command as TeleportXyzCommand;

                Assert.IsNotNull(teleport);
                Assert.AreEqual(0.0,   teleport.X);
                Assert.AreEqual(0.0,   teleport.Y);
                Assert.AreEqual(0.0,   teleport.Z);
                Assert.AreEqual(180.0, teleport.Direction);
            }
        }

        /// <summary>
        /// Tests the <c>texture</c> command.
        /// </summary>
        [TestMethod]
        public void TestTexture()
        {
            {
                Action         action  = Action.Parse("create texture stone1 mask=rail1m tag=foo");
                CommandBase    command = action.Triggers.First().Commands.First();
                TextureCommand texture = command as TextureCommand;

                Assert.IsNotNull(texture);
                Assert.AreEqual("stone1", texture.Texture);
                Assert.AreEqual("rail1m", texture.Mask);
                Assert.AreEqual("foo",    texture.Tag);
            }
        }

        #endregion
    }
}
