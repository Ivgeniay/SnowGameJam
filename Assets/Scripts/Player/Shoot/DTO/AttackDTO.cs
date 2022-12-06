using Assets.Scripts.Player.Weapon;
using UnityEngine;

namespace Assets.Scripts.Player.Shoot.DTO
{
    public class AttackDTO
    {
        public IWeapon Weapon;
        public Transform SpawnPoint;
        public float ThrowForce;
    }
}
