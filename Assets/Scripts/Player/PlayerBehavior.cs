using Assets.Scripts.Game.Pause;
using Assets.Scripts.Player.Control;
using Assets.Scripts.Player.Weapon;
using Assets.Scripts.Player.Weapon.Interfaces;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{

    public class PlayerBehavior : MonoBehaviour, IGameStateHandler, IRestartable
    {
        public event Action AmmoIsOver;
        public event Action AmmoReplenished;

        private PlayerControlContext playerControlContext;
        private IControllable controller;
        private ShootingСontrol shootingСontrol;
        private Inventory inventory;

        private GameState currentGameState { get; set; }
        private Vector3 startPosition;
        private Quaternion startRotation;

        private void Awake() {
            startPosition = transform.position;
            startRotation = transform.rotation;

            if (controller is null) controller = GetComponent<IControllable>();
            if (shootingСontrol is null) shootingСontrol = GetComponent<ShootingСontrol>();
            if (playerControlContext is null) playerControlContext = controller.GetContext();
            if (inventory is null) inventory = GetComponent<Inventory>();
            Game.Game.Manager.OnInitialized += GameManagerOnInitialized;
        }
        private void Start() {
            inventory.AmmoIsOver += OnAmmoIsOver;
            inventory.AmmoReplenished += OnAmmoReplenished;
        }
        private void Update() {
            if (currentGameState == GameState.Gameplay)
                controller.MoveUpdate();
        }
        public void AddAmmo(IWeapon_ weapon, int amount) => inventory.AddAmmo(weapon, amount);
        public bool isAmmoEmpty(IWeapon_ weapon) => inventory.isAmmoEmpty(weapon);
        public void decrimentAmmo(IWeapon_ weapon) => inventory?.decrimentAmmo(weapon);
        public IWeapon_ GetNextWeaponFromInventory(IWeapon_ weapon) => inventory.GetNextWeapon(weapon);
        public IWeapon_ GetPreviousWeaponFromInventory(IWeapon_ weapon) => inventory.GetPreviousWeapon(weapon);
        public IWeapon_ GetWeaponFromInventory(WeaponVariety weapon) => inventory.GetWeapon(weapon);
        public IWeapon_ GetCurrentWeapon() => shootingСontrol.GetCurrentWeapon();
        private void OnAmmoReplenished() => AmmoReplenished?.Invoke();
        private void OnAmmoIsOver() => AmmoIsOver?.Invoke();
        private void GameManagerOnInitialized() {
            Game.Game.Manager.OnInitialized -= GameManagerOnInitialized;
            Game.Game.Manager.GameStateManager.Register(this);
            Game.Game.Manager.Restart.Register(this);
        }
        public void GameStateHandle(GameState gameState) {
            currentGameState = gameState;
            if (gameState != GameState.Gameplay) Time.timeScale = 0;
            else Time.timeScale = 1;
        }

        public void Restart() {
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }
}
