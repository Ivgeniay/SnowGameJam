using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Nickmiste.UltimateMathLibrary.VectorMapping;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Representation of general n-dimensional vectors using integers. </summary>
    public struct VectorNInt : IEnumerable<int>, IEquatable<VectorNInt> {

        public readonly int[] components;

        #region Properties

        /// <summary> Access the nth component of this vector. </summary>
        public int this[int n] {
            get => components[n];
            set => components[n] = value;
        }

        /// <summary> Returns the squared length of this vector (Read Only). </summary>
        public int sqrMagnitude {
            get {
                int result = 0;
                foreach (int x in this)
                    result += UML.Square(x);
                return result;
            }
        }

        /// <summary> Returns the length of this vector (Read Only). </summary>
        public float magnitude => UML.Sqrt(sqrMagnitude);

        /// <summary> Gets the number of dimensions in this vector (Read Only). </summary>
        public int dimensions => components.Length;

        #endregion

        #region Constructors

        /// <summary> Creates a new vector with the given components. </summary>
        public VectorNInt(params int[] components) {
            this.components = components;
        }

        /// <summary> Creates a new vector with the specified number of dimensions and sets all components to 0. </summary>
        public VectorNInt(int dimensions) {
            components = new int[dimensions];
        }

        /// <summary> Returns a new vector with the specified number of dimensions where each component is set to 0. </summary>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown when dimensions is less then 0. </exception>
        public static VectorNInt Zero(int dimensions) {
            if (dimensions < 0) throw new ArgumentOutOfRangeException(nameof(dimensions), "The number of dimensions cannot be less than 0.");
            return new VectorNInt(new int[dimensions]);
        }

        /// <summary> Returns a new vector with the specified number of dimensions where each component is set to 1. </summary>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown when dimensions is less then 0. </exception>
        public static VectorNInt One(int dimensions) {
            if (dimensions < 0) throw new ArgumentOutOfRangeException(nameof(dimensions), "The number of dimensions cannot be less than 0.");
            int[] components = new int[dimensions];
            for (int i = 0; i < components.Length; i++)
                components[i] = 1;
            return new VectorNInt(components);
        }

        /// <summary> Returns the nth standard basis vector in the specified number of dimensions. i.e., all values will be set to 0 except for the nth value, which will be set to 1. </summary>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown when dimensions is less then 0 or n is greater than or equal to the number of dimensions. </exception>
        public static VectorNInt StdBasis(int dimensions, int n) {
            if (dimensions < 0) throw new ArgumentOutOfRangeException(nameof(dimensions), "The number of dimensions cannot be less than 0.");
            if (n >= dimensions) throw new ArgumentOutOfRangeException(nameof(n), "n must be less than the number of dimensions.");
            int[] components = new int[dimensions];
            components[n] = 1;
            return new VectorNInt(components);
        }

        #endregion

        IEnumerator<int> IEnumerable<int>.GetEnumerator() => ((IEnumerable<int>) components).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => components.GetEnumerator();

        public override string ToString() {
            if (dimensions == 0) return "()";
            var stringBuilder = new System.Text.StringBuilder("(");
            for (int i = 0; i < components.Length - 1; i++)
                stringBuilder.Append(components[i]).Append(", ");
            stringBuilder.Append(components[components.Length - 1]).Append(")");
            return stringBuilder.ToString();
        }

        /// <summary> Set all components of an existing VectorNInt. </summary>
        /// <exception cref="DimensionMismatchException"/>
        public void Set(params int[] components) {
            if (components.Length != this.components.Length) throw new DimensionMismatchException();
            for (int i = 0; i < this.components.Length; i++)
                this[i] = components[i];
        }

        /// <summary> Returns the distance between a and b. </summary>
        /// <exception cref="DimensionMismatchException"/>
        public static float Distance(VectorNInt a, VectorNInt b) => (a - b).magnitude;

        /// <summary> Returns the dot product of two vectors. </summary>
        /// <exception cref="DimensionMismatchException"/>
        public static int Dot(VectorNInt a, VectorNInt b) {
            if (a.dimensions != b.dimensions) throw new DimensionMismatchException();
            int dot = 0;
            for (int i = 0; i < a.dimensions; i++)
                dot += a[i] * b[i];
            return dot;
        }

        #region Operations

        /// <summary> Returns a vector that is made from the largest components of two vectors. </summary>
        /// <exception cref="DimensionMismatchException"/>
        public static VectorNInt Max(VectorNInt a, VectorNInt b) => Map(a, b, UML.Max);

        /// <summary> Returns a vector that is made from the smallest components of two vectors. </summary>
        /// <exception cref="DimensionMismatchException"/>
        public static VectorNInt Min(VectorNInt a, VectorNInt b) => Map(a, b, UML.Min);

        /// <summary> Multiplies two vectors component-wise. </summary>
        /// <exception cref="DimensionMismatchException"/>
        public static VectorNInt Scale(VectorNInt a, VectorNInt b) => Map(a, b, (a, b) => a*b);

        //Unary
        public static VectorNInt operator +(VectorNInt v) => v;
        public static VectorNInt operator -(VectorNInt v) => Map(v, x => -x);

        //Binary w/ VectorNInt
        /** <exception cref="DimensionMismatchException"/> */ public static VectorNInt operator +(VectorNInt a, VectorNInt b) => Map(a, b, (a, b) => a + b);
        /** <exception cref="DimensionMismatchException"/> */ public static VectorNInt operator -(VectorNInt a, VectorNInt b) => Map(a, b, (a, b) => a - b);
        /** <exception cref="DimensionMismatchException"/> */ public static VectorNInt operator *(VectorNInt a, VectorNInt b) => Map(a, b, (a, b) => a * b);
        /** <exception cref="DimensionMismatchException"/> */ public static VectorNInt operator /(VectorNInt a, VectorNInt b) => Map(a, b, (a, b) => a / b);

        //Binary w/ int
        public static VectorNInt operator *(int k, VectorNInt v) => Map(v, x => k * x);
        public static VectorNInt operator *(VectorNInt v, int k) => Map(v, x => k * x);
        public static VectorNInt operator /(VectorNInt v, int k) => Map(v, x => x / k);

        //Equality
        public static bool operator ==(VectorNInt a, VectorNInt b) {
            if (a.dimensions != b.dimensions) return false;
            for (int i = 0; i < a.dimensions; i++)
                if (a[i] != b[i])
                    return false;
            return true;
        }

        public static bool operator !=(VectorNInt a, VectorNInt b) {
            if (a.dimensions != b.dimensions) return true;
            for (int i = 0; i < a.dimensions; i++)
                if (a[i] == b[i])
                    return false;
            return true;
        }

        bool IEquatable<VectorNInt>.Equals(VectorNInt other) => other == this;

        public override bool Equals(object obj) => obj is VectorNInt v && v == this;
        public override int GetHashCode() => 1306830545 + EqualityComparer<int[]>.Default.GetHashCode(components);

        #endregion

        #region Type Conversions

        public static implicit operator VectorNInt(Vector2Int v) => new VectorNInt(v.x, v.y);
        public static implicit operator VectorNInt(Vector3Int v) => new VectorNInt(v.x, v.y, v.z);

        public static implicit operator Vector2Int(VectorNInt v) => v.dimensions switch {
            0 => Vector2Int.zero,
            1 => new Vector2Int(v[0], 0),
            _ => new Vector2Int(v[0], v[1]),
        };

        public static implicit operator Vector3Int(VectorNInt v) => v.dimensions switch {
            0 => Vector3Int.zero,
            1 => new Vector3Int(v[0], 0, 0),
            2 => new Vector3Int(v[0], v[1], 0),
            _ => new Vector3Int(v[0], v[1], v[2]),
        };

        public static implicit operator VectorN(VectorNInt v) {
            VectorN result = new VectorN(v.dimensions);
            for (int i = 0; i < v.dimensions; i++)
                result[i] = v[i];
            return result;
        }

        /// <summary> Converts a VectorN to a VectorNInt by doing a Ceiling to each value. </summary>
        public static VectorNInt CeilToInt(VectorN v) {
            VectorNInt result = new VectorNInt(v.dimensions);
            for (int i = 0; i < v.dimensions; i++)
                result[i] = UML.CeilToInt(v[i]);
            return result;
        }

        /// <summary> Converts a VectorN to a VectorNInt by doing a Floor to each value. </summary>
        public static VectorNInt FloorToInt(VectorN v) {
            VectorNInt result = new VectorNInt(v.dimensions);
            for (int i = 0; i < v.dimensions; i++)
                result[i] = UML.FloorToInt(v[i]);
            return result;
        }

        /// <summary> Converts a VectorN to a VectorNInt by doing a Round to each value. </summary>
        public static VectorNInt RoundToInt(VectorN v) {
            VectorNInt result = new VectorNInt(v.dimensions);
            for (int i = 0; i < v.dimensions; i++)
                result[i] = UML.RoundToInt(v[i]);
            return result;
        }

        #endregion

    }
}