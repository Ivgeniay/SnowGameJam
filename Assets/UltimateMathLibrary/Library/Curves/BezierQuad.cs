using System.Collections.Generic;
using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Base class for quadratic Bezier curves. </summary>
    /// <typeparam name="V"> <inheritdoc cref="Curve{V}"/> </typeparam>
    public abstract class BezierQuad<V> : Bezier<V>, I2Differentiable<V> where V : struct {

        public sealed override int degree => 2;

        /// <summary> Creates a new quadratic Bezier curve. </summary>
        /// <param name="controlPoints"> <inheritdoc cref="Spline{V}.controlPoints" path="/summary"/> </param>
        private protected BezierQuad(IVectorSpace<V> vs, IEnumerable<V> controlPoints) : base(vs, controlPoints) { }

        #region Sampling and Derivatives

        public override V Evaluate(float t) {
            (int i, float t2) = GetSegmentPosition(t);
            i *= degree;
            return vs.Sum(vs.Multiply(controlPoints[i], t2.Squared() - 2f*t2 + 1f),
                          vs.Multiply(controlPoints[i+1], -2f*t2.Squared() + 2f*t2),
                          vs.Multiply(controlPoints[i+2],  t2.Squared()));
        }

        public V GetDerivative(float t) {
            (int i, float t2) = GetSegmentPosition(t);
            i *= degree;
            return vs.Sum(vs.Multiply(controlPoints[i], 2f*t2 - 2f),
                          vs.Multiply(controlPoints[i+1], -4f*t2 + 2f),
                          vs.Multiply(controlPoints[i+2], 2f*t2));
        }

        public V GetSecondDerivative(float t) {
            (int i, float t2) = GetSegmentPosition(t);
            i *= degree;
            return vs.Sum(vs.Multiply(controlPoints[i], 2f),
                          vs.Multiply(controlPoints[i+1], -4f),
                          vs.Multiply(controlPoints[i+2],  2f));
        }

        #endregion

    }

    /// <summary> A quadratic Bezier curve in 2D. </summary>
    public class BezierQuad2D : BezierQuad<Vector2> {
        /// <inheritdoc cref="BezierQuad{V}.BezierQuad"/>
        public BezierQuad2D(IEnumerable<Vector2> controlPoints) : base(R2.instance, controlPoints) { }
    }

    /// <summary> A quadratic Bezier curve in 3D. </summary>
    public class BezierQuad3D : BezierQuad<Vector3> {
        /// <inheritdoc cref="BezierQuad{V}.BezierQuad"/>
        public BezierQuad3D(IEnumerable<Vector3> controlPoints) : base(R3.instance, controlPoints) { }
    }
}