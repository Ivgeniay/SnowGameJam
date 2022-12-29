using Assets.Scripts.Game;
using Assets.Scripts.Game.Pause;
using Assets.Scripts.Units.GlobalTarget;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class GameManagerMono : SerializedMonoBehaviour, IGameStateHandler, IRestartable
{
    [BoxGroup("Game State")]
    [OdinSerialize]
    [EnumToggleButtons]
    [OnValueChanged("GameStateChange")]
    private Pause CurrentState;

    [OdinSerialize] public int OnDeathEnemyScore { get; private set; }
    [OdinSerialize] public int OnHeadEnemyScore { get; private set; }
    [OdinSerialize] public int OnStageCompleteScore { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start() {
        Game.Manager.OnInitialized += GameManagerOnInitialized;
        Game.Manager.Initialize();
    }

    private void GameManagerOnInitialized() {
        Game.Manager.OnInitialized -= GameManagerOnInitialized;
        Game.Manager.GameStateManager.Register(this);
        Game.Manager.Restart.Register(this);
    }

    public void GameStateHandle(GameState gameState) {
        if (gameState == GameState.Pause) CurrentState = Pause.Pause;
        else CurrentState = Pause.GamePlay;
    }

    private void GameStateChange(Pause gameState) {
        if (gameState == Pause.Pause) Game.Manager.GameStateManager.SetState(GameState.Pause);
        else Game.Manager.GameStateManager.SetState(GameState.Gameplay);
    }

    private void OnApplicationQuit() {
        Game.Manager.PlayerRepository.Save();
    }

    public void Restart()
    {
        Game.Manager.GameStateManager.SetState(GameState.beforeGamePlay);
    }
}
public enum Pause {
    Pause,
    GamePlay
}
