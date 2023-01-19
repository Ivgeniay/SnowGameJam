namespace Nickmiste.UltimateMathLibrary {

    public static class NumericExtensions {

        /// <summary> Returns x squared (x^2). </summary>
        public static float Squared(this float x) => x * x;
        /// <summary> Returns n squared (n^2). </summary>
        public static int Squared(this int n) => n * n;
        /// <summary> Returns z squared (z^2). </summary>
        public static Complex Squared(this Complex z) => z * z;

        /// <summary> Returns x cubed (x^3). </summary>
        public static float Cubed(this float x) => x * x * x;
        /// <summary> Returns n cubed (n^3). </summary>
        public static int Cubed(this int n) => n * n * n;
        /// <summary> Returns z cubed (z^3). </summary>
        public static Complex Cubed(this Complex z) => z * z * z;

        /// <summary> Computes nCk, the number of ways to choose k items from n total items. </summary>
        public static int Choose(this int n, int k) => UML.BinomialCoefficient(n, k);
    }

}