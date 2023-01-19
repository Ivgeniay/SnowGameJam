using System;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Exception that is thrown when illegally operating on vectors with different dimensions. </summary>
    public class DimensionMismatchException : Exception {

        public DimensionMismatchException() : base() { 

        }

        public DimensionMismatchException(string message) : base(message) {

        }

        public DimensionMismatchException(string message, Exception inner) : base(message, inner) {

        }

    }
}