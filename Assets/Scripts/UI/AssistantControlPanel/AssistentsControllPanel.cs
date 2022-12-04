using Assets.Scripts.Game.Pause;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Assets.Scripts.UI.AssistantControlPanel
{
    public class AssistentsControllPanel : MonoBehaviour, IGameStateHandler
    {
        [Required]
        [SerializeField]
        private GameObject panel;
        private void Awake()
        {
            Game.Game.Manager.OnInitialized += GameManagerOnInitialized;
        }

        private void GameManagerOnInitialized()
        {
            Game.Game.Manager.GameStateManager.Register(this);
        }
        public void GameStateHandle(GameState state)
        {
            switch (state)
            {
                case GameState.AssistentControl:
                    panel.SetActive(true);
                    break;
                default:
                    panel.SetActive(false);
                    break;
            }
        }

    }
}
