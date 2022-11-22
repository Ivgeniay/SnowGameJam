using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Game = Assets.Scripts.Game.Game;
using Random = System.Random;

namespace Assets.Scripts.Spawner
{
    public class Spawner : MonoBehaviour {

        public event Action<int> OnStageComplete;
        public event Action<int> OnStageStart;
        public event Action OnSpawned;

        [SerializeField] private GameObject spanwObject;
        [SerializeField] private int currentWave = 0;
        [SerializeField] private int[] enemyNumDependWave = new int[10];
        [SerializeField] private int currentWaveSpeed;
        [SerializeField] private float multiplyEnemy = 1.5f;
        [SerializeField] private float timeSpawnInSeconds = 0;
        [SerializeField] private float timeSpawnWaveOffset = 0;

        #region
        private void Start()
        {
            StartCoroutine(SpawnByTimer());
            OnStageComplete += Spawner_OnStageComplete;
            OnStageStart += Spawner_OnStageStart;
        }
        #endregion

        public void SpawnSnowman() {
            Game.Game.Manager.InstantiateSnowman(spanwObject, transform.position, Quaternion.identity);
        }


        private void Spawner_OnStageStart(int obj) {
            currentWave = obj;
            //Logger.Logger.SendMsg($"Stage {currentWave} is complete");
        }

        private void Spawner_OnStageComplete(int obj) {
            //Logger.Logger.SendMsg($"Stage {obj} is complete");
        }


        private async Task timerDelay(int timerWithMilliseconds) {
            await Task.Delay(timerWithMilliseconds);
        }

        private IEnumerator SpawnByTimer()
        {
            Random rnd = new Random();
            for (int i = 0; i < 15; i++) {
                yield return new WaitForSeconds(timeSpawnWaveOffset);

                OnStageStart?.Invoke(i + 1);
                if (i < 10)
                {
                    for (int j = 0; j < enemyNumDependWave[i]; j++) {
                        Game.Game.Manager.InstantiateSnowman(spanwObject, new Vector3(rnd.Next(20), 0, rnd.Next(20)), Quaternion.identity);
                        yield return new WaitForSeconds(timeSpawnInSeconds);
                    }
                }
                else
                {
                    for(int j = 0; j < i * multiplyEnemy; j++) {
                        Game.Game.Manager.InstantiateSnowman(spanwObject, new Vector3(rnd.Next(20), 0, rnd.Next(20)), Quaternion.identity);
                        yield return new WaitForSeconds(timeSpawnInSeconds);
                    }
                }
                OnStageComplete?.Invoke(i + 1);
            }
        }

    }
}
