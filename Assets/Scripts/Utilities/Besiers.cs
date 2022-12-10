using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utilities
{

    /*
        For 2 points: P = (1-t)P1 + tP2
        For 3 points: P = (1−t)2P1 + 2(1−t)tP2 + t2P3
        For 4 points: P = (1−t)3P1 + 3(1−t)2tP2 +3(1−t)t2P3 + t3P4
    */

    public class Besiers
    {
        public Vector3 GetPoint (in Vector3[] points, float t) {

            if (points.Count() <= 1) throw new ArgumentException(points.ToString());

            List<Vector3> verts = new List<Vector3>();
            verts.AddRange(points);

            while(verts.Count() != 1) {
                verts = GettingNewPositionList(verts, t);
            }

            return verts[0];
        }

        private List<Vector3> GettingNewPositionList(List<Vector3> oldList, float t) {
            List<Vector3> newList = new List<Vector3>();

            for (int i = 0; i < oldList.Count; i++) {
                if (i < oldList.Count() - 1) 
                    newList.Add(
                        GetPointBetweenTwoPointsByMath(oldList[i], oldList[i + 1], t));
            }
            
            return newList;
        }

        //public Vector3 GetPointFromFourPoint(in Vector3[] points, float t) {
        //    if (points.Count() != 4) throw new ArgumentException(points.ToString());
        //    return GetPointBetweenFourPointsByMath(points[0], points[1], points[2], points[3], t);
        //}
        //public Vector3 GetPointFromFourPoint(in Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4, float t) => GetPointFromFourPoint(new Vector3[4] { point1, point2, point3, point4 }, t);

        //public Vector3 GetPointFromThreePoint(in Vector3[] points, float t) {
        //    if (points.Count() != 3) throw new ArgumentException(points.ToString());
        //    return GetPointBetweenThreePointsByMath(points[0], points[1], points[2], t);
        //}
        //public Vector3 GetPointFromThreePoint(in Vector3 point1, Vector3 point2, Vector3 point3, float t) => GetPointFromThreePoint(new Vector3[3] { point1, point2, point3 }, t);

        //public Vector3 GetPointFromTwoPoint(in Vector3[] points, float t) {
        //    if (points.Count() != 2) throw new ArgumentException(points.ToString());
        //    return GetPointBetweenTwoPointsByMath(points[0], points[1], t);
        //}
        //public Vector3 GetPointFromTwoPoint(in Vector3 point1, Vector3 point2, float t) => GetPointFromTwoPoint(new Vector3[2] { point1, point2 }, t);


        //private Vector3 GetPointBetweenTwoPointsByLerp(in Vector3 p1, in Vector3 p2, float t) {
        //    var result = Vector3.Lerp(p1, p2, t);
        //    return result;
        //}

        private Vector3 GetPointBetweenTwoPointsByMath(in Vector3 p1, in Vector3 p2, float t)
        {
            t = Mathf.Clamp01(t);
            float OneMinusT = 1f - t;

            var result = OneMinusT * p1 + t * p2;
            return result;
        }

        //private Vector3 GetPointBetweenThreePointsByMath(in Vector3 p1, in Vector3 p2, in Vector3 p3, float t) {
        //    t = Mathf.Clamp01(t);
        //    float OneMinusT = 1f - t;

        //    var result = 
        //        OneMinusT * OneMinusT * p1 + 
        //        2f * OneMinusT * t * p2 + t * t * p3;
        //    return result;
        //}

        private Vector3 GetDerivativeBetweenTwoPointsByMath(in Vector3 p1, in Vector3 p2, float t) {
            t = Mathf.Clamp01(t);
            float OneMinusT = 1f - t;

            //var result = OneMinusT * p1 + t * p2;
            var result = p2-p1;
            return result;
        }
        //private Vector3 GetPointBetweenFourPointsByMath(in Vector3 p1, in Vector3 p2, in Vector3 p3, in Vector3 p4, float t) {
        //    t = Mathf.Clamp01(t);
        //    float OneMinusT = 1f - t;

        //    var result = 
        //        OneMinusT * OneMinusT * OneMinusT * p1 + 
        //        3f * OneMinusT * OneMinusT * t * p2 + 
        //        3f * OneMinusT * t * t * p3 + 
        //        t * t * t * p4;
        //    return result;
        //}

        private Vector3 GetDerivativeBetweenFourPointsByMath(in Vector3 p1, in Vector3 p2, in Vector3 p3, in Vector3 p4, float t) {
            t = Mathf.Clamp01(t);
            float OneMinusT = 1f - t;

            var result =
                3f * OneMinusT * OneMinusT * (p2 - p1) +
                6f * OneMinusT * t * (p3 - p2) +
                3f * t * t * (p4 - p3);
            return result;
        }


    }
}
