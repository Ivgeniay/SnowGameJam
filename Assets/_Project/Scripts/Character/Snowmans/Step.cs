using UnityEngine;

namespace Assets._Project.Scripts.Character.Snowmans
{
    internal class Step : MonoBehaviour
    {
        [SerializeField] private HingeJoint hingeJoint;
        [SerializeField] private float nextStepAnglePosition;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space)) 
            {
                MakeStep(ref nextStepAnglePosition, hingeJoint);
            }
        }

        private void FixedUpdate()
        {
            
        }

        private void MakeStep(ref float angle, HingeJoint hingeJoint)
        {
            var spring = hingeJoint.spring;
            spring.targetPosition = nextStepAnglePosition;
            hingeJoint.spring = spring;

            angle = -angle;
        }
    }
}
