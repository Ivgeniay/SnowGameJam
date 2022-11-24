using Assets.Scripts.Player;
using UnityEngine;

public class AnimationEventProxy : MonoBehaviour
{
    [SerializeField] private Shooting—ontrol playerShootingController;
    
    public void OnAttack()
    {
        playerShootingController.OnAttack(playerShootingController.GetCurrentWeapon());
    }
}
