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
        private NavMeshAgent agent;
        private UnitBehavior unitBehavior;
        private StateDisposerType stateDisposerType;

        private void Awake() {
            transform.GetComponent<UnitBehavior>();

            agent = GetComponent<NavMeshAgent>();
            agent.speed = MovingSpeed;
            agent.stoppingDistance = AttackDistance;
        }


        [BoxGroup("Weapons settings")]
        [OdinSerialize] public IWeapon weapon;
        [BoxGroup("Weapons settings")]
        [OdinSerialize] public Transform SpawnPoint;

        [Title("UnitBehaviour settings")]

        [OdinSerialize] public int IndexCurrentAttack { get; set; } = 0;
        [OdinSerialize] public int TypeAttackAnimation { get; private set; } = 0;
        [OdinSerialize] public int TypeWalkAnimation { get; private set; } = 0;
        [OdinSerialize] public int TypeDamagebleWalkAnimation { get; private set; } = 0;
        [OdinSerialize] [PropertyRange(0f, 10f)] public float MovingSpeed { get; private set; } = 1f;
        [OdinSerialize] [PropertyRange(0f, 10f)] public float SpeedAnimation { get; private set; } = 1f;
        [OdinSerialize] public float StunTime { get; private set; } = 0.5f;
        [OdinSerialize] public float Damage { get; private set; } = 1f;
        [OdinSerialize][PropertyRange(0f, 50f)] public float AttackDistance { get; private set; } = 2f;
        [OdinSerialize] public float AttackDelayIsSeconds { get; private set; } = 0.5f;

        [Title("HealthSystem settings")]
        [OdinSerialize] public float Health { get; private set; } = 1f;

        private void OnDrawGizmos() {
            Gizmos.DrawSphere(transform.position, AttackDistance);
        }

        private void OnValidate()
        {
            unitBehavior = GetComponent<UnitBehavior>();

        }
    }
}
