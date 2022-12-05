using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
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
                        GetPointBetweenTwoPoints(oldList[i], oldList[i + 1], t));
            }
            
            return newList;
        }

        private Vector3 GetPointBetweenTwoPoints(in Vector3 p1, in Vector3 p2, float t) {

            var result = Vector3.Lerp(p1, p2, t);
            //Debug.Log($"Point between {p1} фтв {p2} t = {t} => {result}");

            return result;
        }


    }
}
