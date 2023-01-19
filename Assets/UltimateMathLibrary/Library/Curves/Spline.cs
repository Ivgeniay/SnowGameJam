using System.Collections;
using System.Collections.Generic;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Base class for all Spline types. </summary>
    /// <remarks> A spline is a parametric curve defined using a set of control points.</remarks>
    /// <typeparam name="V"> <inheritdoc cref="Curve{V}"/> </typeparam>
    public abstract class Spline<V> : Curve<V>, IEnumerable<V> where V : struct {

        /// <summary> The list of control points used to define the curve. </summary>
        protected readonly List<V> controlPoints;

        /// <summary> Get and set control points by index. </summary>
        public V this[int i] {
            get => controlPoints[i];
            set {
                controlPoints[i] = value;
                UpdateCurve();
            }
        }

        /// <summary> Creates a new spline from the given control points. </summary>
        private protected Spline(IVectorSpace<V> vs, IEnumerable<V> controlPoints) : base(vs) {
            this.controlPoints = new List<V>(controlPoints);
        }

        public IEnumerator<V> GetEnumerator() => ((IEnumerable<V>) controlPoints).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) controlPoints).GetEnumerator();

        /// <summary> The number of control points in the spline. </summary>
        public int Count => controlPoints.Count;

        /// <summary> The total number of control points the internal data structure can hold without resizing. </summary>
        public int Capacity {
            get => controlPoints.Capacity;
            set => controlPoints.Capacity = value;
        }

        #region Add/Remove control points

        /// <summary> Adds a control point to the end of the spline. </summary>
        public void AddControlPoint(V point) {
            controlPoints.Add(point);
            UpdateCurve();
        }

        /// <summary> Adds multiple control points to the end of the spline. </summary>
        public void AddControlPoints(IEnumerable<V> points) {
            controlPoints.AddRange(points);
            UpdateCurve();
        }

        /// <summary> Adds a control point to the spline at the given index. </summary>
        public void InsertControlPoint(int index, V point) {
            controlPoints.Insert(index, point);
            UpdateCurve();
        }

        /// <summary> Adds multiple control points to the spline starting at the given index. </summary>
        public void InsertControlPoints(int index, IEnumerable<V> points) {
            controlPoints.InsertRange(index, points);
            UpdateCurve();
        }

        /// <summary> Removes the control point at the given index. </summary>
        public void RemoveControlPoint(int index) {
            controlPoints.RemoveAt(index);
            UpdateCurve();
        }

        /// <summary> Removes all control points. </summary>
        public void ClearControlPoints() {
            controlPoints.Clear();
            UpdateCurve();
        }

        #endregion
    }
}