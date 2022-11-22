using Assets.Scripts.Enemies.StateMech;
using Assets.Scripts.EventArgs;
using Assets.Scripts.PeripheralManagement._Cursor;
using Assets.Scripts.Utilities;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public sealed class Game
    {
        public event Action isInitialized;
        public event EventHandler OnDeathNpcDestroy;
        public event EventHandler<TakeDamagePartEventArgs> OnNpcGetDamage;
        public event EventHandler<OnNpcInstantiateEventArg> OnNpcInstantiate;
        public CursorSetting cursorSetting { get; private set; }

        private static Game instance;
        private StorageNpc storage;

        public static Game Manager {
            get { 
                if (instance is null) return instance = new Game();
                return instance; 
            }
        }
        private Game() { }

        

        public void Initialize()
        {
            cursorSetting = new CursorSetting();
            storage = new StorageNpc();

            foreach (var el in GameObject.FindObjectsOfType<UnitBehavior>()) {
                storage.AddNpc(el);
                Logger.Logger.SendMsg(el.BehaviourType.ToString());
                OnNpcInstantiate?.Invoke(this, new OnNpcInstantiateEventArg() { type = el.GetComponent<UnitBehavior>().BehaviourType });
            }

            isInitialized?.Invoke();
        }

        public UnitBehavior InstantiateSnowman(GameObject prefab, Vector3 position, Quaternion quaternion)
        {
            var go = Instantiator.Instantiate(prefab, position, quaternion);
            var scr = go.GetComponent<UnitBehavior>();
            storage.AddNpc(scr);

            OnNpcInstantiate?.Invoke(this, new OnNpcInstantiateEventArg() { type = scr.BehaviourType });

            var hs = go.GetComponent<HealthSystem>();
            hs.OnDeath += OnDeathHandler;
            hs.OnTakeDamage += OnTakeDamageHandler;

            return scr;
        }

        public void MoveAssistant(Vector3 position, Type assistentType) {
            var assistents = storage.GetNpcsByTypeDisposer(assistentType);
            var assistant = assistents.Where(i => i.BehaviourType == assistentType).FirstOrDefault();
            if (assistant is null) throw new Exception($"There is no typeof({assistentType}) in the storage (Game)");
            assistant.Follow(position);
        }

        private void OnTakeDamageHandler(object sender, TakeDamagePartEventArgs e) {
            OnNpcGetDamage?.Invoke(sender, e);
        }

        private void OnDeathHandler(object sender, System.EventArgs e) {
            OnDeathNpcDestroy?.Invoke(sender, System.EventArgs.Empty);
        }
    }
}

public class OnNpcInstantiateEventArg
{
    public Type type;
}