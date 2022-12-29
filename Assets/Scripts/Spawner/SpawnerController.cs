using Assets.Scripts.Game.Pause;
using Assets.Scripts.Utilities;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Spawner
{
    public class SpawnerController : SerializedMonoBehaviour, IGameStateHandler, IRestartable
    {

        public event Action<int> OnStageComplete;
        public event Action<int> OnStageStart;
        public event EventHandler<OnNpcInstantiateEventArg> OnNpcInstantiate;

        [SerializeField] private List<SpawnWave> _spawnWaves = new List<SpawnWave>();

        private List<ISpawner> _spawns = new List<ISpawner>();

        [Header("Game Objects setUp")]
        [SerializeField] private Transform XmasTree;
        [Header("Waves")]
        [SerializeField] public int currentWave = 0;
        [SerializeField] private int currentWaveSpeed;
        [Header("Time")]
        [SerializeField] private float timeSpawnInSeconds = 0;
        [SerializeField] private float timeSpawnWaveOffset = 0;
        [Header("Over Wave Multiplier")]
        [SerializeField] private float multiplyEnemy = 1.5f;

        [SerializeField] private int[] enemyNumDependWave = new int[10];

        private List<HealthSystem> productedAlive = new List<HealthSystem>();
        private Coroutine coroutine;

        private bool canProduce { get => Game.Game.Manager.GameStateManager.CurrentGameState == GameState.Gameplay;}

        private void Awake() {
            Game.Game.Manager.OnInitialized += GameManagerOnInitialized;
        }
 
        #region
        private void Start() {
            _spawns = new List<ISpawner>();
            GameObject.FindObjectsOfType<Spawner>().ForEach(el => _spawns.Add(el));

            OnStageComplete += OnStageCompleted;
            OnStageStart += OnNewStageStart;

            Game.Game.Manager.Restart.Register(this);
        }

        private void StartProduce() {
            coroutine = Coroutines.Start(SpawnByTimer());
        }

        private void Update()
        {
            
        }

        private void GameManagerOnInitialized() {
            Game.Game.Manager.GameStateManager.Register(this);
            StartProduce();
        }
        #endregion

        public void SpawnSnowman()
        {
            foreach (ISpawner spawner in _spawns)
            {
                var unit = spawner.ToSpawn();
                var hs = unit.GetComponent<HealthSystem>();
                productedAlive.Add(hs);
                hs.OnDied += OnDied;
                unit.Attack(XmasTree);
            }
        }

        private void OnDied(object sender, OnNpcDieEventArg e)
        {
            var monoSender = sender as MonoBehaviour;
            if (monoSender is not null)
            {
                var hs = monoSender.GetComponent<HealthSystem>();
                hs.OnDied -= OnDied;
                productedAlive.Remove(hs);
            }
        }

        private void OnNewStageStart(int obj) => currentWave = obj;
        private void OnStageCompleted(int obj) { }
        private void OnNpcInstantiatHandlere(object sender, OnNpcInstantiateEventArg e) {
            OnNpcInstantiate?.Invoke(sender, e);
        }

        private void OnEnable() => _spawns.ForEach(spawner => { spawner.OnNpcInstantiate += OnNpcInstantiatHandlere; });
        private void OnDisable() => _spawns.ForEach(spawner => { spawner.OnNpcInstantiate -= OnNpcInstantiatHandlere; });

        private IEnumerator SpawnByTimer()
        {
            Random rnd = new Random();
            for (int i = 0; i < 15; i++)
            {
                yield return new WaitUntil(() => canProduce);
                yield return new WaitForSeconds(timeSpawnWaveOffset);



                OnStageStart?.Invoke(i + 1);
                if (i < 10)
                {
                    for (int j = 0; j < enemyNumDependWave[i]; j++)
                    {
                        yield return new WaitUntil(() => canProduce);
                        SpawnSnowman();
                        yield return new WaitForSeconds(timeSpawnInSeconds);
                    }
                }
                else
                {
                    for (int j = 0; j < i * multiplyEnemy; j++)
                    {
                        yield return new WaitUntil(() => canProduce);
                        SpawnSnowman();
                        yield return new WaitForSeconds(timeSpawnInSeconds);
                    }
                }

                yield return new WaitUntil(() => productedAlive.Count == 0);
                OnStageComplete?.Invoke(i + 1);
            }
        }

        public void GameStateHandle(GameState gameState) {
        }

        private void OnDestroy() {
            Game.Game.Manager.Restart.Unregister(this);
        }

        public void Restart()
        {
            if (coroutine is not null)
            {
                Coroutines.Stop(coroutine);
                StopCoroutine(coroutine);
            }
            
            Coroutines.Stop(SpawnByTimer());
            Coroutines.StopAll();
            StopAllCoroutines();

            productedAlive = new List<HealthSystem>();
            StartProduce();
        }
    }

    [Serializable]
    public class SpawnWave {
        public Transform spawnMonsters;
        public int id;
        public float timeSpawnInSeconds;
        public float timeSpawnWaveOffset;
    }
}
