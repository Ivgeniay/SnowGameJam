using System;
using System.Collections;
using System.Collections.Generic;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Represents a polynomial of a single variable. </summary>
    public struct Polynomial : IEnumerable<Monomial>, IEquatable<Polynomial> {

        private CoefficientList coefficients; //maps degree to coefficient

        /// <summary> The highest exponent with nonzero coefficient. </summary>
        public int degree => coefficients is null || coefficients.Keys.Count == 0 ? 0 : UML.Max(0, coefficients.Keys[coefficients.Keys.Count - 1]);

        #region Polynomial properties

        /// <summary> Returns true if all nontrivial terms are of even degree. Even functions satisfy the property f(x) = f(-x). </summary>
        public bool isEven {
            get {
                foreach (Monomial term in this)
                    if (term.degree % 2 == 1)
                        return false;
                return true;
            }
        }

        /// <summary> Returns true if all nontrivial terms are of odd degree. Odd functions satisfy the property -f(x) = f(-x). </summary>
        public bool isOdd {
            get {
                foreach (Monomial term in this)
                    if (term.degree % 2 == 0)
                        return false;
                return true;
            }
        }

        #endregion

        /// <summary> Access a coefficient by the degree of its term. </summary>
        public float this[int degree] {
            get {
                if (coefficients is null) return 0f;
                return coefficients.TryGetValue(degree, out float coefficient) ? coefficient : 0f;
            }
            set {
                coefficients ??= new CoefficientList(1);
                coefficients[degree] = value;
            } 
        }

        /// <summary> The zero polynomial. Always evaluates to 0. </summary>
        public static Polynomial zero => new Polynomial(0f);

        #region Constructors

        /// <summary> Creates a new polynomial with the given coefficients. </summary>
        /// <param name="coefficients"> 
        ///     The coefficients of the polynomial, corresponding to terms of increasing degree. <para/>
        ///     For example, the collection { 1, 2, 3 } creates the polynomial 3x^2 + 2x + 1.
        /// </param>
        /// 
        public Polynomial(params float[] coefficients) {
            this.coefficients = new CoefficientList();
            for (int i = 0; i < coefficients.Length; i++)
                this.coefficients.Add(i, coefficients[i]);
        }

        /// <summary> Creates a new polynomial with the given coefficients. </summary>
        /// <param name="coefficients"> A dictionary mapping the degree of each term to its coefficient. </param>
        public Polynomial(IDictionary<int, float> coefficients) {
            this.coefficients = new CoefficientList(coefficients);
        }

        /// <summary> Creates a new polynomial that is the sum of a collection of monomials. </summary>
        public Polynomial(IEnumerable<Monomial> terms) : this() {
            foreach (Monomial term in terms)
                this += term;
        }

        #endregion

        #region Evaluation

        /// <summary> Evaluates the polynomial at the given value of x. </summary>
        public float Evaluate(float x) {
            float result = 0f;
            foreach (Monomial term in this)
                result += term.Evaluate(x);
            return result;
        }

        /// <summary> Computes the derivative of the polynomial. </summary>
        public Polynomial GetDerivative() {
            Polynomial derivative = new Polynomial();
            foreach (Monomial term in this)
                derivative += term.GetDerivative();
            return derivative;
        }

        /// <summary> Computes the antiderivative of the polynomial, minus the integration constant. </summary>
        public Polynomial GetAntiderivative() {
            Polynomial antiderivative = new Polynomial();
            foreach (Monomial term in this)
                antiderivative += term.GetAntiderivative();
            return antiderivative;
        }

        /// <summary> Computes the definite integral of the polynomial from a to b. </summary>
        /// <param name="a"> The lower limit of integration. </param>
        /// <param name="b"> The upper limit of integration. </param>
        public float Integrate(float a, float b) {
            Polynomial antiderivative = GetAntiderivative();
            return antiderivative.Evaluate(b) - antiderivative.Evaluate(a);
        }

        /// <summary> Computes the real roots of the polynomial - the values at which the polynomial evaluates to 0. </summary>
        /// <exception cref="InvalidOperationException"> Thrown when the degree is greater than 3. </exception>
        public float[] GetRoots() => degree switch {
            0 => new float[0],
            1 => new float[1] { UML.XIntercept(this[1], this[0]) },
            2 => UML.QuadraticFormula(         this[2], this[1], this[0]),
            3 => UML.CubicFormula(             this[3], this[2], this[1], this[0]),
            _ => throw new InvalidOperationException($"{nameof(GetRoots)} is only available for polynomials of degree 3 or less. " +
                $"Use ${nameof(Polynomial.NewtonsMethod)} instead."),
        };

        /// <summary> Computes the discriminant of the polynomial. This tells you information about the nature of the roots without needing to compute them. </summary>
        /// <exception cref="InvalidOperationException"> Thrown when trying to get the discriminant of a degree 0 polynomial. </exception>
        public float GetDiscriminant() => degree switch {
            0 => throw new InvalidOperationException("Discriminant is undefined on polynomials of degree 0."),
            1 => 1f,
            2 => UML.QuadraticDiscriminant(this[2], this[1], this[0]),
            3 => UML.CubicDiscriminant(    this[3], this[2], this[1], this[0]),
            4 => UML.QuarticDiscriminant(  this[4], this[3], this[2], this[1], this[0]),
            _ => throw new NotImplementedException("General discriminants of degree 5 or greater are not yet implemented."),
        };

        /// <summary> Uses Newton's method on the polynomial to approximate a root. </summary>
        /// <remarks> If the degree is less than 4, is is reccomended to use <see cref="GetRoots"/> instead to find the exact roots. </remarks>
        /// <param name="root"> The approximated root of the polynomial. </param>
        /// <param name="initialGuess"> The x-value to start searching for a root at. The closer this value is to a real root, the better. </param>
        /// <param name="delta"> Consider a root "found" when evaluating at that x-value produces a result no more than delta away from 0. </param>
        /// <param name="maxIterations"> The maximum number of times to repeat the process. </param>
        /// <returns> True if a root was found within the given delta value; false otherwise. </returns>
        public bool NewtonsMethod(out float root, float initialGuess = 0f, float delta = 0.01f, int maxIterations = 30) {
            root = initialGuess;
            Polynomial derivative = GetDerivative();
            for (int i = 0; i < maxIterations; i++) {
                if (UML.Abs(Evaluate(root)) <= delta) return true;
                float slope = derivative.Evaluate(root);
                if (slope == 0f) return false;
                root -= Evaluate(root) / slope;
            }
            return false;
        }

        #endregion

        public override string ToString() => ToString("x");
        public string ToString(string variableName) {
            var result = new System.Text.StringBuilder();
            for (int i = degree; i >= 0; i--) {
                if (UML.ApproximatelyZero(this[i])) continue;
                if (!UML.ApproximatelyOne(UML.Abs(this[i])) || i == 0)
                    result.Append(i == degree ? this[i] : UML.Abs(this[i]));
                else if (this[i] < 0 && i == degree)
                    result.Append('-');
                result.Append(i switch {
                    0 => "",
                    1 => variableName,
                    _ => $"{variableName}^{i}",
                });
                if (i - 1 >= 0)
                    result.Append(this[i - 1] > 0 ? " + " : " - ");
                else result.Append("...");
            }
            if (result.Length == 0) return "0";
            return result.ToString(0, result.Length - 3);
        }

        public IEnumerator<Monomial> GetEnumerator() {
            if (coefficients is null) yield break;
            foreach (var kv in coefficients)
                yield return new Monomial(kv.Value, kv.Key);
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #region Equality
        public override bool Equals(object obj) => obj is Polynomial polynomial && Equals(polynomial);
        public bool Equals(Polynomial other) => System.Linq.Enumerable.SequenceEqual(this, other);
        public override int GetHashCode() => 1112971371 + GetEnumerator().GetHashCode();
        public static bool operator ==(Polynomial a, Polynomial b) => a.Equals(b);
        public static bool operator !=(Polynomial a, Polynomial b) => !(a == b);
        #endregion

        public static implicit operator Polynomial(float constant) => new Polynomial(constant);
        public static implicit operator Polynomial(Monomial monomial) => new Polynomial(new[] { monomial });

        #region Monomial Operations

        public static Polynomial operator +(Monomial m, Polynomial p) => p + m;
        public static Polynomial operator +(Polynomial p, Monomial m) {
            p[m.degree] += m.coefficient;
            return p;
        }

        public static Polynomial operator -(Polynomial p, Monomial m) => p + -m;
        public static Polynomial operator -(Monomial m, Polynomial p) => m + -p;

        public static Polynomial operator *(Monomial m, Polynomial p) => p * m;
        public static Polynomial operator *(Polynomial p, Monomial m) {
            Polynomial result = new Polynomial();
            foreach (Monomial term in p)
                result += term * m;
            return result;
        }

        #endregion

        #region Polynomial Operations

        public static Polynomial operator +(Polynomial p) => p;
        public static Polynomial operator -(Polynomial p) {
            foreach (Monomial term in p)
                p[term.degree] = -p[term.degree];
            return p;
        }

        public static Polynomial operator *(Polynomial p, float k) => k * p;
        public static Polynomial operator *(float k, Polynomial p) {
            foreach (Monomial term in p)
                p[term.degree] *= k;
            return p;
        }

        public static Polynomial operator /(Polynomial p, float k) {
            foreach (Monomial term in p)
                p[term.degree] /= k;
            return p;
        }

        public static Polynomial operator +(Polynomial p1, Polynomial p2) {
            foreach (Monomial term in p2)
                p1 += term;
            return p1;
        }

        public static Polynomial operator -(Polynomial p1, Polynomial p2) {
            foreach (Monomial term in p2)
                p1 -= term;
            return p1;
        }

        public static Polynomial operator *(Polynomial p1, Polynomial p2) {
            Polynomial result = new Polynomial();
            foreach (Monomial term in p2)
                result += p1 * term;
            return result;
        }

        #endregion

        #region Bernstein Polynomials

        /// <summary> Returns the ith Bernstein basis polynomial of degree n. </summary>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown when i is not between 0 and n (inclusive). </exception>
        public static Polynomial Bernstein(int i, int n) {
            if (i < 0 || i > n) throw new ArgumentOutOfRangeException(nameof(i), $"Expected a value between 0 and {nameof(n)} (inclusive).");
            return new Monomial(n.Choose(i), i) * Expand(new Polynomial(1f, -1f), (uint) (n - i));
        }

        /// <summary> Returns an array of all Bernstein basis polynomials of degree n. </summary>
        /// <returns> Returns an array of length n+1 such that BernsteinBasis(n)[i] == Bernstein(i, n). </returns>
        public static Polynomial[] BernsteinBasis(int n) {
            Polynomial[] basis = new Polynomial[n + 1];
            for (int i = 0; i <= n; i++)
                basis[i] = Bernstein(i, n);
            return basis;
        }

        #endregion

        /// <summary> Raises a polynomial to a power. </summary>
        public static Polynomial Expand(Polynomial p, uint power) {
            //TODO: optimize
            Polynomial result = 1f;
            for (int i = 0; i < power; i++)
                result *= p;
            return result;
        }
    }
}