using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <inheritdoc cref="CurveData{V}"/>
    [CreateAssetMenu(fileName = "CatRom3D", menuName = "UltimateMathLibrary/CurveData/CatRom3D", order = 1)]
    public class CatRom3DData : CatRomData<Vector3> {

        public override Curve<Vector3> GetCurve() => new CatRom3D(controlPoints, tension, alpha);

    }

}