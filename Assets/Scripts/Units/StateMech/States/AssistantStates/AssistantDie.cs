using UnityEngine;

namespace Assets.Scripts.Units.StateMech.States.AssistantStates
{
    public class AssistantDie : IState
    {
        private Animator animator;
        private Transform transform;
        public AssistantDie(Transform transform) {
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