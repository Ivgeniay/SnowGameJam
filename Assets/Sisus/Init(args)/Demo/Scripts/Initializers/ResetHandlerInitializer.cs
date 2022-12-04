using Sisus.Init;
using UnityEngine;

namespace Init.Demo
{
    /// <summary>
    /// <see cref="Initializer{,}"/> for the <see cref="ResetHandler"/> component.
    /// </summary>
    [AddComponentMenu("Initialization/Demo/Initializers/Reset Handler Initializer")]
    internal sealed class ResetHandlerInitializer : Initializer<ResetHandler, IInputManager> { }
}