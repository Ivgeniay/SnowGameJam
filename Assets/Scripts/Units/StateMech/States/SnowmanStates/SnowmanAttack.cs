﻿using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Assets.Scripts.Utilities;
using Assets.Scripts.Player.Weapon;
using Assets.Scripts.Player;
using System;

namespace Assets.Scripts.Units.StateMech.States
{
    public class SnowmanAttack : IState
    {
        
        private UnitConfiguration unitConfiguration;
        private HealthSystem healthSystem;
        private Transform targetTransform;
        private Transform transform;
        private Animator animator;
        private NavMeshAgent agent;
        private bool isAttacking = true;

        private Coroutine currentCoroutine;
        public float WalkDelayIsSeconds { get; private set; } = 1;
        public float AttackDelayIsSeconds { get; private set; } = 0.5f;

        private int _walkType;
        public int WalkType {
            get => _walkType;
            set {
                if (value >= 0)
                    _walkType = value;
            }
        }
        private int _attackType;
        public int AttackType { 
            get => _attackType; 
            set{
                if (value >= 0)
                    _attackType = value;
            } 
        }
        private float _damage;
        public float Damage { get => _damage;
            set {
                if (value > 0 ) _damage = value;
            }
        }

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

            WalkType = unitConfiguration.TypeWalkAnimation;
            AttackType = unitConfiguration.TypeAttackAnimation;
            Damage = unitConfiguration.Damage;
        }
        ~SnowmanAttack()
        {
            unitConfiguration.OnMovingSpeedChanged -= OnMovingSpeedChanged;
            unitConfiguration.OnSpeedAnimationChanged -= OnSpeedAnimationChanged;
            unitConfiguration.OnDamageChanged -= OnDamageChanged; ;
            unitConfiguration.OnAttackDistanceChanged -= OnAttackDistanceChanged; ;
            unitConfiguration.OnAttackDelayIsSecondsChanged -= OnAttackDelayIsSecondsChanged; ;
        }
        #endregion

        #region EventHandlers
        private void OnAttackDelayIsSecondsChanged(float obj) => AttackDelayIsSeconds = obj;
        private void OnAttackDistanceChanged(float obj) => agent.stoppingDistance = obj;
        private void OnDamageChanged(float obj) => Damage = obj;
        private void OnSpeedAnimationChanged(float obj) => animator.speed = obj;
        private void OnMovingSpeedChanged(float obj) => agent.speed = obj;

        #endregion

        #region Mono
        public void Start() {
            WalkType = unitConfiguration.TypeWalkAnimation;
            AttackType = unitConfiguration.TypeAttackAnimation;
        }

        public void Update() {
            if (targetTransform is null) return;

            agent.destination = targetTransform.position;

            if (agent.remainingDistance > agent.stoppingDistance)
            {
                agent.destination = targetTransform.position;
                Walk();
            }
            else
            {
                if (isAttacking)
                {
                    Attack();
                }
            }
        }
        public void Exit()
        {
        }
        #endregion

        #region AiControl
        private void Attack() {
            isAttacking = false;
            animator.SetBool(AnimationConstants.IsWalking, false);
            animator.SetTrigger(AnimationConstants.Attack);

            transform.LookAt(targetTransform.position);
            OnAttack(unitConfiguration.weapon);

            currentCoroutine = Coroutines.Start(AttackDelay(AttackDelayIsSeconds));
        }

        public void OnAttack(IWeapon weapon)
        {
            if (weapon is null) {
                Debug.Log("HEY, THIS SNOWMAN HAS NO WEAPON");
                return;
            }

            var instance = Instantiator.Instantiate(weapon.GetPrefab(), unitConfiguration.SpawnPoint.position, unitConfiguration.SpawnPoint.rotation);
            var instanceScr = instance.GetComponent<IWeapon>();
            instanceScr.SetCreator(transform);
            instanceScr.Setup(((Vector3.up / 10f) + (transform.forward)) * unitConfiguration.Damage, unitConfiguration.SpawnPoint);
        }

        private void Walk() {
            WalkTypeAnimationDefinitions();
            animator.SetBool(AnimationConstants.IsWalking, true);
        }

        private void WalkTypeAnimationDefinitions()
        {
            if (animator is null) return;
            if (healthSystem.health >= healthSystem.MaxHealth) animator.SetInteger(AnimationConstants.WalkType, WalkType);
            else animator.SetInteger(AnimationConstants.WalkType, 2);
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


    }
}

