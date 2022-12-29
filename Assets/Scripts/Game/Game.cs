using Assets.Scripts.EventArgs;
using Assets.Scripts.Game;
using Assets.Scripts.Game.Pause;
using Assets.Scripts.PeripheralManagement._Cursor;
using Assets.Scripts.Spawner;
using Assets.Scripts.Units.GlobalTarget;
using Assets.Scripts.Units.StateMech;
using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public sealed class Game
    {
        public event Action OnInitialized;
        public event EventHandler<OnNpcDieEventArg> OnNpcDied;
        public event Action<int> OnStageStart;
        public event Action<int> OnStageComplete;
        public event EventHandler<TakeDamagePartEventArgs> OnNpcGetDamage;
        public event EventHandler<OnNpcInstantiateEventArg> OnNpcInstantiate;
        public event Action<TreeDamageEventArgs> OnXMasTreeTakeDamage;
        public event Action OnXMasTreeDie;
        public event Action<int> OnScoreChanged;

        public readonly StorageNpc storage;
        public readonly PlayerRepository PlayerRepository;
        public readonly Restart Restart;
        public Score Score;

        private static Game instance;
        private IGlobalTarget globalTarget;
        private CursorSetting CursorSetting;
        public GameStateManager GameStateManager { get; private set; }
        public static Game Manager {
            get {
                if (instance is null) {
                    instance = new Game();
                    return instance;
                }
                return instance; 
            }
        }
        private Game() {
            Restart = new();
            storage = new();
            PlayerRepository = new();
        }

        public void Initialize() {
            GameStateManager = new GameStateManager();
            CursorSetting = new CursorSetting();

            var GMMono = GameObject.FindObjectOfType<GameManagerMono>();
            Score = new Score(GMMono.OnDeathEnemyScore, GMMono.OnHeadEnemyScore, GMMono.OnStageCompleteScore);

            globalTarget = GameObject.FindObjectOfType<XMasTree>();
            globalTarget.OnXMasTreeTakeDamage += OnXMasTreeTakeDamageHandler;
            globalTarget.OnXMasTreeDie += OnXMasTreeDieHandler;

            foreach (var el in GameObject.FindObjectsOfType<UnitBehavior>()) {
                storage.AddNpc(el);
                OnNpcInstantiate?.Invoke(this, new OnNpcInstantiateEventArg() { UnitBehavior = el });
            }

            var spawnerController = GameObject.FindObjectOfType<SpawnerController>();
            spawnerController.OnNpcInstantiate += OnNpcInstantiateHandler;
            spawnerController.OnStageStart += OnStageStartHandler;
            spawnerController.OnStageComplete += OnStageCompleteHandler;

            OnInitialized?.Invoke();
        }

        public UnitBehavior InstantiateNpc(GameObject prefab, Vector3 position, Quaternion quaternion)
        {
            var go = Instantiator.Instantiate(prefab, position, quaternion);
            var scr = go.GetComponent<UnitBehavior>();
            storage.AddNpc(scr);

            OnNpcInstantiate?.Invoke(this, new OnNpcInstantiateEventArg() { UnitBehavior = scr });

            var hs = go.GetComponent<HealthSystem>();
            hs.OnDied += OnNpcDiedHandler;
            hs.OnTakeDamage += OnTakeDamageHandler;

            return scr;
        }

        public void MoveAssistant(Vector3 position, Type assistentType) {
            var assistents = storage.GetNpcsByTypeDisposer(assistentType);
            var assistant = assistents.Where(i => i.BehaviourType == assistentType).FirstOrDefault();
            if (assistant is null) throw new Exception($"There is no typeof({assistentType}) in the storage (Game)");
            assistant.Follow(position);
        }

        public void SetPause()
        {

        }


        private void OnStageStartHandler(int obj) => OnStageStart?.Invoke(obj);
        private void OnStageCompleteHandler(int obj) => OnStageComplete?.Invoke(obj);
        private void OnTakeDamageHandler(object sender, TakeDamagePartEventArgs e) => OnNpcGetDamage?.Invoke(sender, e);
        private void OnXMasTreeDieHandler()
        {
            OnXMasTreeDie?.Invoke();
        }
        private void OnXMasTreeTakeDamageHandler(TreeDamageEventArgs obj) => OnXMasTreeTakeDamage?.Invoke(obj);

        private void OnNpcInstantiateHandler(object sender, OnNpcInstantiateEventArg e) {
            OnNpcInstantiate?.Invoke(sender, e);
        }
        private void OnNpcDiedHandler(object sender, OnNpcDieEventArg e) {
            storage.RemoveNpc(e.UnitBehavior);
            var hs = e.UnitBehavior.GetComponent<HealthSystem>();
            hs.OnDied -= OnNpcDiedHandler;
            hs.OnTakeDamage -= OnTakeDamageHandler;

            OnNpcDied?.Invoke(sender, e);
        }
    }
}

public class OnNpcInstantiateEventArg
{
    public UnitBehavior UnitBehavior;
}
public class OnNpcDieEventArg
{
    public UnitBehavior UnitBehavior;
}

public class Restart
{
    private readonly List<IRestartable> restartable = new List<IRestartable>();
    public Restart()
    {

    }

    public void Register(IRestartable gameStateHandler) => this.restartable.Add(gameStateHandler);
    public void Unregister(IRestartable gameStateHandler) => this.restartable.Remove(gameStateHandler);

    public void RestartGame() {
        restartable.ForEach(el => el.Restart());
    }
}