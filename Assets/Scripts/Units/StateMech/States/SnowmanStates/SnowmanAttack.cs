using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Assets.Scripts.Game.Pause;
using Assets.Scripts.Player.Weapon.Interfaces;
using Assets.Scripts.Units.GlobalTarget;
using System;
using Unity.VisualScripting;

namespace Assets.Scripts.Units.StateMech.States
{
    public class SnowmanAttack : IState, IAttack
    {
        
        private UnitConfiguration unitConfiguration;
        private HealthSystem healthSystem;
        private Transform targetTransform;
        private Transform transform;
        private Animator animator;
        private NavMeshAgent agent;

        private Coroutine currentCoroutine;

        private bool isAttacking = false;
        public float WalkDelayIsSeconds { get; private set; }
        public float AttackDelayIsSeconds { get; private set; }
        private float _damage;

        private GameState previoslyState;
        private float temporaryAnimationSpeed;

        #region Mono
        public void Start() {
            animator.ResetTrigger(AnimationConstants.Attack);
            animator.SetBool(AnimationConstants.IsWalking, true);
            animator.SetFloat("TargetDistance", agent.remainingDistance);
        }

        public void Update() {
            if (targetTransform is null) return;

            agent.destination = targetTransform.position;
            animator.SetFloat("TargetDistance", agent.remainingDistance);

            if (agent.remainingDistance > unitConfiguration.AttackDistance) {
                agent.destination = targetTransform.position;
                Walk();
            }
            else {
                Attack();
            }

        }
        public void Exit()
        {
        }
        #endregion

        #region Const/Destr
        public SnowmanAttack(Transform transform, Animator animator) {
            this.transform = transform;
            this.animator = animator;
            this.agent = this.transform.GetComponent<NavMeshAgent>();
            this.healthSystem = transform.GetComponent<HealthSystem>();
            this.unitConfiguration = transform.GetComponent<UnitConfiguration>();


            unitConfiguration.OnMovingSpeedChanged += OnMovingSpeedChanged;
            unitConfiguration.OnSpeedAnimationChanged += OnSpeedAnimationChanged;
            unitConfiguration.OnDamageChanged += OnDamageChanged; ;
            unitConfiguration.OnAttackDistanceChanged += OnAttackDistanceChanged; ;
            unitConfiguration.OnAttackDelayIsSecondsChanged += OnAttackDelayIsSecondsChanged; ;

            _damage = unitConfiguration.Damage;
        }
        #endregion

        #region EventHandlers
        private void OnAttackDelayIsSecondsChanged(float obj) => AttackDelayIsSeconds = obj;
        private void OnAttackDistanceChanged(float obj) => agent.stoppingDistance = obj;
        private void OnDamageChanged(float obj) => _damage = obj;
        private void OnSpeedAnimationChanged(float obj) => animator.speed = obj;
        private void OnMovingSpeedChanged(float obj) => agent.speed = obj;

        #endregion

        #region AiControl
        private void Attack() {
            isAttacking = false;
            animator.SetBool(AnimationConstants.IsWalking, false);
            animator.SetTrigger(AnimationConstants.Attack);

            transform.LookAt(targetTransform.position);
        }

        public void OnAttack() {
            var globalScr = targetTransform.GetComponent<IGlobalTarget>();
            globalScr.TakeDamage(transform.position, Convert.ToInt32(_damage));
        }

        private void Walk() {
            WalkTypeAnimationDefinitions();
            animator.SetBool(AnimationConstants.IsWalking, true);
        }

        private void WalkTypeAnimationDefinitions()
        {
            if (animator is null) return;
            animator.SetInteger(AnimationConstants.WalkType, 0);
        }

        public void ChangeTarget(Transform targetTransform)
        {
            this.targetTransform = targetTransform;
            agent.destination = targetTransform.position;
        }

        private IEnumerator AttackDelay(float delayIsSecond) {
            yield return new WaitForSeconds(delayIsSecond);
            isAttacking = true;
        }

        #endregion
        //public void GameStateHandle(GameState gameState)
        //{
        //    if (transform.gameObject.IsDestroyed()) return;

        //    if (gameState == GameState.Gameplay) {
        //        if (animator is not null) animator.speed = unitConfiguration.SpeedAnimation;
        //    }
        //    else {
        //        if (animator is not null) animator.speed = 0f;
        //    }
        //}
    }
}

