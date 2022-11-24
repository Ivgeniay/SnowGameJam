using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemies.StateMech.States
{
    public class SnowmanFollowPoint : IState
    {
        private Vector3 pointDestination;
        private Transform transform;
        private NavMeshAgent agent;
        private Animator animator;

        public SnowmanFollowPoint(Transform transform, Animator animator)
        {
            this.transform = transform;
            this.animator = animator;
            agent = this.transform.GetComponent<NavMeshAgent>();
        }

        public void Start() {
        }
        public void Update() {
            agent.destination = pointDestination;
        }
        public void Exit() {
        }
        public void ChangePointDestination(Vector3 pointDestination) {
            this.pointDestination = pointDestination;
        }
    }
}
