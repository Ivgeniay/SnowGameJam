using Assets.Scripts.Particles;
using Assets.Scripts.Player.Weapon.DTO;
using Assets.Scripts.Units.StateMech;
using Assets.Scripts.Utilities;
using Init.Demo;
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
        private float damage;
        private Transform creator;
        private CurvatureData curvatureData;

        private NonPhysicParameters nonPhysicParameters;
        private Besiers besie;

        public bool isCollided { get; set; } = false;
        private bool isGhost = false;

        private void Awake() {
            if (rigidbody is null) {
                if (TryGetComponent<Rigidbody>(out Rigidbody rb)) rigidbody = rb;
                else rigidbody = transform.AddComponent<Rigidbody>();
            }
        }

        public void Setup(in Vector3 velocity, Transform snowBallSpawnPoint, CurvatureData curvatureData = null)
        {
            this.snowBallSpawnPoint = snowBallSpawnPoint;
            this.curvatureData = curvatureData;
            rigidbody.AddForce(velocity);

            damage = velocity.magnitude;
        }
        public void GhostSetup(in Vector3 velocity, CurvatureData curvatureData = null)
        {
            isGhost = true;
            this.curvatureData = curvatureData;
            rigidbody.AddForce(velocity);


            damage = velocity.magnitude;
        }

        private void Update() {
            SelfDestroy(-100);
        }

        private void FixedUpdate() {
            if (curvatureData is null) return;
                rigidbody.AddForce(curvatureData.GetForce());
        }

        public void SetCreator(Transform transform) => creator = transform;
        public Transform GetPrefab() => transform;
        public Transform GetCreater() => creator;
        public float GetDamage() => damage;

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

        private void SelfDestroy(float yPosition) {
            if (transform.position.y > yPosition) return;
            Destroy(gameObject);
        }

        public IEnumerator SetNonPhyMove(NonPhysicParameters _nonPhysicParameters)
        {
            if (nonPhysicParameters is null) {
                nonPhysicParameters = (NonPhysicParameters)_nonPhysicParameters.Clone();
                nonPhysicParameters.pastPosition = transform.position;
            }

            if (besie is null) besie = new Besiers();

            float t = nonPhysicParameters.t;
            transform.position = nonPhysicParameters.pastPosition;

            if (t >= 1)
            {
                rigidbody.useGravity = true;
                //rigidbody.isKinematic = false;
                var heading = nonPhysicParameters.positions[2] - nonPhysicParameters.positions[1];
                float distance = Vector3.Distance(nonPhysicParameters.positions[1], nonPhysicParameters.positions[2]);
                Vector3 direction = heading / distance;

                Debug.Log(direction);
                //yield return new WaitForFixedUpdate();

                rigidbody.AddForce(direction * 1500);

                yield break;
            }

            rigidbody.useGravity = false;
            //rigidbody.isKinematic = true;

            nonPhysicParameters.pastPosition = besie.GetPoint(nonPhysicParameters.positions, t);
            nonPhysicParameters.t += nonPhysicParameters.step;

            yield return new WaitForSeconds(nonPhysicParameters.delaySecond);
            StartCoroutine(SetNonPhyMove(nonPhysicParameters));
            
        }
    }
}
