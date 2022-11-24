using Assets.Scripts.Enemies.StateMech;
using Assets.Scripts.EventArgs;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies.DamageMech
{
    public class DamageablePart : MonoBehaviour, IDamageable
    {
        public event EventHandler<TakeDamagePartEventArgs> OnTakeDamage;

        [SerializeField] private float _timerDelay = 0.2f;
        [SerializeField] private float _explotionForse = 5;

        public void GetDamage(Transform shooter, float damageAmount, Vector3 normal)
        {
            OnTakeDamage?.Invoke(this, new TakeDamagePartEventArgs() { Damage = damageAmount, Shooter = shooter, Direction = normal });
        }

        private void OnCollisionEnter(Collision collision)
        {
            var bullet = collision.transform.GetComponent<IBullet>();
            if (bullet == null) return;

            GetDamage(bullet.GetCreater(), bullet.GetDamage(), collision.contacts[0].normal);
        }

        public void Destroy()
        {
            var parentTransform = transform.GetComponentInParent<UnitBehavior>().GetComponent<Transform>();

            if (transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
                rb.AddExplosionForce(_explotionForse * 50, parentTransform.position, 2);
            else
            {
                rb = gameObject.AddComponent<Rigidbody>();
                rb.AddExplosionForce(_explotionForse * 50, parentTransform.position, 2);
            }

            StartCoroutine(DestroyTimer(_timerDelay));
        }

        private IEnumerator DestroyTimer(float timerDelay)
        {
            yield return new WaitForSeconds(timerDelay);
            gameObject.SetActive(false);
        }
    }
    
}
