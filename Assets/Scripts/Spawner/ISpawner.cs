using Assets.Scripts.Units.StateMech;
using Sisus.Init;
using System;

namespace Assets.Scripts.Spawner
{
    public interface ISpawner {
        public event EventHandler<OnNpcInstantiateEventArg> OnNpcInstantiate;
        UnitBehavior ToSpawn();
    }
}
