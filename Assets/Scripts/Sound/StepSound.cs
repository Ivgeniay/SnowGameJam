using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Sound 
{
    public class StepSound : SoundBase
    {
        [SerializeField] private SoundManager soundManager;

        private void Awake() {
            if (soundManager is null) soundManager = transform.GetComponentInParent<SoundManager>();
            if (transform.TryGetComponent<AudioSource>(out AudioSource audioSource)) this.audioSource = audioSource;
            else this.audioSource = transform.AddComponent<AudioSource>();
        }
        public override void OnSound() => OnStepVoice();

        private void OnStepVoice() {
            var rnd = Random.Range(0, soundManager.steps.Length);
            var sound = soundManager.steps[rnd];
            PlayAudio(sound);
        }
    }
}
