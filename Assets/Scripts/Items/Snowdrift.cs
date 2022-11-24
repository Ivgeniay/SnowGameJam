using Assets.Scripts.Player;
using Assets.Scripts.Player.Weapon;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;


public class Snowdrift : SerializedMonoBehaviour
{
    [OdinSerialize] private IWeapon weapon;
    [SerializeField] private int Snowballs;
    [SerializeField] private Vector3 RotateSpeed = new Vector3(0, 0.2f, 0);


    private void OnTriggerEnter(Collider other)
    {
        var collisionBody = other.transform.GetComponent<PlayerBehavior>();
        if (collisionBody is null) return;

        collisionBody.AddAmmo(weapon, Snowballs);
        Destroy(gameObject);
    }


    private void Update()
    {
        transform.Rotate(RotateSpeed);
    }
}
