using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <inheritdoc cref="CurveData{V}"/>
    [CreateAssetMenu(fileName = "CatRom2D", menuName = "UltimateMathLibrary/CurveData/CatRom2D", order = 1)]
    public class CatRom2DData : CatRomData<Vector2> {

        public override Curve<Vector2> GetCurve() => new CatRom2D(controlPoints, tension, alpha);

    }

}