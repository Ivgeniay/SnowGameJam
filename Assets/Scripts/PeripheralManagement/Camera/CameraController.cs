using Assets.Scripts.Player;
using Assets.Scripts.Player.Control;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Assets.Scripts.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private PlayerController PlayerController;
        private IControllable controller;
        private PlayerControlContext playerControlContext;

        [SerializeField] private Transform normalCamera;
        [SerializeField] private Transform controlCamera;
        [SerializeField] private Transform aimCamera;

        private CinemachineFreeLook normalCinemachineVirtualCamera { get; set; }
        private CinemachineVirtualCamera controlCinemachineVirtualCamera { get; set; }
        private CinemachineVirtualCamera aimCinemachineVirtualCamera { get; set; }


        private void Awake()
        {

            controlCinemachineVirtualCamera = controlCamera.GetComponent<CinemachineVirtualCamera>();
            normalCinemachineVirtualCamera = normalCamera.GetComponent<CinemachineFreeLook>();
            aimCinemachineVirtualCamera = aimCamera.GetComponent<CinemachineVirtualCamera>();

        }

        private void Start() {
            controller = PlayerController.GetComponent<IControllable>();
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

            Game.Game.Manager.cursorSetting.ShowUnlock();
        }
        private void SetNormalCamera() {
            controlCinemachineVirtualCamera.Priority = 1;
            normalCinemachineVirtualCamera.Priority = 3;
            aimCinemachineVirtualCamera.Priority = 2;

            Game.Game.Manager.cursorSetting.HideLock();
        }
        private void SetAimCamera() {
            controlCinemachineVirtualCamera.Priority = 1;
            normalCinemachineVirtualCamera.Priority = 2;
            aimCinemachineVirtualCamera.Priority = 3;

            Game.Game.Manager.cursorSetting.HideLock();
        }
    }
}

public class CameraSetting
{
    public int priority;
}