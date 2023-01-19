using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Utilities.Rotations
{
    internal class RotationService : MonoBehaviour, IRotationService
    {
        private List<IRotation> rotations= new List<IRotation>();

        private void Update() {
            rotations.ForEach(el => el.RotateMe());
        }

        public void Register(IRotation IRotation) {
            rotations.Add(IRotation);
        }

        public void Unregister(IRotation IRotation) {
            rotations.Remove(IRotation);
        }
    }
}
