using System.Collections.Generic;
using Unity.VisualScripting;
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
        private LineRenderer lineRenderer;

        private List<Vector3> pointsDest = new List<Vector3>();

        public AssistantFollowPoint(Transform transform)
        {
            this.transform = transform;
            agent = transform.GetComponent<NavMeshAgent>();
            if (!agent.isActiveAndEnabled) {
                Logger.Logger.SendMsg("Agent is not active");
            }

            if (transform.TryGetComponent<LineRenderer>(out LineRenderer lineRenderer))
                this.lineRenderer = lineRenderer;
            else
                this.lineRenderer = transform.AddComponent<LineRenderer>();
        }

        public void Start() {
            agent.destination = destination;
            lineRenderer.positionCount = pointsDest.Count + 1;
        }

        public void Update() {


            agent.destination = destination;
            lineRenderer.positionCount = pointsDest.Count + 1;
            lineRenderer.SetPosition(0, transform.position);

            for(int i = 0; i < pointsDest.Count; i ++) {
                lineRenderer.SetPosition(i + 1, pointsDest[i]);
            }


            var distance = (destination - transform.position).magnitude;
            if (distance > 0.5f) return;
            pointsDest.Remove(destination);

            if (pointsDest.Count > 0) {
                destination = pointsDest[0];
            }
            


        }
        public void Exit() {
            lineRenderer.positionCount = 0;
        }

        public void ChangePointOfDestination(Vector3 point) {
            pointsDest.Add(point);
        }



    }
}
