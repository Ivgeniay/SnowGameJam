using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class HeathBarScaling : MonoBehaviour
    {
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private Transform heathBar;
        [SerializeField] private float duration;

        private float maxHealth;
        private Vector3 startScaling;

        private void Awake()
        {
            if (healthSystem is null) healthSystem = GetComponentInParent<HealthSystem>();
            maxHealth = healthSystem.MaxHealth;
            startScaling = transform.localScale;
        }

        private void Start()
        {
            healthSystem.OnTakeDamage += OnTakeDamage;
        }

        private void OnTakeDamage(object sender, Assets.Scripts.EventArgs.TakeDamagePartEventArgs e)
        {
            StartCoroutine(Scaling(0.1f, e.Damage / maxHealth + 1));
        }

        private IEnumerator Scaling(float duration, float scaling)
        {
            yield return new WaitForSeconds(duration);
        }
    }
}