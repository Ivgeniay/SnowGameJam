using Assets.Scripts.Game.Pause;
using Assets.Scripts.Player.Control;
using Assets.Scripts.Player.Weapon;
using Sirenix.OdinInspector.Editor.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Vector3(0,1.45000005,0)

namespace Assets.Scripts.Player
{
    public class PlayerBehavior : MonoBehaviour, IGameStateHandler
    {
        public event Action AmmoIsOver;
        public event Action AmmoReplenished;

        [SerializeField] private PlayerControlContext playerControlContext;
        [SerializeField] private IControllable controller;
        [SerializeField] private ShootingСontrol shootingСontrol;
        [SerializeField] private Inventory inventory;

        private void Awake() {
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
            controller.Move();
        }
        public void AddAmmo(IWeapon weapon, int amount) => inventory.AddAmmo(weapon, amount);
        public bool isAmmoEmpty(IWeapon weapon) => inventory.isAmmoEmpty(weapon);
        public void decrimentAmmo(IWeapon weapon) => inventory?.decrimentAmmo(weapon);
        public IWeapon GetNextWeaponFromInventory(IWeapon weapon) => inventory.GetNextWeapon(weapon);
        public IWeapon GetPreviousWeaponFromInventory(IWeapon weapon) => inventory.GetPreviousWeapon(weapon);
        public IWeapon GetWeaponFromInventory(WeaponVariety weapon) => inventory.GetWeapon(weapon);
        public IWeapon GetCurrentWeapon() => shootingСontrol.GetCurrentWeapon();
        private void OnAmmoReplenished() => AmmoReplenished?.Invoke();
        private void OnAmmoIsOver() => AmmoIsOver?.Invoke();
        private void GameManagerOnInitialized() {
            Game.Game.Manager.GameStateManager.Register(this);
        }
        public void GameStateHandle(GameState isPaused)
        {
            Debug.Log(isPaused);
        }

    }
}
