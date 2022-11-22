using Assets.Scripts.Enemies.StateMech.States;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies.StateMech
{
    public class SnowmanStateDisposer : StateDisposerBase
    {

        private Animator animator;
        public SnowmanStateDisposer(Transform transform) : base(transform) 
        {
            this.animator = transform.GetComponentInChildren<Animator>(true);
            states = FillStates();
            ChangeState(states[StateName.Idle]);
        }

        #region Mono
        #endregion
        #region Abstract
        public override void Attack(Transform transform) {
            var state = states[StateName.Attack] as SnowmanAttack;
            if (state is null) throw new NullReferenceException($"From SnowmanStateDisposer Attack state is null");
            state.ChangeTarget(transform);
            state.Stan();
            ChangeState(state);
        }
        public override void Die() {
            var state = states[StateName.Die] as SnowmanDie;
            if (state is null) throw new NullReferenceException($"From SnowmanStateDisposer Die state is null");
            ChangeState(states[StateName.Die]);
        }
        public override void Follow(Transform transform) => ChangeState(states[StateName.FollowObject]);
        public override void Follow(Vector3 point) => ChangeState(states[StateName.FollowPoint]);
        #endregion
        protected override Dictionary<StateName, IState> FillStates()
        {
            var states = new Dictionary<StateName, IState>();

            states.Add(StateName.Die, new SnowmanDie(transform, animator));
            states.Add(StateName.Idle, new SnowmanIdle(transform, animator));
            states.Add(StateName.Attack, new SnowmanAttack(transform, animator));
            states.Add(StateName.FollowPoint, new SnowmanFollowPoint(transform, animator));

            return states;
        }
    }
}

public enum StateName
{
    Die,
    Idle,
    Attack,
    FollowPoint,
    FollowObject,
}
