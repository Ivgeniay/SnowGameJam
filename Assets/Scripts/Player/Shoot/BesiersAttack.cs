using Assets.Scripts.Player.Shoot.DTO;
using Assets.Scripts.Player.Weapon.DTO;
using Assets.Scripts.Player.Weapon;
using System.Collections.Generic;
using Assets.Scripts.Utilities;
using System.Linq;
using UnityEngine;
using System;

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
            if (aimDTO.Force < aimDTO.MaxForce) aimDTO.Force += aimDTO.IncreaseInSecond * Time.deltaTime;
            else aimDTO.Force = aimDTO.MaxForce;

            if (points.Count() < 2) throw new Exception("Points in array are less than 2");

            points[0] = spawnPoint.transform.position;
            points[1] = spawnPoint.transform.position + curve;

            _ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            Physics.Raycast(_ray, out RaycastHit hitinfo, throwLength);
            //Debug.DrawRay(_ray.origin, _ray.direction * throwLength, Color.green);

            if (hitinfo.collider is not null) {
                points[2] = hitinfo.point;
            }
            else {
                var distanceScale = aimDTO.Force / aimDTO.MaxForce;
                var currentDistance = throwLength * distanceScale;
                var localPoint = new Vector3(_ray.direction.x * currentDistance, _ray.direction.y * currentDistance, _ray.direction.z * currentDistance);
                points[2] = _ray.origin + localPoint;
                Debug.DrawLine(_ray.origin, points[2]);
            }

            lineRenderer.ResetBounds();
            lineRenderer.positionCount = maxPhysicsFrameIterationsProjection;
            lineRenderer.SetPosition(0, spawnPoint.position);

            for (var i = 1; i < maxPhysicsFrameIterationsProjection; i++) {
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

            var nonPhy = instance.GetComponent<INonPhysicWeapon>();
            Coroutines.Start(nonPhy.SetNonPhyMove(new NonPhysicParameters() 
                {
                    delaySecond = 0.001f,
                    positions = points,
                    step = 0.01f,
                    t = 0
                }));

            //var rig = instance.GetComponent<Rigidbody>();
            //rig.useGravity = false;
            //rig.isKinematic = true;



        }

    }
}
