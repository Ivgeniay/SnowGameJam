using Assets.Scripts.Enemies.StateMech.States.AssistantStates;
using Assets.Scripts.Logger;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using IState = Assets.Scripts.Enemies.StateMech.States.IState;

namespace Assets.Scripts.Enemies.StateMech
{
    public class AssistantDisposer : StateDisposerBase
    {
        private Animator animator;
        public AssistantDisposer(Transform transform) : base(transform)
        {
            if (transform.TryGetComponent<Animator>(out Animator animator))
                this.animator = animator;
            else
                this.animator = transform.AddComponent<Animator>();

            states = FillStates();
        }

        public override void Attack(Transform transform)
        {
            
        }

        public override void Die()
        {
            
        }

        public override void Follow(Transform transform) {
            
        }

        public override void Follow(Vector3 point) {
            var state = states[StateName.FollowPoint] as AssistantFollowPoint;
            if (state != null) {
                state.ChangePointOfDestination(point);
                ChangeState(states[StateName.FollowPoint]);
            }
            else Logger.Logger.SendAlert(this.ToString() + " state is null");
        }

        protected override Dictionary<StateName, IState> FillStates()
        {
            var states = new Dictionary<StateName, IState>();

            states.Add(StateName.Die, new AssistantDie(transform, animator));
            states.Add(StateName.Idle, new AssistantIdle(transform, animator));
            states.Add(StateName.Attack, new AssistanAttack(transform, animator));
            states.Add(StateName.FollowPoint, new AssistantFollowPoint(transform, animator));

            return states;
        }

    }
}
