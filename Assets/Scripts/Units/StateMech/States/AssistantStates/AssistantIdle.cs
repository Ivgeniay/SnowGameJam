using UnityEngine;

namespace Assets.Scripts.Enemies.StateMech.States.AssistantStates
{
    public class AssistantIdle : IState
    {
        private Animator animator;
        private Transform transform;
        public AssistantIdle(Transform transform) {
            this.transform = transform;
        }
        public void Exit()
        {
        }

        public void Start()
        {
        }

        public void Update()
        {
        }
    }
}
