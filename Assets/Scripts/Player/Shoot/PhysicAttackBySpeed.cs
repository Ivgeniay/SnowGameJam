using Assets.Scripts.Utilities;
using Blobcreate.ProjectileToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Shoot
{
    public class PhysicAttackBySpeed : IShoot
    {
        private IBullet bullet;
        private Transform spawnPoint;

        private float speed;
        private float damage;
        private float gravity = Physics.gravity.y;

        public PhysicAttackBySpeed(Transform spawnPoint, IBullet bullet, (float speed, float damage) shootingOptions)
        {
            this.spawnPoint = spawnPoint;
            this.bullet = bullet;
            this.speed = shootingOptions.speed;
            this.damage = shootingOptions.damage;
        }

        public void GetAttack(Vector3 endPoint) {
            if (bullet is not MonoBehaviour monoBullet) return;

            var inst = Instantiator.Instantiate(monoBullet, spawnPoint.position, spawnPoint.rotation);
            var rigitbody = inst.GetComponent<Rigidbody>();
            var IBullet = inst.GetComponent<IBullet>();

            IBullet.SetDamage(damage);
            rigitbody.velocity = GetVelocityByDirection(spawnPoint.position, endPoint, speed);
        }

        private Vector3 GetVelocityByDirection(Vector3 from, Vector3 to, float speed) {
            var heading = GetHeading(from, to);
            var distance = GetDistance(heading);
            var direction = GetDirection(heading, distance);
            return direction * speed;
        }
        private Vector3 GetHeading(Vector3 from, Vector3 to) => to - from; 
        private float GetDistance(Vector3 vector) => vector.magnitude; 
        private float GetDistance(Vector3 from, Vector3 to) => GetDistance(to - from);
        private Vector3 GetDirection(Vector3 vector, float distance) => vector/distance; 

        public void GetAim(TrajectoryPredictor trajectoryPredictor, Vector3 endPoint)
        {
            var velocity = GetVelocityByDirection(spawnPoint.position, endPoint, speed);
            trajectoryPredictor.Render(spawnPoint.position, velocity, GetDistance(spawnPoint.position, endPoint));
        }

    }
}
