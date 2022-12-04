namespace Init.Demo.Tests
{
	public sealed class MockEvent : IEventTrigger
	{
        public bool HasBeenTriggered { get; private set; }
		public void Trigger() => HasBeenTriggered = true;
	}
}