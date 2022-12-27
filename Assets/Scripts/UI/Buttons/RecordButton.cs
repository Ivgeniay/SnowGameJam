using Assets.Scripts.Game.Pause;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.UI.Buttons
{
    public class RecordButton : MonoBehaviour
    {
        public GameObject offGameObject;
        public GameObject onGameObject;

        public void OnRecordButtonClick()
        {
            offGameObject.SetActive(false);
            onGameObject.SetActive(true);
        }

        public void OnBackFromRecordButtlonClick()
        {
            offGameObject.SetActive(true);
            onGameObject.SetActive(false);
        }
    }
}
