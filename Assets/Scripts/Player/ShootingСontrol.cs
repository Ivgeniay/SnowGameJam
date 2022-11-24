using Assets.Scripts.Player.Control;
using Assets.Scripts.Player.Weapon;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace Assets.Scripts.Player
{
    public class ShootingСontrol : MonoBehaviour
    {

        [SerializeField] public Transform SpawnPoint;
        [SerializeField] private IWeapon currentWeapon;

        private PlayerBehavior playerBehavior;

        [SerializeField] private float _forceIncreaseInSecond = 500f;
        [SerializeField] private float _force = 500f;
        [SerializeField] private float _maxForce = 5000f;

        [SerializeField] private float leftForse = 20;
        [SerializeField] private float rightForse = 20;
        [SerializeField] private float duration = 0.5f;

        private void Awake() {
            playerBehavior = GetComponent<PlayerBehavior>();
            currentWeapon = playerBehavior.GetWeapon(WeaponVariety.snowball); //snowBallPrefab.GetComponent<IWeapon>();
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
            _force = 500f;
        }

        public void TakeAim(Projection projection) {
            if (_force < _maxForce) _force += _forceIncreaseInSecond * Time.deltaTime;
            else _force = _maxForce;

            var lForse = leftForse * (-Camera.main.transform.right);
            var rForse = rightForse * Camera.main.transform.right;
            var curv = new CurvatureData(lForse, rForse, duration);

            var weapon = currentWeapon as MonoBehaviour;
            projection.SimulateTrajectory(weapon.GetComponent<IWeapon>(), SpawnPoint, ((Vector3.up / 10f) + (Camera.main!.transform.forward)) * _force, curv);
        }
        public IWeapon GetCurrentWeapon() => currentWeapon;
    }
}

public class CurvatureData
{
    public CurvatureData() { }
    public CurvatureData(Vector3 lForse, Vector3 rForse, float duration)
    {
        this.lForse = lForse;
        this.rForse = rForse;
        this.duration = duration;
    }
    public Vector3 lForse;
    public Vector3 rForse;
    public float duration = 0;

    public Vector3 GetForce() {

        return Vector3.Lerp(lForse, rForse, duration);


        //var lVector = leftForse * Vector3.left;
        //var rVector = rightForse * Vector3.right;
        //return Vector3.Lerp(lVector, rVector, duration);
    }
    
}


