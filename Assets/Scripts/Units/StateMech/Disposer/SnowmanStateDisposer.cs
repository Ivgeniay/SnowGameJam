using Assets.Scripts.Units.StateMech.States;
using Assets.Scripts.EventArgs;
using Assets.Scripts.Units.StateMech.States.SnowmanStates;
using Assets.Scripts.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units.StateMech
{
    public class SnowmanStateDisposer : StateDisposerBase
    {
        private HealthSystem healthSystem;
        private Animator animator;
        private UnitConfiguration unitConfiguration;

        private float stunTime { get; set; } = 0.5f;

        public SnowmanStateDisposer(Transform transform) : base(transform) 
        {
            this.animator = transform.GetComponentInChildren<Animator>();
            this.unitConfiguration = transform.GetComponent<UnitConfiguration>();
            unitConfiguration.OnStunTimeChanged += OnStunTimeChanged;

            states = FillStates();
            ChangeState(states[StateName.Idle]);

            healthSystem = transform.GetComponent<HealthSystem>();
            if (healthSystem is not null) {
                healthSystem.OnDeath += HealthSystem_OnDeath;
                healthSystem.OnTakeDamage += HealthSystem_OnTakeDamage;
            }
        }

        #region Mono
        #endregion
        #region Abstract
        public override void Attack(Transform transform) {
            var state = states[StateName.Attack] as SnowmanAttack;
            if (state is null) throw new NullReferenceException($"From SnowmanStateDisposer Attack state is null");
            state.ChangeTarget(transform);
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
            states.Add(StateName.Idle, new SnowmanIdle());
            states.Add(StateName.Stun, new SnowmanStun(transform, animator));
            states.Add(StateName.Attack, new SnowmanAttack(transform, animator));
            states.Add(StateName.FollowPoint, new SnowmanFollowPoint(transform, animator));

            return states;
        }
        private void HealthSystem_OnTakeDamage(object sender, TakeDamagePartEventArgs e) {
            if (healthSystem.isDead) return;

            ChangeState(states[StateName.Stun]);
            Coroutines.Start(StunExit(stunTime));
        }
        private void HealthSystem_OnDeath(object sender, System.EventArgs e) => Die();
        private IEnumerator StunExit(float seconds) {
            yield return new WaitForSeconds(seconds);
            ChangeState(prevState);
        }
        private void OnStunTimeChanged(float obj) => stunTime = obj;
        ~SnowmanStateDisposer()
        {
            unitConfiguration.OnStunTimeChanged -= OnStunTimeChanged;
        }

    }
}

public enum StateName
{
    Die,
    Idle,
    Stun,
    Attack,
    FollowPoint,
    FollowObject,
}
