using UnityEngine;

namespace Assets._Project.Scripts._Input._InputAction
{
    internal class InputManager : MonoBehaviour
    {
        private InputAct inputActions;

        private void Awake() {
            inputActions = new InputAct();
            DontDestroyOnLoad(this);
        }

        private void OnEnable() {
            inputActions.Enable();
        }
        private void OnDisable() {
            inputActions.Disable();
        }

        public InputAct GetInput() => inputActions;
        public InputAct.Player_Actions GetPlayerInput() => inputActions.Player_;
        public InputAct.Camera_Actions GetIPlayerInput() => inputActions.Camera_;
    }
}
