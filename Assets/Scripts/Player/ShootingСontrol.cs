using Assets.Scripts.Player.Shoot;
using Assets.Scripts.Player.Shoot.DTO;
using Assets.Scripts.Player.Weapon;
using Assets.Scripts.Utilities;
using Sisus.Init;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class ShootingСontrol : MonoBehaviour
    {
        private PlayerBehavior playerBehavior;

        private bool isShowProjection = false;
        private AimDTO aimDTO;

        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Projection projection;

        [SerializeField] private IWeapon currentWeapon;
        [SerializeField] private IShoot shootType;

        [Header("NonPhysics")]
        [SerializeField] public Vector3 curve;
        [SerializeField] public float throwLength;
        [SerializeField] [Range(1, 100)] public int StepNonPhysic;
        [SerializeField] [Range(1, 100)] public int frameDelayNonPhysics;

        private void Awake() {
            if (projection is null) projection = GetComponent<Projection>();
            if (playerBehavior is null) playerBehavior = GetComponent<PlayerBehavior>();
            aimDTO = new();

            //shootType = new PhysicAttack(transform, spawnPoint, (5000, 500, 1000));
            shootType = new BesiersAttack(transform, spawnPoint);
        }
        private void Start() {
            currentWeapon = playerBehavior.GetWeaponFromInventory(WeaponVariety.snowball);

            InputManager.Instance.MouseWheelPerformed += ChangeWeapon;
            InputManager.Instance.AimPerformed += OnAimPerformed;
            InputManager.Instance.AimCanceled += OnAimCanceled;
        }


        private void Update() {
            if (isShowProjection && !playerBehavior.isAmmoEmpty(currentWeapon)) ProjectionConrol();
            else projection.DisableLine();
        }

        public void OnAttack(IWeapon weapon)
        {
            if (weapon is null) {
                Debug.Log("HEY, THERE IS NO WEAPON");
                return;
            }
            if (playerBehavior.isAmmoEmpty(weapon) is true) {
                Debug.Log("HEY, THERE ARE NO AMMO");
                return;
            }

            shootType.GetAttack( new AttackDTO() { 
                                    SpawnPoint = spawnPoint, 
                                    Weapon = weapon });

            playerBehavior.decrimentAmmo(weapon);
        }

        public void TakeAim(Projection projection) {
            aimDTO.Weapon = currentWeapon;
            aimDTO.SpawnPoint = spawnPoint;
            aimDTO.Projection = projection;

            shootType.GetAim(aimDTO);
        }

        public IWeapon GetCurrentWeapon() => currentWeapon;
        private void ProjectionConrol() {
            projection.EnableLine();
            TakeAim(projection);
        }
        private void OnAimCanceled() => isShowProjection = false;
        private void OnAimPerformed() => isShowProjection = true;
        private void ChangeWeapon(float obj)
        {
            if (obj > 0) currentWeapon = playerBehavior.GetNextWeaponFromInventory(GetCurrentWeapon());
            else if (obj < 0) currentWeapon = playerBehavior.GetPreviousWeaponFromInventory(GetCurrentWeapon());
        }
        private void OnEnable()
        {
            if (InputManager.Instance is not null)
            {
                InputManager.Instance.MouseWheelPerformed += ChangeWeapon;
                InputManager.Instance.AimPerformed += OnAimPerformed;
                InputManager.Instance.AimCanceled += OnAimCanceled;
            }
        }
        private void OnDisable()
        {
            InputManager.Instance.MouseWheelPerformed -= ChangeWeapon;
            InputManager.Instance.AimPerformed -= OnAimPerformed;
            InputManager.Instance.AimCanceled -= OnAimCanceled;
        }
        private void OnValidate() {
            if (throwLength < 2) throwLength = 2;  
        }


    }
}


