namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Internal utility class to help with samping curves. </summary>
    internal static class CurveUtil {

        #region Cubic

        /// <summary> Samples the cubic curve defined by the given coefficients at the given t-value. </summary>
        internal static V SampleCubic<V>(float t, (V a, V b, V c, V d) coefficients, IVectorSpace<V> vs) where V : struct {
            return vs.Sum(vs.Multiply(coefficients.a, t.Cubed()),
                          vs.Multiply(coefficients.b, t.Squared()),
                          vs.Multiply(coefficients.c, t),
                          coefficients.d);
        }

        /// <summary> Gets the derivative of the cubic curve defined by the given coefficients at the given t-value. </summary>
        internal static V GetDerivativeCubic<V>(float t, (V a, V b, V c, V d) coefficients, IVectorSpace<V> vs) where V : struct {
            return vs.Sum(vs.Multiply(coefficients.a, 3f * t.Squared()),
                          vs.Multiply(coefficients.b, 2f * t),
                          coefficients.c);
        }

        /// <summary> Gets the second derivative of the cubic curve defined by the given coefficients at the given t-value. </summary>
        internal static V GetSecondDerivativeCubic<V>(float t, (V a, V b, V c, V d) coefficients, IVectorSpace<V> vs) where V : struct {
            return vs.Sum(vs.Multiply(coefficients.a, 6f * t),
                          vs.Multiply(coefficients.b, 2f));
        }

        /// <summary> Gets the third derivative of the cubic curve defined by the given coefficients at the given t-value. </summary>
        internal static V GetThirdDerivativeCubic<V>(float t, (V a, V b, V c, V d) coefficients, IVectorSpace<V> vs) where V : struct {
            return vs.Multiply(coefficients.a, 6f);
        }

        #endregion

        #region Quadratic

        /// <summary> Samples the quadratic curve defined by the given coefficients at the given t-value. </summary>
        internal static V SampleQuadratic<V>(float t, (V a, V b, V c) coefficients, IVectorSpace<V> vs) where V : struct {
            return vs.Sum(vs.Multiply(coefficients.a, t.Squared()),
                          vs.Multiply(coefficients.b, t),
                          coefficients.c);
        }

        /// <summary> Gets the derivative of the quadratic curve defined by the given coefficients at the given t-value. </summary>
        internal static V GetDerivativeQuadratic<V>(float t, (V a, V b, V c) coefficients, IVectorSpace<V> vs) where V : struct {
            return vs.Sum(vs.Multiply(coefficients.a, 2f * t),
                          coefficients.b);
        }

        /// <summary> Gets the second derivative of the quadratic curve defined by the given coefficients at the given t-value. </summary>
        internal static V GetSecondDerivativeQuadratic<V>(float t, (V a, V b, V c) coefficients, IVectorSpace<V> vs) where V : struct{
            return vs.Multiply(coefficients.a, 2f);
        }

        #endregion
    }
}