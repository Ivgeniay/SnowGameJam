using System.Collections.Generic;
using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Base class for cubic Bezier curves. This is the most common type of Bezier curve. </summary>
    /// <typeparam name="V"> <inheritdoc cref="Curve{V}"/> </typeparam>
    public abstract class BezierCubic<V> : Bezier<V>, I3Differentiable<V> where V : struct {

        public sealed override int degree => 3;

        /// <summary> Creates a new cubic Bezier curve. </summary>
        /// <param name="controlPoints"> <inheritdoc cref="Spline{V}.controlPoints" path="/summary"/> </param>
        private protected BezierCubic(IVectorSpace<V> vs, IEnumerable<V> controlPoints) : base(vs, controlPoints) { }

        #region Sampling and Derivatives

        public override V Evaluate(float t) {
            (int i, float t2) = GetSegmentPosition(t);
            i *= degree;
            return vs.Sum(vs.Multiply(controlPoints[i], -t2.Cubed() + 3f*t2.Squared() - 3f*t2 + 1f),
                          vs.Multiply(controlPoints[i+1], 3f*t2.Cubed() - 6f*t2.Squared() +3f*t2),
                          vs.Multiply(controlPoints[i+2], -3f*t2.Cubed() + 3f*t2.Squared()),
                          vs.Multiply(controlPoints[i+3], t2.Cubed()));
        }

        public V GetDerivative(float t) {
            (int i, float t2) = GetSegmentPosition(t);
            i *= degree;
            return vs.Sum(vs.Multiply(controlPoints[i], -3f*t2.Squared() + 6f*t2 - 3f),
                          vs.Multiply(controlPoints[i+1], 9f*t2.Squared() - 12f*t2 + 3f),
                          vs.Multiply(controlPoints[i+2], -9f*t2.Squared() + 6f*t2),
                          vs.Multiply(controlPoints[i+3], 3f*t2.Squared()));
        }

        public V GetSecondDerivative(float t) {
            (int i, float t2) = GetSegmentPosition(t);
            i *= degree;
            return vs.Sum(vs.Multiply(controlPoints[i], -6f*t2 + 6f),
                          vs.Multiply(controlPoints[i+1], 18f*t2 - 12f),
                          vs.Multiply(controlPoints[i+2], -18f*t2 + 6f),
                          vs.Multiply(controlPoints[i+3], 6f*t2));
        }

        public V GetThirdDerivative(float t) {
            (int i, float t2) = GetSegmentPosition(t);
            i *= degree;
            return vs.Sum(vs.Multiply(controlPoints[i], -6f),
                          vs.Multiply(controlPoints[i+1], 18f),
                          vs.Multiply(controlPoints[i+2], -18f),
                          vs.Multiply(controlPoints[i+3], 6f));
        }

        #endregion

    }

    /// <summary> A cubic Bezier curve in 2D. </summary>
    public class BezierCubic2D : BezierCubic<Vector2> {
        /// <inheritdoc cref="Bezier{V}.Bezier"/>
        public BezierCubic2D(IEnumerable<Vector2> controlPoints) : base(R2.instance, controlPoints) { }
    }

    /// <summary> A cubic Bezier curve in 3D. </summary>
    public class BezierCubic3D : BezierCubic<Vector3> {
        /// <inheritdoc cref="Bezier{V}.Bezier"/>
        public BezierCubic3D(IEnumerable<Vector3> controlPoints) : base(R3.instance, controlPoints) { }
    }

}