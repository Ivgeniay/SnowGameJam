using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemies.StateMech.States.AssistantStates
{
    public class AssistantFollowPoint : IState
    {
        private Animator animator;
        private Transform transform;
        private NavMeshAgent agent;
        private Vector3 destination;
        public AssistantFollowPoint(Transform transform, Animator animator)
        {
            this.animator = animator;
            this.transform = transform;
            agent = transform.GetComponent<NavMeshAgent>();
            if (!agent.isActiveAndEnabled)
            {
                Logger.Logger.SendMsg("Agent is not active");
                agent.ActivateCurrentOffMeshLink(true);
            }
        }

        public void Start()
        {
        }

        public void Update() {
            agent.destination = destination;
        }
        public void Exit()
        {
        }

        public void ChangePointOfDestination(Vector3 point) {
            destination = point;
        }
    }
}
