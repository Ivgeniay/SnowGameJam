using Assets.Scripts.Player.Weapon;
using UnityEngine;

namespace Assets.Scripts.Player.Shoot.DTO
{
    public class AimDTO
    {
        public IWeapon Weapon;
        public Transform SpawnPoint;
        public Projection Projection;

        public float IncreaseInSecond;
        public float MaxForce;
        public float Force;
    }
}