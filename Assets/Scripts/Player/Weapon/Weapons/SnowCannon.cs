using UnityEngine;
using Assets.Scripts.Player.Weapon.Interfaces;
using Assets.Scripts.Player.Shoot;
using Blobcreate.ProjectileToolkit;
using System.Net;
using Sirenix.OdinInspector;

namespace Assets.Scripts.Player.Weapon
{
    public class SnowCannon : MonoBehaviour, IWeapon_
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private Snowball snowball;


        [Space]
        [SerializeField] private TypeAttack typeAttack;

        [Tooltip("ByAngle")]
        [ShowIf("typeAttack", TypeAttack.PhysicByAngle)]
        [SerializeField] private float angleFastAttack;
        [ShowIf("typeAttack", TypeAttack.PhysicByAngle)]
        [SerializeField] private float angleAimAttack;

        [Tooltip("BySpeed")]
        [ShowIf("typeAttack", TypeAttack.PhysicBySpeed)]
        [SerializeField] private float speed;

        [Space]
        [SerializeField] private float damage;
        [SerializeField] private float scatterRadius;

        private IShoot shootType;

        private void Start() {
            //shootType = new PhysicAttackByAngle(firePoint, snowball, (angleFastAttack, angleAimAttack));
            shootType = ChangeAttack(typeAttack);
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

        private IShoot ChangeAttack(TypeAttack typeAttack)
        {
            return typeAttack switch
            {
                TypeAttack.PhysicByAngle => new PhysicAttackByAngle(firePoint, snowball, (angleFastAttack, angleAimAttack, damage)),
                TypeAttack.PhysicBySpeed => new PhysicAttackBySpeed(firePoint, snowball, (speed, damage)),
            };
        }

        private void OnValidate() {
            if (angleFastAttack < 1) angleFastAttack = 1;
            if (angleAimAttack < 1) angleAimAttack = 1;
            if (damage < 0) damage = 0;
            if (speed < 1) speed = 1;
        }

    }


    public enum TypeAttack
    {
        PhysicByAngle,
        PhysicBySpeed
    }
}

