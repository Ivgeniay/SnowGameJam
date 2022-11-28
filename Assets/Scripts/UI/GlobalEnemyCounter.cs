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
            Game.Game.Manager.OnNpcInstantiate += Manager_OnNpcInstantiate;
            Game.Game.Manager.OnDeathNpcDestroy += Manager_OnDeathNpcDestroy;
        }

        private void Manager_OnDeathNpcDestroy(object sender, System.EventArgs e) {
            counter--;
            textMeshPro.text = counter.ToString();
        }

        private void Manager_OnNpcInstantiate(object sender, OnNpcInstantiateEventArg e) {
            counter++;
            textMeshPro.text = counter.ToString();
        }

        private void GameOnInitializedHandler() {
            //textMeshPro.text = counter.ToString();
        }
    }
}
