using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> 
    ///     A utility class that contains additional gizmos. 
    ///     For default gizmos included with Unity, see <see cref="Gizmos"/>.
    /// </summary>
    public static class UMLGizmos {

        /// <summary> Draws a dotted line starting at from towards to. </summary>
        /// <param name="frequency"> The number of dotted cycles per distance unit. </param>
        /// <param name="duty"> The ratio between the pulse duration and the period. </param>
        public static void DrawLineDotted(Vector3 from, Vector3 to, float frequency = 20f, float duty = 0.5f) {
            float period = 1f / frequency;
            Vector3 previous = from;
            bool draw = true;
            for (float t = period * duty; t < 1; t += draw ? period * duty: period * (1f-duty)) {
                Vector3 current = Vector3.Lerp(from, to, t);
                if (draw)
                    Gizmos.DrawLine(previous, current);
                previous = current;
                draw = !draw;
            }
            if (draw)
                Gizmos.DrawLine(previous, to);
        }

        /// <summary> Draws a line that goes through any number of points, in order. </summary>
        public static void DrawMultiline(params Vector3[] points) {
            for (int i = 0; i < points.Length - 1; i++)
                Gizmos.DrawLine(points[i], points[i + 1]);
        }

        /// <summary> Draws a circle given its center and normal. </summary>
        /// <param name="center"> The center of the circle. </param>
        /// <param name="normal"> The vector pointing outwards from the center of the circle. </param>
        /// <param name="radius"> The radius of the circle. </param>
        /// <param name="resolution"> The number of segments to draw. </param>
        public static void DrawCircle(Vector3 center, Vector3 normal, float radius = 1f, int resolution = 50) {
            normal.Normalize();
            Vector3 e1 = Vector3.RotateTowards(normal, -normal, UML.PI / 2f, 0f) * radius;
            Vector3 e2 = Vector3.Cross(e1, normal);
            Vector3 previous = center + e1;
            for (float theta = UML.TAU / resolution; theta < UML.TAU; theta += UML.TAU / resolution) {
                Vector3 p = center + e1 * UML.Cos(theta) + e2 * UML.Sin(theta);
                Gizmos.DrawLine(previous, p);
                previous = p;
            }
            Gizmos.DrawLine(previous, center + e1);
        }

        /// <summary> Draws a triangle given three vertices. </summary>
        public static void DrawTriangle(Vector3 a, Vector3 b, Vector3 c) {
            Gizmos.DrawLine(a, b);
            Gizmos.DrawLine(b, c);
            Gizmos.DrawLine(c, a);
        }

        /// <summary> Draws a <see cref="Curve{V}"/>. </summary>
        /// <param name="curve"> The curve to draw. </param>
        /// <param name="resolution"> The number of segments to draw. </param>
        public static void DrawCurve(Curve<Vector3> curve, int resolution = 50) {
            Vector3 previous = curve.startPoint;
            for (int i = 1; i <= resolution; i++) {
                float t = UML.Lerp(curve.tMin, curve.tMax, (float) i / resolution);
                Vector3 p = curve.Evaluate(t);
                Gizmos.DrawLine(previous, p);
                previous = p;
            }
        }

        /// <summary> Draws a <see cref="Curve{V}"/>. </summary>
        /// <param name="curve"> The curve to draw. </param>
        /// <param name="resolution"> The number of segments to draw. </param>
        public static void DrawCurve(Curve<Vector2> curve, int resolution = 50) {
            Vector2 previous = curve.startPoint;
            for (int i = 1; i <= resolution; i++) {
                float t = UML.Lerp(curve.tMin, curve.tMax, (float) i / resolution);
                Vector2 p = curve.Evaluate(t);
                Gizmos.DrawLine(previous, p);
                previous = p;
            }
        }

        /// <summary> Draws a Frenet frame. Note that <see cref="Gizmos.color"/> is ignored. The tangent, normal, and binormal basis vectors are drawn in red, green, and blue, respectively. </summary>
        /// <param name="frame"> The Frenet frame to draw. </param>
        /// <param name="scale"> An optional value to scale the drawing of each of the basis vectors. </param>
        public static void DrawFrenetFrame(FrenetFrame3D frame, float scale = 1f) {
            Color previousColor = Gizmos.color;
            Gizmos.color = UMLColors.red;
            Gizmos.DrawLine(frame.point, frame.point + frame.tangent * scale);
            Gizmos.color = UMLColors.green;
            Gizmos.DrawLine(frame.point, frame.point + frame.normal * scale);
            Gizmos.color = UMLColors.blue;
            Gizmos.DrawLine(frame.point, frame.point + frame.binormal * scale);
            Gizmos.color = previousColor;
        }

        /// <inheritdoc cref="DrawFrenetFrame(FrenetFrame3D, float)"/>
        public static void DrawFrenetFrame(FrenetFrame2D frame, float scale = 1f) {
            Color previousColor = Gizmos.color;
            Gizmos.color = UMLColors.red;
            Gizmos.DrawLine(frame.point, frame.point + frame.tangent * scale);
            Gizmos.color = UMLColors.green;
            Gizmos.DrawLine(frame.point, frame.point + frame.normal * scale);
            Gizmos.color = previousColor;
        }

        /// <summary> Draws a <see cref="Polynomial"/> or <see cref="Monomial"/>. </summary>
        public static void DrawPolynomial(Polynomial p, int resolution = 50) {
            DrawCurve(new PolynomialCurve(p), resolution);
        }

        /// <summary> Draws a <see cref="Polynomial"/> or <see cref="Monomial"/>. </summary>
        public static void DrawPolynomial(Polynomial p, Interval interval, int resolution = 50) {
            DrawCurve(new PolynomialCurve(p, interval), resolution);
        }

    }

}