using UnityEngine;

namespace Assets.Scripts.Units.StateMech.States.AssistantStates
{
    public class AssistanAttack : IState
    {
        private Transform transform;
        public AssistanAttack(Transform transform) {
            this.transform = transform;
        }
        public void Exit() { }
        public void Start() { }
        public void Update() { }
    }
}
