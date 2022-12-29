using Assets.Scripts.EventArgs;
using Assets.Scripts.Game;
using Assets.Scripts.Units.DamageMech;
using System;

public class Score : IRestartable
{
    public event Action<int> OnScoreChange;
    private int _currentScore;
    public int CurrentScore { get => _currentScore; 
        private set {
            _currentScore = value;
            OnScoreChange?.Invoke(value);
    }}

    private int OnDeathEnemyScore;
    private int OnHeadEnemyScore;
    private int OnStageCompleteScore;


    public Score(int OnDeathEnemyScore, int OnHeadEnemyScore, int OnStageCompleteScore) {
        this.OnDeathEnemyScore = OnDeathEnemyScore;
        this.OnHeadEnemyScore = OnHeadEnemyScore;
        this.OnStageCompleteScore = OnStageCompleteScore;
    
        Game.Manager.OnNpcDied += OnDeathNpcDestroy;
        Game.Manager.OnNpcGetDamage += OnNpcGetDamage;
        Game.Manager.OnStageComplete += SpawnerControllerOnStageComplete;
        Game.Manager.OnXMasTreeDie += OnXMasTreeDie;

        Game.Manager.Restart.Register(this);
    }

    private void SpawnerControllerOnStageComplete(int obj) {
        CurrentScore += OnStageCompleteScore;
    }

    private void OnNpcGetDamage(object sender, TakeDamagePartEventArgs e) {
        var head = e.SenderPartOfBody as IUltimateDamageArea;
        if (head is not null)
            CurrentScore += OnHeadEnemyScore;
    }

    private void OnDeathNpcDestroy(object sender, OnNpcDieEventArg e) {
        CurrentScore += OnDeathEnemyScore;
    }

    private void OnXMasTreeDie() {
        Game.Manager.PlayerRepository.Save(CurrentScore);
    }

    public void Restart() {
        CurrentScore = 0;
    }
}
