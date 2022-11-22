using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.Logger;
using System.Collections;
using Assets.Scripts.Utilities;
using Assets.Scripts.Enemies.DamageMech;

namespace Assets.Scripts.Enemies.StateMech.States
{
    public class SnowmanAttack : IState
    {
        private Transform transform;
        private Transform targetTransform;
        private NavMeshAgent agent;
        private Animator animator;
        private float attackDistance = 2;

        private float damage;

        private bool isWalking;
        private bool isAttacking;

        private int walkType;
        private int attackType;

        private float walkSpeed;
        private float attackSpeed;

        private float walkDelayIsSeconds;
        private float attackDelayIsSeconds;


        public SnowmanAttack(Transform transform, Animator animator) {
            this.transform = transform;
            agent = this.transform.GetComponent<NavMeshAgent>();
            this.animator = animator;

            isAttacking = true;
            isWalking = true;
            walkType = 0;
            attackType = 0;
            walkSpeed = 1;
            attackSpeed = 1;
            damage = 1;

            attackDelayIsSeconds = 1;
            walkDelayIsSeconds = 0.5f;
        }
        

        public void Start() {
            if (transform is null) return;
            if (animator is null) Logger.Logger.SendMsg("animator is null");
        }

        public void Update() {

            if (targetTransform is null) return;
            float distance = TakeDistance(transform, targetTransform);

            if (distance > attackDistance) {
                if(isWalking) 
                    Walk(walkType, walkSpeed);
            }
            else { 
                if (isAttacking) Attack(attackDelayIsSeconds, attackType, attackSpeed); 
            }
        }


        private void Walk(int walkType = 0, float walkSpeed = 1)
        {
            if (animator.GetBool(AnimationConstants.IsWalking) == false)
                    animator.SetBool(AnimationConstants.IsWalking, true);
            animator.SetInteger(AnimationConstants.WalkType, walkType);
            agent.destination = targetTransform.position;
            agent.speed = walkSpeed;
            animator.speed = walkSpeed;
        }

        private void Attack(float delayIsSeconds, int typeAttack = 0, float attackSpeed = 1)
        {
            isAttacking = false;
            agent.destination = transform.position;
            if (animator.GetBool(AnimationConstants.IsWalking) == true)
                animator.SetBool(AnimationConstants.IsWalking, false);

            animator.SetTrigger(AnimationConstants.Attack);
            animator.SetInteger(AnimationConstants.AttackType, typeAttack);
            animator.speed = attackSpeed;

            //var heading = targetTransform.position - transform.position;
            //targetTransform.GetComponent<IDamageable>().GetDamage(transform, damage, heading/TakeDistance(transform, targetTransform));
            //Coroutines.Start(AttackDelay(attackDelayIsSeconds));
        }

        public void Exit() {
        }

        public void ChangeTarget(Transform targetTransform) {
            this.targetTransform = targetTransform;
            agent.destination = targetTransform.position;
        }

        public void Stun() {
            Logger.Logger.SendMsg(attackDelayIsSeconds);
            isWalking = false;
            animator.speed = 0; 
            agent.destination = transform.position;
            Coroutines.Start(WalkingDelay(walkDelayIsSeconds));
        }

        private float TakeDistance(Transform transform, Transform targetTansform) {
            return Vector3.Distance(transform.position, targetTansform.position);
        }
        private IEnumerator AttackDelay(float delayIsSecond) {
            yield return new WaitForSeconds(delayIsSecond);
            isAttacking = true;
        }
        private IEnumerator WalkingDelay(float delayIsSecond) {
            yield return new WaitForSeconds(delayIsSecond);
            agent.destination = targetTransform.position;
            isWalking = true;
        }

    }
}

