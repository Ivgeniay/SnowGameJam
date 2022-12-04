using Assets.Scripts.Game;
using Assets.Scripts.Game.Pause;
using Init.Demo;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class GameManagerMono : SerializedMonoBehaviour, IGameStateHandler
{

    [BoxGroup("Game State")]
    [OdinSerialize]
    [EnumToggleButtons]
    [OnValueChanged("GameStateChange")]
    private Pause CurrentState;


    private void Start() {
        Game.Manager.Initialize();
        Game.Manager.OnInitialized += GameManagerOnInitialized;
        Game.Manager.CursorSetting.HideLock();
    }

    private void GameManagerOnInitialized() {
        Game.Manager.GameStateManager.Register(this);
    }

    public void GameStateHandle(GameState gameState) {
        if (gameState == GameState.Pause) CurrentState = Pause.Pause;
        else CurrentState = Pause.Game;
    }

    private void GameStateChange(Pause gameState) {
        if (gameState == Pause.Pause) Game.Manager.GameStateManager.SetState(GameState.Pause);
        else Game.Manager.GameStateManager.SetState(GameState.Gameplay);
    }



}
public enum Pause
{
    Pause,
    Game
}
