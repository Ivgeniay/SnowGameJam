using Assets.Scripts.Game;
using Assets.Scripts.Game.Pause;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistentsControllPanel : MonoBehaviour, IGameStateHandler
{
    [Required]
    [SerializeField] 
    private GameObject panel;
    private void Awake() {
        Game.Manager.OnInitialized += GameManagerOnInitialized;
    }

    private void GameManagerOnInitialized() {
        Game.Manager.GameStateManager.Register(this);
    }
    public void GameStateHandle(GameState state) {
        switch (state)
        {
            case GameState.AssistentControl:
                panel.SetActive(true);
                break;
            default: panel.SetActive(false);
                break;
        }
    }

}
