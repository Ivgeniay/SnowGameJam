using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventProxy : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    
    public void OnAttack()
    {
        playerController.OnAttack();
    }
}
