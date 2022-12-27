using Mono.Cecil;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Sound
{
    public class AudioManager : SerializedMonoBehaviour
    {
        public Sound[] sounds;
        public static AudioManager instance;

        private void Awake()
        {
            if (instance is null) {
                instance = this;
            }
            else {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

        public void PlaySound(string soundName, AudioSource source = null)
        {
            var sound = sounds.FirstOrDefault(el => el.name == soundName);

            if (sound is null) {
                Debug.Log($"There is no sound with name {soundName}");
                return;
            }

            var remove = false;
            if (source is null) {
                source = gameObject.AddComponent<AudioSource>();
                remove = true;
            }

            source.loop = sound.loop;
            source.clip = sound.audioClip;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.spatialBlend = sound.spaceSound;
            source.Play();

            if (!sound.loop && remove) {
                Destroy(source, sound.audioClip.length);
            }
        }


    }

    [Serializable]
    public class Sound
    {
        public AudioClip audioClip;
        public string name;
        public bool loop;
        [Range(0, 1)]public float volume;
        [Range(0, 1)]public float spaceSound;
        public float pitch;
    }
}
