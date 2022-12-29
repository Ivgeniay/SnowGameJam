using Assets.Scripts.Game.Pause;
using Assets.Scripts.PeripheralManagement._Cursor;
using Assets.Scripts.Utilities;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class GameOverUI : MonoBehaviour, IGameStateHandler
    {
        [SerializeField] private GameObject gameOverUiComponent;
        [SerializeField] private TextMeshProUGUI scoretextMeshProComponent;

        private int score;

        private void Awake() {
            Game.Game.Manager.OnInitialized += OnGameInitialized;
            Game.Game.Manager.OnScoreChanged += OnScoreChangedHandler;
        }

        private void OnScoreChangedHandler(int obj)
        {
            score = obj;
        }

        private void OnGameInitialized() {
            Game.Game.Manager.GameStateManager.Register(this);
        }

        public void GameStateHandle(GameState gameState)
        {
            if(gameState == GameState.GameOver) {
                gameOverUiComponent.SetActive(true);
                //gameOverUiComponent.GetComponent<RectTransform>().localPosition = new Vector3(34, 0, 0);
                scoretextMeshProComponent.text = "SCORE: " + Game.Game.Manager.Score.CurrentScore.ToString();
            }
            else
            {
                gameOverUiComponent.SetActive(false);
            }
        }


        //Vector3(34,0,0)
        public void OnReset()
        {
            //gameOverUiComponent.SetActive(false);
            Coroutines.StopAll();
            //if (gameOverUiComponent != null) gameOverUiComponent = GameObject.FindGameObjectWithTag("GameOverUI");
            //gameOverUiComponent.GetComponent<RectTransform>().localPosition = new Vector3(3400, 0, 0);
            //Game.Game.Manager.Restart.RestartGame();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            Game.Game.Manager.Restart.RestartGame();
        }

        public void OnExit()
        {
            Application.Quit();
        }
    }
}
