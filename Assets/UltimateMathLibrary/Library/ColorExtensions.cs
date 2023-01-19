using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    public static class ColorExtensions {

        #region Get/Set HSV

        /// <summary> Determines the hue of this color. </summary>
        public static float GetHue(this Color color) {
            Color.RGBToHSV(color, out float h, out float _, out float _);
            return h;
        }

        /// <summary> Determines the saturation of this color. </summary>
        public static float GetSaturation(this Color color) {
            Color.RGBToHSV(color, out float _, out float s, out float _);
            return s;
        }

        /// <summary> Determines the value of this color. </summary>
        public static float GetValue(this Color color) {
            Color.RGBToHSV(color, out float _, out float _, out float v);
            return v;
        }

        /// <summary> Sets the hue of this color. </summary>
        public static void SetHue(this ref Color color, float hue) {
            Color.RGBToHSV(color, out float _, out float saturation, out float value);
            color = Color.HSVToRGB(hue, saturation, value);
        }

        /// <summary> Sets the saturation of this color. </summary>
        public static void SetSaturation(this ref Color color, float saturation) {
            Color.RGBToHSV(color, out float hue, out float _, out float value);
            color = Color.HSVToRGB(hue, saturation, value);
        }

        /// <summary> Sets the value of this color. </summary>
        public static void SetValue(this ref Color color, float value) {
            Color.RGBToHSV(color, out float hue, out float saturation, out float _);
            color = Color.HSVToRGB(hue, saturation, value);
        }

        #endregion

        /// <summary> Multiplies the red, green, and blue components of this color by a constant <c>k</c> while maintaining the alpha value. </summary>
        public static void MultiplyRGB(this ref Color color, float k) =>
            color = new Color(color.r * k, color.g * k, color.b * k, color.a);

        /// <summary> Multiplies the red, green, and blue components of this color component-wise by another color <c>other</c> while maintaining the alpha value. </summary>
        public static void MultiplyRGB(this ref Color color, Color other) =>
            color = new Color(color.r * other.r, color.g * other.g, color.b * other.b, color.a);

        /// <summary> Return the color with its alpha component set to the given value. </summary>
        public static Color WithAlpha(this Color color, float alpha) {
            color.a = alpha;
            return color;
        }
    }

}