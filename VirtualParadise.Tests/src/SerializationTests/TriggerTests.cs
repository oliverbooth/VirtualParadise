namespace VirtualParadise.Tests.SerializationTests
{
    #region Using Directives

    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Scene.Serialization;
    using Scene.Serialization.Triggers;

    #endregion

    /// <summary>
    /// Tests for trigger serialization.
    /// </summary>
    [TestClass]
    public class TriggerTests
    {
        #region Methods

        /// <summary>
        /// Tests the <c>activate</c> trigger.
        /// </summary>
        [TestMethod]
        public void TestActivate()
        {
            Action action = Action.Parse("activate;");
            Assert.IsTrue(action.Triggers.First() is ActivateTrigger);
        }

        /// <summary>
        /// Tests the <c>adone</c> trigger.
        /// </summary>
        [TestMethod]
        public void TestAdone()
        {
            Action action = Action.Parse("adone;");
            Assert.IsTrue(action.Triggers.First() is AdoneTrigger);
        }

        /// <summary>
        /// Tests the <c>bump</c> trigger.
        /// </summary>
        [TestMethod]
        public void TestBump()
        {
            Action action = Action.Parse("bump;");
            Assert.IsTrue(action.Triggers.First() is BumpTrigger);
        }

        /// <summary>
        /// Tests the <c>bumpend</c> trigger.
        /// </summary>
        [TestMethod]
        public void TestBumpEnd()
        {
            Action action = Action.Parse("bumpend;");
            Assert.IsTrue(action.Triggers.First() is BumpEndTrigger);
        }

        /// <summary>
        /// Tests the <c>create</c> trigger.
        /// </summary>
        [TestMethod]
        public void TestCreate()
        {
            Action action = Action.Parse("create;");
            Assert.IsTrue(action.Triggers.First() is CreateTrigger);
        }

        #endregion
    }
}
