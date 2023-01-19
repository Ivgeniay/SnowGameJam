using UnityEngine;

namespace Assets._Project.Scripts.Character.Snowmans
{
    [RequireComponent(typeof(ConfigurableJoint))]
    internal class PhyBodyPart : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        private ConfigurableJoint configurableJoint;
        private Quaternion startQuaternion;

        private void Start() {
            configurableJoint = GetComponent<ConfigurableJoint>();
            startQuaternion = transform.localRotation;
        }

        private void FixedUpdate() {
            configurableJoint.targetRotation = Quaternion.Inverse(_target.localRotation) * startQuaternion;
        }
    }
}
