using System.Collections.Generic;
using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <inheritdoc cref="CurveData{V}"/>
    public abstract class BezierData<V> : CurveData<V> where V : struct {

        [Tooltip("The degree of the Bezier curve. Set this to 2 for a quadratic bezier curve or 3 for a cubic bezier curve.")]
        [SerializeField] public int degree = 3;

        [Tooltip("A list of control points used to define the curve.")]
        [SerializeField] public List<V> controlPoints;
    }

}