using UnityEngine;

namespace Assets.Scripts.Player.Weapon
{
    public interface IWeapon
    {
        public Transform GetPrefab();
        public bool isCollided { get; set; }
        public void Setup(Vector3 velocity, Transform spawnPointTransform);
        public void GhostSetup(Vector3 velocity);
        public void SetCreator(Transform transform);
    }
}
