using Assets._Project.Scripts._Input._InputAction;
using Assets._Project.Scripts.Character.Control;
using UnityEngine;
using Zenject;
using static Assets._Project.Scripts._Input._InputAction.InputAct;

namespace Assets._Project.Scripts.Character.Player
{
    internal class PlayerBehaviour : Character
    {
        private InputManager inputManager;
        private Player_Actions actions;

        [InjectAttribute]
        private void Construct(InputManager inputManager) {
            this.inputManager = inputManager;
        }

        #region Mono
        private void Awake() {
            _controllable = GetComponent<IControllable>();
            actions = inputManager.GetPlayerInput();
        }

        private void Update() {
            var movement = new Vector3(actions.Move.ReadValue<Vector2>().x,
                                        0,
                                        actions.Move.ReadValue<Vector2>().y);
            _controllable.Move(movement);
        }

        private void OnEnable()
        {
            actions.Jump.performed += JumpPerformed;
            actions.Shoot.performed += ShootPerformed;
            actions.Aim.performed += AimPerformed;
            actions.DuckDown.performed += DuckDownPerformed;
        }

        private void OnDisable() {
            actions.Jump.performed -= JumpPerformed;
            actions.Shoot.performed -= ShootPerformed;
            actions.Aim.performed -= AimPerformed;
            actions.DuckDown.performed -= DuckDownPerformed;
        }
        #endregion

        private void ShootPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _controllable.Attack();
        }

        private void JumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _controllable.Jump();
        }

        private void AimPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {

        }
        private void DuckDownPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {

        }


    }
}
