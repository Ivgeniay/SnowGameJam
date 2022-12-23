using Assets.Scripts.Game.Pause;
using Assets.Scripts.Player;
using Assets.Scripts.Units.StateMech;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;

namespace Assets.Scripts.Units.AssistantControl
{
    public class AssistentsControll : MonoBehaviour, IGameStateHandler
    {
        private bool canControlAssistants = false;
        [SerializeField]private LayerMask GroundlayerMask;

        private void Start()
        {
            InputManager.Instance.AssistanControllStarted += OnAssistanControllStarted;
            InputManager.Instance.AssistanControllCanceled += OnAssistanControllCanceled;
        }

        private void Update() {
            if (canControlAssistants is false) return;
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                CommandToGoAssistant(FindPointDestination(), typeof(AssistantDisposer));
        }

        private void OnAssistanControllStarted() {
            Game.Game.Manager.GameStateManager.SetState(GameState.AssistentControl);
            canControlAssistants = true;
        }

        private void OnAssistanControllCanceled() {
            canControlAssistants = false;
            Game.Game.Manager.GameStateManager.SetState(GameState.Gameplay);
        }

        public void GameStateHandle(GameState gameState)
        {
            
        }

        private Vector3 FindPointDestination() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitinfo, 1000, GroundlayerMask);
            return hitinfo.point;
        }

        private void CommandToGoAssistant(Vector3 pointDestination, Type assistantType) => Game.Game.Manager.MoveAssistant(pointDestination, assistantType);
        

        private void OnEnable() {
            if (InputManager.Instance is not null) {
                InputManager.Instance.AssistanControllStarted += OnAssistanControllStarted;
                InputManager.Instance.AssistanControllCanceled += OnAssistanControllCanceled;
            }
        }

        private void OnDisable() {
            InputManager.Instance.AssistanControllStarted -= OnAssistanControllStarted;
            InputManager.Instance.AssistanControllCanceled -= OnAssistanControllCanceled;
        }
    }
}
