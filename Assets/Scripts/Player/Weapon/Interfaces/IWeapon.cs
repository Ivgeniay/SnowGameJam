using UnityEngine;

namespace Assets.Scripts.Player.Weapon
{
    public interface IWeapon
    {
        public Transform GetPrefab();
        public bool isCollided { get; set; }
        public void Setup(in Vector3 velocity, Transform spawnPointTransform, CurvatureData curvatureData = null);
        public void GhostSetup(in Vector3 velocity, CurvatureData curvatureData = null);
        public void SetCreator(Transform transform);
    }
}
