using Assets.Scripts.Player.Shoot.DTO;
using Assets.Scripts.Player.Weapon;
using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Shoot
{
    public class PhysicAttack : IShoot
    {
        private Transform transform;
        private Transform spawnPoint;
        private PlayerBehavior playerBehavior;
        private AttackDTO attackDTO;

        public PhysicAttack(Transform transform, Transform spawnPoint) {
            this.transform = transform;
            this.spawnPoint = spawnPoint;

            playerBehavior = transform.GetComponent<PlayerBehavior>();
        }

        public void GetAttack(AttackDTO attackDTO)
        {
            var instance = Instantiator.Instantiate(attackDTO.Weapon.GetPrefab(), spawnPoint.position, attackDTO.SpawnPoint.rotation);
            var instanceScr = instance.GetComponent<IWeapon>();
            instanceScr.SetCreator(transform);

            //var lForse = leftForse * (-Camera.main.transform.right);
            //var rForse = rightForse * Camera.main.transform.right;
            //var curv = new CurvatureData(lForse, rForse, duration);
            //instanceScr.Setup(((Vector3.up / 10f) + (Camera.main!.transform.forward)) * _force, spawnPoint, curv);

            instanceScr.Setup(((Vector3.up / 10f) + (Camera.main!.transform.forward)) * attackDTO.ThrowForce, spawnPoint);

            playerBehavior.decrimentAmmo(attackDTO.Weapon);
        }
        public void GetAim(AimDTO aimDTO)
        {
            if (aimDTO.Force < aimDTO.MaxForce) aimDTO.Force += aimDTO.IncreaseInSecond * Time.deltaTime;
            else aimDTO.Force = aimDTO.MaxForce;

            //var lForse = leftForse * (-Camera.main.transform.right);
            //var rForse = rightForse * Camera.main.transform.right;
            //var curv = new CurvatureData(lForse, rForse, duration);
            //projection.SimulateTrajectory(weapon.GetComponent<IWeapon>(), spawnPoint, ((Vector3.up / 10f) + (Camera.main!.transform.forward)) * _force, curv);

            var weapon = aimDTO.Weapon as MonoBehaviour;
            aimDTO.Projection.SimulateTrajectory(weapon.GetComponent<IWeapon>(), spawnPoint, ((Vector3.up / 10f) + (Camera.main!.transform.forward)) * aimDTO.Force);
        }
    }
}
