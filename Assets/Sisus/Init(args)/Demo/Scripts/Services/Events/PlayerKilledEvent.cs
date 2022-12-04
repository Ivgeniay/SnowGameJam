using Sisus.Init;
using UnityEngine;

namespace Init.Demo
{
	/// <summary>
	/// An event that is invoked when the player character has been killed.
	/// <para>
	/// Whenever the event is <see cref="Trigger">triggered</see> all methods
	/// that are listening for the event are invoked.
	/// </para>
	/// </summary>
	[Service(typeof(IPlayerKilledEvent), ResourcePath = Name)]
	[Service(typeof(PlayerKilledEvent), ResourcePath = Name)]
	[CreateAssetMenu(fileName = Name, menuName = CreateAssetMenuDirectory + Name)]
	public sealed class PlayerKilledEvent : Event, IPlayerKilledEvent
	{
		private const string Name = "On Player Killed";
	}
}