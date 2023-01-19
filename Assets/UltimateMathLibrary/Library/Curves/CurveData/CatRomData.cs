using System.Collections.Generic;
using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <inheritdoc cref="CurveData{V}"/>
    public abstract class CatRomData<V> : CurveData<V> where V : struct {

        [Tooltip("The tension of the curve, typically between 0 and 1. This changes how sharply the curve bends.")]
        [SerializeField] [Range(0f, 1f)] public float tension = 0.5f;

        [Tooltip("The alpha value of the curve, typically between 0 and 1. Set to 0 for uniform, 0.5 for centripetal, or 1 for chordal.")]
        [SerializeField] [Range(0f, 1f)] public float alpha = 0f;

        [Tooltip("A list of control points used to define the curve.")]
        [SerializeField] public List<V> controlPoints;
    }

}