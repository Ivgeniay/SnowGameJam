using UnityEngine;

namespace Assets._Project.Scripts.Character.Control
{
    internal interface IControllable
    {
        public void Move(Vector3 vector);
        public void Jump(Object obj = null);
        public void Attack(Object obj = null);
    }
}
