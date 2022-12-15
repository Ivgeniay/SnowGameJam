using Assets.Scripts.Particles;
using Assets.Scripts.Player.Weapon.DTO;
using Assets.Scripts.Units.StateMech;
using Assets.Scripts.Utilities;
using Init.Demo;
using Sirenix.Utilities;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Player.Weapon
{
    public class Snowball : MonoBehaviour, IWeapon, IBullet, INonPhysicWeapon
    {
        [SerializeField] private Transform impactEffect;

        private Transform snowBallSpawnPoint;
        private Rigidbody rigidbody;
        private float damage { get; set; }
        private Transform creator;
        private CurvatureData curvatureData;

        private NonPhysicParameters nonPhysicParameters;
        private Besiers besie;


        #region Mono
        private void Awake() {
            if (rigidbody is null) {
                if (TryGetComponent<Rigidbody>(out Rigidbody rb)) rigidbody = rb;
                else rigidbody = transform.AddComponent<Rigidbody>();
            }
        }


        private void Update() {
            SelfDestroy(-100);
        }

        private void FixedUpdate() {
            if (curvatureData is null) return;
                rigidbody.AddForce(curvatureData.GetForce());
        }


        private void OnCollisionEnter(Collision collision)
        {
            //Debug.Log($"{this} created by {GetCreater()} collision {collision.transform.name}");
            if (isCollided) return;
            if (GetCreater() == collision.transform) return;


            var unitBehaviour = collision.transform.GetComponentInParent<UnitBehavior>();
            if (unitBehaviour != null) {
                var trans = unitBehaviour.GetComponent<Transform>();
                if (GetCreater() == trans) return;
            }

            if (!isGhost) Instantiate(impactEffect, transform.position, Quaternion.LookRotation(collision.contacts[0].normal));
            
            Destroy(gameObject);
            isCollided = true;
        }

        #endregion
        #region IWeapon
        public bool isCollided { get; set; } = false;

        private bool isGhost = false;
        public Transform GetPrefab() => transform;
        public void Setup(in Vector3 velocity, Transform snowBallSpawnPoint = null, CurvatureData curvatureData = null)
        {
            this.snowBallSpawnPoint = snowBallSpawnPoint;
            this.curvatureData = curvatureData;
            rigidbody.AddForce(velocity);

            damage = 1;
        }
        public void GhostSetup(in Vector3 velocity, CurvatureData curvatureData = null)
        {
            isGhost = true;
            this.curvatureData = curvatureData;
            rigidbody.AddForce(velocity);
        }
        public void SetCreator(Transform transform) => creator = transform;
        #endregion  
        #region IBullet
        public Transform GetCreater() => creator;
        public float GetDamage() => damage;
        #endregion  
        #region NonPhy
        public Vector3[] ItineraryPoints { get; set; }
        public IEnumerator SetNonPhyMove(NonPhysicParameters _nonPhysicParameters)
        {
            if (ItineraryPoints == null) throw new NullReferenceException(this.name);

            if (nonPhysicParameters is null) {
                nonPhysicParameters = (NonPhysicParameters)_nonPhysicParameters.Clone();
                nonPhysicParameters.pastPosition = transform.position;
            }
            if (damage == 0) damage = 1;

            if (besie is null) besie = new Besiers();

            float t = nonPhysicParameters.t;
            transform.position = nonPhysicParameters.pastPosition;

            if (t >= 1)
            {
                rigidbody.useGravity = true;
                var heading = ItineraryPoints[2] - ItineraryPoints[1];
                float distance = Vector3.Distance(ItineraryPoints[1], ItineraryPoints[2]);
                Vector3 direction = heading / distance;

                rigidbody.AddForce(direction * 1500);

                yield break;
            }

            rigidbody.useGravity = false;

            nonPhysicParameters.pastPosition = besie.GetPoint(ItineraryPoints, t);
            nonPhysicParameters.t += nonPhysicParameters.step;

            yield return new WaitForSeconds(nonPhysicParameters.delaySecond);
            StartCoroutine(SetNonPhyMove(nonPhysicParameters));
            
        }
        #endregion

        private void SelfDestroy(float yPosition) {
            if (transform.position.y > yPosition) return;
            Destroy(gameObject);
        }
    }
}
