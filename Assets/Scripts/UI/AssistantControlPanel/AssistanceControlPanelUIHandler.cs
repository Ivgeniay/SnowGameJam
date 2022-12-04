using Assets.Scripts.Units.StateMech;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.AssistantControlPanel
{
    public class AssistanceControlPanelUIHandler : MonoBehaviour
    {
        [SerializeField] private GameObject assistantCard;
        [SerializeField] private List<UnitBehavior> unitBehaviors;

        private Canvas canvas;

        private void Awake() {
            canvas = GetComponentInParent<Canvas>();
        }

        private void Start() {
            if (assistantCard is not null) {
                foreach (UnitBehavior unitBehavior in unitBehaviors) {
                    var inst = Instantiate(assistantCard, transform);
                    inst.name = unitBehavior.BehaviourType.ToString();
                    var textMesh = inst.GetComponentInChildren<TextMeshProUGUI>();
                    textMesh.text = unitBehavior.BehaviourType.ToString();
                }
            }
        }

        public void ResetRoute(GameObject assistantCard)
        {
            var type = Type.GetType(assistantCard.name);
            var result = Game.Game.Manager.storage.GetNpcsByTypeDisposer(type);
            foreach (var el in result)
            {
                Debug.Log(el);
                el.Stop();
            }
        }

    }
}
