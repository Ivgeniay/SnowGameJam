using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Units.StateMech.States
{
    public class SnowmanStun : IState
    {
        private readonly Animator animator;
        private Transform transform;
        private NavMeshAgent agent;

        private StateDTO stateDTO;

        public SnowmanStun(Transform transform, Animator animator) {
            this.transform = transform;
            this.animator = animator;
            this.agent = transform.GetComponent<NavMeshAgent>();
        }
        public void Start() {
            animator.SetBool(AnimationConstants.IsWalking, false);

            stateDTO = new StateDTO() 
            {
                WalkType = animator.GetInteger(AnimationConstants.WalkType),
                AttackType = animator.GetInteger(AnimationConstants.AttackType),
                AnimationSpeed = animator.speed,
                WalkSpeed = agent.speed,
                destination = agent.destination
            };

            animator.speed = 0;
            agent.speed = 0;
            agent.destination = transform.position;
        }

        public void Update() {
            
        }

        public void Exit() {
            animator.SetInteger(AnimationConstants.WalkType, stateDTO.WalkType);
            animator.SetInteger(AnimationConstants.AttackType, stateDTO.AttackType);
            animator.speed = stateDTO.AnimationSpeed;
            agent.speed = stateDTO.WalkSpeed;
            agent.destination = stateDTO.destination;
        }
    }
}

public class StateDTO
{
    public int WalkType;
    public int AttackType;
    public float AnimationSpeed;
    public float WalkSpeed;
    public Vector3 destination;
}