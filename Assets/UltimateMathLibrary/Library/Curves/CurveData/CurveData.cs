using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> A scriptable object used to pass curve data in from the inspector. </summary>
    /// <typeparam name="V"> <inheritdoc cref="Curve{V}"/> </typeparam>
    public abstract class CurveData<V> : ScriptableObject where V : struct {
        /// <summary> Returns a new curve with this curve data. </summary>
        public abstract Curve<V> GetCurve();
    }

}