using Blobcreate.ProjectileToolkit;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Player.Shoot
{
    public class PhysicAttackByAngle : IShoot
    {
        private IBullet bullet;
        private Transform spawnPoint;

        private float angleFastAttack;
        private float angleAimAttack;
        private float damege;
        private float gravity = Physics.gravity.y;

        public PhysicAttackByAngle(Transform spawnPoint, IBullet bullet, (float angleFastAttack, float angleAimAttack, float damage) shootingOptions)
        {
            this.spawnPoint = spawnPoint;
            this.bullet = bullet;
            this.angleFastAttack = shootingOptions.angleFastAttack;
            this.angleAimAttack = shootingOptions.angleAimAttack;
            this.damege = shootingOptions.damage;
        }

        public void GetAttack(Vector3 endPoint) {
            if (bullet is not MonoBehaviour monoBullet) return;

            var inst = Instantiator.Instantiate(monoBullet, spawnPoint.position, spawnPoint.rotation);
            var rigitbody = inst.GetComponent<Rigidbody>();
            var IBullet = inst.GetComponent<IBullet>();

            IBullet.SetDamage(damege);
            var velocity = Projectile.VelocityByAngle(spawnPoint.position, endPoint, angleFastAttack);
            
            rigitbody.velocity = velocity;
        }

        public void GetAim(TrajectoryPredictor trajectoryPredictor, Vector3 endPoint) {
            var velocity = Projectile.VelocityByAngle(spawnPoint.position, endPoint, angleFastAttack);
            trajectoryPredictor.Render(spawnPoint.position, velocity, GetDistance(spawnPoint.position, endPoint));
        }



        private Vector3 GetDirection(Vector3 from, Vector3 to)
        {
            var vector = GetVectorBetweenTwoPoints(from, to);
            var distance = GetDistance(from, to);
            return vector/distance;
        }
        private float GetVelocity(float X, float Y, float angle) {
            float andgleRadians = angle * Mathf.Rad2Deg; //Mathf.PI / 180;
            float V2 = (gravity * Mathf.Pow(X, 2)) / (2 * (Y - Mathf.Tan(andgleRadians) * X) * Mathf.Pow(Mathf.Cos(andgleRadians), 2));
            float V = Mathf.Sqrt(Mathf.Abs(V2));
            return V;
        }
        private float GetDistance(Vector3 from, Vector3 to) => (to-from).magnitude;
        private Vector3 GetVectorBetweenTwoPoints(Vector3 pointOne, Vector3 pointTwo) => pointTwo - pointOne;
        private Vector3 GetHorizontalProjection(Vector3 vector) => new Vector3(vector.x, 0, vector.z);
    }
}
