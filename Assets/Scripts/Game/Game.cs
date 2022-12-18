using Assets.Scripts.EventArgs;
using Assets.Scripts.Game.Pause;
using Assets.Scripts.PeripheralManagement._Cursor;
using Assets.Scripts.Units.StateMech;
using Assets.Scripts.Utilities;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public sealed class Game
    {
        public event Action OnInitialized;
        public event EventHandler OnDeathNpcDestroy;
        public event EventHandler<TakeDamagePartEventArgs> OnNpcGetDamage;
        public event EventHandler<OnNpcInstantiateEventArg> OnNpcInstantiate;

        private static Game instance;
        public StorageNpc storage;
        public GameStateManager GameStateManager { get; private set; }
        public CursorSetting CursorSetting { get; private set; }
        public static Game Manager {
            get { 
                if (instance is null) return instance = new Game();
                return instance; 
            }
        }
        private Game() { }
        

        public void Initialize() {
            GameStateManager = new GameStateManager();
            CursorSetting = new CursorSetting();
            storage = new StorageNpc();

            foreach (var el in GameObject.FindObjectsOfType<UnitBehavior>()) {
                storage.AddNpc(el);
                OnNpcInstantiate?.Invoke(this, new OnNpcInstantiateEventArg() { type = el.GetComponent<UnitBehavior>().BehaviourType });
            }

            OnInitialized?.Invoke();
        }

        public UnitBehavior InstantiateNpc(GameObject prefab, Vector3 position, Quaternion quaternion)
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

        public void SetPause()
        {

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