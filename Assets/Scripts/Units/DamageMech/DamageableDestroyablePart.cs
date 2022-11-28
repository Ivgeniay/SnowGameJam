using Assets.Scripts.Units.StateMech;
using Assets.Scripts.EventArgs;
using System;
using System.Collections;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Units.DamageMech
{
    public class DamageableDestroyablePart : MonoBehaviour, IDestroyable
    {
        [SerializeField] private float _timerDelay = 0.2f;
        [SerializeField] private float _explotionForse = 5;
        [SerializeField] private List<Transform> goJoin;
        private bool _isDestroyed;

        public event EventHandler<TakeDamagePartEventArgs> OnTakeDamage;

        public void GetDamage(Transform sender, float damageAmount, Vector3 normal) {
            OnTakeDamage?.Invoke(this, new TakeDamagePartEventArgs() { Damage = damageAmount, Shooter = sender, Direction = normal });

            if (transform.TryGetComponent<Rigidbody>(out Rigidbody rb)) {
                rb.AddForce(normal * 3, ForceMode.Impulse);
            }
            else {
                rb = gameObject.AddComponent<Rigidbody>();
                rb.AddForce(normal * 3, ForceMode.Impulse);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            var bullet = collision.transform.GetComponent<IBullet>();

            if (bullet == null) return;
            if (_isDestroyed) return;

            GetDamage(bullet.GetCreater(), bullet.GetDamage(), collision.contacts[0].normal);

            if (transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.AddForce(collision.contacts[0].normal * 3, ForceMode.Impulse);
                if (goJoin.Count() > 0)
                {
                    goJoin.ForEach(x => {
                        if (x.TryGetComponent<IDestroyable>(out IDestroyable destroyable)) {
                            destroyable.Destroy();
                        }
                        else {
                            if (x.TryGetComponent<Rigidbody>(out Rigidbody joinRb)) 
                                joinRb.AddForce(collision.contacts[0].normal * 3, ForceMode.Impulse);
                            else {
                                joinRb = x.AddComponent<Rigidbody>();
                                joinRb.AddForce(collision.contacts[0].normal * 3, ForceMode.Impulse);
                            }
                        }


                    });
                }
            }
            else {
                rb = gameObject.AddComponent<Rigidbody>();
                rb.AddForce(collision.contacts[0].normal * 3, ForceMode.Impulse);
            }
                

            StartCoroutine(DestroyTimer(_timerDelay));

            _isDestroyed = true;
        }

        public void Destroy() {
            var parentTransform = transform.GetComponentInParent<UnitBehavior>().GetComponent<Transform>();

            if (transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
                rb.AddExplosionForce(_explotionForse * 50, parentTransform.position, 2);
            else
            {
                rb = gameObject.AddComponent<Rigidbody>();
                rb.AddExplosionForce(_explotionForse * 50, parentTransform.position, 2);
            }

            if (gameObject.activeSelf)
                StartCoroutine(DestroyTimer(_timerDelay));
        }

        private IEnumerator DestroyTimer(float timerDelay) {
            yield return new WaitForSeconds(timerDelay);
            gameObject.SetActive(false);
        }


    }
}
