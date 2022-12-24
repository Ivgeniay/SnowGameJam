using Assets.Scripts.Player.Weapon;
using Assets.Scripts.Player.Weapon.Interfaces;
using Assets.Scripts.Utilities;
using Init.Demo;
using Sisus.Init;
using System;
using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using Timer = System.Threading.Timer;

namespace Assets.Scripts.Units.StateMech.States.AssistantStates
{
    public class AssistanAttack : IState, IAttack
    {
        public event Action OnTargerDistroy;
        public event Action<Vector3> OnNeedToMove;

        private Transform transform;
        private Transform targetTransform;
        private UnitConfiguration unitConfiguration;
        private Animator animator;
        private NavMeshAgent agent;

        private float attackDistance = 5;
        private float attackDelayInSeconds = 0.5f;

        private bool canAttack;


        public AssistanAttack(Transform transform) {
            this.transform = transform;
            this.unitConfiguration = transform.GetComponent<UnitConfiguration>();
            this.attackDistance = unitConfiguration.AttackDistance;
            this.attackDelayInSeconds = unitConfiguration.AttackDelayIsSeconds;
            this.animator = transform.GetComponentInChildren<Animator>();
            this.agent = transform.GetComponent<NavMeshAgent>();

        }


        public void Start() {
            targetTransform.GetComponent<HealthSystem>().OnDied += TargetOnDiedHandler;
            Coroutines.Start(TimerAttack(true, attackDelayInSeconds));

            unitConfiguration.OnAttackDelayIsSecondsChanged += OnAttackDelayIsSecondsChangedHandler;
            unitConfiguration.OnAttackDistanceChanged += OnAttackDistanceChangedHandler;
        }


        public void Update() {
            CheckingNull(targetTransform);

            if (GetDistance(transform.position, targetTransform.position) <= attackDistance) {
                if (canAttack) {
                    RotateTransform(transform, targetTransform.position);
                    animator.SetTrigger(AnimationConstants.Attack);
                    canAttack = false;
                }
            }
            else {
                ChangePointOfDestination(
                    FindNearestAttackPoint(transform.position, targetTransform.position, attackDistance), 
                    agent);
            }
        }

        private void CheckingNull(Transform targetTransform) {
            if (targetTransform is null) return;
        }
        private void ChangePointOfDestination(Vector3 point, NavMeshAgent agent) {
            OnNeedToMove?.Invoke(point);
        }

        private Vector3 FindNearestAttackPoint(Vector3 pointFrom, Vector3 pointTo, float minDistance) {
            var distance = GetDistance(pointTo, pointFrom);
            var fraction = minDistance/ distance;
            var result = Vector3.Lerp(pointFrom, pointTo, fraction);
            return result;
        }

        public void Exit() {
            animator.ResetTrigger(AnimationConstants.Attack);
            targetTransform = null;
        }
        public void ChangeTarget(Transform target) => targetTransform = target;

        private float GetDistance(Vector3 position, Vector3 TargetPosition) => (TargetPosition - position).magnitude;
        public void OnAttack() {
            var targetIsDestroy = targetTransform.IsDestroyed();
            if (targetIsDestroy == false) {
                Attack(targetTransform, GetCurrentWeapon());
            }
            Coroutines.Start(TimerAttack(true, attackDelayInSeconds));
        }
        private void Attack(Transform target, IWeapon_ weapon) {
            animator.SetTrigger(AnimationConstants.Attack);
            weapon.Fire(targetTransform.position + Vector3.up);
        }
        public IWeapon_ GetCurrentWeapon() {
            return unitConfiguration.weapon;
        }
        private void RotateTransform(Transform transform, Vector3 to) {
            transform.LookAt(to);
        }

        private IEnumerator TimerAttack(bool canAttack, float delay) {
            yield return null;
            animator.ResetTrigger(AnimationConstants.Attack);
            yield return new WaitForSeconds(delay);
            this.canAttack = canAttack;
        }

        private void OnAttackDistanceChangedHandler(float obj) => attackDistance = obj;
        private void OnAttackDelayIsSecondsChangedHandler(float obj) => attackDelayInSeconds = obj;
        private void TargetOnDiedHandler(object sender, OnNpcDieEventArg e) {
            OnTargerDistroy?.Invoke();
            targetTransform = null;
        }
    }
}
