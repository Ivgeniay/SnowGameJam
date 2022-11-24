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
        public AssistantDisposer(Transform transform) : base(transform) {
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

            states.Add(StateName.Die, new AssistantDie(transform));
            states.Add(StateName.Idle, new AssistantIdle(transform));
            states.Add(StateName.Attack, new AssistanAttack(transform));
            states.Add(StateName.FollowPoint, new AssistantFollowPoint(transform));

            return states;
        }

    }
}
