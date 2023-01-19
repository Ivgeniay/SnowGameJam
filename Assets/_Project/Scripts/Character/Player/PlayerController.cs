using Assets._Project.Scripts._Input._InputAction;
using Assets._Project.Scripts.Character.Control;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Character.Player
{
    internal class PlayerController : MonoBehaviour, IControllable
    {


        public void Move(Vector3 vector) {
            //Debug.Log($"Move to {vector}");
        }
        public void Jump(Object obj = null)
        {
            Debug.Log("Jump");
        }
        public void Attack(Object obj = null)
        {
            Debug.Log($"Attack");
        }
    }
}
