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

        private float _defaultForce;
        private bool isShowProjection = false;
        private AimDTO aimDTO;

        [SerializeField] private Transform spawnPoint;
        [SerializeField] public Transform SpawnPoint1 {get; private set; }
        [SerializeField] private Projection projection;

        [SerializeField] private IWeapon currentWeapon;
        [SerializeField] private IShoot shootType;

        [SerializeField] private float _forceIncreaseInSecond = 500f;
        [SerializeField] private float _force = 750f;
        [SerializeField] private float _maxForce = 5000f;

        //[SerializeField] private float leftForse = 0;
        //[SerializeField] private float rightForse = 0;
        //[SerializeField] private float duration = 0.2f;



        [Header("NonPhysics")]
        private Besiers besiers;
        [SerializeField] public Vector3 curve;
        [SerializeField] public float throwLength;
        [SerializeField] [Range(1, 100)] public int StepNonPhysic;
        [SerializeField] [Range(1, 100)] public int frameDelayNonPhysics;

        private void Awake() {
            if (projection is null) projection = GetComponent<Projection>();
            if (playerBehavior is null) playerBehavior = GetComponent<PlayerBehavior>();
            currentWeapon = playerBehavior.GetWeaponFromInventory(WeaponVariety.snowball);
            _defaultForce = _force;
            aimDTO = new();

            besiers = new Besiers();
            Vector3[] points = new Vector3[3];

            //shootType = new PhysicAttack(transform, spawnPoint);
            shootType = new BesiersAttack(transform, spawnPoint);
        }
        private void Start() {
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
                                    ThrowForce = _force, 
                                    Weapon = weapon });

            playerBehavior.decrimentAmmo(weapon);
            _force = _defaultForce;
        }

        public void TakeAim(Projection projection) {
            if (_force < _maxForce) _force += _forceIncreaseInSecond * Time.deltaTime;
            else _force = _maxForce;


            aimDTO.IncreaseInSecond = _forceIncreaseInSecond;
            aimDTO.MaxForce = _maxForce;
            aimDTO.Weapon = currentWeapon;
            aimDTO.Force = _force;
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


