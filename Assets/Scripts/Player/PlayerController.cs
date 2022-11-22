using Assets.Scripts.Enemies.StateMech;
using Assets.Scripts.Game;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private KeyCode AssistantControllButton;
        [SerializeField] private float playerSpeed = 2.0f;
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float gravityValue = -9.81f;
        [SerializeField] private float aiminDelay = 0.3f;

        [SerializeField] private Transform snowBallPrefab;
        [SerializeField] private Transform snowBallSpawnPoint;
        [SerializeField] private Projection projection;
        [SerializeField] private Transform normalCamera;
        [SerializeField] private Transform aimCamera;
        [SerializeField] private Animator animator;
        private Inventory inventory;

        private CharacterController _controller;
        private Vector3 _playerVelocity;
        private bool _groundedPlayer;
        private float _forceIncreaseInSecond = 500f;
        private float _force = 1000f;
        private float _maxForce = 5000f;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsAiming = Animator.StringToHash("IsAiming");
        private static readonly int IsAttackWithoutAim = Animator.StringToHash("Attack");

        public PlayerControlContext playerControlContext;

        private int _aimLayerIndex;

        private bool aimCanceled;
        private float timer;


        private void Awake()
        {
            playerControlContext = new(PlayerState.Normal);
            projection = GetComponent<Projection>();
            inventory = GetComponent<Inventory>();
        }

        private void Start()
        {
            _controller = gameObject.GetComponent<CharacterController>();
            _aimLayerIndex = animator.GetLayerIndex("UpperBody");
            InputManager.Instance.OnAimCanceled += Instance_OnAimCanceled;
        }

        private void Update()
        {

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


            var movement = InputManager.Instance.GetPlayerMovement();
            var move = new Vector3(movement.x, 0, movement.y);
            move = Camera.main!.transform.forward * move.z + Camera.main!.transform.right * move.x;
            move.y = 0;
            _controller.Move(move * (Time.deltaTime * playerSpeed));


            if (move != Vector3.zero)
            {
                animator.SetBool(IsMoving, true);
                transform.forward = Vector3.Lerp(transform.forward, move, Time.deltaTime * 10);
            }
            else
                animator.SetBool(IsMoving, false);

            if (inventory.isEmpty() is false)
            {
                if (InputManager.Instance.IsPlayerAiming())
                {
                    IncreaseCounter();

                    if (timer >= aiminDelay)
                    {
                        playerControlContext.SetPlayerState(PlayerState.Aim);

                        animator.SetBool(IsAiming, true);
                        animator.SetLayerWeight(_aimLayerIndex, 1);
                        var newForward = Vector3.Lerp(transform.forward, Camera.main!.transform.forward, Time.deltaTime * 30);
                        newForward.y = 0;
                        transform.forward = newForward;
                        projection.EnableLine();
                        if (_force < _maxForce)
                            _force += _forceIncreaseInSecond * Time.deltaTime;
                        else
                            _force = _maxForce;
                        projection.SimulateTrajectory(snowBallPrefab.GetComponent<SnowBallProjectile>(), snowBallSpawnPoint.position, ((Vector3.up / 10f) + (Camera.main!.transform.forward)) * _force);
                    }
                }
                else if (timer <= aiminDelay && aimCanceled && animator.GetBool(IsMoving) is false)
                {
                    playerControlContext.SetPlayerState(PlayerState.Normal);

                    timer = 0;
                    animator.SetBool(IsAttackWithoutAim, true);
                }
                else if (timer <= aiminDelay && aimCanceled && animator.GetBool(IsMoving) is true)
                {
                    animator.SetLayerWeight(_aimLayerIndex, 2);
                    animator.SetBool(IsAttackWithoutAim, true);
                }
                else
                {
                    playerControlContext.SetPlayerState(PlayerState.Normal);

                    timer = 0;

                    animator.SetBool(IsAiming, false);

                    if (animator.GetCurrentAnimatorStateInfo(_aimLayerIndex).IsName("Idle"))
                        animator.SetLayerWeight(_aimLayerIndex, 0);

                    animator.SetBool(IsAttackWithoutAim, false);

                    projection.DisableLine();
                }
            }

            // Changes the height position of the player..
            if (InputManager.Instance.PlayerJumpedThisFrame() && _groundedPlayer)
            {
                _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            _playerVelocity.y += gravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);
        }

        private void LateUpdate()
        {
            aimCanceled = false;
        }

        public void OnAttack()
        {
            var snowBall = Instantiate(snowBallPrefab, snowBallSpawnPoint.position, snowBallSpawnPoint.rotation);
            var snowBallscr = snowBall.GetComponent<SnowBallProjectile>();
            snowBallscr.Creator = transform;
            snowBallscr.Setup(((Vector3.up / 10f) + (Camera.main!.transform.forward)) * _force);
            _force = 1000f;
            inventory.decrimentSnowball();
        }

        private void Instance_OnAimCanceled()
        {
            aimCanceled = true;
        }

        private void IncreaseCounter()
        {
            timer += Time.deltaTime;
        }

        private void AssistantControl(Type assistantType)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitinfo);
            if (Input.GetMouseButtonDown(0))
                Game.Game.Manager.MoveAssistant(hitinfo.point, assistantType);
        }
    }
}
