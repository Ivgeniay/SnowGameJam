using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIManager : MonoBehaviour, IRestartable
    {
        [SerializeField] private GameObject PauseGO;
        [SerializeField] private GameObject GameOverGO;
        private void Awake()
        {
            Game.Game.Manager.OnInitialized += OnGameInitialized;
        }

        private void OnGameInitialized()
        {
            Game.Game.Manager.Restart.Register(this);
        }

        public void Restart()
        {
            PauseGO.SetActive(true);
            //GameOverGO.SetActive(false);
        }
    }
}
