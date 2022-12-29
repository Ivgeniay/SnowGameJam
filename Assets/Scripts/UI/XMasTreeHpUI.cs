using Assets.Scripts.Game.Pause;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class XMasTreeHpUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hpTree_TextMeshProUGUI;

        private void Awake() {
            Game.Game.Manager.OnInitialized += Manager_OnInitialized;
        }

        private void Manager_OnInitialized()
        {
            Game.Game.Manager.OnXMasTreeTakeDamage += OnXMasTreeTakeDamage;
            Game.Game.Manager.OnXMasTreeDie += OnXMasTreeDie;
        }



        private void OnXMasTreeDie() {
            hpTree_TextMeshProUGUI.text = "0";
        }

        private void OnXMasTreeTakeDamage(TreeDamageEventArgs obj) {
            hpTree_TextMeshProUGUI.text = obj.Left.ToString();
        }

    }
}
