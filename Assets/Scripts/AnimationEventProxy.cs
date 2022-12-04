using Assets.Scripts.Player;
using Assets.Scripts.Sound;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationEventProxy : MonoBehaviour
{
    [SerializeField] private Shooting—ontrol playerShootingController;
    [SerializeField] private SoundManager soundManager;

    private AudioSource audioSource;

    private void Awake()
    {
        if (audioSource is null) audioSource = GetComponent<AudioSource>();
        if (audioSource is null) audioSource = transform.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    public void OnAttack() {
        playerShootingController.OnAttack(playerShootingController.GetCurrentWeapon());
        OnAttackVoice();
    }

    public void OnAttackVoice()
    {
        var rnd = Random.Range(0, soundManager.hits.Length);
        var sound = soundManager.hits[rnd];
        PlayAudio(sound);
    }

    public void OnStepVoice()
    {
        var rnd = Random.Range(0, soundManager.steps.Length);
        var sound = soundManager.steps[rnd];
        PlayAudio(sound);
    }

    private void PlayAudio(AudioClip audioClip) {
        audioSource.PlayOneShot(audioClip);
    }
}
