using Assets.Scripts.Player.Weapon;
using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player.Shoot
{
    public class BesiersAttack : MonoBehaviour, IShoot
    {
        private ShootingСontrol shootingСontrol;
        private LineRenderer lineRenderer;
        private Transform transform;
        private Transform spawnPoint;
        private Besiers besiers;

        private int maxPhysicsFrameIterationsProjection = 100; 

        private Vector3[] points;
        private Vector3 position;
        private Vector3 curve { get => shootingСontrol.curve; }
        private float throwLength { get => shootingСontrol.throwLength; }

        private Ray _ray = new Ray();

        private Dictionary<Transform, List<Vector3>> weaponsTraectory = new Dictionary<Transform, List<Vector3>>();
        private List<Transform> weapons = new List<Transform>();

        public BesiersAttack(Transform transform, Transform spawnPoint) {

            this.transform = transform;
            this.spawnPoint = spawnPoint;
            this.lineRenderer = transform.GetComponent<LineRenderer>();
            this.shootingСontrol = transform.GetComponent<ShootingСontrol>();

            besiers = new Besiers();
            points = new Vector3[3];
        }
        public void GetAim(AimDTO aimDTO)
        {
            if (points.Count() < 2) throw new Exception("Points in array are less than 2");

            points[0] = spawnPoint.transform.position;
            points[1] = spawnPoint.transform.position + curve;
            points[2] = default;


            _ray = new Ray(spawnPoint.position, Camera.main.transform.forward);
            Physics.Raycast(_ray, out RaycastHit hitinfo, throwLength);
            if (hitinfo.collider is not null) {
                points[2] = hitinfo.point;
            }
            else {
                points[2] = _ray.origin + new Vector3(_ray.direction.x * throwLength, _ray.direction.y * throwLength, _ray.direction.z * throwLength);
            }

            lineRenderer.ResetBounds();
            lineRenderer.positionCount = maxPhysicsFrameIterationsProjection;
            lineRenderer.SetPosition(0, spawnPoint.position);

            for (var i = 1; i < maxPhysicsFrameIterationsProjection; i++)
            {
                float t = 0.01f * i;
                var resultVector = besiers.GetPoint(points, t);
                lineRenderer.SetPosition(i, resultVector);
            }
        }

        public void GetAttack(AttackDTO attackDTO)
        {
            var instance = Instantiator.Instantiate(attackDTO.Weapon.GetPrefab(), spawnPoint.position, attackDTO.SpawnPoint.rotation);
            weapons.Add(instance.transform);
            var instanceScr = instance.GetComponent<IWeapon>();
            instanceScr.SetCreator(transform);

            var rig = instance.GetComponent<Rigidbody>();
            rig.useGravity = false;
            rig.isKinematic = true;
            

        }

        private void Update()
        {
            if (weapons.Count <= 0) return;

            //foreach (var weapon in weapons) {
            //    weapon.transform.position = spawnPoint.position;
            //}
        }
    }
}
