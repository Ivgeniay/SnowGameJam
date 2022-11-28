using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Units.StateMech.States.AssistantStates
{
    public class AssistantFollowPoint : IState
    {
        private readonly Animator animator;
        private Transform transform;
        private NavMeshAgent agent;
        private Vector3 destination;
        public AssistantFollowPoint(Transform transform)
        {
            this.transform = transform;
            agent = transform.GetComponent<NavMeshAgent>();
            if (!agent.isActiveAndEnabled) {
                Logger.Logger.SendMsg("Agent is not active");
            }
        }

        public void Start() {
            agent.destination = destination;
        }

        public void Update() {
            agent.destination = destination;
        }
        public void Exit() {
        }

        public void ChangePointOfDestination(Vector3 point) {
            destination = point;
        }
    }
}
