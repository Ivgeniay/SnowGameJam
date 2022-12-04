using UnityEngine;

namespace Assets.Scripts.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class SoundBase : MonoBehaviour
    {
        protected AudioSource audioSource;
        protected void PlayAudio(AudioClip audioClip) => audioSource.PlayOneShot(audioClip);
        public abstract void OnSound();
    }
}
