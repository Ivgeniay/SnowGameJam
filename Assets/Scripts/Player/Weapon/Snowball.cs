using Assets.Scripts.Particles;
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

        public bool isCollided { get; set; }
        private bool isGhost = false;

        private void Awake() {
            if (rigidbody is null) {
                if (TryGetComponent<Rigidbody>(out Rigidbody rb)) rigidbody = rb;
                else rigidbody = transform.AddComponent<Rigidbody>();
            }
        }

        public void Setup(Vector3 velocity, Transform snowBallSpawnPoint)
        {
            this.snowBallSpawnPoint = snowBallSpawnPoint;
            rigidbody.AddForce(velocity);

            //velocity = velocity / 100;
            rigidbody.velocity = velocity;

            damage = velocity.magnitude;
        }
        public void GhostSetup(Vector3 velocity)
        {
            isGhost = true;
            rigidbody.AddForce(velocity);

            damage = velocity.magnitude;
        }
        public void SetCreator(Transform transform) => creator = transform;
        public Transform GetPrefab() => transform;
        public Transform GetCreater() => creator;
        public float GetDamage() => damage;

        private void OnCollisionEnter(Collision collision)
        {
            if (!isGhost) {
                Instantiate(impactEffect, transform.position, Quaternion.LookRotation(collision.contacts[0].normal));
            }
            Destroy(gameObject);
            isCollided = true;
        }
    }
}
