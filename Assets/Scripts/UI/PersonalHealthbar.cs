using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PersonalHealthbar : MonoBehaviour
    {
        [SerializeField] private GameObject followedObject;
        [SerializeField] private Slider controlSlider;
        [SerializeField] private Image healthImage;
        [SerializeField] private Gradient gradient;
        [SerializeField] private Canvas canvas;

        private HealthSystem hs;

        private void Awake() {
            if (followedObject is null) throw new Exception("followedObject is null");
            if (hs is null) hs = followedObject.GetComponent<HealthSystem>();
            if (controlSlider is null) controlSlider = GetComponentInChildren<Slider>(true);
            if (canvas is null) canvas = GetComponentInParent<Canvas>(true);
        }
        private void Start() {
            Subscribe();
            SetSlider();
            controlSlider.maxValue = hs.MaxHealth;
            controlSlider.value = hs.health;
        }

        private void LateUpdate() {
            canvas.transform.LookAt(Camera.main.transform.position);
        }
        private void OnEnable() => Subscribe();
        private void OnDestroy() => Unsubscribe();
        private void OnDisable() => Unsubscribe();
        private void OnTakeDamageHandler(object sender, EventArgs.TakeDamagePartEventArgs e) => SetSlider();
        private void OnDeathHandler(object sender, System.EventArgs e)
        {
            controlSlider.value = 0;
            Unsubscribe();
        }
        private void SetSlider() {
            controlSlider.maxValue = hs.MaxHealth;
            controlSlider.value = hs.health;
            healthImage.color = gradient.Evaluate(hs.health / hs.MaxHealth);
        }
        private void Subscribe() {
            if (hs is null) hs = followedObject.GetComponent<HealthSystem>();
            hs.OnTakeDamage += OnTakeDamageHandler;
            hs.OnDeath += OnDeathHandler;
        }
        private void Unsubscribe() {
            if (hs is null) hs = followedObject.GetComponent<HealthSystem>();
            hs.OnTakeDamage -= OnTakeDamageHandler;
            hs.OnDeath += OnDeathHandler;
        }

    }
}
