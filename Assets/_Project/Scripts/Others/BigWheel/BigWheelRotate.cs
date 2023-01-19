using Assets._Project.Scripts.Utilities.Rotations;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Others.BigWheel
{
    internal class BigWheelRotate : MonoBehaviour, IRotation
    {
        [SerializeField] private Vector3 rotateSpeed;

        private IRotationService rotationService;
        [Inject]
        private void Construct(IRotationService rotationService)
        {
            this.rotationService = rotationService;
        }

        private void OnEnable()
        {
            rotationService.Register(this);
        }
        private void OnDisable()
        {
            rotationService.Unregister(this); }

        public void RotateMe()
        {
            transform.Rotate(rotateSpeed);
        }
    }


    
}
