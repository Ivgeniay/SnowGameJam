using Assets.Scripts.Sound;
using UnityEngine;

namespace Assets.Scripts.Animation
{
    public class AnimationEventStep : MonoBehaviour
    {
        [SerializeField] private string[] soundsName;
        [SerializeField] public AudioSource audioSource;
        public void OnStep() {
            if (audioSource is not null && soundsName.Length > 0) AudioManager.instance.PlaySound(soundsName[Random.Range(0, soundsName.Length)], audioSource);
        }
    }
}
