using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Assets.Scripts.Utilities;

namespace Assets.Scripts.Units.StateMech.States
{
    public class SnowmanAttack : IState
    {
        
        private Transform transform;
        private Transform targetTransform;
        private NavMeshAgent agent;
        private Animator animator;
        private HealthSystem healthSystem;

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
        public float damage { get => _damage;
            set {
                if (value > 0 ) _damage = value;
            }
        }

        private bool isAttacking = false;

        public SnowmanAttack(Transform transform, Animator animator) {
            this.transform = transform;
            this.animator = animator;
            this.agent = this.transform.GetComponent<NavMeshAgent>();
            this.healthSystem = transform.GetComponent<HealthSystem>();

            WalkType = Random.Range(0, 2);
            AttackType = 0;
        }
        

        public void Start() {
        }

        public void Update() {
            if (targetTransform is null) return;

            agent.destination = targetTransform.position;

            if (agent.remainingDistance > agent.stoppingDistance) {
                agent.destination = targetTransform.position;
                Walk();
            }
            else
                if(isAttacking)
                    Attack();
        }

        private void Attack() {
            isAttacking = false;
            animator.SetBool(AnimationConstants.IsWalking, false);
            animator.SetTrigger(AnimationConstants.Attack);
            currentCoroutine = Coroutines.Start(AttackDelay(AttackDelayIsSeconds));
        }

        private void Walk() {
            WalkTypeAnimationDefinitions();
            animator.SetBool(AnimationConstants.IsWalking, true);
        }
        private void WalkTypeAnimationDefinitions()
        {
            if (healthSystem.health >= healthSystem.MaxHealth) animator.SetInteger(AnimationConstants.WalkType, WalkType);
            else animator.SetInteger(AnimationConstants.WalkType, 2);
        }

        public void Exit() {
            //Coroutines.Stop(currentCoroutine);
            //targetTransform = null;
        }

        public void ChangeTarget(Transform targetTransform)
        {
            this.targetTransform = targetTransform;
            agent.destination = targetTransform.position;
        }

        //public void Stun() {
        //    isAttacking = false;
        //    animator.speed = 0;
        //    agent.speed = 0;
        //    agent.destination = transform.position;
        //    currentCoroutine = Coroutines.Start(WalkingAfterStunDelay(WalkDelayIsSeconds));
        //}
        //private IEnumerator WalkingAfterStunDelay(float delayIsSecond) {
        //    yield return new WaitForSeconds(delayIsSecond);

        //    agent.destination = targetTransform.position;
        //    agent.speed = 1;

        //    animator.speed = 1;
        //    isAttacking = true;
        //}
        private IEnumerator AttackDelay(float delayIsSecond) {
            yield return new WaitForSeconds(delayIsSecond);
            isAttacking = true;
        }


    }
}

