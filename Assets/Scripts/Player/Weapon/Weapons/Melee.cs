using Assets.Scripts.Player.Weapon.Interfaces;
using Blobcreate.ProjectileToolkit;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.Weapon
{
    internal class Melee : MonoBehaviour, IWeapon_
    {
        public bool isCollided { get; set; }
        private float damage;
        private Transform creator;

        private void Start() {
            Destroy(gameObject);
        }



        public void GhostSetup(in Vector3 velocity, CurvatureData curvatureData = null)
        {
            //throw new NotImplementedException();
        }

        public void Setup(in Vector3 velocity, Transform spawnPointTransform, CurvatureData curvatureData = null)
        {
            //throw new NotImplementedException();
        }


        public void SetCreator(Transform transform) => creator = transform;
        public Transform GetPrefab() => transform;
        public Transform GetCreater() => creator;
        public float GetDamage() => damage;

        private IEnumerator Destroy()
        {
            yield return null;
            Destroy(this);
        }

        public void Fire(Vector3 firePosition)
        {
            
        }

        public void GetAim(TrajectoryPredictor trajectoryPredictor, Vector3 endpoint)
        {
            
        }
    }
}
