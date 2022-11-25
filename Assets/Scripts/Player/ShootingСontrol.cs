using Assets.Scripts.Player.Control;
using Assets.Scripts.Player.Weapon;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace Assets.Scripts.Player
{
    public class ShootingСontrol : MonoBehaviour
    {

        [SerializeField] public Transform SpawnPoint;
        [SerializeField] private Projection projection;
        [SerializeField] private IWeapon currentWeapon;

        private PlayerBehavior playerBehavior;

        [SerializeField] private float _forceIncreaseInSecond = 500f;
        [SerializeField] private float _force = 500f;
        [SerializeField] private float _maxForce = 5000f;
        private float _defaultForce;
        private bool isShowProjection = false;

        [SerializeField] private float leftForse = 20;
        [SerializeField] private float rightForse = 20;
        [SerializeField] private float duration = 0.5f;

        private void Awake() {
            if (projection is null) projection = GetComponent<Projection>();
            if (playerBehavior is null) playerBehavior = GetComponent<PlayerBehavior>();
            currentWeapon = playerBehavior.GetWeaponFromInventory(WeaponVariety.snowball);
            _defaultForce = _force;

        }
        private void Start()
        {
            InputManager.Instance.MouseWheelPerformed += ChangeWeapon;
            InputManager.Instance.AimPerformed += OnAimPerformed;
            InputManager.Instance.AimCanceled += OnAimCanceled;
        }

        private void ChangeWeapon(float obj)
        {
            if (obj > 0) currentWeapon = playerBehavior.GetNextWeaponFromInventory(GetCurrentWeapon());
            else if (obj < 0) currentWeapon = playerBehavior.GetPreviousWeaponFromInventory(GetCurrentWeapon());
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
            

            var instance = Instantiate(weapon.GetPrefab(), SpawnPoint.position, SpawnPoint.rotation);
            var instanceScr = instance.GetComponent<IWeapon>();
            instanceScr.SetCreator(transform);

            var lForse = leftForse * (-Camera.main.transform.right);
            var rForse = rightForse * Camera.main.transform.right;
            var curv = new CurvatureData(lForse, rForse, duration);

            instanceScr.Setup(((Vector3.up / 10f) + (Camera.main!.transform.forward)) * _force, SpawnPoint, curv);

            playerBehavior.decrimentAmmo(weapon);
            _force = _defaultForce;
        }

        public void TakeAim(Projection projection) {
            IncreasePower();

            var lForse = leftForse * (-Camera.main.transform.right);
            var rForse = rightForse * Camera.main.transform.right;
            var curv = new CurvatureData(lForse, rForse, duration);

            var weapon = currentWeapon as MonoBehaviour;
            projection.SimulateTrajectory(weapon.GetComponent<IWeapon>(), SpawnPoint, ((Vector3.up / 10f) + (Camera.main!.transform.forward)) * _force, curv);
        }

        public void IncreasePower() {
            if (_force < _maxForce) _force += _forceIncreaseInSecond * Time.deltaTime;
            else _force = _maxForce;
        }

        private void ProjectionConrol() {
            projection.EnableLine();
            TakeAim(projection);
        }

        public IWeapon GetCurrentWeapon() => currentWeapon;
        private void OnAimCanceled() => isShowProjection = false;
        private void OnAimPerformed() => isShowProjection = true;
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
}


