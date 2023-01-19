using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    public class PolynomialCurve : Curve<Vector2>, I3Differentiable<Vector2> {

        /// <summary> The polynomial represented by this curve. </summary>
        public Polynomial polynomial;

        /// <summary> The interval that the curve is defined on. </summary>
        public Interval interval;

        public override float tMin => interval.a;
        public override float tMax => interval.b;

        /// <summary> Creates a new polynomial curve with a given polynomial on the interval [-10, 10]. </summary>
        /// <param name="polynomial"> <inheritdoc cref="polynomial" path="/summary"/> </param>
        public PolynomialCurve(Polynomial polynomial) : this(polynomial, Interval.Closed(-10f, 10f)) { }

        /// <summary> Creates a new polynomial curve with a given polynomial and interval. </summary>
        /// <param name="polynomial"> <inheritdoc cref="polynomial" path="/summary"/> </param>
        /// <param name="interval"> <inheritdoc cref="interval" path="/summary""/> </param>
        public PolynomialCurve(Polynomial polynomial, Interval interval) : base(R2.instance) {
            this.polynomial = polynomial;
            this.interval = interval;
        }

        public override Vector2 Evaluate(float t) {
            return new Vector2(t, polynomial.Evaluate(t));
        }

        public Vector2 GetDerivative(float t) {
            return new Vector2(1f, polynomial.GetDerivative().Evaluate(t));
        }

        public Vector2 GetSecondDerivative(float t) {
            return new Vector2(1f, polynomial.GetDerivative().GetDerivative().Evaluate(t));
        }

        public Vector2 GetThirdDerivative(float t) {
            return new Vector2(1f, polynomial.GetDerivative().GetDerivative().GetDerivative().Evaluate(t));
        }

        public override float GetLength(int _ = 100) => float.PositiveInfinity;

    }
}