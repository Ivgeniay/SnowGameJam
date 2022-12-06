using System;
using UnityEngine;

namespace Assets.Scripts.Player.Weapon.DTO
{
    public class NonPhysicParameters : ICloneable
    {
        public Vector3[] positions;
        public Vector3 pastPosition;
        public float force;
        public float delaySecond;
        public float step;
        public float t;

        public object Clone()
        {
            return new NonPhysicParameters() { 
                positions = this.positions, 
                pastPosition = this.pastPosition, 
                force = this.force, 
                delaySecond = this.delaySecond, 
                step = this.step,
                t = this.t
            };
        }
    }
}
