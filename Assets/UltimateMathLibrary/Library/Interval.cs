using System;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> An interval between two values. </summary>
    [Serializable]
    public struct Interval {

        /// <summary> Determines whether the bound of an interval is open (exclusive) or closed (inclusive). </summary>
        public enum BoundType { Open, Closed }

        private float _a;
        /// <summary> The value of the lower bound of the interval. </summary>
        public float a {
            get => _a;
            set {
                if (a >= b) throw NewBoundException();
                _a = value;
            }
        }

        private float _b;
        /// <summary> The value of the upper bound of the interval. </summary>
        public float b {
            get => _b;
            set {
                if (a >= b) throw NewBoundException();
                _b = value;
            }
        }

        public BoundType lowerBoundType;
        public BoundType upperBoundType;

        private Interval(float a, float b, BoundType lowerBoundType, BoundType upperBoundType) {
            _a = a;
            _b = b;
            this.lowerBoundType = lowerBoundType;
            this.upperBoundType = upperBoundType;
            if (a >= b) 
                throw NewBoundException();
        }

        /// <summary> Returns a new open interval (a, b). </summary>
        public static Interval Open(float a, float b) => new Interval(a, b, BoundType.Open, BoundType.Open);

        /// <summary> Returns a new closed interval [a, b]. </summary>
        public static Interval Closed(float a, float b) => new Interval(a, b, BoundType.Closed, BoundType.Closed);

        /// <summary> Returns a new half open interval (a, b]. </summary>
        public static Interval HalfOpenLeft(float a, float b) => new Interval(a, b, BoundType.Open, BoundType.Closed);

        /// <summary> Returns a new half open interval [a, b). </summary>
        public static Interval HalfOpenRight(float a, float b) => new Interval(a, b, BoundType.Closed, BoundType.Open);

        private InvalidOperationException NewBoundException() => new InvalidOperationException("The lower bound must be strictly less than the upper bound.");

        /// <summary> Returns true if x is contained within the interval; false otherwise </summary>
        public bool Contains(float x) =>
            (lowerBoundType == BoundType.Open ? x > a : x >= a) &&
            (upperBoundType == BoundType.Open ? x < b : x <= b);

        /// <summary> Uniformly picks a random value within the interval </summary>
        public float RandomSample() {
            float min = lowerBoundType == BoundType.Open ? a : a + UML.Epsilon;
            float max = upperBoundType == BoundType.Open ? b : b - UML.Epsilon;
            return UMLRandom.Range(min, max);
        }

        /// <summary> Linearly interpolates between the lower and upper bounds of this interval by t. </summary>
        /// <param name="t"> A t-value between 0 and 1. </param>
        public float Lerp(float t) => UML.Lerp(a, b, t);
    }
}