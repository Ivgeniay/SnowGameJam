using Assets.Scripts.Units.StateMech;
using UnityEngine;

namespace Assets.Scripts.Spawner
{
    public class Spawner : MonoBehaviour, ISpawner {

        [SerializeField] private GameObject spanwObject;

        public UnitBehavior ToSpawn() {
            return Game.Game.Manager.InstantiateNpc(spanwObject, transform.position, Quaternion.identity);
        }
    }
}