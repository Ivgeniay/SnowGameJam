using System;
using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Represents a single term of a <see cref="Polynomial"/>. </summary>
    [Serializable]
    public struct Monomial : IEquatable<Monomial> {

        /// <summary> The coefficient of the monomial. </summary>
        [SerializeField] public float coefficient;
        /// <summary> The degree of the monomial. </summary>
        [SerializeField] public int degree;

        /// <summary> Creates a new monomial given its coefficient and degree. </summary>
        /// <param name="coefficient"> <inheritdoc cref="coefficient" path="/summary"/> </param>
        /// <param name="degree"> <inheritdoc cref="degree" path="/summary"/> </param>
        public Monomial(float coefficient, int degree) {
            this.coefficient = coefficient;
            this.degree = degree;
        }

        #region Evaluation

        /// <summary> Evaluates the monomial at the given value of x. </summary>
        public float Evaluate(float x) {
            return coefficient * UML.Pow(x, degree);
        }

        /// <summary> Computes the derivative of the monomial. </summary>
        public Monomial GetDerivative() {
            return new Monomial(coefficient * degree, degree - 1);
        }

        /// <summary> Computes the antiderivative of the monomial, minus the integration constant. </summary>
        public Monomial GetAntiderivative() {
            return new Monomial(coefficient / (degree + 1), degree + 1);
        }

        /// <summary> Computes the definite integral of the polynomial from a to b. </summary>
        /// <param name="a"> The lower limit of integration. </param>
        /// <param name="b"> The upper limit of integration. </param>
        public float Integrate(float a, float b) {
            Monomial antiderivative = GetAntiderivative();
            return antiderivative.Evaluate(b) - antiderivative.Evaluate(a);
        }

        #endregion

        public override string ToString() => ToString("x");
        public string ToString(string variableName) => degree switch {
            0 => coefficient.ToString(),
            1 => coefficient + variableName,
            _ => $"{coefficient}{variableName}^{degree}",
        };

        #region Equality
        public override bool Equals(object obj) => obj is Monomial monomial && Equals(monomial);
        public bool Equals(Monomial other) => coefficient == other.coefficient && degree == other.degree;
        public override int GetHashCode() {
            int hashCode = 1900527627;
            hashCode = hashCode * -1521134295 + coefficient.GetHashCode();
            hashCode = hashCode * -1521134295 + degree.GetHashCode();
            return hashCode;
        }
        public static bool operator ==(Monomial a, Monomial b) => a.Equals(b);
        public static bool operator !=(Monomial a, Monomial b) => !(a == b);
        #endregion

        public static implicit operator Monomial(float constant) => new Monomial(constant, 0);

        #region Operations

        public static Monomial operator +(Monomial m) => m;
        public static Monomial operator -(Monomial m) {
            m.coefficient = -m.coefficient;
            return m;
        }

        public static Monomial operator *(Monomial m, float k) => k * m;
        public static Monomial operator *(float k, Monomial m) {
            m.coefficient *= k;
            return m;
        }
        public static Monomial operator *(Monomial a, Monomial b) {
            return new Monomial(a.coefficient * b.coefficient, a.degree + b.degree);
        }

        public static Monomial operator /(Monomial m, float k) {
            m.coefficient /= k;
            return m;
        }
        public static Monomial operator /(Monomial a, Monomial b) {
            return new Monomial(a.coefficient / b.coefficient, a.degree - b.degree);
        }

        public static Polynomial operator +(Monomial a, Monomial b) {
            return new Polynomial(new[] { a, b });
        }

        public static Polynomial operator -(Monomial a, Monomial b) {
            return new Polynomial(new[] { a, -b });
        }

        #endregion
    }
}
