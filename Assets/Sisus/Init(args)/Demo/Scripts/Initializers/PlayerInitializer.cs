using Sisus.Init;
using UnityEngine;

namespace Init.Demo
{
    /// <summary>
    /// <see cref="Initializer{,}"/> for the <see cref="Player"/> component.
    /// </summary>
    [AddComponentMenu("Initialization/Demo/Initializers/Player Initializer")]
    internal sealed class PlayerInitializer : Initializer<Player, ITrackable> { }
}