using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class Wave : MonoBehaviour, IRestartable
    {
        [SerializeField] private TextMeshProUGUI numberWave_TextMeshProUGUI;

        [SerializeField] private GameObject newWaveLable;
        [SerializeField] private TextMeshProUGUI textFadeOut_TextMeshProUGUI;
        [SerializeField] private float delay;
        [SerializeField] private int durationFadeSpeed;

        private Coroutine currentCoroutine;

        private void Awake()
        {
            Game.Game.Manager.OnInitialized += GameManagerOnInitialized;
        }

        private void GameManagerOnInitialized()
        {
            Game.Game.Manager.OnInitialized -= GameManagerOnInitialized;
            Game.Game.Manager.OnStageStart += OnStageStartHandler;
            Game.Game.Manager.Restart.Register(this);
        }

        private void OnStageStartHandler(int obj)
        {
            numberWave_TextMeshProUGUI.text = obj.ToString();
            //SetVisible(textFadeOut_TextMeshProUGUI);
            //newWaveLable.gameObject.SetActive(true);
            //currentCoroutine = StartCoroutine(NewWaveLableSetUnActive(delay));
        }

        private IEnumerator NewWaveLableSetUnActive(float delay) {
            yield return new WaitForSeconds(delay);
            //newWaveLable.SetActive(false);
            StartCoroutine(FadeOut(textFadeOut_TextMeshProUGUI, durationFadeSpeed));
        }

        private IEnumerator FadeOut(TextMeshProUGUI textMeshPro, int duration)
        {
            for(int i = 255; i > 0; i = i - duration)
            {
                yield return null;
                if (i <= 0) break;
                textMeshPro.color = new Color32(255, 255, 255, (byte)i);
            }
            newWaveLable.gameObject.SetActive(false);
        }

        private void SetVisible(TextMeshProUGUI textMeshPro)
        {
            textMeshPro.color = new Color32(255, 255, 255, 255);
        }

        public void Restart() {
            if (currentCoroutine is not null)
                StopCoroutine(currentCoroutine);
            StopAllCoroutines();

            numberWave_TextMeshProUGUI.text = "1";
        }
    }
}
