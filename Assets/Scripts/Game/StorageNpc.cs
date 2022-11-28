using Assets.Scripts.Units.StateMech;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Game
{
    public class StorageNpc
    {
        private Dictionary<Type, List<UnitBehavior>> _npcs = new();
        public StorageNpc() { }
        

        public IEnumerable<UnitBehavior> GetNpcsByTypeDisposer(Type type) {
            if (_npcs.TryGetValue(type, out var units)) return units;
            else return null;
        }
        public void AddNpc(UnitBehavior unitBehavior) {
            Type type = unitBehavior.BehaviourType;
            if (_npcs.TryGetValue(type, out var npcList)) _npcs[type].Add(unitBehavior);
            else {
                _npcs[type] = new List<UnitBehavior>();
                _npcs[type].Add(unitBehavior);
            }
        }

        public void RemoveNpc(UnitBehavior unitBehavior)
        {
            Type type = unitBehavior.BehaviourType;
            _npcs[type].Remove(unitBehavior);
        }

        public int GetCountOfTypes (Type type) {
            if (_npcs.TryGetValue(type, out var npcs)) return npcs.Count;
            else return default;
        }
        public int GetCountNpc(Type type) {
            int count = default;
            foreach (KeyValuePair<Type, List<UnitBehavior>> el in _npcs) count += el.Value.Count;
            return count;
        }
        public int GetCountWithoutTypes(Type type) {
            int count = default;
            foreach (KeyValuePair<Type, List<UnitBehavior>> el in _npcs) {
                if (el.Key == type) continue;
                count += el.Value.Count;
            }
            return count;
        }

    }
}
