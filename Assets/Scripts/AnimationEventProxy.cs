using Assets.Scripts.Player;
using Assets.Scripts.Sound;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationEventProxy : MonoBehaviour
{
    [SerializeField] private Shooting—ontrol playerShootingController;
    [SerializeField] private SoundBase sound;

    public void OnAttack() {
        playerShootingController.OnAttack(playerShootingController.GetCurrentWeapon());
        sound.OnSound();
    }

}
