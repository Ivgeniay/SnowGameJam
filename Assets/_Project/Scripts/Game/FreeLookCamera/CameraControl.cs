
using Assets._Project.Scripts._Input._InputAction;
using Assets._Project.Scripts.Ammo.Bullet;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Game.FreeLookCamera
{
    internal class CameraControl : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform bullet;
        [SerializeField] private Transform followTarget;
        [SerializeField] private float speed;

        [SerializeField] private ShootType type;
        [SerializeField] private float force;

        private InputManager inputManager;
        private InputAct.Player_Actions playerAction;


        [Inject]
        private void Construct(InputManager inputManager) {
            this.inputManager = inputManager;
        }

        private void Awake()
        {
            playerAction = inputManager.GetPlayerInput();
        }

        private void Update()
        {
            if (followTarget is null) return;

            var move = FindMoveDirection();
            followTarget.transform.position = followTarget.transform.position + (move * speed);
        }

        private void OnEnable()
        {
            playerAction.Shoot.performed += ShootPerformed;
        }


        private void OnDisable()
        {
            playerAction.Shoot.performed -= ShootPerformed;
        }
        private void ShootPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (bullet is null) return;

            var camera = Camera.main;

            var inst = Instantiate(bullet, camera.transform.position, Quaternion.identity);
            inst.GetComponent<Bullet>().SetUp(type);
            var rb = inst.GetComponent<Rigidbody>();
            var direction = camera.transform.forward;

            var _force = force;

            if (type == ShootType.ordinary) {
                _force = force / 3;
            }

            rb.AddForce(direction * _force);
        }

        private Vector3 FindMoveDirection()
        {
            var movement = playerAction.Move.ReadValue<Vector2>();
            var move = new Vector3(movement.x, 0, movement.y);
            move = _camera!.transform.forward * move.z + _camera!.transform.right * move.x;
            move.y = 0;

            return move;
        }
    }
}
