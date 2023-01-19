using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <inheritdoc cref="CurveData{V}"/>
    [CreateAssetMenu(fileName = "PolynomialCurve", menuName = "UltimateMathLibrary/CurveData/Polynomial", order = 2)]
    public class PolynomialCurveData : CurveData<Vector2> {

        [Header("Polynomial")]
        [Tooltip("The coefficients of the polynomial.")]
        [SerializeField] public float[] coefficients;

        [Header("Domain")]
        [Tooltip("The x-value of the lower bound of the interval that the polynomial curve is defined on.")]
        [SerializeField] public float lowerBound = -10f;
        [Tooltip("The x-value of the upper bound of the interval that the polynomial curve is defined on.")]
        [SerializeField] public float upperBound = 10f;

        public override Curve<Vector2> GetCurve() {
            Polynomial p = new Polynomial(coefficients);
            return new PolynomialCurve(p, Interval.Closed(lowerBound, upperBound));
        }
    }
}