using TMPro;
using UnityEngine;
using System;
using Assets.Scripts.Units.DamageMech;
using Assets.Scripts.Spawner;

namespace Assets.Scripts.UI
{
    public class Score : MonoBehaviour, IRestartable
    {
        [SerializeField] private TextMeshProUGUI score_TextMeshProUGUI;


        private void Awake() {
            Game.Game.Manager.OnInitialized += GameManagerOnInitialized;
        }

        private void GameManagerOnInitialized() {
            Game.Game.Manager.Score.OnScoreChange += Score_OnScoreChange;
        }

        private void Score_OnScoreChange(int obj) {
            score_TextMeshProUGUI.text = obj.ToString();
        }

        public void Restart() {
            //score_TextMeshProUGUI.text = "0";
        }

    }
}
