using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Internal interface that encapsulated various vector methods. Useful for defining generic vector types. </summary>
    /// <typeparam name="V"> The type of vectors in the vector space (i.e., Vector2, Vector3). </typeparam>
    internal interface IVectorSpace<V> where V : struct {
        public float Distance(V a, V b);
        public float Magnitude(V v);
        public V Normalized(V v);
        public V Lerp(V a, V b, float t);
        public V Multiply(float k, V v);
        public V Multiply(V v, float k);
        public V Sum(params V[] v);
    }

    /// <summary> A standard implementation of <see cref="IVectorSpace{V}"/> for 2D vectors. </summary>
    internal class R2 : IVectorSpace<Vector2> {

        public static readonly R2 instance = new R2();
        private R2() { }

        public float Distance(Vector2 a, Vector2 b) => Vector2.Distance(a, b);
        public float Magnitude(Vector2 v) => v.magnitude;
        public Vector2 Normalized(Vector2 v) => v.normalized;
        public Vector2 Lerp(Vector2 a, Vector2 b, float t) => Vector2.Lerp(a, b, t);
        public Vector2 Multiply(float k, Vector2 v) => k * v;
        public Vector2 Multiply(Vector2 v, float k) => k * v;
        public Vector2 Sum(params Vector2[] v) => UML.Sum(v);
    }

    /// <summary> A standard implementation of <see cref="IVectorSpace{V}"/> for 3D vectors. </summary>
    internal class R3 : IVectorSpace<Vector3> {

        public static readonly R3 instance = new R3();
        private R3() { }

        public float Distance(Vector3 a, Vector3 b) => Vector3.Distance(a, b);
        public float Magnitude(Vector3 v) => v.magnitude;
        public Vector3 Normalized(Vector3 v) => v.normalized;
        public Vector3 Lerp(Vector3 a, Vector3 b, float t) => Vector3.Lerp(a, b, t);
        public Vector3 Multiply(float k, Vector3 v) => k * v;
        public Vector3 Multiply(Vector3 v, float k) => k * v;
        public Vector3 Sum(params Vector3[] v) => UML.Sum(v);
    }
}