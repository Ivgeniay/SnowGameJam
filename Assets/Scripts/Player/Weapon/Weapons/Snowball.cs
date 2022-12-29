using Assets.Scripts.Particles;
using Assets.Scripts.Player.Weapon.DTO;
using Assets.Scripts.Player.Weapon.Interfaces;
using Assets.Scripts.Sound;
using Assets.Scripts.Units.StateMech;
using Assets.Scripts.Utilities;
using Init.Demo;
using Sirenix.Utilities;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Player.Weapon
{
    public class Snowball : MonoBehaviour, IBullet      //, IWeapon, INonPhysicWeapon
    {
        [SerializeField] private Transform impactEffect;
        [SerializeField] private string[] nameWooshSounds;
        [SerializeField] private string[] nameHitSounds;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private LayerMask ignoreLayer;
 
        private Transform snowBallSpawnPoint;
        private Rigidbody rigidbody;
        private float damage { get; set; }

        private CurvatureData curvatureData;
        private Transform creator;
        private NonPhysicParameters nonPhysicParameters;
        private Besiers besie;


        #region Mono
        private void Awake() {
            if (rigidbody is null) {
                if (TryGetComponent<Rigidbody>(out Rigidbody rb)) rigidbody = rb;
                else rigidbody = transform.AddComponent<Rigidbody>();
            }
        }

        private void Start() {
            if (audioSource is not null && nameWooshSounds.Length > 0) AudioManager.instance.PlaySound(nameWooshSounds[Random.Range(0, nameWooshSounds.Length)], audioSource);
        }

        private void Update() {
            SelfDestroy(-15);
        }

        private void FixedUpdate() {
            if (curvatureData is null) return;
                rigidbody.AddForce(curvatureData.GetForce());
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (GetCreater() == collision.transform) return;
            if (collision.gameObject.layer == ignoreLayer) return;

            var unitBehaviour = collision.transform.GetComponentInParent<UnitBehavior>();
            if (unitBehaviour != null) {
                var trans = unitBehaviour.GetComponent<Transform>();
                if (GetCreater() == trans) return;
            }

            var audioSource = collision.gameObject.AddComponent<AudioSource>();
            if (audioSource is not null && nameHitSounds.Length > 0) AudioManager.instance.PlaySound(nameHitSounds[Random.Range(0, nameHitSounds.Length)], audioSource);

            Destroy(gameObject);
        }

        #endregion
        #region IBullet
        public Transform GetCreater() => creator;
        public void SetDamage(float damage) => this.damage = damage;
        public float GetDamage() => damage;
        #endregion  

        private void SelfDestroy(float yPosition) {
            if (transform.position.y > yPosition) return;
            Destroy(gameObject);
        }

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
                nonPhysicParameters = null;
                yield break;
            }

            rigidbody.useGravity = false;

            nonPhysicParameters.pastPosition = besie.GetPoint(ItineraryPoints, t);
            nonPhysicParameters.t += nonPhysicParameters.step;

            yield return new WaitForSeconds(nonPhysicParameters.delaySecond);
            StartCoroutine(SetNonPhyMove(nonPhysicParameters));
            
        }
        #endregion
    }
}
