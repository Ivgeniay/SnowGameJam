using System.Collections.Generic;
using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Represents an element with an associated weight. Create an enumerable collection of these to randomly pick an element. </summary>
    /// <typeparam name="T"> The type of element. </typeparam>
    [System.Serializable]
    public class WeightedElement<T> {
        public T value;
        public float weight;

        public WeightedElement(T value, float weight) {
            this.value = value;
            this.weight = weight;
        }
    }

    public static class WeightedElementCollectionExtensions {

        /// <summary> Returns the sum of all weights in this collection. </summary>
        public static float GetTotalWeight<T>(this IEnumerable<WeightedElement<T>> arr) {
            float totalWeight = 0f;
            foreach (WeightedElement<T> e in arr)
                totalWeight += e.weight;
            return totalWeight;
        }

        /// <summary> Randomly pick an element from this collection using the associated weight. </summary>
        public static T Choose<T>(this IEnumerable<WeightedElement<T>> arr) {
            float totalWeight = arr.GetTotalWeight();
            float rand = UMLRandom.Range(0, totalWeight);
            float cum = 0f;

            var enumerator = arr.GetEnumerator();
            while (enumerator.MoveNext()) {
                cum += enumerator.Current.weight;
                if (rand < cum)
                    return enumerator.Current.value;
            }
            return enumerator.Current.value;
        }
    }
}
