using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Contains mapping functions for all vector types. Useful for performing component-wise operations on vectors. </summary>
    public static class VectorMapping {

        public delegate T UnaryOperation<T>(T component);
        public delegate T BinaryOperation<T>(T leftComponent, T rightComponent);

        #region Vector2
        /// <summary>
        ///     Creates a new vector by performing a custom operation on each component.
        ///     The ith component of the resulting vector is defined by operation(V[i]).
        /// </summary>
        public static Vector2 Map(Vector2 v, UnaryOperation<float> operation) {
            Vector2 result = new Vector2();
            result.x = operation.Invoke(v.x);
            result.y = operation.Invoke(v.y);
            return result;
        }

        /// <summary> 
        ///     Creates a new vector by combining two vectors with a given operation. 
        ///     The ith component of the resulting vector is defined by operation(a[i], b[i]).
        /// </summary>
        public static Vector2 Map(Vector2 a, Vector2 b, BinaryOperation<float> operation) {
            Vector2 result = new Vector2();
            result.x = operation.Invoke(a.x, b.x);
            result.y = operation.Invoke(a.y, b.y);
            return result;
        }
        #endregion

        #region Vector3
        /// <inheritdoc cref="Map(Vector2, UnaryOperation{float})"/>
        public static Vector3 Map(Vector3 v, UnaryOperation<float> operation) {
            Vector3 result = new Vector3();
            result.x = operation.Invoke(v.x);
            result.y = operation.Invoke(v.y);
            result.z = operation.Invoke(v.z);
            return result;
        }

        /// <inheritdoc cref="Map(Vector2, Vector2, BinaryOperation{float})"/>
        public static Vector3 Map(Vector3 a, Vector3 b, BinaryOperation<float> operation) {
            Vector3 result = new Vector3();
            result.x = operation.Invoke(a.x, b.x);
            result.y = operation.Invoke(a.y, b.y);
            result.z = operation.Invoke(a.z, b.z);
            return result;
        }
        #endregion

        #region Vector4
        /// <inheritdoc cref="Map(Vector2, UnaryOperation{float})"/>
        public static Vector4 Map(Vector4 v, UnaryOperation<float> operation) {
            Vector4 result = new Vector4();
            result.x = operation.Invoke(v.x);
            result.y = operation.Invoke(v.y);
            result.z = operation.Invoke(v.z);
            result.w = operation.Invoke(v.w);
            return result;
        }

        /// <inheritdoc cref="Map(Vector2, Vector2, BinaryOperation{float})"/>
        public static Vector4 Map(Vector4 a, Vector4 b, BinaryOperation<float> operation) {
            Vector4 result = new Vector4();
            result.x = operation.Invoke(a.x, b.x);
            result.y = operation.Invoke(a.y, b.y);
            result.z = operation.Invoke(a.z, b.z);
            result.w = operation.Invoke(a.w, b.w);
            return result;
        }
        #endregion

        #region Vector2Int
        /// <inheritdoc cref="Map(Vector2, UnaryOperation{float})"/>
        public static Vector2Int Map(Vector2Int v, UnaryOperation<int> operation) {
            Vector2Int result = new Vector2Int();
            result.x = operation.Invoke(v.x);
            result.y = operation.Invoke(v.y);
            return result;
        }

        /// <inheritdoc cref="Map(Vector2, Vector2, BinaryOperation{float})"/>
        public static Vector2Int Map(Vector2Int a, Vector2Int b, BinaryOperation<int> operation) {
            Vector2Int result = new Vector2Int();
            result.x = operation.Invoke(a.x, b.x);
            result.y = operation.Invoke(a.y, b.y);
            return result;
        }
        #endregion

        #region Vector3Int
        /// <inheritdoc cref="Map(Vector2, UnaryOperation{float})"/>
        public static Vector3Int Map(Vector3Int v, UnaryOperation<int> operation) {
            Vector3Int result = new Vector3Int();
            result.x = operation.Invoke(v.x);
            result.y = operation.Invoke(v.y);
            result.z = operation.Invoke(v.z);
            return result;
        }

        /// <inheritdoc cref="Map(Vector2, Vector2, BinaryOperation{float})"/>
        public static Vector3Int Map(Vector3Int a, Vector3Int b, BinaryOperation<int> operation) {
            Vector3Int result = new Vector3Int();
            result.x = operation.Invoke(a.x, b.x);
            result.y = operation.Invoke(a.y, b.y);
            result.z = operation.Invoke(a.z, b.z);
            return result;
        }
        #endregion

        #region VectorN
        /// <inheritdoc cref="Map(Vector2, UnaryOperation{float})"/>
        public static VectorN Map(VectorN v, UnaryOperation<float> operation) {
            VectorN result = new VectorN(v.dimensions);
            for (int i = 0; i < v.dimensions; i++)
                result[i] = operation.Invoke(v[i]);
            return result;
        }

        /// <inheritdoc cref="Map(Vector2, Vector2, BinaryOperation{float})"/>
        /// <exception cref="DimensionMismatchException"/>
        public static VectorN Map(VectorN a, VectorN b, BinaryOperation<float> operation) {
            if (a.dimensions != b.dimensions) throw new DimensionMismatchException();
            VectorN result = new VectorN(a.dimensions);
            for (int i = 0; i < a.dimensions; i++)
                result[i] = operation.Invoke(a[i], b[i]);
            return result;
        }
        #endregion

        #region VectorNInt
        /// <inheritdoc cref="Map(Vector2, UnaryOperation{float})"/>
        public static VectorNInt Map(VectorNInt v, UnaryOperation<int> operation) {
            VectorNInt result = new VectorNInt(v.dimensions);
            for (int i = 0; i < v.dimensions; i++)
                result[i] = operation.Invoke(v[i]);
            return result;
        }

        /// <inheritdoc cref="Map(Vector2, Vector2, BinaryOperation{float})"/>
        /// <exception cref="DimensionMismatchException"/>
        public static VectorNInt Map(VectorNInt a, VectorNInt b, BinaryOperation<int> operation) {
            if (a.dimensions != b.dimensions) throw new DimensionMismatchException();
            VectorNInt result = new VectorNInt(a.dimensions);
            for (int i = 0; i < a.dimensions; i++)
                result[i] = operation.Invoke(a[i], b[i]);
            return result;
        }
        #endregion
    }
}