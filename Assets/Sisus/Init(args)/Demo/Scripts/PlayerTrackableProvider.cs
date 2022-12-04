using Sisus.Init;
using UnityEngine;
using static Sisus.NullExtensions;

namespace Init.Demo
{
	/// <summary>
	/// An object that can provide the <see cref="ITrackable"/> component of the <see cref="IPlayer"/>.
	/// </summary>
	[CreateAssetMenu]
    public sealed class PlayerTrackableProvider : ScriptableObject, IValueProvider<ITrackable>
	{
		public ITrackable Value
		{
			get
			{
				var player = Service.Get<IPlayer>();
				return player != Null ? player.Trackable : null;
			}
		}
		
		object IValueProvider.Value => Value;
	}
}