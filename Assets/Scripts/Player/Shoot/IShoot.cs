using Blobcreate.ProjectileToolkit;
using UnityEngine;

namespace Assets.Scripts.Player.Shoot
{
    public interface IShoot
    {
        public void GetAttack(Vector3 endPoint);
        public void GetAim(TrajectoryPredictor trajectoryPredictor, Vector3 endPoint);
        public Vector3 GetPointImpact();
    }
}
