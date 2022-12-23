using Init.Demo;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Units.StateMech.States.AssistantStates
{
    public class AssistantIdle : IState
    {
        private Animator animator;
        private Transform transform;
        private NavMeshAgent agent;

        private float slowdown = 5;
        public AssistantIdle(Transform transform) {
            this.transform = transform;
            animator = transform.GetComponentInChildren<Animator>();
            agent = transform.GetComponentInChildren<NavMeshAgent>();

            slowdown = agent.acceleration;
        }

        public void Start()
        {
        }

        public void Update()
        {
            if (GetFractionFromAnimator(animator) > 0.002f)
            {
                var speed = CalculateFractionToAnimator(animator, 0, slowdown);
                SetFractionToAnimator(animator, speed);
            }
        }
        public void Exit()
        {
        }


        #region Speed
        private void SetAgentSpeed(NavMeshAgent agent, float speed) => agent.speed = speed;
        private float GetFractionFromAnimator(Animator animator) => animator.GetFloat(AnimationConstants.BaseLayerBlend);
        private void SetFractionToAnimator(Animator animator, float currentFraction) => animator.SetFloat(AnimationConstants.BaseLayerBlend, currentFraction);
        private float CalculateFractionToAnimator(Animator animator, float goalFraction, float acceleration) => Mathf.Lerp(GetFractionFromAnimator(animator), goalFraction, acceleration * Time.fixedDeltaTime);
        private float GetCurrentMoveSpeedFromAnimator(Animator animator, float maxSpeed) => Mathf.Lerp(0, maxSpeed, GetFractionFromAnimator(animator));
        #endregion
    }
}
