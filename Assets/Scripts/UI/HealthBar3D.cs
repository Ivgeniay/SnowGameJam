using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI
{
    public class HealthBar3D : MonoBehaviour
    {
        [SerializeField] private HealthSystem _healthSystem;
        [SerializeField] private GameObject redLine;
        [SerializeField] private Gradient gradient;

        private Renderer renderer;
        private float maxHp;

        private void Start() {
            if(_healthSystem is null) _healthSystem = GetComponent<HealthSystem>();
            if(redLine is not null) renderer = redLine.GetComponent<Renderer>();

            _healthSystem.OnTakeDamage += _healthSystem_OnTakeDamage;
            _healthSystem.OnDeath += _healthSystem_OnDeath;

            maxHp = _healthSystem.MaxHealth;
            renderer.material.color = gradient.Evaluate(1);
        }

        private void _healthSystem_OnDeath(object sender, System.EventArgs e) {
            
        }

        private void _healthSystem_OnTakeDamage(object sender, EventArgs.TakeDamagePartEventArgs e) {
            var scaleX = e.currentHealth / maxHp;
            var positionX = (1 - e.currentHealth / maxHp)/2;


            redLine.transform.localScale = new Vector3(scaleX, redLine.transform.localScale.y, redLine.transform.localScale.z);
            redLine.transform.localPosition = new Vector3(positionX, redLine.transform.localPosition.y, redLine.transform.localPosition.z);
            renderer.material.color = gradient.Evaluate(e.currentHealth / maxHp);
        }
    }
}
