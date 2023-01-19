using System.Collections.Generic;
using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> The type of Catmull-Rom curve, corresponding to the curve's alpha value. </summary>
    /// <remarks> Uniform represents an alpha value of 0. Centripetal repreents and alpha value of 1/2. Chordal represents alpha value of 1. </remarks>
    public enum CatRomType { Uniform, Centripetal, Chordal }

    /// <summary> Base class for Catmull-Rom curves. </summary>
    /// <typeparam name="V"> <inheritdoc cref="Curve{V}"/> </typeparam>
    public abstract class CatRom<V> : Spline<V>, I3Differentiable<V> where V : struct {

        /// <summary> The tension of the curve, typically between 0 and 1. This changes how sharply the curve bends. </summary>
        public float tension;

        /// <summary> The alpha value of the curve, typically between 0 and 1. Determines the type of Catmull-Rom curve; see <see cref="CatRomType"/>. </summary>
        public float alpha;

        /// <summary> Creates a new Catmull-Rom curve. </summary>
        /// <param name="controlPoints"> <inheritdoc cref="Spline{V}.controlPoints" path="/summary"/> </param>
        /// <param name="tension"> <inheritdoc cref="tension" path="/summary"/> </param>
        /// <param name="alpha"> <inheritdoc cref="alpha" path="/summary"/> </param>
        private protected CatRom(IVectorSpace<V> vs, IEnumerable<V> controlPoints, float tension, float alpha) : base(vs, controlPoints) {
            this.tension = tension;
            this.alpha = alpha;
        }

        /// <summary> <inheritdoc cref="CatRom{V}.CatRom(List{V}, float, float)"/> </summary>
        /// <param name="controlPoints"> <inheritdoc cref="Spline{V}.controlPoints" path="/summary"/> </param>
        /// <param name="tension"> <inheritdoc cref="tension" path="/summary"/> </param>
        /// <param name="type"> The <see cref="CatRomType"/> representing the alpha value of the curve. </param>
        private protected CatRom(IVectorSpace<V> vs, IEnumerable<V> controlPoints, float tension = 0.5f, CatRomType type = CatRomType.Centripetal) :
            this(vs, controlPoints, tension, type switch {
                CatRomType.Uniform => 0f,
                CatRomType.Centripetal => 0.5f,
                CatRomType.Chordal => 1f,
                _ => throw new System.ComponentModel.InvalidEnumArgumentException(nameof(type), (int) type, type.GetType())
            }) {
        }

        public override float tMin => 0f;
        public override float tMax => controlPoints.Count - 3f;

        #region Sampling and derivatives

        public override V Evaluate(float t) {
            if (UML.IsApproxInt(t, out int n))
                return controlPoints[n + 1];
            return CurveUtil.SampleCubic(UML.Frac(t), CalculateCoefficients(UML.FloorToInt(t)), vs);
        }

        public V GetDerivative(float t) {
            (int i, float t2) = GetSegmentPosition(t);
            return CurveUtil.GetDerivativeCubic(t2, CalculateCoefficients(i), vs);
        }

        public V GetSecondDerivative(float t) {
            (int i, float t2) = GetSegmentPosition(t);
            return CurveUtil.GetSecondDerivativeCubic(t2, CalculateCoefficients(i), vs);
        }

        public V GetThirdDerivative(float t) {
            (int i, float t2) = GetSegmentPosition(t);
            return CurveUtil.GetThirdDerivativeCubic(t2, CalculateCoefficients(i), vs);
        }

        #endregion

        /// <summary> Compute the coefficients of the point of the curve. </summary>
        /// <param name="i"> The index of the control point at t=0. </param>
        protected abstract (V a, V b, V c, V d) CalculateCoefficients(int i);

        /// <summary> Uses <see cref="CatRomType"/> to set the alpha value. </summary>
        public void SetCatRomType(CatRomType type) {
            alpha = type switch {
                CatRomType.Uniform => 0f,
                CatRomType.Centripetal => 0.5f,
                CatRomType.Chordal => 1f,
                _ => throw new System.ComponentModel.InvalidEnumArgumentException(nameof(type), (int) type, type.GetType())
            };
        }
    }

    /// <summary> A Catmull-Rom curve in 2D. </summary>
    public class CatRom2D : CatRom<Vector2> {
        /// <inheritdoc cref="CatRom{V}.CatRom(List{V}, float)"/>
        public CatRom2D(IEnumerable<Vector2> controlPoints, float tension, float alpha) :
            base(R2.instance, controlPoints, tension, alpha) { }
        /// <inheritdoc cref="CatRom{V}.CatRom(List{V}, float, CatRomType)"/>
        public CatRom2D(IEnumerable<Vector2> controlPoints, float tension = 0.5f, CatRomType type = CatRomType.Centripetal) : 
            base(R2.instance, controlPoints, tension, type) { }

        protected override (Vector2 a, Vector2 b, Vector2 c, Vector2 d) CalculateCoefficients(int i) {
            var (p0, p1, p2, p3) = (controlPoints[i], controlPoints[i + 1], controlPoints[i + 2], controlPoints[i + 3]);
            float k0 = 0f;
            float k1 = k0 + UML.Pow(Vector2.Distance(p0, p1), alpha);
            float k2 = k1 + UML.Pow(Vector2.Distance(p1, p2), alpha);
            float k3 = k2 + UML.Pow(Vector2.Distance(p2, p3), alpha);
            Vector2 m1 = (1f - tension) * (k2 - k1) * ((p1 - p0) / (k1 - k0) - (p2 - p0) / (k2 - k0) + (p2 - p1) / (k2 - k1));
            Vector2 m2 = (1f - tension) * (k2 - k1) * ((p2 - p1) / (k2 - k1) - (p3 - p1) / (k3 - k1) + (p3 - p2) / (k3 - k2));
            Vector2 a = 2*p1 - 2*p2 + m1 + m2;
            Vector2 b = -3*p1 + 3*p2 - 2*m1 - m2;
            Vector2 c = m1;
            Vector2 d = p1;
            return (a, b, c, d);
        }
    }

    /// <summary> A Catmull-Rom curve in 3D. </summary>
    public class CatRom3D : CatRom<Vector3> {
        /// <inheritdoc cref="CatRom{V}.CatRom(List{V}, float)"/>
        public CatRom3D(IEnumerable<Vector3> controlPoints, float tension, float alpha) : 
            base(R3.instance, controlPoints, tension, alpha) { }
        /// <inheritdoc cref="CatRom{V}.CatRom(List{V}, float, CatRomType)"/>
        public CatRom3D(IEnumerable<Vector3> controlPoints, float tension = 0.5f, CatRomType type = CatRomType.Centripetal) : 
            base(R3.instance, controlPoints, tension, type) { }

        protected override (Vector3 a, Vector3 b, Vector3 c, Vector3 d) CalculateCoefficients(int i) {
            var (p0, p1, p2, p3) = (controlPoints[i], controlPoints[i + 1], controlPoints[i + 2], controlPoints[i + 3]);
            float k0 = 0f;
            float k1 = k0 + UML.Pow(Vector2.Distance(p0, p1), alpha);
            float k2 = k1 + UML.Pow(Vector2.Distance(p1, p2), alpha);
            float k3 = k2 + UML.Pow(Vector2.Distance(p2, p3), alpha);
            Vector3 m1 = (1f - tension) * (k2 - k1) * ((p1 - p0) / (k1 - k0) - (p2 - p0) / (k2 - k0) + (p2 - p1) / (k2 - k1));
            Vector3 m2 = (1f - tension) * (k2 - k1) * ((p2 - p1) / (k2 - k1) - (p3 - p1) / (k3 - k1) + (p3 - p2) / (k3 - k2));
            Vector3 a = 2*p1 - 2*p2 + m1 + m2;
            Vector3 b = -3*p1 + 3*p2 - 2*m1 - m2;
            Vector3 c = m1;
            Vector3 d = p1;
            return (a, b, c, d);
        }
    }

}