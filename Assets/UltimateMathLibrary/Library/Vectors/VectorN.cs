using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Nickmiste.UltimateMathLibrary.VectorMapping;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Representation of general n-dimensional vectors using integers. </summary>
    public struct VectorN : IEnumerable<float>, IEquatable<VectorN> {

        public readonly float[] components;

        #region Properties

        /// <summary> Access the nth component of this vector. </summary>
        public float this[int n] {
            get => components[n];
            set => components[n] = value;
        }

        /// <summary> Returns the squared length of this vector (Read Only). </summary>
        public float sqrMagnitude {
            get {
                float result = 0;
                foreach (float x in this)
                    result += UML.Square(x);
                return result;
            }
        }

        /// <summary> Returns the length of this vector (Read Only). </summary>
        public float magnitude => UML.Sqrt(sqrMagnitude);

        /// <summary> Gets the number of dimensions in this vector (Read Only). </summary>
        public int dimensions => components.Length;

        /// <summary> Returns this vector with a magnitude of 1 (Read Only). </summary>
        public VectorN normalized => this / magnitude;

        #endregion

        #region Constructors

        /// <summary> Creates a new vector with the given components. </summary>
        public VectorN(params float[] components) {
            this.components = components;
        }

        /// <summary> Creates a new vector with the specified number of dimensions and sets all components to 0. </summary>
        public VectorN(int dimensions) {
            components = new float[dimensions];
        }

        /// <summary> Returns a new vector with the specified number of dimensions where each component is set to 0. </summary>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown when dimensions is less then 0. </exception>
        public static VectorN Zero(int dimensions) {
            if (dimensions < 0) throw new ArgumentOutOfRangeException(nameof(dimensions), "The number of dimensions cannot be less than 0.");
            return new VectorN(new float[dimensions]);
        }

        /// <summary> Returns a new vector with the specified number of dimensions where each component is set to 1. </summary>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown when dimensions is less then 0. </exception>
        public static VectorN One(int dimensions) {
            if (dimensions < 0) throw new ArgumentOutOfRangeException(nameof(dimensions), "The number of dimensions cannot be less than 0.");
            float[] components = new float[dimensions];
            for (int i = 0; i < components.Length; i++)
                components[i] = 1f;
            return new VectorN(components);
        }

        /// <summary> Returns the nth standard basis vector in the specified number of dimensions. i.e., all values will be set to 0 except for the nth value, which will be set to 1. </summary>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown when dimensions is less then 0 or n is greater than or equal to the number of dimensions. </exception>
        public static VectorN StdBasis(int dimensions, int n) {
            if (dimensions < 0) throw new ArgumentOutOfRangeException(nameof(dimensions), "The number of dimensions cannot be less than 0.");
            if (n >= dimensions) throw new ArgumentOutOfRangeException(nameof(n), "n must be less than the number of dimensions.");
            float[] components = new float[dimensions];
            components[n] = 1f;
            return new VectorN(components);
        }

        #endregion

        IEnumerator<float> IEnumerable<float>.GetEnumerator() => ((IEnumerable<float>) components).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => components.GetEnumerator();

        public override string ToString() {
            if (dimensions == 0) return "()";
            var stringBuilder = new System.Text.StringBuilder("(");
            for (int i = 0; i < components.Length - 1; i++)
                stringBuilder.Append(components[i]).Append(", ");
            stringBuilder.Append(components[components.Length - 1]).Append(")");
            return stringBuilder.ToString();
        }

        /// <summary> Set all components of an existing VectorN. </summary>
        /// <exception cref="DimensionMismatchException"/>
        public void Set(params float[] components) {
            if (components.Length != this.components.Length) throw new DimensionMismatchException();
            for (int i = 0; i < this.components.Length; i++)
                this[i] = components[i];
        }

        /// <summary> Makes this vector have a magnitude of 1. </summary>
        public void Normalize() {
            float magnitude = this.magnitude;
            for (int i = 0; i < components.Length; i++)
                components[i] /= magnitude;
        }

        /// <summary> Returns the distance between a and b. </summary>
        /// <exception cref="DimensionMismatchException"/>
        public static float Distance(VectorN a, VectorN b) => (a - b).magnitude;

        /// <summary> Returns the dot product of two vectors. </summary>
        /// <exception cref="DimensionMismatchException"/>
        public static float Dot(VectorN a, VectorN b) {
            if (a.dimensions != b.dimensions) throw new DimensionMismatchException();
            float dot = 0;
            for (int i = 0; i < a.dimensions; i++)
                dot += a[i] * b[i];
            return dot;
        }

        #region Operations

        /// <summary> Returns a vector that is made from the largest components of two vectors. </summary>
        /// <exception cref="DimensionMismatchException"/>
        public static VectorN Max(VectorN a, VectorN b) => Map(a, b, UML.Max);

        /// <summary> Returns a vector that is made from the smallest components of two vectors. </summary>
        /// <exception cref="DimensionMismatchException"/>
        public static VectorN Min(VectorN a, VectorN b) => Map(a, b, UML.Min);

        /// <summary> Multiplies two vectors component-wise. </summary>
        /// <exception cref="DimensionMismatchException"/>
        public static VectorN Scale(VectorN a, VectorN b) => Map(a, b, (a, b) => a * b);

        //Unary
        public static VectorN operator +(VectorN v) => v;
        public static VectorN operator -(VectorN v) => Map(v, x => -x);

        //Binary w/ VectorN
        /** <exception cref="DimensionMismatchException"/> */ public static VectorN operator +(VectorN a, VectorN b) => Map(a, b, (a, b) => a + b);
        /** <exception cref="DimensionMismatchException"/> */ public static VectorN operator -(VectorN a, VectorN b) => Map(a, b, (a, b) => a - b);
        /** <exception cref="DimensionMismatchException"/> */ public static VectorN operator *(VectorN a, VectorN b) => Map(a, b, (a, b) => a * b);
        /** <exception cref="DimensionMismatchException"/> */ public static VectorN operator /(VectorN a, VectorN b) => Map(a, b, (a, b) => a / b);

        //Binary w/ float
        public static VectorN operator *(float k, VectorN v) => Map(v, x => k * x);
        public static VectorN operator *(VectorN v, float k) => Map(v, x => k * x);
        public static VectorN operator /(VectorN v, float k) => Map(v, x => x / k);

        //Equality
        public static bool operator ==(VectorN a, VectorN b) {
            if (a.dimensions != b.dimensions) return false;
            for (int i = 0; i < a.dimensions; i++)
                if (!UML.Approximately(a[i], b[i]))
                    return false;
            return true;
        }

        public static bool operator !=(VectorN a, VectorN b) {
            if (a.dimensions != b.dimensions) return true;
            for (int i = 0; i < a.dimensions; i++)
                if (UML.Approximately(a[i], b[i]))
                    return false;
            return true;
        }

        bool IEquatable<VectorN>.Equals(VectorN other) => other == this;

        public override bool Equals(object obj) => obj is VectorN v && v == this;
        public override int GetHashCode() => 1306830545 + EqualityComparer<float[]>.Default.GetHashCode(components);

        #endregion

        #region Type Conversions

        public static implicit operator VectorN(Vector2 v) => new VectorN(v.x, v.y);
        public static implicit operator VectorN(Vector3 v) => new VectorN(v.x, v.y, v.z);
        public static implicit operator VectorN(Vector4 v) => new VectorN(v.x, v.y, v.z, v.w);

        public static implicit operator Vector2(VectorN v) => v.dimensions switch {
            0 => Vector2.zero,
            1 => new Vector2(v[0], 0f),
            _ => new Vector2(v[0], v[1]),
        };

        public static implicit operator Vector3(VectorN v) => v.dimensions switch {
            0 => Vector3.zero,
            1 => new Vector3(v[0], 0f, 0f),
            2 => new Vector3(v[0], v[1], 0f),
            _ => new Vector3(v[0], v[1], v[2]),
        };

        public static implicit operator Vector4(VectorN v) => v.dimensions switch {
            0 => Vector4.zero,
            1 => new Vector4(v[0], 0f, 0f, 0f),
            2 => new Vector4(v[0], v[1], 0f, 0f),
            3 => new Vector4(v[0], v[1], v[2], 0f),
            _ => new Vector4(v[0], v[1], v[2], v[3]),
        };

        #endregion

    }
}