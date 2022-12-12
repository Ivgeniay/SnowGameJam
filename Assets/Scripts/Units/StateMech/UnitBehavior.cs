using Assets.Scripts.Units.StateMech.Disposer;
using Assets.Scripts.Game.Pause;
using System;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

namespace Assets.Scripts.Units.StateMech
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(HealthSystem))]
    [RequireComponent(typeof(UnitConfiguration))]
    public class UnitBehavior : MonoBehaviour, IBehaviour, IGameStateHandler
    {
        [SerializeField] private StateDisposerType stateDisposerType;

        private StateDisposerBase stateDisposer;
        private bool isFreezed = false;

        #region Mono
        private void Awake() {
            stateDisposer = DisposerFactory.GetDisposer(stateDisposerType, transform);
        }
        private void Start() {
            if (stateDisposer is null) throw new Exception($"stateDisposer is null {this}");
            stateDisposer.StartAction();
            Game.Game.Manager.OnInitialized += GameManagerOnInitialized;
        }
        private void Update() {
            if (stateDisposer is null) throw new Exception($"stateDisposer is null {this}");
            if (isFreezed is false) return; 
            stateDisposer.FrameAction();
        }
        #endregion
        public void Attack(Transform target) => stateDisposer.Attack(target);
        public void Follow(Vector3 vector3) => stateDisposer.Follow(vector3);
        public void Follow(Transform transform) => stateDisposer.Follow(transform);
        public void Stop() => Debug.Log("Stop machine " + transform.position);
        public Type BehaviourType { get {   
                if (stateDisposer is not null) return stateDisposer.GetType(); 
                else{
                    Logger.Logger.SendMsg("", 3);
                    throw new Exception("BehaviourType is null"); 
                }
            }
        }
        private void GameManagerOnInitialized() {
            Game.Game.Manager.GameStateManager.Register(this);
        }
        public void GameStateHandle(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.AssistentControl:
                    isFreezed = true;
                    break;
                case GameState.Pause:
                    isFreezed = true;
                    break;
                case GameState.Gameplay:
                    isFreezed = false;
                    break;
                case GameState.GameOver:
                    isFreezed = true;
                    break;
            }
        }
    }
}