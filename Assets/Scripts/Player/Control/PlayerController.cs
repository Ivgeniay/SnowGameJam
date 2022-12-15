using Assets.Scripts.Units.StateMech;
using Assets.Scripts.Game.Pause;
using Assets.Scripts.Player.Control;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IControllable, IGameStateHandler
    {
        [SerializeField] private float playerSpeed = 2.0f;
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float gravityValue = -9.81f;
        [SerializeField] private Animator animator;


        private PlayerBehavior playerBehavior;
        private PlayerControlContext playerControlContext;
        private CharacterController _controller;

        private Vector3 _playerVelocity;
        private int _aimLayerIndex;
        private bool _isAimingNow;


        private static readonly int fastAttack = Animator.StringToHash("FastAttack");
        private static readonly int canAttack = Animator.StringToHash("CanAttack");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsAiming = Animator.StringToHash("IsAiming");
        //private static readonly int jump = Animator.StringToHash("Jump");

        private GameState currentGameState;


        private void Awake(){
            if (playerBehavior is null) playerBehavior = GetComponent<PlayerBehavior>();
            playerControlContext = new(PlayerState.Normal);
            Game.Game.Manager.OnInitialized += ManagerOnInitialized;
        }

        private void ManagerOnInitialized() {
            Game.Game.Manager.OnInitialized -= ManagerOnInitialized;
            currentGameState = Game.Game.Manager.GameStateManager.CurrentGameState;
            Game.Game.Manager.GameStateManager.Register(this);
        }

        private void Start() {
            _controller = gameObject.GetComponent<CharacterController>();
            _aimLayerIndex = animator.GetLayerIndex("UpperBody");

            playerBehavior.AmmoReplenished += OnAmmoReplenished;
            playerBehavior.AmmoIsOver += OnAmmoIsOver;
            InputManager.Instance.FastAttackPerformed += OnFastAttackPerformed;
            InputManager.Instance.JumpPerformed += OnJumpPerformed;
            InputManager.Instance.AimStarted += OnAimStarted;
            InputManager.Instance.AimCanceled += OnAimCanceled;
            InputManager.Instance.AimPerformed += OnAimPerformed;
            InputManager.Instance.AssistanControllStarted += OnAssistanControllStarted;
            InputManager.Instance.AssistanControllCanceled += OnAssistanControllCanceled;
        }

        public PlayerControlContext GetContext() => playerControlContext;
        public void Move() {


            if (_controller == null) return;
            if (_controller.isGrounded && _playerVelocity.y < 0)
                _playerVelocity.y = 0f;

            PlayerMove();

            _playerVelocity.y += gravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);
        }

        private void PlayerMove()
        {
            var movement = InputManager.Instance.GetPlayerMovement();
            var move = new Vector3(movement.x, 0, movement.y);
            move = Camera.main!.transform.forward * move.z + Camera.main!.transform.right * move.x;
            move.y = 0;
            _controller.Move(move * (Time.deltaTime * playerSpeed));


            if (move != Vector3.zero)
            {
                animator.SetBool(IsMoving, true);
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
                transform.forward = Vector3.Lerp(transform.forward, move, Time.deltaTime * 10);
            }
            else
            {
                animator.SetBool(IsMoving, false);
                if (_isAimingNow == true)
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
        }

        private void OnJumpPerformed() {
            if (_controller.isGrounded is false) return;
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -1.0f * gravityValue);
            //animator.SetTrigger(jump);
        }
        private void OnAimStarted()
        {
            if (currentGameState is not GameState.Gameplay) return;


            switch (playerControlContext.GetPlayerState())
            {
                case PlayerState.AssistantControl:
                    AssistantControl(typeof(AssistantDisposer));
                    break;
                default: return;
            }

        }
        private void OnAimPerformed()
        {
            if (currentGameState is not GameState.Gameplay) return;

            if (playerBehavior.isAmmoEmpty(playerBehavior.GetCurrentWeapon()) is true) return;

            _isAimingNow = true;
            playerControlContext.SetPlayerState(PlayerState.Aim);
            animator.SetBool(IsAiming, true);
            animator.SetLayerWeight(_aimLayerIndex, 1);
        }
        private void OnAimCanceled()
        {
            if (currentGameState is not GameState.Gameplay) return;

            if (_isAimingNow == false) return;

            _isAimingNow = false;
            playerControlContext.SetPlayerState(PlayerState.Normal);
            animator.SetBool(IsAiming, false);
            if (animator.GetCurrentAnimatorStateInfo(_aimLayerIndex).IsName("Idle"))
                animator.SetLayerWeight(_aimLayerIndex, 0);
        }
        private void OnAssistanControllStarted()
        {
            animator.SetBool(canAttack, false);

            playerControlContext.SetPlayerState(PlayerState.AssistantControl);
            Game.Game.Manager.GameStateManager.SetState(GameState.AssistentControl);

            animator.SetBool(IsMoving, false);

            AssistantControl(typeof(AssistantDisposer));
        }
        private void OnAssistanControllCanceled()
        {
            animator.SetBool(canAttack, true);

            Game.Game.Manager.GameStateManager.SetState(GameState.Gameplay);
            playerControlContext.SetPlayerState(PlayerState.Normal);
        }

        private void AssistantControl(Type assistantType)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitinfo);
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                Game.Game.Manager.MoveAssistant(hitinfo.point, assistantType);
        }
        private void OnFastAttackPerformed()
        {
            if (currentGameState is not GameState.Gameplay) return;

            if (animator.GetBool(fastAttack) == true) animator.ResetTrigger(fastAttack);
            if (playerControlContext.GetPlayerState() == PlayerState.Normal &&
                animator.GetBool(canAttack) == true)
            {
                animator.SetTrigger(fastAttack);
            }
        }
        private void OnAmmoReplenished() => animator.SetBool(canAttack, true);
        private void OnAmmoIsOver() {
            animator.SetBool(canAttack, false);
            animator.ResetTrigger(fastAttack);
        }
        private void OnDisable() {
            playerBehavior.AmmoReplenished -= OnAmmoReplenished;
            playerBehavior.AmmoIsOver -= OnAmmoIsOver;
            InputManager.Instance.FastAttackPerformed -= OnFastAttackPerformed;
            InputManager.Instance.JumpPerformed -= OnJumpPerformed;
            InputManager.Instance.AimStarted -= OnAimStarted;
            InputManager.Instance.AimCanceled -= OnAimCanceled;
            InputManager.Instance.AimPerformed -= OnAimPerformed;
            InputManager.Instance.AssistanControllStarted -= OnAssistanControllStarted;
            InputManager.Instance.AssistanControllCanceled -= OnAssistanControllCanceled;

        }
        private void OnEnable()
        {
            if (InputManager.Instance is not null) {
                InputManager.Instance.FastAttackPerformed += OnFastAttackPerformed;
                InputManager.Instance.JumpPerformed += OnJumpPerformed;
                InputManager.Instance.AimStarted += OnAimStarted;
                InputManager.Instance.AimCanceled += OnAimCanceled;
                InputManager.Instance.AimPerformed += OnAimPerformed;
                InputManager.Instance.AssistanControllStarted += OnAssistanControllStarted;
                InputManager.Instance.AssistanControllCanceled += OnAssistanControllCanceled;
            }
            if (playerBehavior is not null) {
                playerBehavior.AmmoReplenished += OnAmmoReplenished;
                playerBehavior.AmmoIsOver += OnAmmoIsOver;
            }
        }

        public void GameStateHandle(GameState gameState) {
            currentGameState = gameState;
        }
    }
}