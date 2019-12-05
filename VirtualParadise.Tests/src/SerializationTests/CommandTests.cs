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
                AmbientCommand ambient = action.Create.OfType<AmbientCommand>().First();

                Assert.IsNotNull(ambient);
                Assert.AreEqual(1.0, ambient.Intensity);
            }

            {
                Action         action  = Action.Parse("create ambient 0.8 tag=foo;");
                AmbientCommand ambient = action.Create.OfType<AmbientCommand>().First();

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
                AnimateCommand animate = action.Create.OfType<AnimateCommand>().First();

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
                Action         action  = Action.Parse("create animate me jump 5 3 100 1 2 1 global;");
                AnimateCommand animate = action.Create.OfType<AnimateCommand>().First();

                Assert.IsNotNull(animate);
                Assert.IsFalse(animate.Mask);
                Assert.AreEqual("me",   animate.Name);
                Assert.AreEqual("jump", animate.Animation);
                Assert.AreEqual(5,      animate.ImageCount);
                Assert.AreEqual(3,      animate.FrameCount);
                Assert.AreEqual(100,    animate.FrameDelay);
                CollectionAssert.AreEqual(
                    new[] {1, 2, 1}, animate.FrameList.ToArray());
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
                Action        action = Action.Parse("create astart anim1;");
                AstartCommand astart = action.Create.OfType<AstartCommand>().First();

                Assert.IsNotNull(astart);
                Assert.AreEqual("anim1", astart.Name);
                Assert.IsFalse(astart.Loop);
            }

            {
                Action        action = Action.Parse("create astart anim1 looping;");
                AstartCommand astart = action.Create.OfType<AstartCommand>().First();

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
            Action       action = Action.Parse("create astop anim1;");
            AstopCommand astop  = action.Create.OfType<AstopCommand>().First();

            Assert.IsNotNull(astop);
            Assert.AreEqual("anim1", astop.Name);
        }

        /// <summary>
        /// Tests the <c>camera</c> command.
        /// </summary>
        [TestMethod]
        public void TestCamera()
        {
            {
                Action        action = Action.Parse("create camera location=foo target=bar");
                CameraCommand camera = action.Create.OfType<CameraCommand>().First();

                Assert.IsNotNull(camera);
                Assert.AreEqual("foo", camera.Location);
                Assert.AreEqual("bar", camera.Target);
            }
        }

        /// <summary>
        /// Tests the <c>color</c> command.
        /// </summary>
        [TestMethod]
        public void TestColor()
        {
            {
                Action       action = Action.Parse("activate color blue global;");
                ColorCommand color  = action.Activate.OfType<ColorCommand>().First();

                Assert.IsNotNull(color);
                Assert.AreEqual(Color.Blue, color.Color);
                Assert.IsTrue(color.IsGlobal);
            }

            {
                Action       action = Action.Parse("create color tint 238e23 tag=foo;");
                ColorCommand color  = action.Create.OfType<ColorCommand>().First();

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
                DiffuseCommand diffuse = action.Create.OfType<DiffuseCommand>().First();

                Assert.IsNotNull(diffuse);
                Assert.AreEqual(0.5, diffuse.Intensity);
            }

            {
                Action         action  = Action.Parse("bump diffuse 0.12345;");
                DiffuseCommand diffuse = action.Bump.OfType<DiffuseCommand>().First();

                Assert.IsNotNull(diffuse);
                Assert.AreEqual(0.12345, diffuse.Intensity);
            }

            {
                Action         action  = Action.Parse(" activate diffuse 1.0 tag=foo lock;");
                DiffuseCommand diffuse = action.Activate.OfType<DiffuseCommand>().First();

                Assert.IsNotNull(diffuse);
                Assert.AreEqual("foo", diffuse.Tag);
                Assert.AreEqual(1.0,   diffuse.Intensity);
                Assert.IsTrue(diffuse.IsLocked);
            }
        }

        /// <summary>
        /// Tests the <c>framerate</c> command.
        /// </summary>
        [TestMethod]
        public void TestFrameRate()
        {
            Action           action    = Action.Parse("create framerate 60;");
            FrameRateCommand framerate = action.Create.OfType<FrameRateCommand>().First();

            Assert.IsNotNull(framerate);
            Assert.AreEqual(60, framerate.Value);
        }

        /// <summary>
        /// Tests the <c>light</c> command.
        /// </summary>
        [TestMethod]
        public void TestLight()
        {
            {
                Action       action = Action.Parse("create light;");
                LightCommand light  = action.Create.OfType<LightCommand>().First();

                Assert.IsNotNull(light);
                Assert.AreEqual(45.0,             light.Angle);
                Assert.AreEqual(0.5,              light.Brightness);
                Assert.AreEqual(Color.White,      light.Color);
                Assert.AreEqual(LightEffect.None, light.Effect);
                Assert.AreEqual(1000.0,           light.MaxDistance);
                Assert.AreEqual(10.0,             light.Radius);
                Assert.AreEqual(1.0,              light.Time);
                Assert.AreEqual(LightType.Point,  light.Type);
            }

            {
                Action action = Action.Parse(
                    "create light fx=blink radius=5 maxdist=50 brightness=1 angle=90 color=00ff00 time=3 type=spot;");
                LightCommand light = action.Create.OfType<LightCommand>().First();

                Assert.IsNotNull(light);
                Assert.AreEqual(90.0,              light.Angle);
                Assert.AreEqual(1.0,               light.Brightness);
                Assert.AreEqual(Color.Green,       light.Color);
                Assert.AreEqual(LightEffect.Blink, light.Effect);
                Assert.AreEqual(50.0,              light.MaxDistance);
                Assert.AreEqual(5.0,               light.Radius);
                Assert.AreEqual(3.0,               light.Time);
                Assert.AreEqual(LightType.Spot,    light.Type);
            }
        }

        /// <summary>
        /// Tests the <c>move</c> command.
        /// </summary>
        [TestMethod]
        public void TestMove()
        {
            {
                Action      action = Action.Parse("create move 5");
                MoveCommand move   = action.Create.OfType<MoveCommand>().First();

                Assert.IsNotNull(move);
                Assert.AreEqual(5.0, move.X);
                Assert.AreEqual(0.0, move.Y);
                Assert.AreEqual(0.0, move.Z);
                Assert.IsFalse(move.Loop);
                Assert.IsFalse(move.Sync);
                Assert.IsFalse(move.Smooth);
                Assert.IsFalse(move.Reset);
                Assert.IsFalse(move.LocalAxis);
                Assert.AreEqual(0.0, move.Wait);
                Assert.AreEqual(1.0, move.Time);
                Assert.AreEqual(0.0, move.Offset);
            }

            {
                Action      action = Action.Parse("create move 0 10 0 loop smooth ltm time=5 wait=3");
                MoveCommand move   = action.Create.OfType<MoveCommand>().First();

                Assert.IsNotNull(move);
                Assert.AreEqual(0.0,  move.X);
                Assert.AreEqual(10.0, move.Y);
                Assert.AreEqual(0.0,  move.Z);
                Assert.IsTrue(move.Loop);
                Assert.IsFalse(move.Sync);
                Assert.IsTrue(move.Smooth);
                Assert.IsFalse(move.Reset);
                Assert.IsTrue(move.LocalAxis);
                Assert.AreEqual(3.0, move.Wait);
                Assert.AreEqual(5.0, move.Time);
                Assert.AreEqual(0.0, move.Offset);
            }
        }

        /// <summary>
        /// Tests the <c>name</c> command.
        /// </summary>
        [TestMethod]
        public void TestName()
        {
            {
                Action      action = Action.Parse("create name foo;");
                NameCommand name   = action.Create.OfType<NameCommand>().First();

                Assert.IsNotNull(name);
                Assert.AreEqual("foo", name.Name);
            }

            {
                Action      action = Action.Parse("activate   name bar name=foo;");
                NameCommand name   = action.Activate.OfType<NameCommand>().First();

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
                Action       action = Action.Parse("create noise ambient1 loop volume=0.8");
                NoiseCommand noise  = action.Create.OfType<NoiseCommand>().First();

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
                NormalMapCommand normalMap = action.Create.OfType<NormalMapCommand>().First();

                Assert.IsNotNull(normalMap);
                Assert.AreEqual("stone1",  normalMap.Texture);
                Assert.AreEqual("stone1m", normalMap.Mask);
            }
        }

        /// <summary>
        /// Tests the <c>opacity</c> command.
        /// </summary>
        [TestMethod]
        public void TestOpacity()
        {
            {
                Action         action  = Action.Parse("create opacity;");
                OpacityCommand opacity = action.Create.OfType<OpacityCommand>().First();

                Assert.IsNotNull(opacity);
                Assert.AreEqual(1.0, opacity.Value);
            }

            {
                Action         action  = Action.Parse("create opacity 0.5;");
                OpacityCommand opacity = action.Create.OfType<OpacityCommand>().First();

                Assert.IsNotNull(opacity);
                Assert.AreEqual(0.5, opacity.Value);
            }
        }

        /// <summary>
        /// Tests the <c>rotate</c> command.
        /// </summary>
        [TestMethod]
        public void TestRotate()
        {
            {
                Action        action = Action.Parse("create rotate 1 90 3");
                RotateCommand rotate = action.Create.OfType<RotateCommand>().First();

                Assert.IsNotNull(rotate);
                Assert.AreEqual(1.0,  rotate.X);
                Assert.AreEqual(90.0, rotate.Y);
                Assert.AreEqual(3.0,  rotate.Z);
                Assert.IsFalse(rotate.Loop);
                Assert.IsFalse(rotate.Sync);
                Assert.IsFalse(rotate.Smooth);
                Assert.IsFalse(rotate.Reset);
                Assert.IsFalse(rotate.LocalAxis);
                Assert.AreEqual(0.0, rotate.Wait);
                Assert.AreEqual(1.0, rotate.Time);
                Assert.AreEqual(0.0, rotate.Offset);
            }

            {
                Action        action = Action.Parse("create rotate 45");
                RotateCommand rotate = action.Create.OfType<RotateCommand>().First();

                Assert.IsNotNull(rotate);
                Assert.AreEqual(0.0,  rotate.X);
                Assert.AreEqual(45.0, rotate.Y);
                Assert.AreEqual(0.0,  rotate.Z);
            }

            {
                Action        action = Action.Parse("create rotate 45 90 time=1 loop");
                RotateCommand rotate = action.Create.OfType<RotateCommand>().First();

                Assert.IsNotNull(rotate);
                Assert.AreEqual(45.0, rotate.X);
                Assert.AreEqual(90.0, rotate.Y);
                Assert.AreEqual(0.0,  rotate.Z);
                Assert.AreEqual(1.0,  rotate.Time);
                Assert.IsTrue(rotate.Loop);
            }

            {
                Action        action = Action.Parse("create rotate 45 90 reset wait=3");
                RotateCommand rotate = action.Create.OfType<RotateCommand>().First();

                Assert.IsNotNull(rotate);
                Assert.AreEqual(45.0, rotate.X);
                Assert.AreEqual(90.0, rotate.Y);
                Assert.AreEqual(0.0,  rotate.Z);
                Assert.AreEqual(3.0,  rotate.Wait);
                Assert.IsTrue(rotate.Reset);
            }
        }

        /// <summary>
        /// Tests the <c>solid</c> command.
        /// </summary>
        [TestMethod]
        public void TestSolid()
        {
            {
                Action       action = Action.Parse("create solid yes");
                SolidCommand solid  = action.Create.OfType<SolidCommand>().First();

                Assert.IsNotNull(solid);
                Assert.IsTrue(solid.Value);
                Assert.AreEqual(String.Empty, solid.TargetName);
            }

            {
                Action       action = Action.Parse("create solid 0");
                SolidCommand solid  = action.Create.OfType<SolidCommand>().First();

                Assert.IsNotNull(solid);
                Assert.IsFalse(solid.Value);
                Assert.AreEqual(String.Empty, solid.TargetName);
            }

            {
                Action       action = Action.Parse("create solid foo no");
                SolidCommand solid  = action.Create.OfType<SolidCommand>().First();

                Assert.IsNotNull(solid);
                Assert.IsFalse(solid.Value);
                Assert.AreEqual("foo", solid.TargetName);
            }

            {
                Action       action = Action.Parse("create solid false name=foo");
                SolidCommand solid  = action.Create.OfType<SolidCommand>().First();

                Assert.IsNotNull(solid);
                Assert.IsFalse(solid.Value);
                Assert.AreEqual("foo", solid.TargetName);
            }
        }

        /// <summary>
        /// Tests the <c>normalmap</c> command.
        /// </summary>
        [TestMethod]
        public void TestScale()
        {
            {
                Action       action = Action.Parse("create scale 2 0.5 3");
                ScaleCommand scale  = action.Create.OfType<ScaleCommand>().First();

                Assert.IsNotNull(scale);
                Assert.AreEqual(2.0, scale.X);
                Assert.AreEqual(0.5, scale.Y);
                Assert.AreEqual(3.0, scale.Z);
            }

            {
                Action       action = Action.Parse("create scale 1.5");
                ScaleCommand scale  = action.Create.OfType<ScaleCommand>().First();

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
                SignCommand sign = action.Create.OfType<SignCommand>().First();

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
                Action      action = Action.Parse("create sign shadow \"Shadow Unit Test\"");
                SignCommand sign   = action.Create.OfType<SignCommand>().First();

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
                Action       action = Action.Parse("create sound ambient1");
                SoundCommand sound  = action.Create.OfType<SoundCommand>().First();

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
                SoundCommand sound = action.Create.OfType<SoundCommand>().First();

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
                SpecularCommand specular = action.Create.OfType<SpecularCommand>().First();

                Assert.IsNotNull(specular);
                Assert.AreEqual(1.0,  specular.Intensity);
                Assert.AreEqual(30.0, specular.Shininess);
                Assert.IsFalse(specular.Alpha);
            }

            {
                Action          action   = Action.Parse("create specular 0.4 22.3 alpha;");
                SpecularCommand specular = action.Create.OfType<SpecularCommand>().First();

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
                SpecularMapCommand specularMap = action.Create.OfType<SpecularMapCommand>().First();

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
                TeleportCommand teleport = action.Activate.OfType<TeleportCommand>().First();

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
                TeleportXyzCommand teleport = action.Activate.OfType<TeleportXyzCommand>().First();

                Assert.IsNotNull(teleport);
                Assert.AreEqual(1.0,   teleport.X);
                Assert.AreEqual(-3.14, teleport.Y);
                Assert.AreEqual(12.0,  teleport.Z);
                Assert.AreEqual(0.0,   teleport.Direction);
            }

            {
                Action             action   = Action.Parse("bump teleportxyz 0 0 0 180");
                TeleportXyzCommand teleport = action.Bump.OfType<TeleportXyzCommand>().First();

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
                TextureCommand texture = action.Create.OfType<TextureCommand>().First();

                Assert.IsNotNull(texture);
                Assert.AreEqual("stone1", texture.Texture);
                Assert.AreEqual("rail1m", texture.Mask);
                Assert.AreEqual("foo",    texture.Tag);
            }
        }

        /// <summary>
        /// Tests the <c>visible</c> command.
        /// </summary>
        [TestMethod]
        public void TestVisible()
        {
            {
                Action         action  = Action.Parse("create visible yes");
                VisibleCommand visible = action.Create.OfType<VisibleCommand>().First();

                Assert.IsNotNull(visible);
                Assert.IsTrue(visible.Value);
                Assert.AreEqual(-1.0,         visible.Radius);
                Assert.AreEqual(String.Empty, visible.TargetName);
            }

            {
                Action         action  = Action.Parse("create visible 0");
                VisibleCommand visible = action.Create.OfType<VisibleCommand>().First();

                Assert.IsNotNull(visible);
                Assert.IsFalse(visible.Value);
                Assert.AreEqual(-1.0,         visible.Radius);
                Assert.AreEqual(String.Empty, visible.TargetName);
            }

            {
                Action         action  = Action.Parse("create visible foo no radius=2");
                VisibleCommand visible = action.Create.OfType<VisibleCommand>().First();

                Assert.IsNotNull(visible);
                Assert.IsFalse(visible.Value);
                Assert.AreEqual(2.0,   visible.Radius);
                Assert.AreEqual("foo", visible.TargetName);
            }

            {
                Action         action  = Action.Parse("create visible true name=foo radius=3.14");
                VisibleCommand visible = action.Create.OfType<VisibleCommand>().First();

                Assert.IsNotNull(visible);
                Assert.IsTrue(visible.Value);
                Assert.AreEqual(3.14,  visible.Radius);
                Assert.AreEqual("foo", visible.TargetName);
            }
        }

        #endregion
    }
}
