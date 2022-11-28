using Assets.Scripts.Player.Weapon;
using Assets.Scripts.Player;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class Rotater : SerializedMonoBehaviour
    {
        [SerializeField] private Vector3 RotateSpeed = new Vector3(0, 0.2f, 0);


        private void Update() {
            transform.Rotate(RotateSpeed);
        }
    }

}
