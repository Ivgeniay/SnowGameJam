using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> A collection of common math functions from the Ultimate Math Library. This class is a replacement for Unity's <see cref="Mathf"/>. </summary>
    public static class UML {

        private const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

        #region Constants

        /// <inheritdoc cref="Mathf.PI"/>
        public const float PI = Mathf.PI;

        /// <summary> The ratio of a circle's circumference to its radius (equal to 2pi). </summary>
        public const float TAU = 6.28318530718f;

        /// <summary> Euler's number. </summary>
        public const float E = 2.71828182846f;

        /// <summary> The square root of 2. </summary>
        public const float SQRT2 = 1.41421356237f;

        /// <summary> The golden ratio (equal to (1 + Sqrt(5)) / 2). </summary>
        public const float PHI = 1.61803398875f;

        /// <inheritdoc cref="Mathf.Deg2Rad"/>
        public const float Deg2Rad = Mathf.Deg2Rad;

        /// <inheritdoc cref="Mathf.Rad2Deg"/>
        public const float Rad2Deg = Mathf.Rad2Deg;

        /// <inheritdoc cref="Mathf.Infinity"/>
        public const float Infinity = Mathf.Infinity;

        /// <inheritdoc cref="Mathf.NegativeInfinity"/>
        public const float NegativeInfinity = Mathf.NegativeInfinity;

        /// <inheritdoc cref="Mathf.Epsilon"/>
        public static readonly float Epsilon = Mathf.Epsilon;

        #endregion

        #region Mathf Base

        /// <inheritdoc cref="Mathf.CorrelatedColorTemperatureToRGB(float)"/>
        [MethodImpl(INLINE)] public static Color CorrelatedColorTemperatureToRGB(float kelvin) => Mathf.CorrelatedColorTemperatureToRGB(kelvin);

        /// <inheritdoc cref="Mathf.DeltaAngle(float, float)"/>
        [MethodImpl(INLINE)] public static float DeltaAngle(float current, float target) => Mathf.DeltaAngle(current, target);

        /// <inheritdoc cref="Mathf.FloatToHalf(float)"/>
        [MethodImpl(INLINE)] public static ushort FloatToHalf(float val) => Mathf.FloatToHalf(val);

        /// <inheritdoc cref="Mathf.Gamma(float, float, float)"/>
        [MethodImpl(INLINE)] public static float Gamma(float value, float absmax, float gamma) => Mathf.Gamma(value, absmax, gamma);

        /// <inheritdoc cref="Mathf.GammaToLinearSpace(float)"/>
        [MethodImpl(INLINE)] public static float GammaToLinearSpace(float value) => Mathf.GammaToLinearSpace(value);

        /// <inheritdoc cref="Mathf.HalfToFloat(ushort)"/>
        [MethodImpl(INLINE)] public static float HalfToFloat(ushort val) => Mathf.HalfToFloat(val);

        /// <inheritdoc cref="Mathf.InverseLerp(float, float, float)"/>
        [MethodImpl(INLINE)] public static float InverseLerp(float a, float b, float value) => Mathf.InverseLerp(a, b, value);

        /// <inheritdoc cref="Mathf.Lerp(float, float, float)"/>
        [MethodImpl(INLINE)] public static float Lerp(float a, float b, float t) => Mathf.Lerp(a, b, t);

        /// <inheritdoc cref="Mathf.LerpAngle(float, float, float)"/>
        [MethodImpl(INLINE)] public static float LerpAngle(float a, float b, float t) => Mathf.LerpAngle(a, b, t);

        /// <inheritdoc cref="Mathf.LerpUnclamped(float, float, float)"/>
        [MethodImpl(INLINE)] public static float LerpUnclamped(float a, float b, float t) => Mathf.LerpUnclamped(a, b, t);

        /// <inheritdoc cref="Mathf.LinearToGammaSpace(float)"/>
        [MethodImpl(INLINE)] public static float LinearToGammaSpace(float value) => Mathf.LinearToGammaSpace(value);

        /// <inheritdoc cref="Mathf.MoveTowards(float, float, float)"/>
        [MethodImpl(INLINE)] public static float MoveTowards(float current, float target, float maxDelta) => Mathf.MoveTowards(current, target, maxDelta);

        /// <inheritdoc cref="Mathf.MoveTowardsAngle(float, float, float)"/>
        [MethodImpl(INLINE)] public static float MoveTowardsAngle(float current, float target, float maxDelta) => Mathf.MoveTowardsAngle(current, target, maxDelta);

        /// <inheritdoc cref="Mathf.PerlinNoise(float, float)"/>
        [MethodImpl(INLINE)] public static float PerlinNoise(float x, float y) => Mathf.PerlinNoise(x, y);

        /// <inheritdoc cref="Mathf.PingPong(float, float)"/>
        [MethodImpl(INLINE)] public static float PingPong(float t, float length) => Mathf.PingPong(t, length);

        /// <inheritdoc cref="Mathf.Repeat(float, float)"/>
        [MethodImpl(INLINE)] public static float Repeat(float t, float length) => Mathf.Repeat(t, length);

        /// <inheritdoc cref="Mathf.Sign(float)"/>
        [MethodImpl(INLINE)] public static float Sign(float f) => Mathf.Sign(f);

        /// <inheritdoc cref="Mathf.SmoothDamp(float, float, ref float, float)"/>
        [MethodImpl(INLINE)]
        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime) =>
            Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime);

        /// <inheritdoc cref="Mathf.SmoothDamp(float, float, ref float, float, float)"/>
        [MethodImpl(INLINE)]
        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed) =>
            Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed);

        /// <inheritdoc cref="Mathf.SmoothDamp(float, float, ref float, float, float, float)"/>
        [MethodImpl(INLINE)]
        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime) =>
            Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);

        /// <inheritdoc cref="Mathf.SmoothDampAngle(float, float, ref float, float)"/>
        [MethodImpl(INLINE)]
        public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime) =>
            Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime);

        /// <inheritdoc cref="Mathf.SmoothDampAngle(float, float, ref float, float, float)"/>
        [MethodImpl(INLINE)]
        public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed) =>
            Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed);

        /// <inheritdoc cref="Mathf.SmoothDampAngle(float, float, ref float, float, float, float)"/>
        [MethodImpl(INLINE)]
        public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime) =>
            Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);

        /// <inheritdoc cref="Mathf.SmoothStep(float, float, float)"/>
        [MethodImpl(INLINE)] public static float SmoothStep(float from, float to, float t) => Mathf.SmoothStep(from, to, t);

        #endregion

        #region Trig

        //Basic
        /// <inheritdoc cref="Mathf.Sin(float)"/>
        [MethodImpl(INLINE)] public static float Sin(float f) => Mathf.Sin(f);

        /// <inheritdoc cref="Mathf.Cos(float)"/>
        [MethodImpl(INLINE)] public static float Cos(float f) => Mathf.Cos(f);

        /// <inheritdoc cref="Mathf.Tan(float)"/>
        [MethodImpl(INLINE)] public static float Tan(float f) => Mathf.Tan(f);

        /// <summary> Returns the secant of angle f. </summary>
        /// <param name="f"> The input angle, in radians. </param>
        [MethodImpl(INLINE)] public static float Sec(float f) => 1f / Cos(f);

        /// <summary> Returns the cosecant of angle f. </summary>
        /// <param name="f"> The input angle, in radians. </param>
        [MethodImpl(INLINE)] public static float Csc(float f) => 1f / Sin(f);

        /// <summary> Returns the cotangent of angle f. </summary>
        /// <param name="f"> The input angle, in radians. </param>
        [MethodImpl(INLINE)] public static float Cot(float f) => 1f / Tan(f);

        //Inverse
        /// <inheritdoc cref="Mathf.Asin(float)"/>
        [MethodImpl(INLINE)] public static float Asin(float f) => Mathf.Asin(f);

        /// <inheritdoc cref="Mathf.Acos(float)"/>
        [MethodImpl(INLINE)] public static float Acos(float f) => Mathf.Acos(f);

        /// <inheritdoc cref="Mathf.Atan(float)"/>
        [MethodImpl(INLINE)] public static float Atan(float f) => Mathf.Atan(f);

        /// <inheritdoc cref="Mathf.Atan2(float, float)"/>
        [MethodImpl(INLINE)] public static float Atan2(float y, float x) => Mathf.Atan2(y, x);

        //Hyperbolic
        /// <summary> Returns the hyperbolic sine of angle f. </summary>
        [MethodImpl(INLINE)] public static float Sinh(float f) => (float) Math.Sinh(f);

        /// <summary> Returns the hyperbolic cosine of angle f. </summary>
        [MethodImpl(INLINE)] public static float Cosh(float f) => (float) Math.Cosh(f);

        /// <summary> Returns the hyperbolic tangent of angle f. </summary>
        [MethodImpl(INLINE)] public static float Tanh(float f) => (float) Math.Tanh(f);

        /// <summary> Returns the hyperbolic secant of angle f. </summary>
        [MethodImpl(INLINE)] public static float Sech(float f) => 1f / Cosh(f);

        /// <summary> Returns the hyperbolic cosecant of angle f. </summary>
        [MethodImpl(INLINE)] public static float Csch(float f) => 1f / Sinh(f);

        /// <summary> Returns the hyperbolic cotangent of angle f. </summary>
        [MethodImpl(INLINE)] public static float Coth(float f) => 1f / Tanh(f);

        /// <summary> Shorthand for Cos(theta) + i*Sin(theta). Equivalent to e^(i*theta). </summary>
        [MethodImpl(INLINE)] public static Complex Cis(float theta) => new Complex(Cos(theta), Sin(theta));

        #endregion

        #region Exponents

        #region Powers
        /// <inheritdoc cref="Mathf.Exp(float)"/>
        [MethodImpl(INLINE)] public static float Exp(float power) => Mathf.Exp(power);

        /// <inheritdoc cref="Mathf.Pow(float, float)"/>
        [MethodImpl(INLINE)] public static float Pow(float f, float p) => Mathf.Pow(f, p);

        /// <summary> Returns z raised to the real power p. </summary>
        public static Complex ComplexPow(Complex z, float p) => Complex.FromPolar(Pow(z.sqrModulus, p/2f), z.argument * p);

        /// <summary> Returns x squared (x^2). </summary>
        public static float Square(float x) => x * x;
        /// <summary> Returns n squared (n^2). </summary>
        public static int Square(int n) => n * n;
        /// <summary> Returns z squared (z^2). </summary>
        public static Complex Square(Complex z) => z * z;

        /// <summary> Returns x cubed (x^3). </summary>
        public static float Cube(float x) => x * x * x;
        /// <summary> Returns n cubed (n^3). </summary>
        public static int Cube(int n) => n * n * n;
        /// <summary> Returns z cubed. </summary>
        public static Complex Cube(Complex z) => z * z * z;
        #endregion

        #region Powers of 2
        /// <inheritdoc cref="Mathf.IsPowerOfTwo(int)"/>
        [MethodImpl(INLINE)] public static bool IsPowerOfTwo(int value) => Mathf.IsPowerOfTwo(value);

        /// <inheritdoc cref="Mathf.NextPowerOfTwo(int)"/>
        [MethodImpl(INLINE)] public static int NextPowerOfTwo(int value) => Mathf.NextPowerOfTwo(value);

        /// <inheritdoc cref="Mathf.ClosestPowerOfTwo(int)"/>
        [MethodImpl(INLINE)] public static int ClosestPowerOfTwo(int value) => Mathf.ClosestPowerOfTwo(value);
        #endregion

        #region Logs
        /// <inheritdoc cref="Mathf.Log(float)"/>
        [MethodImpl(INLINE)] public static float Log(float f) => Mathf.Log(f);

        /// <inheritdoc cref="Mathf.Log(float, float)"/>
        [MethodImpl(INLINE)] public static float Log(float f, float p) => Mathf.Log(f, p);

        /// <inheritdoc cref="Mathf.Log10(float)"/>
        [MethodImpl(INLINE)] public static float Log10(float f) => Mathf.Log10(f);

        /// <summary> Returns the base 2 logarithm of a specified number. </summary>
        [MethodImpl(INLINE)] public static float Log2(float f) => Mathf.Log(f, 2f);
        #endregion

        #region Roots
        /// <summary> Returns the square root of f. </summary>
        [MethodImpl(INLINE)] public static float Sqrt(float f) => Mathf.Sqrt(f);

        /// <summary> Returns the cube root of f. Correctly handles negative values. </summary>
        public static float Cbrt(float f) => f < 0 ? -Pow(-f, 1f / 3f) : Pow(f, 1f / 3f);

        /// <summary> Returns the square root of f, where f may be a negative number. </summary>
        public static Complex ComplexSqrt(float f) => f >= 0f ? Sqrt(f) : new Complex(0f, Sqrt(-f));

        /// <summary> Returns the principal square root of a complex number. </summary>
        public static Complex ComplexSqrt(Complex z) => Complex.FromPolar(Pow(z.sqrModulus, 0.25f), z.argument / 2f);

        /// <summary> Returns the principal cube root of a complex numer. </summary>
        public static Complex ComplexCbrt(Complex z) => Complex.FromPolar(Pow(z.sqrModulus, 1f / 6f), z.argument / 3f);
        #endregion

        #endregion

        #region Rounding

        /// <inheritdoc cref="Mathf.Floor(float)"/>
        [MethodImpl(INLINE)] public static float Floor(float f) => Mathf.Floor(f);

        /// <inheritdoc cref="Mathf.FloorToInt(float)"/>
        [MethodImpl(INLINE)] public static int FloorToInt(float f) => Mathf.FloorToInt(f);

        /// <inheritdoc cref="Mathf.Ceil(float)"/>
        [MethodImpl(INLINE)] public static float Ceil(float f) => Mathf.Ceil(f);

        /// <inheritdoc cref="Mathf.CeilToInt(float)"/>
        [MethodImpl(INLINE)] public static int CeilToInt(float f) => Mathf.CeilToInt(f);

        /// <inheritdoc cref="Mathf.Round(float)"/>
        [MethodImpl(INLINE)] public static float Round(float f) => Mathf.Round(f);

        /// <inheritdoc cref="Mathf.RoundToInt(float)"/>
        [MethodImpl(INLINE)] public static int RoundToInt(float f) => Mathf.RoundToInt(f);

        /// <summary> Applies Floor to all components of the vector. See <see cref="Ceil(float)"/> </summary>
        [MethodImpl(INLINE)] public static Vector2 Floor(Vector2 v) => VectorMapping.Map(v, x => Floor(x));
        /** <inheritdoc cref="Floor(Vector2)"/> */
        [MethodImpl(INLINE)] public static Vector3 Floor(Vector3 v) => VectorMapping.Map(v, x => Floor(x));
        /** <inheritdoc cref="Floor(Vector2)"/> */
        [MethodImpl(INLINE)] public static Vector4 Floor(Vector4 v) => VectorMapping.Map(v, x => Floor(x));
        /** <inheritdoc cref="Floor(Vector2)"/> */
        [MethodImpl(INLINE)] public static VectorN Floor(VectorN v) => VectorMapping.Map(v, x => Floor(x));

        /// <summary> Applies Ceil to all components of the vector. See <see cref="Ceil(float)"/> </summary>
        [MethodImpl(INLINE)] public static Vector2 Ceil(Vector2 v) => VectorMapping.Map(v, x => Ceil(x));
        /** <inheritdoc cref="Ceil(Vector2)"/> */
        [MethodImpl(INLINE)] public static Vector3 Ceil(Vector3 v) => VectorMapping.Map(v, x => Ceil(x));
        /** <inheritdoc cref="Ceil(Vector2)"/> */
        [MethodImpl(INLINE)] public static Vector4 Ceil(Vector4 v) => VectorMapping.Map(v, x => Ceil(x));
        /** <inheritdoc cref="Ceil(Vector2)"/> */
        [MethodImpl(INLINE)] public static VectorN Ceil(VectorN v) => VectorMapping.Map(v, x => Ceil(x));

        /// <summary> Applies Round to all components of the vector. See <see cref="Ceil(float)"/> </summary>
        [MethodImpl(INLINE)] public static Vector2 Round(Vector2 v) => VectorMapping.Map(v, x => Round(x));
        /** <inheritdoc cref="Round(Vector2)"/> */
        [MethodImpl(INLINE)] public static Vector3 Round(Vector3 v) => VectorMapping.Map(v, x => Round(x));
        /** <inheritdoc cref="Round(Vector2)"/> */
        [MethodImpl(INLINE)] public static Vector4 Round(Vector4 v) => VectorMapping.Map(v, x => Round(x));
        /** <inheritdoc cref="Round(Vector2)"/> */
        [MethodImpl(INLINE)] public static VectorN Round(VectorN v) => VectorMapping.Map(v, x => Round(x));

        /// <summary> Returns the fractional component of a given value (e.g., Frac(2.57) = 0.57). </summary>
        [MethodImpl(INLINE)] public static float Frac(float f) => f - Floor(f);

        /// <summary> Applies Frac to all components of the vector. See <see cref="Frac(float)"/>. </summary>
        [MethodImpl(INLINE)] public static Vector2 Frac(Vector2 v) => v - Floor(v);
        /** <inheritdoc cref="Frac(Vector2)"/> */
        [MethodImpl(INLINE)] public static Vector3 Frac(Vector3 v) => v - Floor(v);
        /** <inheritdoc cref="Frac(Vector2)"/> */
        [MethodImpl(INLINE)] public static Vector4 Frac(Vector4 v) => v - Floor(v);
        /** <inheritdoc cref="Frac(Vector2)"/> */
        [MethodImpl(INLINE)] public static VectorN Frac(VectorN v) => v - Floor(v);

        #endregion

        #region Clamping

        /// <inheritdoc cref="Mathf.Clamp(float, float, float)"/>
        [MethodImpl(INLINE)] public static float Clamp(float value, float min, float max) => Mathf.Clamp(value, min, max);

        /// <inheritdoc cref="Mathf.Clamp(int, int, int)"/>
        [MethodImpl(INLINE)] public static int Clamp(int value, int min, int max) => Mathf.Clamp(value, min, max);

        /** <summary> Clamps value between 0 and 1. </summary> */
        [MethodImpl(INLINE)] public static float Clamp01(float value) => Mathf.Clamp01(value);

        /// <summary> Clamps value between -1 and 1. </summary>
        [MethodImpl(INLINE)] public static float ClampNeg1To1(float value) => Clamp(value, -1f, 1f);

        /// <summary> Clamps each component of the vector. See <see cref="Clamp(float, float, float)"/> </summary>
        [MethodImpl(INLINE)] public static Vector2 Clamp(Vector2 v, float min, float max) => VectorMapping.Map(v, x => Clamp(x, min, max));
        /** <inheritdoc cref="Clamp(Vector2, float, float)"/> */
        [MethodImpl(INLINE)] public static Vector3 Clamp(Vector3 v, float min, float max) => VectorMapping.Map(v, x => Clamp(x, min, max));
        /** <inheritdoc cref="Clamp(Vector2, float, float)"/> */
        [MethodImpl(INLINE)] public static Vector4 Clamp(Vector4 v, float min, float max) => VectorMapping.Map(v, x => Clamp(x, min, max));
        /** <inheritdoc cref="Clamp(Vector2, float, float)"/> */
        [MethodImpl(INLINE)] public static VectorN Clamp(VectorN v, float min, float max) => VectorMapping.Map(v, x => Clamp(x, min, max));

        /// <summary> Clamps each component of the vector. See <see cref="Clamp(int, int, int)"/> </summary>
        [MethodImpl(INLINE)] public static Vector2Int Clamp(Vector2Int v, int min, int max) => VectorMapping.Map(v, x => Clamp(x, min, max));
        /** <inheritdoc cref="Clamp(Vector2Int, int, int)"/> */
        [MethodImpl(INLINE)] public static Vector3Int Clamp(Vector3Int v, int min, int max) => VectorMapping.Map(v, x => Clamp(x, min, max));
        /** <inheritdoc cref="Clamp(Vector2Int, int, int)"/> */
        [MethodImpl(INLINE)] public static VectorNInt Clamp(VectorNInt v, int min, int max) => VectorMapping.Map(v, x => Clamp(x, min, max));

        /// <summary> Clamps each component of the vector between 0 and 1. See <see cref="Clamp01(float)"/> </summary>
        [MethodImpl(INLINE)] public static Vector2 Clamp01(Vector2 v) => VectorMapping.Map(v, x => Clamp01(x));
        /** <inheritdoc cref="Clamp01(Vector2)"/> */
        [MethodImpl(INLINE)] public static Vector3 Clamp01(Vector3 v) => VectorMapping.Map(v, x => Clamp01(x));
        /** <inheritdoc cref="Clamp01(Vector2)"/> */
        [MethodImpl(INLINE)] public static Vector4 Clamp01(Vector4 v) => VectorMapping.Map(v, x => Clamp01(x));
        /** <inheritdoc cref="Clamp01(Vector2)"/> */
        [MethodImpl(INLINE)] public static VectorN Clamp01(VectorN v) => VectorMapping.Map(v, x => Clamp01(x));

        /// <summary> Clamps each component of the vector between -1 and 1. See <see cref="ClampNeg1To1(float)"/> </summary>
        [MethodImpl(INLINE)] public static Vector2 ClampNeg1To1(Vector2 v) => VectorMapping.Map(v, x => ClampNeg1To1(x));
        /** <inheritdoc cref="ClampNeg1To1(Vector2)"/> */
        [MethodImpl(INLINE)] public static Vector3 ClampNeg1To1(Vector3 v) => VectorMapping.Map(v, x => ClampNeg1To1(x));
        /** <inheritdoc cref="ClampNeg1To1(Vector2)"/> */
        [MethodImpl(INLINE)] public static Vector4 ClampNeg1To1(Vector4 v) => VectorMapping.Map(v, x => ClampNeg1To1(x));
        /** <inheritdoc cref="ClampNeg1To1(Vector2)"/> */
        [MethodImpl(INLINE)] public static VectorN ClampNeg1To1(VectorN v) => VectorMapping.Map(v, x => ClampNeg1To1(x));

        /// <summary> Clamps the magnitude of the vector between the given minimum and maximum values. </summary>
        public static Vector2 ClampMagnitude(Vector2 v, float min, float max) => v.sqrMagnitude < Square(min) ? v.normalized * min : v.sqrMagnitude > Square(max) ? v.normalized * max : v;
        /// <inheritdoc cref="ClampMagnitude(Vector2, float, float)"/>
        public static Vector3 ClampMagnitude(Vector3 v, float min, float max) => v.sqrMagnitude < Square(min) ? v.normalized * min : v.sqrMagnitude > Square(max) ? v.normalized * max : v;
        /// <inheritdoc cref="ClampMagnitude(Vector2, float, float)"/>
        public static Vector4 ClampMagnitude(Vector4 v, float min, float max) => v.sqrMagnitude < Square(min) ? v.normalized * min : v.sqrMagnitude > Square(max) ? v.normalized * max : v;
        /// <inheritdoc cref="ClampMagnitude(Vector2, float, float)"/>
        public static VectorN ClampMagnitude(VectorN v, float min, float max) => v.sqrMagnitude < Square(min) ? v.normalized * min : v.sqrMagnitude > Square(max) ? v.normalized * max : v;

        #endregion

        #region Min/Max

        /// <inheritdoc cref="Mathf.Max(float, float)"/>
        [MethodImpl(INLINE)] public static float Max(float a, float b) => Mathf.Max(a, b);

        /// <inheritdoc cref="Mathf.Max(float[])"/>
        [MethodImpl(INLINE)] public static float Max(params float[] values) => Mathf.Max(values);

        /// <inheritdoc cref="Mathf.Max(int, int)"/>
        [MethodImpl(INLINE)] public static int Max(int a, int b) => Mathf.Max(a, b);

        /// <inheritdoc cref="Mathf.Max(int[])"/>
        [MethodImpl(INLINE)] public static int Max(params int[] values) => Mathf.Max(values);

        /// <inheritdoc cref="Mathf.Min(float, float)"/>
        [MethodImpl(INLINE)] public static float Min(float a, float b) => Mathf.Min(a, b);

        /// <inheritdoc cref="Mathf.Min(float[])"/>
        [MethodImpl(INLINE)] public static float Min(params float[] values) => Mathf.Min(values);

        /// <inheritdoc cref="Mathf.Min(int, int)"/>
        [MethodImpl(INLINE)] public static int Min(int a, int b) => Mathf.Min(a, b);

        /// <inheritdoc cref="Mathf.Min(int[])"/>
        [MethodImpl(INLINE)] public static int Min(params int[] values) => Mathf.Min(values);

        /// <summary> Returns the smallest component of the given vector. </summary>
        public static float Min(Vector2 v) => Min(v.x, v.y);
        /** <inheritdoc cref="Min(Vector2)"/> */
        public static float Min(Vector3 v) => Min(v.x, v.y, v.z);
        /** <inheritdoc cref="Min(Vector2)"/> */
        public static float Min(Vector4 v) => Min(v.x, v.y, v.z, v.w);
        /** <inheritdoc cref="Min(Vector2)"/> */
        public static float Min(VectorN v) => Min(v.components);
        /** <inheritdoc cref="Min(Vector2)"/> */
        public static float Min(Vector2Int v) => Min(v.x, v.y);
        /** <inheritdoc cref="Min(Vector2)"/> */
        public static float Min(Vector3Int v) => Min(v.x, v.y, v.z);
        /** <inheritdoc cref="Min(Vector2)"/> */
        public static float Min(VectorNInt v) => Min(v.components);

        /// <summary> Returns the largest component of the given vector. </summary>
        public static float Max(Vector2 v) => Max(v.x, v.y);
        /** <inheritdoc cref="Max(Vector2)"/> */
        public static float Max(Vector3 v) => Max(v.x, v.y, v.z);
        /** <inheritdoc cref="Max(Vector2)"/> */
        public static float Max(Vector4 v) => Max(v.x, v.y, v.z, v.w);
        /** <inheritdoc cref="Max(Vector2)"/> */
        public static float Max(VectorN v) => Max(v.components);
        /** <inheritdoc cref="Max(Vector2)"/> */
        public static float Max(Vector2Int v) => Max(v.x, v.y);
        /** <inheritdoc cref="Max(Vector2)"/> */
        public static float Max(Vector3Int v) => Max(v.x, v.y, v.z);
        /** <inheritdoc cref="Max(Vector2)"/> */
        public static float Max(VectorNInt v) => Max(v.components);

        #endregion

        #region Stats

        #region Factorial

        /// <summary> Returns n factorial (n!). </summary>
        /// <seealso cref="FactorialLong(uint)"/>
        /// <param name="n"> A number between 0 and 12. </param>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown when n is greater than 12. </exception>
        public static int Factorial(uint n) => n switch {
            0U => 1,
            1U => 1,
            2U => 2,
            3U => 6,
            4U => 24,
            5U => 120,
            6U => 720,
            7U => 5040,
            8U => 40320,
            9U => 362880,
            10U => 3628800,
            11U => 39916800,
            12U => 479001600,
            _ => throw new ArgumentOutOfRangeException(nameof(n), n <= 20 ? $"The factorial of the input value exceeds 32-bits. Try using {nameof(FactorialLong)} instead." :
                                                                            $"The factorial of the input value exceeds 64-bits and cannot be stored as an int or long."),
        };

        /// <summary> Returns n factorial (n!). </summary>
        /// <seealso cref="Factorial(uint)"/>
        /// <param name="n"> A number between 0 and 20. </param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static long FactorialLong(uint n) => n switch {
            0U => 1L,
            1U => 1L,
            2U => 2L,
            3U => 6L,
            4U => 24L,
            5U => 120L,
            6U => 720L,
            7U => 5040L,
            8U => 40320L,
            9U => 362880L,
            10U => 3628800L,
            11U => 39916800L,
            12U => 479001600L,
            13U => 6227020800L,
            14U => 87178291200L,
            15U => 1307674368000L,
            16U => 20922789888000L,
            17U => 355687428096000L,
            18U => 6402373705728000L,
            19U => 121645100408832000L,
            20U => 2432902008176640000L,
            _ => throw new ArgumentOutOfRangeException(nameof(n), "The factorial of the input value exceeds 64-bits and cannot be stored as a long."),
        };

        #endregion

        /// <summary> Returns the arithmetic mean (average) of two or more numbers. </summary>
        public static float Mean(float a, float b) => checked((a + b) / 2f);

        /// <inheritdoc cref="Mean(float, float)"/>
        public static float Mean(params float[] values) {
            checked {
                float mean = 0f;
                foreach (float value in values)
                    mean += value;
                return mean / values.Length;
            }
        }

        /// <summary> Returns the geometric mean of two or more numbers. </summary>
        public static float GeometricMean(float a, float b) => checked(Sqrt(a * b));

        /// <inheritdoc cref="GeometricMean(float, float)"/>
        public static float GeometricMean(params float[] values) {
            checked {
                float mean = 1f;
                foreach (float value in values)
                    mean *= value;
                return Pow(mean, 1f / values.Length);
            }
        }

        /// <summary> Returns the median of a set of numbers. </summary>
        public static float Median(params float[] values) {
            //TODO: optimize
            float[] sorted = (float[]) values.Clone();
            Array.Sort(sorted);
            if (sorted.Length % 2 == 0)
                return Mean(sorted[sorted.Length / 2 - 1], sorted[sorted.Length / 2]);
            else
                return sorted[sorted.Length / 2];

        }

        /// <summary> Returns the mode (or modes) of a set of numbers. </summary>
        public static float[] Mode(params float[] values) {
            //TODO: optimize
            var map = new Dictionary<float, int>(); //maps value to frequency
            foreach (float value in values) {
                if (map.ContainsKey(value))
                    map[value]++;
                else
                    map[value] = 1;
            }

            var modes = new List<float>(1);
            int freq = 0;
            foreach (var kv in map) {
                if (kv.Value > freq) {
                    freq = kv.Value;
                    modes.Clear();
                    modes.Add(kv.Key);
                }
                else if (kv.Value == freq)
                    modes.Add(kv.Key);
            }

            return modes.ToArray();
        }

        /// <summary> Computes the binomial coefficient nCk. </summary>
        public static int BinomialCoefficient(int n, int k) {
            if (k == 0) return 1;
            if (n == 0) return 0;
            return BinomialCoefficient(n - 1, k - 1) * n / k;
        }

        #endregion

        #region Linear Transformations

        /// <summary> Linearly remaps a value from [a, b] to [c, d]. </summary>
        [MethodImpl(INLINE)]
        public static float Remap(float value, float a, float b, float c, float d) =>
            Mathf.Lerp(c, d, Mathf.InverseLerp(a, b, value));

        /// <summary> Linearly remaps a value from [a, b] to [c, d] without clamping. </summary>
        [MethodImpl(INLINE)]
        public static float RemapUnclamped(float value, float a, float b, float c, float d) =>
            Mathf.LerpUnclamped(c, d, Mathf.InverseLerp(a, b, value));

        #endregion

        #region Polar, Spherical, and Cylindrical Transformations

        #region 2D transformations
        /// <summary> Converts a vector from polar (r, theta) coordinates to Cartestian (x, y) coordinates. </summary>
        /// <remarks>
        ///     <para>
        ///         This method is part of a group of methods that converts between Cartesian, polar, spherical, and cylindrical coordinates.
        ///     </para>
        ///     <para>
        ///         Cartesian coordinates are the standard (x, y) or (x, y, z) coordinates. <br/>
        ///         For polar (r, theta) coordinates, r is the distance to the origin and theta is the reference angle on the xy-plane. <br/>
        ///         For cylindrical (r, theta, y) coordinates, r is the distance to the origin, theta is the reference angle on the xz-plane, and y is the height above the xz-plane. <br/>
        ///         For spherical (rho, theta, phi) coordinates, rho is the radial distance to the origin, theta is the reference angle on the xz-plane, and phi is the elevation angle from the xz-plane.
        ///     </para>
        ///     <para>
        ///         All angles are in radians.
        ///     </para>
        /// </remarks>
        /// <param name="polar"> The input vector, in polar coordiantes. </param>
        /// <returns> The corresponding vector in Cartesian coordinates. </returns>
        public static Vector2 PolarToCartesian(Vector2 polar) => new Vector2(polar[0] * Cos(polar[1]), polar[0] * Sin(polar[1]));

        /// <summary> Converts a vector from Cartesian (x, y) coordinates to polar (r, theta) coordinates. </summary>
        /// <remarks> <inheritdoc cref="PolarToCartesian(Vector2)"/> </remarks>
        /// <param name="cartesian"> The input vector, in Cartesian coordiantes. </param>
        /// <returns> The corresponding vector in polar coordinates. </returns>
        public static Vector2 CartesianToPolar(Vector2 cartesian) => new Vector2(cartesian.magnitude, Atan2(cartesian.y, cartesian.x));
        #endregion

        #region 3D Transformation
        /// <summary> Converts a vector from cylindrical (r, theta, y) coordinate to Cartesian (x, y, z) coordinates. </summary>
        /// <remarks> <inheritdoc cref="PolarToCartesian(Vector2)"/> </remarks>
        /// <param name="cylindrical"> The input vector, in cylindrical coordinates. </param>
        /// <returns> The corresponding vector in Cartesian coordinates. </returns>
        public static Vector3 CylindricalToCartesian(Vector3 cylindrical) {
            Vector2 xz = PolarToCartesian(cylindrical);
            return new Vector3(xz[0], cylindrical[2], xz[1]);
        }

        /// <summary> Converts a vector from Cartesian (x, y, z) to cylindrical (r, theta, y) coordinates. </summary>
        /// <param name="cartesian"> The input vector, in Cartesian coordinates. </param>
        /// <remarks> <inheritdoc cref="PolarToCartesian(Vector2)"/> </remarks>
        /// <returns> The corresponding vector in cylindrical coordinates. </returns>
        public static Vector3 CartesianToCylindrical(Vector3 cartesian) {
            Vector2 rtheta = CartesianToPolar(cartesian.XZ());
            return new Vector3(rtheta[0], rtheta[1], cartesian.y);
        }

        /// <summary> Converts a vector from spherical (rho, theta, phi) to Cartesian (x, y, z) coordinates. </summary>
        /// <remarks> <inheritdoc cref="PolarToCartesian(Vector2)"/> </remarks>
        /// <param name="spherical"> The input vector, in spherical coordinates. </param>
        /// <returns> The corresponding vector in Cartesian coordinates. </returns>
        public static Vector3 SphericalToCartesian(Vector3 spherical) {
            Vector3 xz = spherical.x * new Vector3(Cos(spherical.y), 0, Sin(spherical.y));
            return Vector3.RotateTowards(xz, Vector3.up, spherical.z, 0);
        }

        /// <summary> Converts a vector from Cartesian (x, y, z) to spherical (rho, theta, phi) coordinates. </summary>
        /// <param name="cartesian"> The input vector, in Cartesian coordinates. </param>
        /// <remarks> <inheritdoc cref="PolarToCartesian(Vector2)"/> </remarks>
        /// <returns> The corresponding vector in spherical coordinates. </returns>
        public static Vector3 CartesianToSpherical(Vector3 cartesian) {
            float rho = cartesian.magnitude;
            float theta = Atan2(cartesian.z, cartesian.x);
            float phi = Atan2(cartesian.y, Vector3.ProjectOnPlane(cartesian, Vector3.up).magnitude);
            return new Vector3(rho, theta, phi);
        }

        /// <summary> Converts a vector from spherical (rho, theta, phi) to cylindrical (r, theta, phi) coordinates. </summary>
        /// <remarks> <inheritdoc cref="PolarToCartesian(Vector2)"/> </remarks>
        /// <param name="spherical"> The input vector, in spherical coordinates. </param>
        /// <returns> The corresponding vector, in cylindrical coordinates. </returns>
        public static Vector3 SphericalToCylindrical(Vector3 spherical) =>
            CartesianToCylindrical(SphericalToCartesian(spherical)); //TODO: Compute transformation directly

        public static Vector3 CylindricalToSpherical(Vector3 cylindrical) =>
            CartesianToSpherical(CylindricalToCartesian(cylindrical)); //TODO: Compute transformation directly
        #endregion

        #endregion

        #region Summation

        /// <summary> Returns the sum of multiple numbers or vectors. </summary>
        public static float Sum(params float[] nums) {
            float sum = 0f;
            foreach (float n in nums)
                sum += n;
            return sum;
        }

        /// <inheritdoc cref="Sum(float[])"/>
        public static Vector2 Sum(params Vector2[] vectors) {
            Vector2 sum = Vector2.zero;
            foreach (Vector2 v in vectors)
                sum += v;
            return sum;
        }

        /// <inheritdoc cref="Sum(float[])"/>
        public static Vector3 Sum(params Vector3[] vectors) {
            Vector3 sum = Vector3.zero;
            foreach (Vector3 v in vectors)
                sum += v;
            return sum;
        }

        #endregion

        #region Gram-Schmidt

        /// <summary> Takes a basis and uses the Gram-Schmidt process to orthogonalize it. The magnitudes of the vectors are not necessarily preserved.</summary>
        /// <remarks> If the input vectors are not linearly independent, some of the output vectors will be 0. </remarks>
        /// <param name="v0"> The first basis vector. </param>
        /// <param name="v1"> The second basis vector. </param>
        /// <returns> A <see cref="Vector2"/>[] of length 2 where each vector is orthogonal to the others. </returns>
        public static Vector2[] Orthogonalize(Vector2 v0, Vector2 v1) {
            Vector2[] u = new Vector2[2];
            u[0] = v0;
            u[1] = v1 - (Vector2) Vector3.Project(v1, u[0]);
            return u;
        }

        /// <summary> <inheritdoc cref="Orthogonalize(Vector2, Vector2)"/> </summary>
        /// <remarks> <inheritdoc cref="Orthogonalize(Vector2, Vector2)"/> </remarks>
        /// <param name="v0"> The first basis vector. </param>
        /// <param name="v1"> The second basis vector. </param>
        /// <param name="v2"> The third basis vector. </param>
        /// <returns> A <see cref="Vector3"/>[] of length 3 where each vector is orthogonal to the others. </returns>
        public static Vector3[] Orthogonalize(Vector3 v0, Vector3 v1, Vector3 v2) {
            Vector3[] u = new Vector3[3];
            u[0] = v0;
            u[1] = v1 - Vector3.Project(v1, u[0]);
            u[2] = v2 - Vector3.Project(v2, u[0]) - Vector3.Project(v2, u[1]);
            return u;
        }

        /// <summary> Takes a basis and uses the Gram-Schmidt process to orthonormalize it. </summary>
        /// <remarks> <inheritdoc cref="Orthogonalize(Vector2, Vector2)"/> </remarks>
        /// <param name="v0"> The first basis vector. </param>
        /// <param name="v1"> The second basis vector. </param>
        /// <returns> A <see cref="Vector2"/>[] of length 2 where each vector is normalized and orthogonal to the others. </returns>
        public static Vector2[] Orthonormalize(Vector2 v0, Vector2 v1) {
            Vector2[] e = new Vector2[2];
            e[0] = v0.normalized;
            e[1] = (v1 - (Vector2) Vector3.Project(v1, e[0])).normalized;
            return e;
        }

        /// <summary> <inheritdoc cref="Orthonormalize(Vector2, Vector2)"/> </summary>
        /// <remarks> <inheritdoc cref="Orthogonalize(Vector2, Vector2)"/> </remarks>
        /// <param name="v0"> The first basis vector. </param>
        /// <param name="v1"> The second basis vector. </param>
        /// <param name="v2"> The third basis vector. </param>
        /// <returns> A <see cref="Vector3"/>[] of length 3 where each vector is normalized and orthogonal to the others. </returns>
        public static Vector3[] Orthonormalize(Vector3 v0, Vector3 v1, Vector3 v2) {
            Vector3[] e = new Vector3[3];
            e[0] = v0.normalized;
            e[1] = (v1 - Vector3.Project(v1, e[0])).normalized;
            e[2] = (v2 - Vector3.Project(v2, e[0]) - Vector3.Project(v2, e[1])).normalized;
            return e;
        }

        #endregion

        #region Polynomials

        /// <summary> Computes the root of the line y = mx + b. </summary>
        /// <param name="m"> The slope of the line. </param>
        /// <param name="b"> The y-intercept of the line. </param>
        public static float XIntercept(float m, float b) {
            checked {
                return -b / m;
            }
        }

        /// <summary> Computes the discriminant of the quadratic polynomial ax^2 + bx + c. </summary>
        public static float QuadraticDiscriminant(float a, float b, float c) {
            checked {
                return b.Squared() - 4f * a * c;
            }
        }

        /// <summary> Computes the discriminant of the cubic polynomial ax^3 + bx^2 + cx + d. </summary>
        public static float CubicDiscriminant(float a, float b, float c, float d) {
            checked {
                return b.Squared() * c.Squared() -
                       4f * a * c.Cubed() -
                       4f * b.Cubed() * d -
                       27f * a.Squared() * d.Squared() +
                       18f * a * b * c * d;
            }
        }

        /// <summary> Computes the discriminant of the quartic polynomial ax^4 + bx^3 + cx^2 + dx + e. </summary>
        public static float QuarticDiscriminant(float a, float b, float c, float d, float e) {
            checked {
                return 256f * a.Cubed() * e.Cubed() -
                       192f * a.Squared() * b * d * e.Squared() -
                       128f * a.Squared() * c.Squared() * e.Squared() +
                       144f * a.Squared() * c * d.Squared() * e -
                       27f * a.Squared() * Pow(d, 4) +
                       144f * a * b.Squared() * c * e.Squared() -
                       6f * a * b.Squared() * d.Squared() * e -
                       80f * a * b * c.Squared() * d * e +
                       18f * a * b * c * d.Cubed() +
                       16f * a * Pow(c, 4) * e -
                       4f * a * c.Cubed() * d.Squared() -
                       27f * Pow(b, 4) * e.Squared() +
                       18f * b.Cubed() * c * d * e -
                       4f * b.Cubed() * d.Cubed() -
                       4f * b.Squared() * c.Cubed() * e +
                       b.Squared() * c.Squared() * d.Squared();
            }
        }

        /// <summary> Computes the real roots of the quadratic polynomial ax^2 + bx + c. </summary>
        public static float[] QuadraticFormula(float a, float b, float c) {
            checked {
                float discriminant = QuadraticDiscriminant(a, b, c);
                if (discriminant < 0f) return new float[0];
                if (discriminant == 0) return new float[1] { -b / (2f * a) };
                float sqrtDiscriminant = Sqrt(discriminant);
                return new float[2] {
                    (-b + sqrtDiscriminant) / (2f * a),
                    (-b - sqrtDiscriminant) / (2f * a),
                };
            }
        }

        /// <summary> Computes the real roots of the cubic polynomial ax^3 + bx^2 + cx + d. </summary>
        public static float[] CubicFormula(float a, float b, float c, float d) {
            checked {
                float discriminant = CubicDiscriminant(a, b, c, d);
                float p = c / a - b.Squared() / (3f * a.Squared());
                float q = 2f * b.Cubed() / (27f * a.Cubed()) - b * c / (3f * a.Squared()) + d / a;

                if (discriminant == 0f) //1 or 2 distinct real roots
                    return p == 0f ? new float[1] { 0f } : new float[2] { 3f * q / p - b / (3f * a), -3f * q / (2f * p) - b / (3f * a) };
                if (discriminant < 0f) { //1 distinct real root
                    float radical = Sqrt(q.Squared() / 4f + p.Cubed() / 27f);
                    return new float[1] { Cbrt(-q / 2f + radical) + Cbrt(-q / 2f - radical) - b / (3f * a) };
                }
                else { //3 distinct real roots
                    Complex radical = ComplexSqrt(q.Squared() / 4f + p.Cubed() / 27f);
                    Complex cbrtUnity = Complex.RootOfUnity(3);
                    Complex cbrt1 = ComplexCbrt(-q / 2f + radical);
                    Complex cbrt2 = cbrt1 * cbrtUnity;
                    Complex cbrt3 = cbrt2 * cbrtUnity;
                    return new float[3] { Re(cbrt1 + cbrt1.complexConjugate) - b/(3f*a),
                                          Re(cbrt2 + cbrt2.complexConjugate) - b/(3f*a),
                                          Re(cbrt3 + cbrt3.complexConjugate) - b/(3f*a),
                    };
                }
            }
        }

        #endregion

        #region Misc

        /** <summary> Returns the squared distance between vectors a and b. </summary> */
        [MethodImpl(INLINE)] public static float Dst2(Vector2 a, Vector2 b) => (a - b).sqrMagnitude;
        /** <summary> <inheritdoc cref="Dst2(Vector2, Vector2)"/> </summary> */
        [MethodImpl(INLINE)] public static float Dst2(Vector3 a, Vector3 b) => (a - b).sqrMagnitude;
        /** <summary> <inheritdoc cref="Dst2(Vector2, Vector2)"/> </summary> */
        [MethodImpl(INLINE)] public static float Dst2(Vector4 a, Vector4 b) => (a - b).sqrMagnitude;
        /** <summary> <inheritdoc cref="Dst2(Vector2, Vector2)"/> </summary> */
        [MethodImpl(INLINE)] public static float Dst2(Vector2Int a, Vector2Int b) => (a - b).sqrMagnitude;
        /** <summary> <inheritdoc cref="Dst2(Vector2, Vector2)"/> </summary> */
        [MethodImpl(INLINE)] public static float Dst2(Vector3Int a, Vector3Int b) => (a - b).sqrMagnitude;
        /** <summary> <inheritdoc cref="Dst2(Vector2, Vector2)"/> </summary> */
        [MethodImpl(INLINE)] public static float Dst2(VectorNInt a, VectorNInt b) => (a - b).sqrMagnitude;

        /// <inheritdoc cref="Mathf.Approximately(float, float)"/>
        [MethodImpl(INLINE)] public static bool Approximately(float a, float b) => Mathf.Approximately(a, b);

        /// <summary> Compares two vectors and returns true if their components are similar. </summary>
        public static bool Approximately(Vector3 a, Vector3 b) => Approximately(a.x, b.x) && Approximately(a.y, b.y) && Approximately(a.z, b.z);

        /// <summary> Returns true if the given floating point value is similar to zero. </summary>
        [MethodImpl(INLINE)] public static bool ApproximatelyZero(float f) => Approximately(f, 0f);

        /// <summary> Returns true if the given vector is similar to the zero vector. </summary>
        public static bool ApproximatelyZero(Vector3 v) => Approximately(v.sqrMagnitude, 0f);

        /// <summary> Returns true if the given floating point value is similar to one. </summary>
        [MethodImpl(INLINE)] public static bool ApproximatelyOne(float f) => Approximately(f, 1f);

        /// <summary> Returns true if the given floating point value is approximately equal to an integer. </summary>
        public static bool IsApproxInt(float f) => Approximately(f, Round(f));

        /// <summary> Returns true if the given floating point value is approximately equal to an integer and outputs the nearest integer. </summary>
        public static bool IsApproxInt(float f, out int n) => Approximately(f, n = RoundToInt(f));

        /// <summary> Returns the real part of a complex number. </summary>
        public static float Re(Complex z) => z.real;

        /// <summary> Returns the imaginary part of a complex number. </summary>
        public static float Im(Complex z) => z.imaginary;

        /// <inheritdoc cref="Mathf.Abs(float)"/>
        [MethodImpl(INLINE)] public static float Abs(float f) => Mathf.Abs(f);

        /// <inheritdoc cref="Mathf.Abs(int)"/>
        [MethodImpl(INLINE)] public static float Abs(int value) => Mathf.Abs(value);

        /// <summary> The distance on the complex plane between this number and zero. Equivalent to the property <see cref="Complex.modulus"/>. </summary>
        public static float Abs(Complex z) => Sqrt(z.real.Squared() + z.imaginary.Squared());

        #endregion
    }
}
