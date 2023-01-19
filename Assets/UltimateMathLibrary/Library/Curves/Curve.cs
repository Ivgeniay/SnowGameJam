namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Interface for getting a <see cref="Curve{V}"/> reference. </summary>
    /// <typeparam name="V"> <inheritdoc cref="Curve{V}"/> </typeparam>
    public interface ICurve<V> where V : struct {
        /// <summary> Returns the instance of the <see cref="Curve{V}"/> associated with this <see cref="ICurve{V}"/>. </summary>
        public Curve<V> curve { get; }
    }

    /// <summary> Base class for all parametric curves. </summary>
    /// <typeparam name="V"> The type of elements in the codomain of the curve (i.e., Vector2, Vector3). </typeparam>
    public abstract class Curve<V> : ICurve<V> where V : struct {

        Curve<V> ICurve<V>.curve => this;

        /// <summary> The vector space used to interface with vectors of type <see cref="V"/>. </summary>
        internal readonly IVectorSpace<V> vs;

        /// <summary> An event that is invoked each time the curve is updated. This is useful for caching certain data to increase performance. </summary>
        protected event System.Action curveUpdated;

        #region Abstract properties

        /// <summary> The t-value at the start of the curve. </summary>
        public abstract float tMin { get; }
        /// <summary> The t-value at the end of the curve. </summary>
        public abstract float tMax { get; }

        #endregion

        private V? _startPoint, _endPoint = null;
        /// <summary> The start point of the curve. </summary>
        public V startPoint => _startPoint ??= Evaluate(tMin);
        /// <summary> The end point of the curve. </summary>
        public V endPoint => _endPoint ??= Evaluate(tMax);

        private protected Curve(IVectorSpace<V> vs) {
            this.vs = vs;
            curveUpdated += () => {
                _startPoint = _endPoint = null;
            };
        }

        /// <summary> This method should be called every time a change is made to the curve. </summary>
        protected void UpdateCurve() => curveUpdated?.Invoke();

        /// <summary> Gets the point on the curve at the given t-value. </summary>
        /// <param name="t"> The t-value to sample at. </param>
        public abstract V Evaluate(float t);

        /// <summary> Approximates the length of the curve. </summary>
        /// <param name="segments"> The number of segments to approximate the curve length with. Higher values are more accurate but slower. </param>
        public virtual float GetLength(int segments = 100) {
            float length = 0f;
            V lastSample = startPoint;
            for (int i = 1; i <= segments; i++) {
                float t = UML.Lerp(tMin, tMax, (float) i / segments);
                V sample = Evaluate(t);
                length += vs.Distance(sample, lastSample);
                lastSample = sample;
            }
            return length;
        }

        /// <summary> Helper method for evaluating curves with multiple segments. </summary>
        /// <param name="i"> The index of the first point in the curve segment. </param>
        /// <param name="t"> The t-value between the ith control point and the next segment of the curve. </param>
        protected (int i, float t) GetSegmentPosition(float t) {
            if (UML.IsApproxInt(t, out int n) && n > 0)
                return (n - 1, 1f);
            return (UML.FloorToInt(t), UML.Frac(t));
        }
    }
}