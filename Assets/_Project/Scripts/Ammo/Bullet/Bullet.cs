using DG.Tweening;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Assets._Project.Scripts.Ammo.Bullet
{
    internal class Bullet : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private Transform[] target;

        [SerializeField] private Transform lumbago;
        [SerializeField] private Transform damage;

        private ShootType type;

        public void SetUp(ShootType type)
        {
            this.type = type;
        }


        private void OnCollisionEnter(Collision collision)
        {
            foreach(var el in target)
            {
                if (collision.transform == el)
                {
                    var position = collision.GetContact(0).point;

                    var normal = - collision.GetContact(0).normal;

                    if (type == ShootType.lumbago)
                    {
                        var inst = Instantiate(lumbago, position, Quaternion.identity, el);
                        inst.LookAt(normal * 100);
                    }
                    else if (type == ShootType.ordinary)
                    {
                        var inst = Instantiate(damage, position, Quaternion.identity, el);
                    }

                    foreach (var item in collision.contacts) {
                        Debug.DrawRay(item.point, item.normal * 100, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 10f);
                    }
                }

                if (type == ShootType.lumbago) {
                    Destroy(this, 1);
                }
                else {
                    Destroy(this, 0);
                }
            }
        }

        private Vector3 GetRotaation(Vector3 normal)
        {
            var angleX = Vector3.Dot(new Vector3(normal.x, 0, 0), normal);
            var angleY = Vector3.Dot(new Vector3(0, normal.y, 0), normal);
            var angleZ = Vector3.Dot(new Vector3(0, 0, normal.z), normal);
            return new Vector3(angleX, angleY, angleZ);
        }

    }

    public enum ShootType
    {
        ordinary,
        lumbago
    }
}
