using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Sound
{
    public class AtmoSound : SoundBase
    {
        [SerializeField] private SoundManager soundManager;

        private void Awake() {
            if (soundManager is null) soundManager = transform.GetComponentInParent<SoundManager>();
            if (transform.TryGetComponent<AudioSource>(out AudioSource audioSource)) this.audioSource = audioSource;
            else this.audioSource = transform.AddComponent<AudioSource>();
        }

        private void Start() 
        {
            OnSound();
        }

        public override void OnSound()
        {
            var rnd = Random.Range(0, soundManager.atmo.Length);
            var sound = soundManager.atmo[rnd];
            PlayAudio(sound);
        }
    }
}
