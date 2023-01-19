using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> 
    ///     The base class for displaying an <see cref="Curve{V}"/> using Gizmos.
    ///     To attach to a game object, see <see cref="CurveExplorer2D"/> and <see cref="CurveExplorer3D"/>. 
    /// </summary>
    /// <typeparam name="V"> <inheritdoc cref="Curve{V}"/> </typeparam>
    [ExecuteAlways]
    public abstract class CurveExplorer<V> : MonoBehaviour where V : struct {

        [Header("Curve Settings")]
        
        [Tooltip("The curve to display.")]
        [SerializeField] private CurveData<V> curveData;

        [Header("Display Settings")]

        [Tooltip("The color to draw the curve with.")]
        [SerializeField] public Color color = Color.blue;

        [Tooltip("The number of samples taken to approximate the curve. Higher values will display a higher resolution curve at the cost of performance.")]
        [SerializeField] [Range(2, 500)] public int samples = 50;

        [Tooltip("When set to true, draw the control points of the curve. Only compatible with splines.")]
        [SerializeField] public bool showControlPoints = false;

        [Header("Frenet Frame")]

        [Tooltip("When set to true, draw the Frenet frame at the given t-value of the curve. Only compatible with 3D 2-differentiable curves.")]
        [SerializeField] public bool showFrenetFrame = false;

        [Tooltip("The normalized t-value to draw the Frenet frame at when enabled. Note that this t-value goes from 0 to 1, regardless of the interval the curve is defined on.")]
        [SerializeField] [Range(0f, 1f)] public float tValue = 0f;

        // When curveData is set (through the inspector), use that to get the curve
        // When curve is set through another script, override curveData and use that
        private Curve<V> _curve;
        /// <summary> Gets or sets the curve to draw. Can be indirectly set via the inspector. </summary>
        public Curve<V> curve {
            get => curveData?.GetCurve() ?? _curve;
            set {
                curveData = null;
                _curve = value;
            }
        }

        /// <summary> Transforms the input point into object space, accounting for the game object's transform. </summary>
        protected abstract Vector3 Transform(V point);

        /// <summary> Implement this to draw gizmos specific to certain types of curve explorers. </summary>
        protected virtual void DrawAdditionalGizmos() { }

        /// <summary> Clears the curve and curveData. Intended for use by subclasses to create a context menu option. </summary>
        protected void ClearCurve() => curve = null;

        private void OnDrawGizmos() {
            if (curve == null)
                return;

            Gizmos.color = color;

            Vector3 lastSample = Transform(curve.startPoint);
            for (int i = 1; i <= samples; i++) {
                float t = UML.Lerp(curve.tMin, curve.tMax, i / (samples - 1f));
                Vector3 sample = Transform(curve.Evaluate(t));
                Gizmos.DrawLine(lastSample, sample);
                lastSample = sample;
            }

            if (showControlPoints)
                DrawControlPoints();
            if (showFrenetFrame)
                DrawFrenetFrame();

            DrawAdditionalGizmos();
        }

        private void DrawControlPoints() {
            if (!(curve is Spline<V> spline))
                return;

            Gizmos.color = UMLColors.red.WithAlpha(0.75f);
            float scale = UML.Mean(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
            foreach (V p in spline)
                Gizmos.DrawSphere(Transform(p), 0.02f * scale);
            if (curve is Bezier<V> bezier)
                DrawHandles(bezier);
        }

        //TODO: optimize - remove redundant calls to Transform()
        private void DrawHandles(Bezier<V> bezier) {
            if (bezier.degree == 3) {
                for (int i = 1; i < bezier.Count; i++)
                    if (i % 3 != 2)
                        UMLGizmos.DrawLineDotted(Transform(bezier[i - 1]), Transform(bezier[i]));
            }
            else {
                for (int i = 1; i < bezier.Count; i++)
                    UMLGizmos.DrawLineDotted(Transform(bezier[i - 1]), Transform(bezier[i]));
            }
        }

        private void DrawFrenetFrame() {
            float t = UML.Lerp(curve.tMin, curve.tMax, tValue);
            float scale = UML.Mean(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
            if (curve is I2Differentiable<Vector3> diff3) {
                FrenetFrame3D frame = diff3.GetFrenetFrame(t);
                frame.point = Transform(curve.Evaluate(t));
                UMLGizmos.DrawFrenetFrame(frame, scale);
            }
            else if (curve is I2Differentiable<Vector2> diff2) {
                FrenetFrame2D frame = diff2.GetFrenetFrame(t);
                frame.point = Transform(curve.Evaluate(t));
                UMLGizmos.DrawFrenetFrame(frame, scale);
            }
        }

    }
}