using Assets.Scripts.Player.Weapon;
using Assets.Scripts.Player.Weapon.Interfaces;
using Assets.Scripts.Units.StateMech;
using Assets.Scripts.Units.StateMech.Disposer;
using Assets.Scripts.Utilities;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Units
{
    public class UnitConfiguration : SerializedMonoBehaviour
    {
        public event Action<float> OnMovingSpeedChanged;
        public event Action<float> OnSpeedAnimationChanged;
        public event Action<float> OnStunTimeChanged;
        public event Action<float> OnDamageChanged;
        public event Action<float> OnAttackDistanceChanged;
        public event Action<float> OnAttackDelayIsSecondsChanged;
        public event Action<float> OnEnemyDetectionDistanceChanged;

        private NavMeshAgent agent;

        private void Awake() {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = MovingSpeed;
        }


        [BoxGroup("Weapons settings")]
        [OdinSerialize] public IWeapon_ weapon;
        [BoxGroup("Weapons settings")]
        [OdinSerialize] public Transform SpawnPoint;
        [SerializeField] private StateDisposerType stateDisposerType;

        [Title("UnitBehaviour settings")]
        private float _movingSpeed;
        private float _speedAnimation;
        private float _stunTime;
        private float _damage;
        private float _attackDistance;
        private float _attackDelayIsSeconds;
        private float _enemyDetectionDistance;


        [OdinSerialize] 
        [PropertyRange(0f, 10f)]
        [HideIf("stateDisposerType", StateDisposerType.none)]
        public float MovingSpeed 
        { 
            get => _movingSpeed;
            set
            {
                _movingSpeed = value;
                OnMovingSpeedChanged?.Invoke(value);
            }
        }
        [OdinSerialize] 
        [PropertyRange(0f, 10f)] 

        public float SpeedAnimation
        {
            get => _speedAnimation;
            set
            {
                _speedAnimation = value;
                OnSpeedAnimationChanged?.Invoke(value);
            }
        }

        [OdinSerialize]
        [ShowIf("stateDisposerType", StateDisposerType.Snowman)] 
        public float StunTime
        {
            get => _stunTime;
            set
            {
                _stunTime = value;
                OnStunTimeChanged?.Invoke(value);
            }
        }

        [OdinSerialize]
        [PropertyRange(0f, 50f)]
        [HideIf("stateDisposerType", StateDisposerType.none)]
        public float Damage
        {
            get => _damage;
            set
            {
                if (value < 0) return; 
                _damage= value;
                OnDamageChanged?.Invoke(value);
            }
        }
        [OdinSerialize]
        [PropertyRange(0f, 50f)] 
        [HideIf("stateDisposerType", StateDisposerType.none)]
        public float AttackDistance
        {
            get => _attackDistance;
            set
            {
                if (value < 0) return;
                _attackDistance = value;
                OnAttackDistanceChanged?.Invoke(value);
            }
        }

        [OdinSerialize]
        [PropertyRange(0f, 50f)]
        [ShowIf("stateDisposerType", StateDisposerType.Assistant)]
        public float EnemyDetectionDistance
        {
            get => _enemyDetectionDistance;
            set
            {
                if (value < 0) return;
                _enemyDetectionDistance = value;
                OnEnemyDetectionDistanceChanged?.Invoke(value);
            }
        }
        
        [OdinSerialize]
        [PropertyRange(0.1f, 10f)]
        [HideIf("stateDisposerType", StateDisposerType.none)]
        public float AttackDelayIsSeconds
        {
            get => _attackDelayIsSeconds;
            set
            {
                if (value < 0) return;
                _attackDelayIsSeconds = value;
                OnAttackDelayIsSecondsChanged?.Invoke(value);
            }
        }

        [Title("HealthSystem settings")]
        [OdinSerialize] public float Health { get; private set; } = 1f;

        private void OnDrawGizmos() {
            Gizmos.DrawSphere(transform.position, EnemyDetectionDistance);
        }

        private void OnValidate()
        {
            if (Damage < 0) Damage = 0;
            if (StunTime < 0) StunTime = 0;
            if (AttackDelayIsSeconds < 0.1f) AttackDelayIsSeconds = 0.1f;
            if (EnemyDetectionDistance < 0) EnemyDetectionDistance = 0;
        }
    }
}
