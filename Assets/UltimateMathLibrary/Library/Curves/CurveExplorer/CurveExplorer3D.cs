using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> A component for displaying an <see cref="Curve{V}"/> using Gizmos where <c>V</c> is a <see cref="Vector3"/>. </summary>
    public class CurveExplorer3D : CurveExplorer<Vector3> {
        protected override Vector3 Transform(Vector3 point) {
            Vector3 e1 = transform.right * transform.lossyScale.x;
            Vector3 e2 = transform.up * transform.lossyScale.y;
            Vector3 e3 = transform.forward * transform.lossyScale.z;
            return transform.position + e1 * point.x + e2 * point.y + e3 * point.z;
        }

        [ContextMenu("Clear Curve")]
        private new void ClearCurve() => base.ClearCurve();
    }

}