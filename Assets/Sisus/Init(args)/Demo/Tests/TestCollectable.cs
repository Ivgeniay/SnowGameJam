using NUnit.Framework;
using Sisus.Init;
using Sisus.Init.Testing;

namespace Init.Demo.Tests
{
    /// <summary>
    /// Unit tests for <see cref="Collectable"/>.
    /// </summary>
    public sealed class TestCollectable
    {
        private MockEvent onCollectedEvent;
        private Collectable collectable;
        private Testable testable;

        [SetUp]
        public void Setup()
        {
            onCollectedEvent = new MockEvent();
            collectable = new GameObject<Collectable>().Init(onCollectedEvent as IEventTrigger);
            testable = new Testable(collectable.gameObject);
            Assert.IsFalse(onCollectedEvent.HasBeenTriggered);
        }

        [TearDown]
        public void TearDown()
        {
            testable.Destroy();
            onCollectedEvent = null;
        }

        [Test]
        public void Collect_Triggers_Event()
        {
            collectable.Collect();
            Assert.IsTrue(onCollectedEvent.HasBeenTriggered);
        }

        [Test]
        public void Collect_Sets_GameObject_Inactive()
        {
            collectable.Collect();
            Assert.IsFalse(collectable.gameObject.activeSelf);
        }
    }
}