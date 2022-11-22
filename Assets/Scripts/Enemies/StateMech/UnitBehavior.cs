using Assets.Scripts.Enemies.DamageMech;
using Assets.Scripts.Enemies.Repository;
using Assets.Scripts.Enemies.StateMech;
using Assets.Scripts.Enemies.StateMech.Disposer;
using Assets.Scripts.EventArgs;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EventArgs = System.EventArgs;

namespace Assets.Scripts.Enemies.StateMech
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(HealthSystem))]
    public class UnitBehavior : MonoBehaviour, IBehaviour
    {
        [SerializeField] private StateDisposerType stateDisposerType;

        private StateDisposerBase stateDisposer;

        private List<IDamageable> damageablesParts;
        private HealthSystem healthSystem;

        #region Mono
        private void Awake() {
            stateDisposer = DisposerFactory.GetDisposer(stateDisposerType, transform);

            healthSystem = GetComponent<HealthSystem>();
            if (healthSystem is not null) {
                healthSystem.OnDeath += HealthSystem_OnDeath;
                healthSystem.OnTakeDamage += HealthSystem_OnTakeDamage;
            }
        }
        private void Start() {
            if (stateDisposer is null) throw new Exception($"stateDisposer is null {this}");
            stateDisposer.StartAction();
        }
        private void Update() {
            if (stateDisposer is null) throw new Exception($"stateDisposer is null {this}");
            stateDisposer.FrameAction();
        }
        #endregion
        public void Attack(Transform target) => stateDisposer.Attack(target);
        public void Follow(Vector3 vector3) => stateDisposer.Follow(vector3);
        public void Follow(Transform transform) => stateDisposer.Follow(transform);
        public Type BehaviourType { get
            {   if (stateDisposer is not null) return stateDisposer.GetType(); 
                throw new Exception("BehaviourType is null"); 
            }
        }
        public float GetCurrentHealth() => healthSystem.health;
        private void HealthSystem_OnTakeDamage(object sender, TakeDamagePartEventArgs e)
        {
            if (healthSystem.isDead) return;
            Attack(e.Shooter);
        }
        private void HealthSystem_OnDeath(object sender, System.EventArgs e) => stateDisposer.Die();

    }
}
