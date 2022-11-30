using Assets.Scripts.Particles;
using Assets.Scripts.Units.StateMech;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Player.Weapon
{
    public class Snowball : MonoBehaviour, IWeapon, IBullet
    {
        [SerializeField] private Transform impactEffect;

        private Transform snowBallSpawnPoint;
        private Rigidbody rigidbody;
        private float damage;
        private Transform creator;
        private CurvatureData curvatureData;

        public bool isCollided { get; set; }
        private bool isGhost = false;

        private void Awake() {
            if (rigidbody is null) {
                if (TryGetComponent<Rigidbody>(out Rigidbody rb)) rigidbody = rb;
                else rigidbody = transform.AddComponent<Rigidbody>();
            }
        }

        public void Setup(Vector3 velocity, Transform snowBallSpawnPoint, CurvatureData curvatureData = null)
        {
            this.snowBallSpawnPoint = snowBallSpawnPoint;
            this.curvatureData = curvatureData;
            rigidbody.AddForce(velocity);

            damage = velocity.magnitude;
        }
        public void GhostSetup(Vector3 velocity, CurvatureData curvatureData = null)
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


    }
}
