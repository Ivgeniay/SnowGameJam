using Assets.Scripts.Units.StateMech;
using Sisus.Init;

namespace Assets.Scripts.Spawner
{
    public interface ISpawner {
        UnitBehavior ToSpawn();
    }
}
