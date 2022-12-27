using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Units.StateMech.States.AssistantStates
{
    public class AssistantFollowPoint : IState
    {
        public event Action<Vector3> OnFollowPointDone;
        public event Action<List<Vector3>> OnFollowPointLeft;

        private readonly Animator animator;
        private Transform transform;
        private NavMeshAgent agent;
        private NavMeshPath meshPath;
        private Vector3 destination;
        private LineRenderer lineRenderer;
        private UnitConfiguration unitConfiguration;


        private bool isReachablePath;
        private float acceleration = 5;
        private int BaseLayer;

        private List<Vector3> pointsDest = new List<Vector3>();

        public AssistantFollowPoint(Transform transform)
        {
            this.transform = transform;
            agent = transform.GetComponent<NavMeshAgent>();
            animator = transform.GetComponentInChildren<Animator>();
            unitConfiguration = transform.GetComponent<UnitConfiguration>();

            if (!agent.isActiveAndEnabled) Logger.Logger.SendMsg("Agent is not active");
            meshPath = new NavMeshPath();

            if (transform.TryGetComponent<LineRenderer>(out LineRenderer lineRenderer))
                this.lineRenderer = lineRenderer;
            else
                this.lineRenderer = transform.AddComponent<LineRenderer>();

            BaseLayer = animator.GetLayerIndex(AnimationConstants.BaseLayer);
            acceleration = agent.acceleration;
        }

        public void Start() {
            lineRenderer.positionCount = pointsDest.Count + 1;
            if (destination == Vector3.zero) ChangePointOfDestination(GetNextPointPositionToFolow(pointsDest), agent);
        }

        public void Update() {
            DrawLineRender(lineRenderer, pointsDest);
            SetFractionToAnimator(animator, CalculateFractionToAnimator(animator, 1, acceleration));
            if (GetDistance(destination, transform.position) < agent.stoppingDistance) {
                DeletePointOfDestination(destination);
                if (pointsDest.Count == 0) {
                    OnFollowPointDone?.Invoke(transform.position);
                    return;
                }
                ChangePointOfDestination(GetNextPointPositionToFolow(pointsDest), agent);
            }
        }
        public void Exit() {
            SetFractionToAnimator(animator, 0);
            agent.destination = transform.position;
            pointsDest = new List<Vector3>();
            destination = Vector3.zero;
            lineRenderer.positionCount = 0;
        }

        public bool CalculatePath(Vector3 pointDestination, NavMeshPath meshPath) => agent.CalculatePath(pointDestination, meshPath);
        public void AddNewPointOfDestination(Vector3 point) 
        {
            if (CalculatePath(point, meshPath))
                pointsDest.Add(point);
        }

        public void DeletePointOfDestination(Vector3 point) {
            pointsDest.Remove(point);
            OnFollowPointLeft?.Invoke(pointsDest);
        }
        private void DrawLineRender(LineRenderer lineRenderer, List<Vector3> pointsList) {
            lineRenderer.positionCount = pointsList.Count + 1;
            lineRenderer.SetPosition(0, transform.position);

            for (int i = 0; i < pointsList.Count; i++)
                lineRenderer.SetPosition(i + 1, pointsList[i]);
        }
        private float GetDistance(Vector3 position, Vector3 TargetPosition)  => (TargetPosition - position).magnitude;
        private Vector3 GetNextPointPositionToFolow(List<Vector3> pointsList) {
            if (pointsList.Count > 0) return destination = pointsList[0];
            else return transform.position;
        }
        private void ChangePointOfDestination(Vector3 point, NavMeshAgent agent) {
            agent.SetDestination(point);
            
        }

        private void SetInMotion(NavMeshAgent agent, Vector3 destination) => agent.SetDestination(destination);


        #region Speed
        private float GetFractionFromAnimator(Animator animator) => animator.GetFloat(AnimationConstants.BaseLayerBlend);
        private void SetFractionToAnimator(Animator animator, float currentFraction) => animator.SetFloat(AnimationConstants.BaseLayerBlend, currentFraction);
        private float CalculateFractionToAnimator(Animator animator, float goalFraction, float acceleration) => Mathf.Lerp(GetFractionFromAnimator(animator), goalFraction, acceleration * Time.fixedDeltaTime);
        private float GetCurrentMoveSpeedFromAnimator(Animator animator, float maxSpeed) => Mathf.Lerp(0, maxSpeed, GetFractionFromAnimator(animator));
        #endregion

    }
}
