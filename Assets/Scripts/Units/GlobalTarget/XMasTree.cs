using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.Units.GlobalTarget
{
    [Required]
    [RequireComponent(typeof(HealthSystem))]
    internal class XMasTree : SerializedMonoBehaviour, IGlobalTarget
    {
        public Transform GetTransform() => this.transform;
    }
}
