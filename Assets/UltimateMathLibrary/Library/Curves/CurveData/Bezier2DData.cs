using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <inheritdoc cref="CurveData{V}"/>
    [CreateAssetMenu(fileName = "Bezier2D", menuName = "UltimateMathLibrary/CurveData/Bezier2D", order = 1)]
    public class Bezier2DData : BezierData<Vector2> {

        public override Curve<Vector2> GetCurve() => degree switch {
            2 => new BezierQuad2D(controlPoints),
            3 => new BezierCubic2D(controlPoints),
            _ => new BezierGeneral2D(controlPoints, degree),
        };

    }

}