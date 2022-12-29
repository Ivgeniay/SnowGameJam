using Assets.Scripts.Units.StateMech.Disposer;
using Assets.Scripts.Game.Pause;
using System;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

namespace Assets.Scripts.Units.StateMech
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(HealthSystem))]
    [RequireComponent(typeof(UnitConfiguration))]
    public class UnitBehavior : MonoBehaviour, IBehaviour, IGameStateHandler, IRestartable
    {
        [SerializeField] private StateDisposerType stateDisposerType;

        private StateDisposerBase stateDisposer;
        private bool isFreezed = false;
        private Animator animator;
        private UnitConfiguration unitConfiguration;

        private Vector3 startPosition;
        private Quaternion startRotation;

        #region Mono
        private void Awake() {
            startPosition = transform.position;
            startRotation = transform.rotation;

            stateDisposer = DisposerFactory.GetDisposer(stateDisposerType, transform);
            animator = GetComponentInChildren<Animator>();
            unitConfiguration = GetComponent<UnitConfiguration>();
        }
        private void Start() {
            if (stateDisposer is null) throw new Exception($"stateDisposer is null {this}");
            stateDisposer.StartAction();
            //Game.Game.Manager.OnInitialized += GameManagerOnInitialized;
            Game.Game.Manager.GameStateManager.Register(this);
            Game.Game.Manager.Restart.Register(this);
        }
        private void Update() {
            if (stateDisposer is null) throw new Exception($"stateDisposer is null {this}");
            if (isFreezed is true) return; 
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
            if (transform.gameObject.IsDestroyed()) return;

            if (gameState == GameState.Gameplay) {
                if (animator is not null) animator.speed = unitConfiguration.SpeedAnimation;
            }
            else {
                if (animator is not null) animator.speed = 0f;
            }
        }

        private void OnDestroy() {
            Game.Game.Manager.GameStateManager.Unregister(this);
            Game.Game.Manager.Restart.Unregister(this);
        }

        public void Restart() {
            if (stateDisposerType == StateDisposerType.Snowman) {
                Destroy(gameObject);
            }
            else if (stateDisposerType == StateDisposerType.Assistant) {
                transform.position = startPosition;
                transform.rotation = startRotation;
            }
        }
    }
}