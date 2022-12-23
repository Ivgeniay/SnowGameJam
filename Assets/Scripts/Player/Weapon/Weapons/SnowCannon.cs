using UnityEngine;
using Assets.Scripts.Player.Weapon.Interfaces;
using Assets.Scripts.Player.Shoot;

namespace Assets.Scripts.Player.Weapon
{
    public class SnowCannon : MonoBehaviour, IWeapon_
    {
        [Header("NonPhysics")]
        [SerializeField] public Vector3 curve;
        [SerializeField] public float throwLength;
        [SerializeField][Range(1, 100)] public int StepNonPhysic;
        [SerializeField][Range(1, 100)] public int frameDelayNonPhysics;

        [SerializeField] private Transform firePoint;
        [SerializeField] private Snowball snowball;

        [SerializeField] private float force = 3000;
        [SerializeField] private float beginingForse = 700;

        private IShoot shootType;

        private void Start()
        {
            //shootType = new PhysicAttack(transform, firePoint, (5000, 500, 1000));
            shootType = new BesiersAttack(transform, firePoint, snowball, (StepNonPhysic, frameDelayNonPhysics));
        }

        public void Fire(Vector3 fireEndPointPosition)
        {
            shootType.GetAttack(fireEndPointPosition);
        }




        //public void GetPlayerAttack()//AttackDTO attackDTO)
        //{
        //    var instance = Instantiate(snowball, firePoint.position, firePoint.rotation);
        //    //var instanceScr = instance.GetComponent<IWeapon>();
        //    instance.SetCreator(transform);
        //    instance.Setup(((Vector3.up / 10f) + (Camera.main!.transform.forward)) * force, firePoint);

        //    force = beginingForse;
        //}

        public void GetAim(Projection projection) => shootType.GetAim();

        private void OnValidate() {
            if (throwLength < 2) throwLength = 2;
        }

    }
}
