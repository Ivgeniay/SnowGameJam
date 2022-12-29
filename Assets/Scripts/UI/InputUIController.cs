using Assets.Scripts.Game.Pause;
using System;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class InputUIController : MonoBehaviour, IGameStateHandler
    {
        [SerializeField] private GameObject pauseMenuGO;
        private GameState currentState;

        private void Awake() {
            Game.Game.Manager.OnInitialized += OnGameInitialized;
        }

        private void OnGameInitialized() {
            Game.Game.Manager.GameStateManager.Register(this);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape) && currentState == GameState.Gameplay) {
                PauseEnable();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && currentState == GameState.Pause) {
                PauseDisable();
            }
        }

        private void PauseDisable()
        {
            pauseMenuGO.SetActive(false);
            Game.Game.Manager.GameStateManager.SetState(GameState.Gameplay);
        }

        public void PauseEnable()
        {
            pauseMenuGO.SetActive(true);
            Game.Game.Manager.GameStateManager.SetState(GameState.Pause);
        }

        public void GameStateHandle(GameState gameState) {
            currentState = gameState;
        }
    }
}
