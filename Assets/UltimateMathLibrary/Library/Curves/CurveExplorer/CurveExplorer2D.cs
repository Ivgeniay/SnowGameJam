using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> A component for displaying an <see cref="Curve{V}"/> using Gizmos where <c>V</c> is a <see cref="Vector2"/>. </summary>
    public class CurveExplorer2D : CurveExplorer<Vector2> {
        protected override Vector3 Transform(Vector2 point) {
            Vector3 e1 = transform.right * transform.lossyScale.x;
            Vector3 e2 = transform.up * transform.lossyScale.y;
            return transform.position + e1 * point.x + e2 * point.y;
        }

        [ContextMenu("Clear Curve")]
        private new void ClearCurve() => base.ClearCurve();

        protected override void DrawAdditionalGizmos() {
            DrawAxes();
        }

        private void DrawAxes() {
            if (!(curve is PolynomialCurve polynomial)) return;

            Gizmos.color = UMLColors.red;
            Gizmos.DrawLine(Transform(Vector2.right * polynomial.interval.a), Transform(Vector2.right * polynomial.interval.b));

            //TODO: draw to exact height of polynomial
            Gizmos.color = UMLColors.green;
            Gizmos.DrawLine(Transform(Vector2.up * 100f), Transform(Vector2.up * -100f));
        }
    }

}