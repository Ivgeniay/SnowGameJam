using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Sound
{
    public class BackgroundMusic : MonoBehaviour
    {
        [SerializeField] private string[] menuMusic;
        [SerializeField] private string[] gamePlayMusic;
        [SerializeField] public AudioSource audioSource;

        private void Start()
        {
            PlayMenuMusic();
        }

        public void PlayMenuMusic()
        {
            if (audioSource is not null && menuMusic.Length > 0) AudioManager.instance.PlaySound(menuMusic[Random.Range(0, menuMusic.Length)], audioSource);
        }

        public void PlayGamplaySound()
        {
            if (audioSource is not null && gamePlayMusic.Length > 0) AudioManager.instance.PlaySound(gamePlayMusic[Random.Range(0, gamePlayMusic.Length)], audioSource);
        }

        public void StopSound()
        {
            audioSource?.Stop();
        }
    }
}
