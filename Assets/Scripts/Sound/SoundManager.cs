using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Sound
{
    public class SoundManager : SerializedMonoBehaviour
    {
        [SerializeField] public readonly AudioClip[] atmo;
        [SerializeField] public readonly AudioClip[] hits;
        [SerializeField] public readonly AudioClip[] whooshs;
        [SerializeField] public readonly AudioClip[] steps;
    }
}
