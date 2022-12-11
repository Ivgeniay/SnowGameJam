using System;
using UnityEngine;

namespace Assets.Scripts.Player.Weapon.DTO
{
    public class NonPhysicParameters : ICloneable
    {
        public Vector3 pastPosition;
        public float delaySecond;
        public float step;
        public float t;

        public object Clone()
        {
            return new NonPhysicParameters() { 
                pastPosition = this.pastPosition, 
                delaySecond = this.delaySecond, 
                step = this.step,
                t = this.t
            };
        }
    }
}
