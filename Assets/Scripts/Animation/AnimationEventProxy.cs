using Assets.Scripts.Player;
using Assets.Scripts.Sound;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationEventProxy : SerializedMonoBehaviour, IAttack
{
    [OdinSerialize] public IAttack PersonAttackController;
    [SerializeField] private string[] soundsName;
    [SerializeField] private AudioSource audioSource;

    public void OnAttack() {
        PersonAttackController.OnAttack();
        if (audioSource is not null && soundsName.Length > 0) AudioManager.instance.PlaySound(soundsName[Random.Range(0, soundsName.Length)], audioSource);
    }

}
