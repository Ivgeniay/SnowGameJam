using Assets.Scripts.Player.Control;
using Assets.Scripts.Player.Weapon;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace Assets.Scripts.Player
{
    public class ShootingСontrol : MonoBehaviour
    {

        [SerializeField] public Transform snowBallSpawnPoint;
        [SerializeField] private IWeapon currentWeapon;

        private Inventory inventory;

        private float _forceIncreaseInSecond = 500f;
        private float _force = 500f;
        private float _maxForce = 5000f;

        private void Awake() {
            inventory = GetComponent<Inventory>();
            currentWeapon = inventory.GetWeapon(WeaponVariety.snowball); //snowBallPrefab.GetComponent<IWeapon>();
        }

        public void OnAttack(IWeapon weapon)
        {
            if (weapon is null) Debug.Log("HEY, THERE IS NO WEAPON");
            if (inventory.isAmmoLeft(weapon) is false) Debug.Log("HEY, THERE ARE NO AMMO");

            var instance = Instantiate(weapon.GetPrefab(), snowBallSpawnPoint.position, snowBallSpawnPoint.rotation);
            var instanceScr = instance.GetComponent<IWeapon>();
            instanceScr.Setup(((Vector3.up / 10f) + (Camera.main!.transform.forward)) * _force, snowBallSpawnPoint);
            instanceScr.SetCreator(transform);

            inventory.decrimentAmmo(weapon);
            _force = 500f;
        }

        public IWeapon GetCurrentWeapon() {
            return currentWeapon;
        }

        public void TakeAim(Projection projection) {
            if (_force < _maxForce) _force += _forceIncreaseInSecond * Time.deltaTime;
            else _force = _maxForce;
            var weapon = currentWeapon as MonoBehaviour;
            projection.SimulateTrajectory(weapon.GetComponent<IWeapon>(), snowBallSpawnPoint.position, ((Vector3.up / 10f) + (Camera.main!.transform.forward)) * _force);
        }
    }
}

    

