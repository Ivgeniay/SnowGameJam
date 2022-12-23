using TMPro;
using UnityEngine;
using System;
using Assets.Scripts.Units.DamageMech;
using Assets.Scripts.Spawner;

namespace Assets.Scripts.UI
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI score_TextMeshProUGUI;
        [SerializeField] private int OnDeathEnemyScore;
        [SerializeField] private int OnHeadEnemyScore;
        [SerializeField] private int OnStageCompleteScore;

        private void Awake() {
            Game.Game.Manager.OnInitialized += GameManagerOnInitialized;
        }

        private void GameManagerOnInitialized() {
            Game.Game.Manager.OnInitialized -= GameManagerOnInitialized;
            Game.Game.Manager.OnNpcDied += OnDeathNpcDestroy;
            Game.Game.Manager.OnNpcGetDamage += OnNpcGetDamage;
            Game.Game.Manager.OnStageComplete += SpawnerControllerOnStageComplete;
        }

        private void SpawnerControllerOnStageComplete(int obj) =>
            score_TextMeshProUGUI.text = ParseAndCalculateScore(score_TextMeshProUGUI.text, OnStageCompleteScore);

        private void OnDeathNpcDestroy(object sender, OnNpcDieEventArg e) =>
            score_TextMeshProUGUI.text = ParseAndCalculateScore(score_TextMeshProUGUI.text, OnDeathEnemyScore);
        

        private void OnNpcGetDamage(object sender, EventArgs.TakeDamagePartEventArgs e) {
            var head = e.SenderPartOfBody as IUltimateDamageArea;
            if (head is not null) score_TextMeshProUGUI.text = ParseAndCalculateScore(score_TextMeshProUGUI.text, OnHeadEnemyScore);
        }

        private string ParseAndCalculateScore (string msg, int secondNumber) {
            if (int.TryParse(msg, out int score)) msg = (score + secondNumber).ToString();
            else throw new Exception(this + " try parse score invalid");
            return msg;
        }

        private void OnDisable() {
            Game.Game.Manager.OnNpcDied -= OnDeathNpcDestroy;
            Game.Game.Manager.OnNpcGetDamage -= OnNpcGetDamage;
            Game.Game.Manager.OnStageComplete -= SpawnerControllerOnStageComplete;
        }
    }
}
