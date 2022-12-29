using Assets.Scripts.Player;
using Assets.Scripts.Player.Control;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Assets.Scripts.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform Player;
        private IControllable controller;
        private PlayerControlContext playerControlContext;

        [SerializeField] private Transform normalCamera;
        [SerializeField] private Transform controlCamera;
        [SerializeField] private Transform aimCamera;

        private CinemachineVirtualCamera normalCinemachineVirtualCamera { get; set; }
        private CinemachineVirtualCamera controlCinemachineVirtualCamera { get; set; }
        private CinemachineVirtualCamera aimCinemachineVirtualCamera { get; set; }


        private void Awake()
        {

            controlCinemachineVirtualCamera = controlCamera.GetComponent<CinemachineVirtualCamera>();
            normalCinemachineVirtualCamera = normalCamera.GetComponent<CinemachineVirtualCamera>();
            aimCinemachineVirtualCamera = aimCamera.GetComponent<CinemachineVirtualCamera>();

        }

        private void Start() {
            controller = Player.GetComponent<IControllable>();
            playerControlContext = controller.GetContext();
            playerControlContext.OnPlayerStateChanged += OnPlayerStateChangedHandler;
        }

        private void OnPlayerStateChangedHandler(object sender, PlayerState e)
        {
            switch (playerControlContext.GetPlayerState())
            {
                case PlayerState.Aim:
                    SetAimCamera();
                break;
                case PlayerState.AssistantControl:
                    SetAssistantControlCamera();
                break;
                default:
                    SetNormalCamera();
                break;
            }
        }

        private void SetAssistantControlCamera() {
            controlCinemachineVirtualCamera.Priority = 3;
            normalCinemachineVirtualCamera.Priority = 2;
            aimCinemachineVirtualCamera.Priority = 1;
        }
        private void SetNormalCamera() {
            controlCinemachineVirtualCamera.Priority = 1;
            normalCinemachineVirtualCamera.Priority = 3;
            aimCinemachineVirtualCamera.Priority = 2;
        }
        private void SetAimCamera() {
            controlCinemachineVirtualCamera.Priority = 1;
            normalCinemachineVirtualCamera.Priority = 2;
            aimCinemachineVirtualCamera.Priority = 3;
        }
    }
}

public class CameraSetting
{
    public int priority;
}