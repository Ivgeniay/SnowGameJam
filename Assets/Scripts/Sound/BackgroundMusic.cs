using Assets.Scripts.Game.Pause;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Sound
{
    public class BackgroundMusic : MonoBehaviour, IGameStateHandler
    {
        [SerializeField] private string[] menuMusic;
        [SerializeField] private string[] gamePlayMusic;
        [SerializeField] public AudioSource audioSource;

        private GameState previouslyState;

        private void Awake()
        {
            Game.Game.Manager.OnInitialized += OnGameInitialized;
        }

        private void OnGameInitialized() {
            Game.Game.Manager.GameStateManager.Register(this);
            previouslyState = Game.Game.Manager.GameStateManager.CurrentGameState;
        }

        private void Start() {
            PlayMenuMusic();
        }

        public void PlayMenuMusic()
        {
            if (audioSource is not null && menuMusic.Length > 0)
            {
                AudioManager.instance.PlaySound(menuMusic[Random.Range(0, menuMusic.Length)], audioSource);
            }
        }

        public void PlayGamplaySound()
        {
            if (audioSource is not null && gamePlayMusic.Length > 0) AudioManager.instance.PlaySound(gamePlayMusic[Random.Range(0, gamePlayMusic.Length)], audioSource);
        }

        public void StopSound()
        {
            audioSource?.Stop();
        }

        private void OnDestroy() {
            Game.Game.Manager.GameStateManager.Unregister(this);
        }

        public void GameStateHandle(GameState gameState) {
            if (gameState == GameState.beforeGamePlay) {
                PlayMenuMusic();
            }
            else if (gameState == GameState.Gameplay && previouslyState == GameState.beforeGamePlay) {
                PlayGamplaySound();
            }

            previouslyState = gameState;
        }
    }
}
