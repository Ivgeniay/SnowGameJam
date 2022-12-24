using Assets.Scripts.Player.Weapon;
using Assets.Scripts.Player.Weapon.Interfaces;
using Blobcreate.ProjectileToolkit;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class ShootingСontrol : MonoBehaviour, IAttack
    {
        private PlayerBehavior playerBehavior;
        private bool isShowProjection = false;
        private Camera mainCamera;

        [SerializeField] private Transform spawnPoint;
        [SerializeField] private IWeapon_ currentWeapon;
        [SerializeField] private TrajectoryPredictor trajectoryPredictor;
        [SerializeField] private Transform Pointer;

        private void Awake() {
            gameObject.GetComponentInChildren<AnimationEventProxy>().PersonAttackController = this;
            mainCamera = Camera.main;

            if (trajectoryPredictor is null) trajectoryPredictor = GetComponent<TrajectoryPredictor>();
            if (playerBehavior is null) playerBehavior = GetComponent<PlayerBehavior>();
        }

        private void Start() {
            currentWeapon = playerBehavior.GetWeaponFromInventory(WeaponVariety.snowCannon);

            if (InputManager.Instance is not null)
            {
                InputManager.Instance.MouseWheelPerformed += ChangeWeapon;
                InputManager.Instance.AimPerformed += OnAimPerformed;
                InputManager.Instance.AimCanceled += OnAimCanceled;
            }
        }


        private void Update() {
            if (isShowProjection && !playerBehavior.isAmmoEmpty(currentWeapon)) ProjectionConrol();
            else trajectoryPredictor.enabled = false;
        }
        public void OnAttack() {
            var weapon = GetCurrentWeapon();
            var endpoint = CalculatingEndPointShot();
            Attack(weapon, endpoint);
        }
        private void Attack(IWeapon_ weapon_, Vector3 fireEndpointPosition)
        {
            if (weapon_ == null) return;
            if (playerBehavior.isAmmoEmpty(weapon_) is true) return;

            weapon_.Fire(fireEndpointPosition);
            DecrimentAmmo(weapon_);
        }
        public IWeapon_ GetCurrentWeapon() => currentWeapon;
        private void ProjectionConrol() {
            trajectoryPredictor.enabled = true;
            var endpoint = CalculatingEndPointShot();
            currentWeapon.GetAim(trajectoryPredictor, endpoint);
        }
        private void OnAimCanceled() => isShowProjection = false;
        private void OnAimPerformed() => isShowProjection = true;
        private void ChangeWeapon(float obj) {
            if (obj > 0) currentWeapon = playerBehavior.GetNextWeaponFromInventory(GetCurrentWeapon());
            else if (obj < 0) currentWeapon = playerBehavior.GetPreviousWeaponFromInventory(GetCurrentWeapon());
        }
        private void DecrimentAmmo(IWeapon_ weapon_) => playerBehavior.decrimentAmmo(weapon_);

        private Vector3 CalculatingEndPointShot() {
            var _ray = new Ray(mainCamera.transform.position, Camera.main.transform.forward);
            Physics.Raycast(_ray, out RaycastHit hitinfo);
            Debug.Log(hitinfo.collider.name);
            Instantiate(Pointer, hitinfo.point, Quaternion.identity);
            return hitinfo.point;
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

    }

    public enum TypeAttack {
        Physics,
        Nonphysics
    }
}


