using Assets.Scripts.Player.Control;
using Assets.Scripts.Player.Weapon;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerBehavior : MonoBehaviour
    {
        [SerializeField] private PlayerControlContext playerControlContext;
        [SerializeField] private IControllable controller;
        [SerializeField] private ShootingСontrol shootingСontrol;
        [SerializeField] private Projection projection;
        [SerializeField] private Inventory inventory;


        private void Awake() {
            if (controller is null) controller = GetComponent<IControllable>();
            if (shootingСontrol is null) shootingСontrol = GetComponent<ShootingСontrol>();
            if (playerControlContext is null) playerControlContext = controller.GetContext();
            if (projection is null) projection = GetComponent<Projection>();
            if (inventory is null) inventory = GetComponent<Inventory>();

        }

        private void Start() {
            playerControlContext.OnPlayerStateChanged += PlayerControlContext_OnPlayerStateChanged;
        }


        private void Update() {
            controller.Move();
        }

        private void PlayerControlContext_OnPlayerStateChanged(object sender, PlayerState e) {
            if (playerControlContext.GetPlayerState() == PlayerState.Aim)
            {
                projection.EnableLine();
                shootingСontrol.TakeAim(projection);
            }
            else projection.DisableLine();
        }

        public void AddAmmo(IWeapon weapon, int amount) => inventory.AddAmmo(weapon, amount);
        public bool isAmmoEmpty(IWeapon weapon) => inventory.isAmmoEmpty(weapon);
        public void decrimentAmmo(IWeapon weapon) => inventory?.decrimentAmmo(weapon);
        public IWeapon GetWeapon(WeaponVariety weapon) => inventory.GetWeapon(weapon);
        public IWeapon GetCurrentWeapon() => shootingСontrol.GetCurrentWeapon();
    }
}
