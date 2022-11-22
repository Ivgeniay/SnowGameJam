using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private PlayerController controller;

        [SerializeField] private Transform normalCamera;
        [SerializeField] private Transform controlCamera;
        [SerializeField] private Transform aimCamera;

        private CinemachineVirtualCamera normalCinemachineVirtualCamera;
        private CinemachineVirtualCamera controlCinemachineVirtualCamera;
        private CinemachineVirtualCamera aimCinemachineVirtualCamera;

        private void Awake()
        {
            controlCinemachineVirtualCamera = controlCamera.GetComponent<CinemachineVirtualCamera>();
            normalCinemachineVirtualCamera = normalCamera.GetComponent<CinemachineVirtualCamera>();
            aimCinemachineVirtualCamera = aimCamera.GetComponent<CinemachineVirtualCamera>();

        }

        private void Start()
        {
            controller.playerControlContext.OnPlayerStateChanged += OnPlayerStateChangedHandler;
            
        }

        private void OnPlayerStateChangedHandler(object sender, PlayerState e)
        {
            switch (controller.playerControlContext.GetPlayerState())
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
            //if (controlCinemachineVirtualCamera != null) {
            //    controlCinemachineVirtualCamera.Priority = 11;
            //}
            aimCamera.gameObject.SetActive(false);
            normalCamera.gameObject.SetActive(false);
            controlCamera.gameObject.SetActive(true);

            Game.Game.Manager.cursorSetting.ShowUnlock();
        }
        private void SetNormalCamera() {
            aimCamera.gameObject.SetActive(false);
            normalCamera.gameObject.SetActive(true);
            controlCamera.gameObject.SetActive(false);

            Game.Game.Manager.cursorSetting.HideLock();
        }
        private void SetAimCamera()
        {
            aimCamera.gameObject.SetActive(true);
            normalCamera.gameObject.SetActive(false);
            controlCamera.gameObject.SetActive(false);
        }
    }
}

public class CameraSetting
{
    public int priority;
}