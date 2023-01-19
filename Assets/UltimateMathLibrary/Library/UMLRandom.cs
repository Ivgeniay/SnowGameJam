using UnityEngine;
using System.Text;
using System.Runtime.CompilerServices;
using UnityRandom = UnityEngine.Random;
using SystemRandom = System.Random;
using static Nickmiste.UltimateMathLibrary.UML;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> A utility class for random number generation. This class is a replacement for Unity's <see cref="UnityEngine.Random"/>. </summary>
    public static class UMLRandom {

        private const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

        #region State

        /// <inheritdoc cref="UnityRandom.InitState(int)"/>
        [MethodImpl(INLINE)] public static void InitState(int seed) => UnityRandom.InitState(seed);

        /// <inheritdoc cref="UnityRandom.state"/>
        public static UnityRandom.State state {
            get => UnityRandom.state;
            set => UnityRandom.state = value;
        }

        #endregion

        #region Numbers

        /// <inheritdoc cref="UnityRandom.value"/>
        public static float value => UnityRandom.value;

        /// <inheritdoc cref="UnityRandom.RandomRange(int, int)"/>
        [MethodImpl(INLINE)] public static int Range(int minInclusive, int maxExclusive) => UnityRandom.Range(minInclusive, maxExclusive);

        /// <inheritdoc cref="UnityRandom.RandomRange(float, float)"/>
        [MethodImpl(INLINE)] public static float Range(float minInclusive, float maxInclusive) => UnityRandom.Range(minInclusive, maxInclusive);

        /// <summary> Returns either -1 or 1. </summary>
        public static float sign => value < 0.5 ? 1f : -1f;

        /// <summary> Returns a random angle in the range [0, TAU). </summary>
        public static float angle => value * TAU;

        #endregion

        #region Vectors and Quaternions

        /// <inheritdoc cref="UnityRandom.insideUnitCircle"/>
        public static Vector2 insideUnitCircle => UnityRandom.insideUnitCircle;

        /// <inheritdoc cref="UnityRandom.insideUnitSphere"/>
        public static Vector3 insideUnitSphere => UnityRandom.insideUnitSphere;

        /// <inheritdoc cref="UnityRandom.onUnitSphere"/>
        public static Vector3 onUnitSphere => UnityRandom.onUnitSphere;

        /// <inheritdoc cref="UnityRandom.rotation"/>
        public static Quaternion rotation => UnityRandom.rotation;

        /// <inheritdoc cref="UnityRandom.rotationUniform"/>
        public static Quaternion rotationUniform => UnityRandom.rotationUniform;

        #endregion

        #region ColorHSV

        /// <inheritdoc cref="UnityRandom.ColorHSV"/>
        [MethodImpl(INLINE)] public static Color ColorHSV() =>
            UnityRandom.ColorHSV();

        /// <inheritdoc cref="UnityRandom.ColorHSV(float, float)"/>
        [MethodImpl(INLINE)] public static Color ColorHSV(float hueMin, float hueMax) =>
            UnityRandom.ColorHSV(hueMin, hueMax);

        /// <inheritdoc cref="UnityRandom.ColorHSV(float, float, float, float)"/>
        [MethodImpl(INLINE)] public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax) =>
            UnityRandom.ColorHSV(hueMin, hueMax, saturationMin, saturationMax);

        /// <inheritdoc cref="UnityRandom.ColorHSV(float, float, float, float, float, float)"/>
        [MethodImpl(INLINE)] public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax) =>
            UnityRandom.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);

        /// <inheritdoc cref="UnityRandom.ColorHSV(float, float, float, float, float, float, float, float)"/>
        [MethodImpl(INLINE)] public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax) => 
            UnityRandom.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, alphaMin, alphaMax);

        #endregion

        #region Text

        /// <summary> Returns a random lowercase letter from a-z. </summary>
        public static char letter => (char) Range(97, 123);

        /// <summary> Returns a random uppercase letter from A-Z. </summary>
        public static char letterCapital => (char) Range(65, 91);

        /// <summary> 
        ///     Returns a random gibberish word of the given length.
        ///     If no length is specified, returns a random gibberish word between 3 and 8 letters long.
        /// </summary>
        /// <param name="capitalized"> When true, the first letter of the word will be capitalized. </param>
        public static string Word(bool capitalized = false) => Word(Range(3, 9), capitalized);

        /// <inheritdoc cref="Word"/>
        public static string Word(int length, bool capitalized = false) {
            StringBuilder sb = new StringBuilder();
            sb.Append(capitalized ? letterCapital : letter);
            for (int i = 0; i < length - 1; i++)
                sb.Append(letter);
            return sb.ToString();
        }

        //TODO: Revisit and make generation nicer
        /// <summary> Returns gibberish text of roughly the given length, split into sentences. </summary>
        /// <param name="length"> The approximate length of the text. The actual length may vary slightly. </param>
        public static string Text(int length) {
            StringBuilder sb = new StringBuilder();
            int targetSentenceLength = Range(3, 9);
            int currentSentenceLength = 0;
            do {
                sb.Append(Word(currentSentenceLength == 0));
                if (++currentSentenceLength >= targetSentenceLength) {
                    targetSentenceLength = Range(3, 9);
                    currentSentenceLength = 0;
                    sb.Append(". ");
                }
                else
                    sb.Append(' ');
            } while (sb.Length < length);
            sb.Remove(sb.Length - 1, 1); //remove trailing space
            if (!sb.ToString(sb.Length - 1, 1).Equals('.'))
                sb.Append('.');
            return sb.ToString();
        }

        #endregion

        /// <summary> Randomly picks an element from the given array. </summary>
        public static T Pick<T>(params T[] items) => items[Range(0, items.Length)];
    }

}