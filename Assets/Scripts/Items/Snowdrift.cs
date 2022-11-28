using Assets.Scripts.Player;
using Assets.Scripts.Player.Weapon;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;


public class Snowdrift : SerializedMonoBehaviour
{
    [OdinSerialize] private IWeapon weapon;
    [SerializeField] private int AmmoQuantity;

    private void OnTriggerEnter(Collider other)
    {
        var collisionBody = other.transform.GetComponent<PlayerBehavior>();
        if (collisionBody is null) return;

        collisionBody.AddAmmo(weapon, AmmoQuantity);
        Destroy(gameObject);
    }

}
