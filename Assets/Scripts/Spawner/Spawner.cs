using Assets.Scripts.Game.Pause;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Spawner
{
    public class Spawner : MonoBehaviour, IGameStateHandler {

        public event Action<int> OnStageComplete;
        public event Action<int> OnStageStart;
        //public event Action OnSpawned;

        [Header("Game Objects setUp")]
        [SerializeField] private GameObject spanwObject;
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

        #region
        private void Start()
        {
            StartCoroutine(SpawnByTimer());
            OnStageComplete += Spawner_OnStageComplete;
            OnStageStart += Spawner_OnStageStart;
            Game.Game.Manager.OnInitialized += GameManagerOnInitialized;
        }

        private void GameManagerOnInitialized() {
            Game.Game.Manager.GameStateManager.Register(this);
        }
        #endregion

        public void SpawnSnowman(Vector3 position) {
            var newSnowman = Game.Game.Manager.InstantiateNpc(spanwObject, transform.position, Quaternion.identity);
            newSnowman.Attack(XmasTree);
        }


        private void Spawner_OnStageStart(int obj) {
            currentWave = obj;
        }

        private void Spawner_OnStageComplete(int obj) {
        }

        private IEnumerator SpawnByTimer()
        {
            Random rnd = new Random();
            for (int i = 0; i < 15; i++) {
                yield return new WaitForSeconds(timeSpawnWaveOffset);

                OnStageStart?.Invoke(i + 1);
                if (i < 10) {
                    for (int j = 0; j < enemyNumDependWave[i]; j++) {
                        SpawnSnowman(transform.position);
                        yield return new WaitForSeconds(timeSpawnInSeconds);
                    }
                }
                else {
                    for(int j = 0; j < i * multiplyEnemy; j++) {
                        SpawnSnowman(transform.position);
                        yield return new WaitForSeconds(timeSpawnInSeconds);
                    }
                }
                OnStageComplete?.Invoke(i + 1);
            }
        }

        public void GameStateHandle(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.AssistentControl:
                    break;
                case GameState.Pause:
                    break;
                case GameState.Gameplay:
                    break;
                case GameState.GameOver:
                    break;
            }
        }
    }
}

public class Timer
{

}