using UnityEngine;
using Assets.Scripts.Player.Weapon.Interfaces;
using Assets.Scripts.Player.Shoot;
using Blobcreate.ProjectileToolkit;
using System.Net;

namespace Assets.Scripts.Player.Weapon
{
    public class SnowCannon : MonoBehaviour, IWeapon_
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private Snowball snowball;

        [SerializeField] private float angleFastAttack;
        [SerializeField] private float angleAimAttack;
        [SerializeField] private float scatterRadius;

        private IShoot shootType;

        private void Start() {
            shootType = new NewPhysicAttack(firePoint, snowball, (angleFastAttack, angleAimAttack));
        }

        public void Fire(Vector3 fireEndPointPosition) {
            var scatterX = Random.Range(0, scatterRadius);
            var scatterY = Random.Range(0, scatterRadius);
            var scatterVector = new Vector3(scatterX, scatterY, 0);

            var fireEndPointPosition_ = fireEndPointPosition + scatterVector;
            shootType.GetAttack(fireEndPointPosition_);
        }

        public void GetAim(TrajectoryPredictor trajectoryPredictor, Vector3 endPoint) {
            shootType.GetAim(trajectoryPredictor, endPoint);
        }




        //public void GetPlayerAttack()//AttackDTO attackDTO)
        //{
        //    var instance = Instantiate(snowball, firePoint.position, firePoint.rotation);
        //    //var instanceScr = instance.GetComponent<IWeapon>();
        //    instance.SetCreator(transform);
        //    instance.Setup(((Vector3.up / 10f) + (Camera.main!.transform.forward)) * force, firePoint);

        //    force = beginingForse;
        //}

        private void OnValidate() {
        }

    }
}
