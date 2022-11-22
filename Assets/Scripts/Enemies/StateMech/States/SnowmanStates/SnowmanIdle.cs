using UnityEngine;

namespace Assets.Scripts.Enemies.StateMech.States
{
    public class SnowmanIdle : IState
    {
        private Animator animator;
        private Transform transform;

        public SnowmanIdle(Transform transform, Animator animator)
        {
            this.transform = transform;
            this.animator = animator;
        }
        public void Start() {
            animator.SetBool(AnimationConstants.IsWalking, false);
        }

        public void Update() {
            //Debug.Log("Idle mooving");
        }

        public void Exit() {
            //Debug.Log("Stop Idle mooving");
        }
    }
}
