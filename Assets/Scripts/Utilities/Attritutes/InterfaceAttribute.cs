using System;
using UnityEngine;

namespace Assets.Scripts.Utilities.Attritutes
{
    public class InterfaceAttribute : PropertyAttribute
    {
        public Type type { get; }
        public bool objectFromScene { get; }
        public InterfaceAttribute (Type type, bool objectFromScene = true) {
            this.type = type;
            this.objectFromScene = objectFromScene;
        }
    }
}
