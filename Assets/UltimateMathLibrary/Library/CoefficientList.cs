using System.Collections.Generic;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> A data type used internally by <see cref="Polynomial"/> to ensure terms with trivial coefficients are not stored. </summary>
    internal class CoefficientList : SortedList<int, float> {

        internal CoefficientList() : base() { }
        internal CoefficientList(int capacity) : base(capacity) { }
        internal CoefficientList(IDictionary<int, float> dictionary) : base() {
            foreach (var kv in dictionary)
                Add(kv.Key, kv.Value);
        }

        internal new float this[int degree] {
            get => base[degree];
            set {
                if (UML.ApproximatelyZero(value))
                    Remove(degree);
                else base[degree] = value;
            }
        }

        internal new void Add(int degree, float coefficient) {
            if (UML.ApproximatelyZero(coefficient)) return;
            base.Add(degree, coefficient);
        }
    }
}