using Assets.Scripts.Player;
using UnityEngine;

public class AnimationEventProxy : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    
    public void OnAttack()
    {
        playerController.OnAttack();
    }
}
