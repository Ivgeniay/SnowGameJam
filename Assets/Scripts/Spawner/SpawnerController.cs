using Assets.Scripts.Game.Pause;
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
    public class SpawnerController : SerializedMonoBehaviour, IGameStateHandler
    {

        public event Action<int> OnStageComplete;
        public event Action<int> OnStageStart;
        //public event Action OnSpawned;

        [OdinSerialize] private List<ISpawner> _spawns = new List<ISpawner>();

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

        private bool canProduce { get => Game.Game.Manager.GameStateManager.CurrentGameState == GameState.Gameplay;}

        private void Awake() {
            Game.Game.Manager.OnInitialized += GameManagerOnInitialized;
        }

        #region
        private void Start() {
            GameObject.FindObjectsOfType<Spawner>().ForEach(el => _spawns.Add(el));
            OnStageComplete += OnStageCompleted;
            OnStageStart += OnNewStageStart;
        }

        private void Update()
        {
            //Debug.Log($"Can produce: {canProduce}");
        }

        private void GameManagerOnInitialized() {
            Game.Game.Manager.GameStateManager.Register(this);
            StartCoroutine(SpawnByTimer());
        }
        #endregion

        public void SpawnSnowman()
        {
            foreach (ISpawner spawner in _spawns)
            {
                var unit = spawner.ToSpawn();
                var hs = unit.GetComponent<HealthSystem>();
                productedAlive.Add(hs);
                hs.OnDeath += OnDeath;
                unit.Attack(XmasTree);
            }
        }

        private void OnDeath(object sender, System.EventArgs e)
        {
            var monoSender = sender as MonoBehaviour;
            if (monoSender is not null)
            {
                var hs = monoSender.GetComponent<HealthSystem>();
                hs.OnDeath -= OnDeath;
                productedAlive.Remove(hs);
            }
        }

        private void OnNewStageStart(int obj) {
            currentWave = obj;
        }

        private void OnStageCompleted(int obj) {
        }

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



    }
}
