using UnityEngine;

namespace Assets.Scripts.Player.Shoot
{
    public interface IShoot
    {
        public void GetAttack(Vector3 endPoint);
        public void GetAim();
        public Vector3 GetPointImpact();
    }
}
