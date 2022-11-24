using Assets.Scripts.Enemies.StateMech;
using Assets.Scripts.Game;
using Assets.Scripts.Player.Control;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IControllable
    {
        [SerializeField] private KeyCode AssistantControllButton;
        [SerializeField] private float playerSpeed = 2.0f;
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float gravityValue = -9.81f;
        [SerializeField] private float aiminDelay = 0.3f;
        [SerializeField] private Animator animator;

        private PlayerBehavior playerBehavior;

        private CharacterController _controller;
        private Vector3 _playerVelocity;
        private bool _groundedPlayer;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsAiming = Animator.StringToHash("IsAiming");
        private static readonly int IsAttackWithoutAim = Animator.StringToHash("Attack");

        private PlayerControlContext playerControlContext;

        private int _aimLayerIndex;

        private bool aimCanceled;
        private float timer;


        private void Awake(){
            if (playerBehavior is null) playerBehavior = GetComponent<PlayerBehavior>();
            playerControlContext = new(PlayerState.Normal);
        }

        private void Start() {
            _controller = gameObject.GetComponent<CharacterController>();
            _aimLayerIndex = animator.GetLayerIndex("UpperBody");
            InputManager.Instance.OnAimCanceled += Instance_OnAimCanceled;
        }

        public void Move() {
            _groundedPlayer = _controller.isGrounded;
            if (_groundedPlayer && _playerVelocity.y < 0)
                _playerVelocity.y = 0f;

            if (Input.GetKey(AssistantControllButton))
            {
                playerControlContext.SetPlayerState(PlayerState.AssistantControl);
                animator.SetBool(IsMoving, false);

                AssistantControl(typeof(AssistantDisposer));
                return;
            }
            else
                playerControlContext.SetPlayerState(PlayerState.Normal);


            var movement = InputManager.Instance.GetPlayerMovement();
            var move = new Vector3(movement.x, 0, movement.y);
            move = Camera.main!.transform.forward * move.z + Camera.main!.transform.right * move.x;
            move.y = 0;
            _controller.Move(move * (Time.deltaTime * playerSpeed));



            if (move != Vector3.zero) {
                animator.SetBool(IsMoving, true);
                transform.forward = Vector3.Lerp(transform.forward, move, Time.deltaTime * 10);
            }
            else
                animator.SetBool(IsMoving, false);

            if (playerBehavior.isAmmoEmpty(playerBehavior.GetCurrentWeapon()) is false) {
                if (InputManager.Instance.IsPlayerAiming()) {
                    IncreaseCounter();
                    if (timer >= aiminDelay)
                    {
                        playerControlContext.SetPlayerState(PlayerState.Aim);

                        animator.SetBool(IsAiming, true);
                        animator.SetLayerWeight(_aimLayerIndex, 1);
                    }
                }
                else if (timer <= aiminDelay && aimCanceled && animator.GetBool(IsMoving) is false) {
                    playerControlContext.SetPlayerState(PlayerState.Normal);

                    timer = 0;
                    animator.SetBool(IsAttackWithoutAim, true);

                    RotateBody();
                }
                else if (timer <= aiminDelay && aimCanceled && animator.GetBool(IsMoving) is true) {
                    animator.SetLayerWeight(_aimLayerIndex, 2);
                    animator.SetBool(IsAttackWithoutAim, true);
                }
                else {
                    playerControlContext.SetPlayerState(PlayerState.Normal);

                    timer = 0;

                    animator.SetBool(IsAiming, false);

                    if (animator.GetCurrentAnimatorStateInfo(_aimLayerIndex).IsName("Idle"))
                        animator.SetLayerWeight(_aimLayerIndex, 0);

                    animator.SetBool(IsAttackWithoutAim, false);
                }
            }

            if (InputManager.Instance.PlayerJumpedThisFrame() && _groundedPlayer) {
                Jump();
            }

            _playerVelocity.y += gravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);
        }

        public PlayerControlContext GetContext() {
            return playerControlContext;
        }

        private void LateUpdate() {
            aimCanceled = false;
        }
        private void Jump() {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        private void RotateBody()
        {
            transform.rotation = Quaternion.AngleAxis(Camera.main.transform.rotation.eulerAngles.y, Vector3.up);
        }
        private void Instance_OnAimCanceled() => aimCanceled = true;
        private void IncreaseCounter() => timer += Time.deltaTime;
        private void AssistantControl(Type assistantType)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitinfo);
            if (Input.GetMouseButtonDown(0))
                Game.Game.Manager.MoveAssistant(hitinfo.point, assistantType);
        }
    }
}
