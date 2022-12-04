using Random = UnityEngine.Random;
using Unity.VisualScripting;
using UnityEngine;
using System;

namespace Assets.Scripts.Sound
{
    public class AttackSound : SoundBase
    {
        [SerializeField] private SoundManager soundManager;

        private void Awake()
        {
            if (soundManager is null) soundManager = transform.GetComponentInParent<SoundManager>();
            if (transform.TryGetComponent<AudioSource>(out AudioSource audioSource)) this.audioSource = audioSource;
            else this.audioSource = transform.AddComponent<AudioSource>();
        }
        public override void OnSound() => OnAttackVoice();

        private void OnAttackVoice()
        {
            var rnd = Random.Range(0, soundManager.hits.Length);
            var sound = soundManager.hits[rnd];
            PlayAudio(sound);
        }
    }
}
