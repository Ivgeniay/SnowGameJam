using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <inheritdoc cref="CurveData{V}"/>
    [CreateAssetMenu(fileName = "Bezier3D", menuName = "UltimateMathLibrary/CurveData/Bezier3D", order = 1)]
    public class Bezier3DData : BezierData<Vector3> {

        public override Curve<Vector3> GetCurve() => degree switch {
            2 => new BezierQuad3D(controlPoints),
            3 => new BezierCubic3D(controlPoints),
            _ => new BezierGeneral3D(controlPoints, degree),
        };

    }

}