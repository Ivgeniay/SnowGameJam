using Blobcreate.ProjectileToolkit;
using UnityEngine;

namespace Assets.Scripts.Player.Weapon.Interfaces
{
    public interface IWeapon_
    {
        public void Fire(Vector3 firePosition);
        public void GetAim(TrajectoryPredictor trajectoryPredictor, Vector3 endPoint);
    }
}
