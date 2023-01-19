namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Represents a differentiable <see cref="Curve{V}"/> of degree 1 or more. </summary>
    /// <typeparam name="V"> <inheritdoc cref="Curve{V}"/> </typeparam>
    public interface IDifferentiable<V> : ICurve<V> where V : struct {

        /// <summary> Calculates the first derivative (velocity) of the curve at the given t-value. </summary>
        public V GetDerivative(float t);
    }

    /// <summary> Represents a differentiable <see cref="Curve{V}"/> of degree 2 or more. </summary>
    /// <typeparam name="V"> <inheritdoc cref="Curve{V}"/> </typeparam>
    public interface I2Differentiable<V> : IDifferentiable<V> where V : struct {

        /// <summary> Calculates the second derivative (acceleration) of the curve at the given t-value. </summary>
        public V GetSecondDerivative(float t);
    }

    /// <summary> Represents a differentiable <see cref="Curve{V}"/> of degree 3 or more. </summary>
    /// <typeparam name="V"> <inheritdoc cref="Curve{V}"/> </typeparam>
    public interface I3Differentiable<V> : I2Differentiable<V> where V :struct {

        /// <summary> Calculates the third derivative (jerk) of the curve at the given t-value. </summary>
        public V GetThirdDerivative(float t);
    }
}