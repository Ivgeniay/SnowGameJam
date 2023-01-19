using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {
    public static class SwizzleExtensions {

        #region Vector2, Vector3

        // Vector2 -> Vector2
        public static Vector2 XX(this Vector2 v) => new Vector2(v.x, v.x);
        public static Vector2 XY(this Vector2 v) => new Vector2(v.x, v.y);
        public static Vector2 YX(this Vector2 v) => new Vector2(v.y, v.x);
        public static Vector2 YY(this Vector2 v) => new Vector2(v.y, v.y);

        // Vector2 -> Vector3
        public static Vector3 XXX(this Vector2 v) => new Vector3(v.x, v.x, v.x);
        public static Vector3 XXY(this Vector2 v) => new Vector3(v.x, v.x, v.y);
        public static Vector3 XYX(this Vector2 v) => new Vector3(v.x, v.y, v.x);
        public static Vector3 XYY(this Vector2 v) => new Vector3(v.x, v.y, v.y);
        public static Vector3 YXX(this Vector2 v) => new Vector3(v.y, v.x, v.x);
        public static Vector3 YXY(this Vector2 v) => new Vector3(v.y, v.x, v.y);
        public static Vector3 YYX(this Vector2 v) => new Vector3(v.y, v.y, v.x);
        public static Vector3 YYY(this Vector2 v) => new Vector3(v.y, v.y, v.y);

        // Vector3 -> Vector2
        public static Vector2 XX(this Vector3 v) => new Vector2(v.x, v.x);
        public static Vector2 XY(this Vector3 v) => new Vector2(v.x, v.y);
        public static Vector2 XZ(this Vector3 v) => new Vector2(v.x, v.z);
        public static Vector2 YX(this Vector3 v) => new Vector2(v.y, v.x);
        public static Vector2 YY(this Vector3 v) => new Vector2(v.y, v.y);
        public static Vector2 YZ(this Vector3 v) => new Vector2(v.y, v.z);
        public static Vector2 ZX(this Vector3 v) => new Vector2(v.z, v.x);
        public static Vector2 ZY(this Vector3 v) => new Vector2(v.z, v.y);
        public static Vector2 ZZ(this Vector3 v) => new Vector2(v.z, v.z);

        // Vector3 -> Vector3
        public static Vector3 XXX(this Vector3 v) => new Vector3(v.x, v.x, v.x);
        public static Vector3 XXY(this Vector3 v) => new Vector3(v.x, v.x, v.y);
        public static Vector3 XXZ(this Vector3 v) => new Vector3(v.x, v.x, v.z);
        public static Vector3 XYX(this Vector3 v) => new Vector3(v.x, v.y, v.x);
        public static Vector3 XYY(this Vector3 v) => new Vector3(v.x, v.y, v.y);
        public static Vector3 XYZ(this Vector3 v) => new Vector3(v.x, v.y, v.z);
        public static Vector3 XZX(this Vector3 v) => new Vector3(v.x, v.z, v.x);
        public static Vector3 XZY(this Vector3 v) => new Vector3(v.x, v.z, v.y);
        public static Vector3 XZZ(this Vector3 v) => new Vector3(v.x, v.z, v.z);
        public static Vector3 YXX(this Vector3 v) => new Vector3(v.y, v.x, v.x);
        public static Vector3 YXY(this Vector3 v) => new Vector3(v.y, v.x, v.y);
        public static Vector3 YXZ(this Vector3 v) => new Vector3(v.y, v.x, v.z);
        public static Vector3 YYX(this Vector3 v) => new Vector3(v.y, v.y, v.x);
        public static Vector3 YYY(this Vector3 v) => new Vector3(v.y, v.y, v.y);
        public static Vector3 YYZ(this Vector3 v) => new Vector3(v.y, v.y, v.z);
        public static Vector3 YZX(this Vector3 v) => new Vector3(v.y, v.z, v.x);
        public static Vector3 YZY(this Vector3 v) => new Vector3(v.y, v.z, v.y);
        public static Vector3 YZZ(this Vector3 v) => new Vector3(v.y, v.z, v.z);
        public static Vector3 ZXX(this Vector3 v) => new Vector3(v.z, v.x, v.x);
        public static Vector3 ZXY(this Vector3 v) => new Vector3(v.z, v.x, v.y);
        public static Vector3 ZXZ(this Vector3 v) => new Vector3(v.z, v.x, v.z);
        public static Vector3 ZYX(this Vector3 v) => new Vector3(v.z, v.y, v.x);
        public static Vector3 ZYY(this Vector3 v) => new Vector3(v.z, v.y, v.y);
        public static Vector3 ZYZ(this Vector3 v) => new Vector3(v.z, v.y, v.z);
        public static Vector3 ZZX(this Vector3 v) => new Vector3(v.z, v.z, v.x);
        public static Vector3 ZZY(this Vector3 v) => new Vector3(v.z, v.z, v.y);
        public static Vector3 ZZZ(this Vector3 v) => new Vector3(v.z, v.z, v.z);

        #endregion

        #region Vector2Int, Vector3Int

        // Vector2 -> Vector2
        public static Vector2Int XX(this Vector2Int v) => new Vector2Int(v.x, v.x);
        public static Vector2Int XY(this Vector2Int v) => new Vector2Int(v.x, v.y);
        public static Vector2Int YX(this Vector2Int v) => new Vector2Int(v.y, v.x);
        public static Vector2Int YY(this Vector2Int v) => new Vector2Int(v.y, v.y);

        // Vector2 -> Vector3
        public static Vector3Int XXX(this Vector2Int v) => new Vector3Int(v.x, v.x, v.x);
        public static Vector3Int XXY(this Vector2Int v) => new Vector3Int(v.x, v.x, v.y);
        public static Vector3Int XYX(this Vector2Int v) => new Vector3Int(v.x, v.y, v.x);
        public static Vector3Int XYY(this Vector2Int v) => new Vector3Int(v.x, v.y, v.y);
        public static Vector3Int YXX(this Vector2Int v) => new Vector3Int(v.y, v.x, v.x);
        public static Vector3Int YXY(this Vector2Int v) => new Vector3Int(v.y, v.x, v.y);
        public static Vector3Int YYX(this Vector2Int v) => new Vector3Int(v.y, v.y, v.x);
        public static Vector3Int YYY(this Vector2Int v) => new Vector3Int(v.y, v.y, v.y);

        // Vector3 -> Vector2
        public static Vector2Int XX(this Vector3Int v) => new Vector2Int(v.x, v.x);
        public static Vector2Int XY(this Vector3Int v) => new Vector2Int(v.x, v.y);
        public static Vector2Int XZ(this Vector3Int v) => new Vector2Int(v.x, v.z);
        public static Vector2Int YX(this Vector3Int v) => new Vector2Int(v.y, v.x);
        public static Vector2Int YY(this Vector3Int v) => new Vector2Int(v.y, v.y);
        public static Vector2Int YZ(this Vector3Int v) => new Vector2Int(v.y, v.z);
        public static Vector2Int ZX(this Vector3Int v) => new Vector2Int(v.z, v.x);
        public static Vector2Int ZY(this Vector3Int v) => new Vector2Int(v.z, v.y);
        public static Vector2Int ZZ(this Vector3Int v) => new Vector2Int(v.z, v.z);

        // Vector3 -> Vector3
        public static Vector3Int XXX(this Vector3Int v) => new Vector3Int(v.x, v.x, v.x);
        public static Vector3Int XXY(this Vector3Int v) => new Vector3Int(v.x, v.x, v.y);
        public static Vector3Int XXZ(this Vector3Int v) => new Vector3Int(v.x, v.x, v.z);
        public static Vector3Int XYX(this Vector3Int v) => new Vector3Int(v.x, v.y, v.x);
        public static Vector3Int XYY(this Vector3Int v) => new Vector3Int(v.x, v.y, v.y);
        public static Vector3Int XYZ(this Vector3Int v) => new Vector3Int(v.x, v.y, v.z);
        public static Vector3Int XZX(this Vector3Int v) => new Vector3Int(v.x, v.z, v.x);
        public static Vector3Int XZY(this Vector3Int v) => new Vector3Int(v.x, v.z, v.y);
        public static Vector3Int XZZ(this Vector3Int v) => new Vector3Int(v.x, v.z, v.z);
        public static Vector3Int YXX(this Vector3Int v) => new Vector3Int(v.y, v.x, v.x);
        public static Vector3Int YXY(this Vector3Int v) => new Vector3Int(v.y, v.x, v.y);
        public static Vector3Int YXZ(this Vector3Int v) => new Vector3Int(v.y, v.x, v.z);
        public static Vector3Int YYX(this Vector3Int v) => new Vector3Int(v.y, v.y, v.x);
        public static Vector3Int YYY(this Vector3Int v) => new Vector3Int(v.y, v.y, v.y);
        public static Vector3Int YYZ(this Vector3Int v) => new Vector3Int(v.y, v.y, v.z);
        public static Vector3Int YZX(this Vector3Int v) => new Vector3Int(v.y, v.z, v.x);
        public static Vector3Int YZY(this Vector3Int v) => new Vector3Int(v.y, v.z, v.y);
        public static Vector3Int YZZ(this Vector3Int v) => new Vector3Int(v.y, v.z, v.z);
        public static Vector3Int ZXX(this Vector3Int v) => new Vector3Int(v.z, v.x, v.x);
        public static Vector3Int ZXY(this Vector3Int v) => new Vector3Int(v.z, v.x, v.y);
        public static Vector3Int ZXZ(this Vector3Int v) => new Vector3Int(v.z, v.x, v.z);
        public static Vector3Int ZYX(this Vector3Int v) => new Vector3Int(v.z, v.y, v.x);
        public static Vector3Int ZYY(this Vector3Int v) => new Vector3Int(v.z, v.y, v.y);
        public static Vector3Int ZYZ(this Vector3Int v) => new Vector3Int(v.z, v.y, v.z);
        public static Vector3Int ZZX(this Vector3Int v) => new Vector3Int(v.z, v.z, v.x);
        public static Vector3Int ZZY(this Vector3Int v) => new Vector3Int(v.z, v.z, v.y);
        public static Vector3Int ZZZ(this Vector3Int v) => new Vector3Int(v.z, v.z, v.z);

        #endregion

    }
}