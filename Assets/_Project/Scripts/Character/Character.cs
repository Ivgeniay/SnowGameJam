using Assets._Project.Scripts.Character.Control;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Assets._Project.Scripts.Character
{
    internal class Character : SerializedMonoBehaviour
    {
        [OdinSerialize] protected IControllable _controllable;
        protected void Jump() => _controllable.Jump();
        protected void Move(Vector3 vector) => _controllable.Move(vector);
    }
}
