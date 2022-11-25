using Assets.Scripts.Player.Control;
using Assets.Scripts.Player.Weapon;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerBehavior : MonoBehaviour
    {
        public event Action AmmoIsOver;
        public event Action AmmoReplenished;

        [SerializeField] private PlayerControlContext playerControlContext;
        [SerializeField] private IControllable controller;
        [SerializeField] private ShootingСontrol shootingСontrol;
        [SerializeField] private Inventory inventory;

        private bool isShowProjection = false;


        private void Awake() {
            if (controller is null) controller = GetComponent<IControllable>();
            if (shootingСontrol is null) shootingСontrol = GetComponent<ShootingСontrol>();
            if (playerControlContext is null) playerControlContext = controller.GetContext();
            if (inventory is null) inventory = GetComponent<Inventory>();
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
    }
}
