using Assets.Scripts.Player;
using Assets.Scripts.Player.Weapon;
using Assets.Scripts.Player.Weapon.Interfaces;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;


public class Snowdrift : SerializedMonoBehaviour
{
    [OdinSerialize] private IWeapon_ weapon;
    [SerializeField] private int AmmoQuantity;

    private void OnTriggerEnter(Collider other)
    {
        var collisionBody = other.transform.GetComponent<PlayerBehavior>();
        if (collisionBody is null) return;

        collisionBody.AddAmmo(weapon, AmmoQuantity);
        Destroy(gameObject);
    }

}
