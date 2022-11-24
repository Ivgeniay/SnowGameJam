using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class Extensions
    {
        public static IEnumerable<Transform> GetTransformsWithComponents<T>(this Transform transform)
        {
            List<Transform> list = new List<Transform>();
            foreach (Transform child in transform) {

                if (child.childCount > 0) {
                    foreach (var el in GetTransformsWithComponents<T>(child).ToList())
                        list.Add(el);
                }

                if (child.GetComponent<T>() is not null)
                    list.Add(child);
            }
            return list.ToArray();

        }

    }
}
