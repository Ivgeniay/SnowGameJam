using Assets.Scripts.Units.StateMech.States.AssistantStates;
using Assets.Scripts.Logger;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using IState = Assets.Scripts.Units.StateMech.States.IState;
using System;
using Assets.Scripts.Units.StateMech.States;
using System.Drawing;

namespace Assets.Scripts.Units.StateMech
{
    public class AssistantDisposer : StateDisposerBase, IRestartable
    {
        private float enemyDetectionDistance;
        private List<Transform> enemiesTransforms = new List<Transform>();
        private UnitConfiguration unitConfiguration;
        private Vector3 assistantCombatPosition;
        private Vector3 assistantTemporaryPoint;

        public AssistantDisposer(Transform transform) : base(transform) {
            unitConfiguration = transform.GetComponent<UnitConfiguration>();
            enemyDetectionDistance = unitConfiguration.EnemyDetectionDistance;
            assistantCombatPosition = transform.position;

            states = FillStates();
            ChangeState(states[StateName.Idle]);

            unitConfiguration.OnAttackDistanceChanged += OnEnemyDetectionDistanceChangedHandler;
            Game.Game.Manager.OnNpcInstantiate += OnEnemySpawnHandler;
            Game.Game.Manager.OnNpcDied += OnEnemyDiedHandler;
            Game.Game.Manager.Restart.Register(this);
        }

        #region UnitDisposer
        public override void Attack(Transform transform) {
            var state = states[StateName.Attack] as AssistanAttack;
            if (currentState is AssistantFollowPoint) assistantCombatPosition = assistantTemporaryPoint;
            if (state is null) throw new NullReferenceException($"From SnowmanStateDisposer Attack state is null");
            state.ChangeTarget(transform);
            ChangeState(state);
        }

        public override void Die()
        {
            
        }

        public override void Follow(Transform transform) {
            
        }

        public override void FrameAction() {
            base.FrameAction();
            ScaningEnemy(enemyDetectionDistance, enemiesTransforms);
        }

        public override void Follow(Vector3 point) {
            var state = states[StateName.FollowPoint] as AssistantFollowPoint;
            if (state is not null) {
                assistantTemporaryPoint = point;
                state.AddNewPointOfDestination(point);
                ChangeState(states[StateName.FollowPoint]);
            }
            else Logger.Logger.SendAlert(this.ToString() + " state is null");
        }
        private void FollowDecoratorWithRememberPath(Vector3 point, bool isRememberPoint = false) {
            if (isRememberPoint == true) assistantTemporaryPoint = point;
            Follow(point);
        }
        #endregion

        protected override Dictionary<StateName, IState> FillStates()
        {
            var states = new Dictionary<StateName, IState>();

            var stateFollow = new AssistantFollowPoint(transform);
            var stateAttack = new AssistanAttack(transform);
            states.Add(StateName.Idle, new AssistantIdle(transform));
            states.Add(StateName.Attack, stateAttack);
            states.Add(StateName.FollowPoint, stateFollow);

            stateFollow.OnFollowPointDone += OnFollowPointDoneHandler;
            stateFollow.OnFollowPointLeft += OnFollowPointLeftHandler;

            stateAttack.OnTargerDistroy += OnTargerDistroyHandler;
            stateAttack.OnNeedToMove += OnNeedToMoveHandler;
            this.transform.GetComponentInChildren<AnimationEventProxy>().PersonAttackController = stateAttack;

            return states;
        }
        

        private void ScaningEnemy(float distance, List<Transform> EnemiesTransformList) {
            if (currentState is AssistanAttack) return;
            EnemiesTransformList.ForEach(el => {
                if (GetDistance(transform.position, el.position) < distance) 
                    Attack(el);
                });
        }
        private float GetDistance(Vector3 position, Vector3 TargetPosition) => (TargetPosition - position).magnitude;
        private void OnFollowPointLeftHandler(List<Vector3> obj) { }
        private void OnFollowPointDoneHandler(Vector3 finalAssistantPosition) {
            assistantCombatPosition = finalAssistantPosition;
            ChangeState(states[StateName.Idle]);
        }
        private void OnEnemyDiedHandler(object sender, OnNpcDieEventArg e) {
            if (sender is HealthSystem hs) enemiesTransforms.Remove(hs.GetComponent<Transform>());
        }
        private void OnEnemySpawnHandler(object sender, OnNpcInstantiateEventArg e) {
            var disposer = e.UnitBehavior.BehaviourType;
            if (disposer == typeof(SnowmanStateDisposer) )  
                enemiesTransforms.Add(e.UnitBehavior.GetComponent<Transform>());
        }
        private void OnEnemyDetectionDistanceChangedHandler(float obj) => enemyDetectionDistance = obj;
        private void OnTargerDistroyHandler() => Follow(assistantCombatPosition);
        private void OnNeedToMoveHandler(Vector3 obj) => FollowDecoratorWithRememberPath(obj, true);

        public void Restart() {
            enemiesTransforms = new List<Transform>();
            assistantCombatPosition = transform.position;
            assistantTemporaryPoint = transform.position;
        }
    }
}
