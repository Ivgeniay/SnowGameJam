using System.Collections.Generic;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Base class for Bezier curves. </summary>
    /// <typeparam name="V"> <inheritdoc cref="Curve{V}"/> </typeparam>
    public abstract class Bezier<V> : Spline<V> where V : struct {

        public sealed override float tMin => 0f;
        public sealed override float tMax => (controlPoints.Count - 1) / degree;

        /// <summary> The degree of the Bezier curve. </summary>
        public abstract int degree { get; }

        /// <summary> Creates a new Bezier curve. </summary>
        /// <param name="controlPoints"> <inheritdoc cref="Spline{V}.controlPoints" path="/summary"/> </param>
        private protected Bezier(IVectorSpace<V> vs, IEnumerable<V> controlPoints) : base(vs, controlPoints) { }
    }
}