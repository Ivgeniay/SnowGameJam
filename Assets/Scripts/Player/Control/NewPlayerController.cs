using Assets.Scripts.Game.Pause;
using Assets.Scripts.Player.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Assets.Scripts.Player
{
    public class NewPlayerController : MonoBehaviour, IControllable, IGameStateHandler
    {
        public event Action<bool> OnIsGround;

        [SerializeField] private float playerSpeed = 2.0f;
        [SerializeField] private float acceleration = 1.0f;
        [SerializeField] private float slowdown = 1.0f;
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float gravityValue = -9.81f;
        [SerializeField] private Animator animator;


        private PlayerBehavior playerBehavior;
        private PlayerControlContext playerControlContext;
        private CharacterController _controller;

        [SerializeField] private Vector3 _playerVelocity;
        private Vector3 pastMoveDirection;
        private CapsuleCollider _capsuleCollider;

        private int LegsLayer; 
        private int HandsLayer;
        private int BaseLayer;


        private bool _isAimingNow = false;
        private bool isAimingNow { get =>_isAimingNow; 
            set {
                _isAimingNow = value;
                SetAimAnimator(animator, _isAimingNow);
            }
        }

        [SerializeField] private LayerMask groundLayerMask;
        private bool _isGround;
        private bool isGround
        {
            get => _isGround;
            set {
                _isGround = value;
                OnIsGround?.Invoke(value);
            }
        }


        private GameState currentGameState;

        private void Awake()
        {
            if (playerBehavior is null) playerBehavior = GetComponent<PlayerBehavior>();
            playerControlContext = new(PlayerState.Normal);
            _capsuleCollider = GetComponent<CapsuleCollider>();
            Game.Game.Manager.OnInitialized += ManagerOnInitialized;
            OnIsGround += OnIsGroundHandler;
        }

        private void ManagerOnInitialized()
        {
            Game.Game.Manager.OnInitialized -= ManagerOnInitialized;
            currentGameState = Game.Game.Manager.GameStateManager.CurrentGameState;
            Game.Game.Manager.GameStateManager.Register(this);
        }

        private void Start()
        {
            if (_controller is null)
                _controller = gameObject.GetComponent<CharacterController>();

            LegsLayer = animator.GetLayerIndex(AnimationConstants.LegsLayer);
            HandsLayer = animator.GetLayerIndex(AnimationConstants.HandsLayer);
            BaseLayer = animator.GetLayerIndex(AnimationConstants.BaseLayer);


            //playerBehavior.AmmoReplenished += OnAmmoReplenished;
            //playerBehavior.AmmoIsOver += OnAmmoIsOver;
            //InputManager.Instance.FastAttackPerformed += OnFastAttackPerformed;
            InputManager.Instance.JumpPerformed += OnJumpPerformed;
            //InputManager.Instance.AimStarted += OnAimStarted;
            InputManager.Instance.AimCanceled += OnAimCanceled;
            InputManager.Instance.AimPerformed += OnAimPerformed;
            //InputManager.Instance.AssistanControllStarted += OnAssistanControllStarted;
            //InputManager.Instance.AssistanControllCanceled += OnAssistanControllCanceled;
        }


        public void MoveUpdate()
        {
            if (_controller == null) return;

            var groundedPlayer = _controller.isGrounded;
            if (groundedPlayer && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }

            //Debug.Log(TestMethod());
            Debug.Log(groundedPlayer);

            CheckIsGround(_controller.isGrounded);
            PlayerMove();
            //ApplyGravity(isGround);

            _playerVelocity.y += gravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);
        }

        private bool TestMethod()
        {
            return Physics.CheckSphere(transform.position, 0);
        }

        private void PlayerMove()
        {
            var moveDirection = FindMoveDirection();
            _controller.Move(moveDirection * (GetCurrentMoveSpeedFromAnimator(animator, playerSpeed) * Time.deltaTime));


            if (moveDirection != Vector3.zero) {
                RotatePlayerAhead();
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * 10);

                pastMoveDirection = moveDirection;
                SetFractionToAnimator(animator, CalculateFractionToAnimator(animator, 1, acceleration) );
            }
            else {
                if (_isAimingNow == true) 
                        RotatePlayerAhead();

                if (GetFractionFromAnimator(animator) > 0.002f) {
                    SetFractionToAnimator(animator, CalculateFractionToAnimator(animator, 0, slowdown));
                    _controller.Move(pastMoveDirection * (GetCurrentMoveSpeedFromAnimator(animator, playerSpeed) * Time.deltaTime));
                }
            }
        }

        #region Direction
        private Vector3 FindMoveDirection()
        {
            var movement = InputManager.Instance.GetPlayerMovement();
            var move = new Vector3(movement.x, 0, movement.y);
            move = Camera.main!.transform.forward * move.z + Camera.main!.transform.right * move.x;
            move.y = 0;

            return move;
        }
        private Vector3 RotatePlayerAhead() => transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);

        #endregion

        #region Speed
        private float GetFractionFromAnimator(Animator animator) => animator.GetFloat(AnimationConstants.BaseLayerBlend);
        private void SetFractionToAnimator(Animator animator, float currentFraction) => animator.SetFloat(AnimationConstants.BaseLayerBlend, currentFraction);
        private float CalculateFractionToAnimator(Animator animator, float goalFraction, float acceleration) => Mathf.Lerp(GetFractionFromAnimator(animator), goalFraction, acceleration * Time.fixedDeltaTime);
        private float GetCurrentMoveSpeedFromAnimator(Animator animator, float maxSpeed) => Mathf.Lerp(0, maxSpeed, GetFractionFromAnimator(animator));
        #endregion

        #region Jump/Gravity
        private void OnJumpPerformed()
        {
            if (isGround) 
                _playerVelocity.y += Mathf.Sqrt(jumpHeight * -1.0f * gravityValue);
        }

        private void ApplyGravity(bool isGrounded)
        {
            if (isGrounded is false)
            {
                _playerVelocity.y += gravityValue * Time.deltaTime;
                _controller.Move(_playerVelocity * Time.deltaTime);
            }
            else
            {
                if (_playerVelocity.y < 0f)
                {
                    _playerVelocity.y = 0f;
                }
            }
        }

        private void CheckIsGround(bool value) {
            if (value == isGround) return ;
            isGround = value;
        }

        private void OnIsGroundHandler(bool value)
        {
            if (value is true) {
                animator.SetBool(AnimationConstants.IsGround, value);
                animator.SetLayerWeight(LegsLayer, 0f);
                animator.SetTrigger(AnimationConstants.JumpTrigger);
                //Debug.Log(_isGround);
            }
            else if (value is false) {
                animator.SetBool(AnimationConstants.IsGround, value);
                animator.SetLayerWeight(LegsLayer, 1f);
                //Debug.Log(_isGround);
            }
        }
        #endregion

        #region AIM
        private void OnAimCanceled() {
            playerControlContext.SetPlayerState(PlayerState.Normal);
            isAimingNow = false;
        }
        private void OnAimPerformed() {
            if (currentGameState is not GameState.Gameplay) return;
            if (playerBehavior.isAmmoEmpty(playerBehavior.GetCurrentWeapon()) is true) return;
            isAimingNow = true;
            playerControlContext.SetPlayerState(PlayerState.Aim);
        } 
        private void SetAimAnimator(Animator animator, bool isAiming) => animator.SetBool(AnimationConstants.IsAiming, isAiming);
        
        #endregion


        private void OnDisable()
        {
        //    playerBehavior.AmmoReplenished -= OnAmmoReplenished;
        //    playerBehavior.AmmoIsOver -= OnAmmoIsOver;
        //    InputManager.Instance.FastAttackPerformed -= OnFastAttackPerformed;
            InputManager.Instance.JumpPerformed -= OnJumpPerformed;
            //    InputManager.Instance.AimStarted -= OnAimStarted;
            InputManager.Instance.AimCanceled -= OnAimCanceled;
            InputManager.Instance.AimPerformed -= OnAimPerformed;
            //    InputManager.Instance.AssistanControllStarted -= OnAssistanControllStarted;
            //    InputManager.Instance.AssistanControllCanceled -= OnAssistanControllCanceled;

        }
        private void OnEnable()
        {
            if (InputManager.Instance is not null)
            {
        //        InputManager.Instance.FastAttackPerformed += OnFastAttackPerformed;
                InputManager.Instance.JumpPerformed += OnJumpPerformed;
        //        InputManager.Instance.AimStarted += OnAimStarted;
                InputManager.Instance.AimCanceled += OnAimCanceled;
                InputManager.Instance.AimPerformed += OnAimPerformed;
        //        InputManager.Instance.AssistanControllStarted += OnAssistanControllStarted;
        //        InputManager.Instance.AssistanControllCanceled += OnAssistanControllCanceled;
            }
            if (playerBehavior is not null)
            {
        //        playerBehavior.AmmoReplenished += OnAmmoReplenished;
        //        playerBehavior.AmmoIsOver += OnAmmoIsOver;
            }
        }


        public void GameStateHandle(GameState gameState)
        {
            currentGameState = gameState;
        }
        public PlayerControlContext GetContext() => playerControlContext;

        private void OnValidate() {
            if (playerSpeed < 0) playerSpeed = 0;
            if (acceleration < 0) acceleration = 0f;
            if (slowdown < 0) slowdown = 0f;
            if (jumpHeight < 0) jumpHeight = 0f;
        }
    }
}