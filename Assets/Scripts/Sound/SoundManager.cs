using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : SerializedMonoBehaviour
    {
        [SerializeField] public readonly AudioClip[] atmo;
        [SerializeField] public readonly AudioClip[] hits;
        [SerializeField] public readonly AudioClip[] whooshs;
        [SerializeField] public readonly AudioClip[] steps;

        private AudioSource audioSource;

        private void Awake()
        {
            if (transform.TryGetComponent<AudioSource>(out AudioSource audioSource)) this.audioSource = audioSource;
            else this.audioSource = transform.AddComponent<AudioSource>();
        }

        private void Start()
        {
            var rnd = Random.Range(0, atmo.Length);
            var sound = atmo[rnd];
            PlayAudio(sound);
        }
        private void PlayAudio(AudioClip audioClip) {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
