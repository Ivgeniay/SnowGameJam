using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Weapon
{
    internal class Arm : MonoBehaviour, IWeapon, IBullet
    {
        public bool isCollided { get; set; }
        private float damage;
        private Transform creator;



        public void GhostSetup(Vector3 velocity, CurvatureData curvatureData = null)
        {
            throw new NotImplementedException();
        }

        public void Setup(Vector3 velocity, Transform spawnPointTransform, CurvatureData curvatureData = null)
        {
            throw new NotImplementedException();
        }


        public void SetCreator(Transform transform) => creator = transform;
        public Transform GetPrefab() => transform;
        public Transform GetCreater() => creator;
        public float GetDamage() => damage;

    }
}
