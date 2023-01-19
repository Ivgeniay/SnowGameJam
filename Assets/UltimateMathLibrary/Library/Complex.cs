using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Represents a complex number of the form a + bi. </summary>
    [Serializable]
    public struct Complex : IEquatable<Complex> {

        /// <summary> The real part of the complex number. </summary>
        [SerializeField] public float real;
        /// <summary> The imaginary part of the complex number. </summary>
        [SerializeField] public float imaginary;

        #region Constants

        /// <summary> The imaginary unit. Defined by the property i^2 = -1. </summary>
        public static Complex i => new Complex(0f, 1f);

        /// <summary> The real number 0 + 0i. </summary>
        public static Complex zero => new Complex(0f, 0f);

        /// <summary> The real number 1 + 0i. </summary>
        public static Complex one => new Complex(1f, 0f);

        #endregion

        #region Properties

        /// <summary> The complex conjugate, a - bi. </summary>
        public Complex complexConjugate => new Complex(real, -imaginary);

        /// <summary> The modulus (magnitude) of this complex number. The distance on the complex plane between this number and zero. </summary>
        public float modulus => UML.Sqrt(real.Squared() + imaginary.Squared());

        /// <summary> The squared modulus (magnitude) of this complex number. </summary>
        public float sqrModulus => real.Squared() + imaginary.Squared();

        /// <summary> The angle, in radians, that this complex number forms with the positive real axis. </summary>
        public float argument => UML.Atan2(imaginary, real);

        /// <summary> Returns true if the complex number has no imaginary part. </summary>
        public bool isReal => UML.ApproximatelyZero(imaginary);

        /// <summary> Returns true if the complex number has no real part. </summary>
        public bool isPurelyImaginary => UML.ApproximatelyZero(real);

        #endregion

        /// <summary> Creates a new complex number given the real and imaginary parts. </summary>
        /// <param name="real"> <inheritdoc cref="real" path="/summary"/> </param>
        /// <param name="imaginary"> <inheritdoc cref="imaginary" path="/summary"/> </param>
        public Complex(float real, float imaginary) {
            this.real = real;
            this.imaginary = imaginary;
        }

        /// <summary> Creates a new complex number using polar coordinates. </summary>
        /// <param name="r"> <inheritdoc cref="modulus" path="/summary"/> </param>
        /// <param name="theta"> <inheritdoc cref="argument" path="/summary"/> </param>
        public static Complex FromPolar(float r, float theta) {
            return r * UML.Cis(theta);
        }

        /// <summary> Returns the kth nth root of unity. The kth solution to the equation z^n = 1. </summary>
        public static Complex RootOfUnity(int n, int k = 1) {
            return UML.Cis(k * UML.TAU / n);
        }

        public override string ToString() {
            if (isReal) return real.ToString();
            if (isPurelyImaginary) {
                if (UML.ApproximatelyOne(imaginary))
                    return "i"; ;
                if (UML.Approximately(imaginary, -1f))
                    return "-i";
                return imaginary.ToString() + "i";
            }
            return $"{real} {(imaginary >= 0 ? '+' : '-')} {UML.Abs(imaginary)}i";
        }

        #region Equality
        public override bool Equals(object obj) => obj is Complex complex && Equals(complex);
        public bool Equals(Complex z) => real == z.real && imaginary == z.imaginary;
        public override int GetHashCode() {
            int hashCode = -1613305685;
            hashCode = hashCode * -1521134295 + real.GetHashCode();
            hashCode = hashCode * -1521134295 + imaginary.GetHashCode();
            return hashCode;
        }
        public static bool operator ==(Complex z, Complex w) => z.Equals(w);
        public static bool operator !=(Complex z, Complex w) => !(z == w);
        #endregion

        public static implicit operator Complex(float real) => new Complex(real, 0f);
        public static implicit operator Complex(Vector2 v) => new Complex(v.x, v.y);
        public static implicit operator Vector2(Complex z) => new Vector2(z.real, z.imaginary);

        #region Operations
        public static Complex operator +(Complex z) => z;
        public static Complex operator -(Complex z) => new Complex(-z.real, -z.imaginary);

        public static Complex operator +(Complex z, Complex w) => new Complex(z.real + w.real, z.imaginary + w.imaginary);
        public static Complex operator -(Complex z, Complex w) => new Complex(z.real - w.real, z.imaginary - w.imaginary);

        public static Complex operator *(Complex z, Complex w) =>
            new Complex(z.real * w.real - z.imaginary * w.imaginary, z.real * w.imaginary + z.imaginary * w.real);
        public static Complex operator /(Complex z, Complex w) =>
            new Complex((z.real * w.real + z.imaginary * w.imaginary) / w.sqrModulus, (z.imaginary * w.real - z.real * w.imaginary) / w.sqrModulus);
        #endregion
    }
}