using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Scripts.Units.StateMech.States;

namespace Assets.Scripts.Units.StateMech
{
    public abstract class StateDisposerBase
    {
        protected Transform transform;
        protected IState prevState;
        protected IState currentState;
        protected Dictionary<StateName, IState> states;


        public StateDisposerBase(Transform transform) {
            this.transform = transform;
        }

        #region Mono
        public virtual void StartAction() {
            if (currentState is null) return;
            try { currentState.Start(); }
            catch (Exception e)
            {
                Logger.Logger.SendWarning(e.ToString());
                if (prevState is not null)
                {
                    currentState = prevState;
                    currentState.Start();
                }
                throw;
            }
        }
        public virtual void FrameAction() {
            if (currentState is null) return;
            try { currentState.Update(); }
            catch (Exception e)
            {
                Logger.Logger.SendWarning(e.ToString());
                if (prevState is not null)
                {
                    currentState = prevState;
                    currentState.Start();
                }
                throw;
            }
        }
        #endregion
        public abstract void Attack(Transform transform);
        public abstract void Die();
        public abstract void Follow(Transform transform);
        public abstract void Follow(Vector3 point);

        protected virtual Dictionary<StateName, IState> FillStates()
        {
            var states = new Dictionary<StateName, IState>();
            return states;
        }
        protected virtual void ChangeState(IState newState)
        {
            if (newState is null) throw new ArgumentNullException();
            if (currentState is null) currentState = newState;
            if (currentState == newState) return;

            currentState.Exit();
            prevState = currentState;
            currentState = newState;
            currentState.Start();
        }
    }
}
