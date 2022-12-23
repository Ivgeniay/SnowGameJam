using Assets.Scripts.Player;
using Assets.Scripts.Sound;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationEventProxy : SerializedMonoBehaviour, IAttack
{
    [OdinSerialize] public IAttack PersonAttackController;
    [SerializeField] private SoundBase sound;

    public void OnAttack() {
        PersonAttackController.OnAttack();
        //playerShootingController.OnAttack(playerShootingController.GetCurrentWeapon());
        sound.OnSound();
    }

}
