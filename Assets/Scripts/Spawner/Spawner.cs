using Assets.Scripts.Units.StateMech;
using System;
using UnityEngine;

namespace Assets.Scripts.Spawner
{
    public class Spawner : MonoBehaviour, ISpawner {

        [SerializeField] private GameObject spanwObject;
        public event EventHandler<OnNpcInstantiateEventArg> OnNpcInstantiate;
        public UnitBehavior ToSpawn() {
            var unit = Game.Game.Manager.InstantiateNpc(spanwObject, transform.position, Quaternion.identity);
            OnNpcInstantiate?.Invoke(this, new OnNpcInstantiateEventArg() { UnitBehavior = unit});
            return unit;
        }
    }
}