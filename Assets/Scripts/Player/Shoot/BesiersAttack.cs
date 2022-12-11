using Assets.Scripts.Player.Shoot.DTO;
using Assets.Scripts.Player.Weapon.DTO;
using Assets.Scripts.Player.Weapon;
using System.Collections.Generic;
using Assets.Scripts.Utilities;
using System.Linq;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.UIElements;

namespace Assets.Scripts.Player.Shoot
{
    public class BesiersAttack : IShoot
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

            _ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            Physics.Raycast(_ray, out RaycastHit hitinfo, throwLength);

            if (hitinfo.collider is not null) {
                points[2] = hitinfo.point;
                var heading = points[2] - points[0];
                var distance = Vector3.Distance(points[2], points[0]);
                //var points[1] = spawnPoint.transform.position + curve;
            }
            else {
                var currentDistance = throwLength; 
                var localPoint = new Vector3(_ray.direction.x * currentDistance, _ray.direction.y * currentDistance, _ray.direction.z * currentDistance);
                points[2] = _ray.origin + localPoint;
                Debug.DrawLine(_ray.origin, points[2]);
            }

            //Debug.LogError(IsTargetCloserThanPlayer(_ray.origin, points[2], transform.position));

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
            if (IsTargetCloserThanPlayer(points[0], points[2], transform.position)) return;

            var instance = Instantiator.Instantiate(attackDTO.Weapon.GetPrefab(), spawnPoint.position, attackDTO.SpawnPoint.rotation);

            var instanceScr = instance.GetComponent<IWeapon>();
            instanceScr.SetCreator(transform);

            var nonPhy = instance.GetComponent<INonPhysicWeapon>();
            nonPhy.ItineraryPoints = points;

            Coroutines.Start(nonPhy.SetNonPhyMove(new NonPhysicParameters() 
                {
                    delaySecond = shootingСontrol.frameDelayNonPhysics * 0.001f,
                    step = shootingСontrol.StepNonPhysic * 0.001f,
                    t = 0
                }));
        }

        private bool IsTargetCloserThanPlayer(Vector3 viewPointFrom, Vector3 viewPointTo, Vector3 player) {
            var headingToPlayer = player - viewPointFrom;
            var headingToTarger = player - viewPointTo;
            return headingToPlayer.magnitude > headingToTarger.magnitude;
        }
    }
}

