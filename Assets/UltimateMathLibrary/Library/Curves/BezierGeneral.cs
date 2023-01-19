using System.Collections.Generic;
using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Base class for generalized Bezier curves. If you are looking for cubic Bezier curves, see <see cref="BezierCubic{V}"/>. </summary>
    /// <typeparam name="V"> <inheritdoc cref="Curve{V}"/> </typeparam>
    public abstract class BezierGeneral<V> : Bezier<V>, I3Differentiable<V> where V : struct {

        private Polynomial[] basis;

        /// <summary> 
        ///     The degree of the Bezier curve. <para/>
        ///     If you are using <see cref="BezierGeneral{V}"/> and this is set to 2 or 3,
        ///         consider using <see cref="BezierQuad{V}"/> or <see cref="BezierCubic{V}"/> instead.
        /// </summary>
        public sealed override int degree => basis.Length - 1;
        
        /// <summary> Creates a new generalized Bezier curve. </summary>
        /// <param name="controlPoints"> <inheritdoc cref="Spline{V}.controlPoints" path="/summary"/> </param>
        private protected BezierGeneral(IVectorSpace<V> vs, IEnumerable<V> controlPoints, int degree) : base(vs, controlPoints) {
            SetDegree(degree);
        }

        /// <summary> Sets the degree of this generalized Bezier curve. </summary>
        public void SetDegree(int degree) {
            basis = Polynomial.BernsteinBasis(degree);
        }

        public override V Evaluate(float t) {
            (int i, float t2) = GetSegmentPosition(t);
            i *= degree;
            V result = new V();
            for (int j = 0; j <= degree; j++) {
                V contribution = vs.Multiply(controlPoints[i + j], basis[j].Evaluate(t2));
                result = vs.Sum(result, contribution);
            }
            return result;
        }

        public V GetDerivative(float t) {
            (int i, float t2) = GetSegmentPosition(t);
            i *= degree;
            V result = new V();
            for (int j = 0; j <= degree; j++) {
                V contribution = vs.Multiply(controlPoints[i + j], basis[j].GetDerivative().Evaluate(t2));
                result = vs.Sum(result, contribution);
            }
            return result;
        }

        public V GetSecondDerivative(float t) {
            (int i, float t2) = GetSegmentPosition(t);
            i *= degree;
            V result = new V();
            for (int j = 0; j <= degree; j++) {
                V contribution = vs.Multiply(controlPoints[i + j], basis[j].GetDerivative().GetDerivative().Evaluate(t2));
                result = vs.Sum(result, contribution);
            }
            return result;
        }

        public V GetThirdDerivative(float t) {
            (int i, float t2) = GetSegmentPosition(t);
            i *= degree;
            V result = new V();
            for (int j = 0; j <= degree; j++) {
                V contribution = vs.Multiply(controlPoints[i + j], basis[j].GetDerivative().GetDerivative().GetDerivative().Evaluate(t2));
                result = vs.Sum(result, contribution);
            }
            return result;
        }
    }

    /// <summary> A generalized Bezier curve in 2D. </summary>
    public class BezierGeneral2D : BezierGeneral<Vector2> {
        /// <inheritdoc cref="BezierGeneral{V}.BezierGeneral"/>
        public BezierGeneral2D(IEnumerable<Vector2> controlPoints, int degree) : 
            base(R2.instance, controlPoints, degree) { }
    }

    /// <summary> A generalized Bezier curve in 3D. </summary>
    public class BezierGeneral3D : BezierGeneral<Vector3> {
        /// <inheritdoc cref="BezierGeneral{V}.BezierGeneral"/>
        public BezierGeneral3D(IEnumerable<Vector3> controlPoints, int degree) : 
            base(R3.instance, controlPoints, degree) { }
    }
}