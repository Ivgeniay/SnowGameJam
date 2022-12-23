using Assets.Scripts.Player.Weapon;
using Assets.Scripts.Utilities;
using System.Net;
using UnityEngine;

namespace Assets.Scripts.Player.Shoot
{
    public class PhysicAttack : IShoot
    {
        private Transform transform;
        private Transform spawnPoint;
        private PlayerBehavior playerBehavior;

        private float maxForce;
        private float increaseInSecond;
        private float beginingForse;
        private float force;


        public PhysicAttack(Transform transform, Transform spawnPoint, (float MaxForce, float beginingForse, float IncreaseInSecond) forceTuple) {
            this.transform = transform;
            this.spawnPoint = spawnPoint;

            maxForce = forceTuple.MaxForce;
            increaseInSecond = forceTuple.IncreaseInSecond;
            beginingForse = forceTuple.beginingForse;

            playerBehavior = transform.GetComponent<PlayerBehavior>();
        }

        public void GetAttack(Vector3 endPoint)
        {
            //var instance = Instantiator.Instantiate(attackDTO.Weapon.GetPrefab(), spawnPoint.position, attackDTO.SpawnPoint.rotation);
            //var instanceScr = instance.GetComponent<IWeapon>();
            //instanceScr.SetCreator(transform);

            //var lForse = leftForse * (-Camera.main.transform.right);
            //var rForse = rightForse * Camera.main.transform.right;
            //var curv = new CurvatureData(lForse, rForse, duration);
            //instanceScr.Setup(((Vector3.up / 10f) + (Camera.main!.transform.forward)) * _force, spawnPoint, curv);

            //instanceScr.Setup(((Vector3.up / 10f) + (Camera.main!.transform.forward)) * force, spawnPoint);

            //force = beginingForse;
        }
        public void GetAim()
        {
            if (force < maxForce) force += increaseInSecond * Time.deltaTime;
            else force = maxForce;

            //var lForse = leftForse * (-Camera.main.transform.right);
            //var rForse = rightForse * Camera.main.transform.right;
            //var curv = new CurvatureData(lForse, rForse, duration);
            //projection.SimulateTrajectory(weapon.GetComponent<IWeapon>(), spawnPoint, ((Vector3.up / 10f) + (Camera.main!.transform.forward)) * _force, curv);

            //var weapon = aimDTO.Weapon as MonoBehaviour;
            //aimDTO.Projection.SimulateTrajectory(weapon.GetComponent<IWeapon>(), spawnPoint, ((Vector3.up / 10f) + (Camera.main!.transform.forward)) * force);
        }

        public Vector3 GetPointImpact() => Vector3.zero;
    }
}
