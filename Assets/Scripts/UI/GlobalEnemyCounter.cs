using UnityEngine;
using TMPro;
using System;
using System.Globalization;

namespace Assets.Scripts.UI
{
    public class GlobalEnemyCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text textMeshPro;
        private int counter = 0;

        private void Awake() {
            Game.Game.Manager.OnInitialized += GameOnInitializedHandler;
            Game.Game.Manager.OnNpcInstantiate += OnNpcInstantiateHandler;
            Game.Game.Manager.OnNpcDied += OnDeathNpcDestroyHandler;
        }

        private void OnDeathNpcDestroyHandler(object sender, OnNpcDieEventArg e) {
            counter--;
            textMeshPro.text = counter.ToString();
        }

        private void OnNpcInstantiateHandler(object sender, OnNpcInstantiateEventArg e) {
            counter++;
            textMeshPro.text = counter.ToString();
        }

        private void GameOnInitializedHandler() {
        }
    }
}
