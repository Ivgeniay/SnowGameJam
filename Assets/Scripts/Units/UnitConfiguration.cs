using Assets.Scripts.Player.Weapon;
using Assets.Scripts.Units.StateMech;
using Assets.Scripts.Units.StateMech.Disposer;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Units
{
    [ExecuteAlways]
    public class UnitConfiguration : SerializedMonoBehaviour
    {
        public event Action<float> OnMovingSpeedChanged;
        public event Action<float> OnSpeedAnimationChanged;
        public event Action<float> OnStunTimeChanged;
        public event Action<float> OnDamageChanged;
        public event Action<float> OnAttackDistanceChanged;
        public event Action<float> OnAttackDelayIsSecondsChanged;

        private NavMeshAgent agent;
        //private UnitBehavior unitBehavior;
        private StateDisposerType stateDisposerType;

        private void Awake() {
            //unitBehavior = transform.GetComponent<UnitBehavior>();

            agent = GetComponent<NavMeshAgent>();
            agent.speed = MovingSpeed;
            agent.stoppingDistance = AttackDistance;
        }


        [BoxGroup("Weapons settings")]
        [OdinSerialize] public IWeapon weapon;
        [BoxGroup("Weapons settings")]
        [OdinSerialize] public Transform SpawnPoint;

        [Title("UnitBehaviour settings")]
        private float _movingSpeed;
        private float _speedAnimation;
        private float _stunTime;
        private float _damage;
        private float _attackDistance;
        private float _attackDelayIsSeconds;


        [OdinSerialize] public int IndexCurrentAttack { get; set; } = 0;
        [OdinSerialize] public int TypeAttackAnimation { get; private set; } = 0;
        [OdinSerialize] public int TypeWalkAnimation { get; private set; } = 0;
        [OdinSerialize] public int TypeDamagebleWalkAnimation { get; private set; } = 0;

        [OdinSerialize] [PropertyRange(0f, 10f)] public float MovingSpeed 
        { 
            get => _movingSpeed;
            set
            {
                _movingSpeed = value;
                OnMovingSpeedChanged?.Invoke(value);
            }
        }
        [OdinSerialize] [PropertyRange(0f, 10f)] public float SpeedAnimation
        {
            get => _speedAnimation;
            set
            {
                _speedAnimation = value;
                OnSpeedAnimationChanged?.Invoke(value);
            }
        }

        [OdinSerialize] public float StunTime
        {
            get => _stunTime;
            set
            {
                _stunTime = value;
                OnStunTimeChanged?.Invoke(value);
            }
        }

        [OdinSerialize] public float Damage
        {
            get => _damage;
            set
            {
                if (value < 0) return; 
                _damage = value;
                OnDamageChanged?.Invoke(value);
            }
        }
        [OdinSerialize][PropertyRange(0f, 50f)] public float AttackDistance
        {
            get => _attackDistance;
            set
            {
                if (value < 0) return;
                _attackDistance = value;
                OnAttackDistanceChanged?.Invoke(value);
            }
        }
        [OdinSerialize] public float AttackDelayIsSeconds
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
            Gizmos.DrawSphere(transform.position, AttackDistance);
        }

        private void OnValidate()
        {
            if (IndexCurrentAttack < 0) IndexCurrentAttack = 0;
            if (TypeAttackAnimation < 0) TypeAttackAnimation = 0;
            if (TypeWalkAnimation < 0) TypeWalkAnimation = 0;
            if (TypeDamagebleWalkAnimation < 0) TypeDamagebleWalkAnimation = 0;
            if (Damage < 0) Damage = 0;
            if (StunTime < 0) StunTime = 0;
            if (AttackDelayIsSeconds < 0) AttackDelayIsSeconds = 0;
        }
    }
}
