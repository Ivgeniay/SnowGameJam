using UnityEngine;

namespace Assets.Scripts.Enemies.StateMech.States.AssistantStates
{
    public class AssistanAttack : IState
    {
        private Animator animator;
        private Transform transform;
        public AssistanAttack(Transform transform, Animator animator)
        {
            this.animator = animator;
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
