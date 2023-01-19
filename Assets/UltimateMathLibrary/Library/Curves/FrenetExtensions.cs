using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    public static class FrenetExtensions {

        /// <summary>
        ///     Gets the normalized tangnet vector of the curve at t.
        ///     The tangent vector points in the direction that the curve is moving.
        /// </summary>
        public static V GetTangent<V>(this IDifferentiable<V> diff, float t) where V : struct {
            return diff.curve.vs.Normalized(diff.GetDerivative(t));
        }

        /// <summary>
        ///     Gets the normalized normal vector of the curve at t.
        ///     The normal vector points in the direction of the curve's curvature.
        /// </summary>
        public static Vector2 GetNormal(this I2Differentiable<Vector2> diff, float t) {
            Vector3 velocity = diff.GetDerivative(t);
            return Vector3.Cross(velocity, Vector3.Cross(diff.GetSecondDerivative(t), velocity)).normalized;
        }

        /// <inheritdoc cref="GetNormal(I2Differentiable{Vector2}, float)"/>
        public static Vector3 GetNormal(this I2Differentiable<Vector3> diff, float t) {
            Vector3 velocity = diff.GetDerivative(t);
            return Vector3.Cross(velocity, Vector3.Cross(diff.GetSecondDerivative(t), velocity)).normalized;
        }

        /// <summary> 
        ///     Gets the normalized binormal vector of the curve at t.
        ///     The binormal is equal to the cross product of the tangent and normal vectors.
        /// </summary>
        public static Vector3 GetBinormal(this I2Differentiable<Vector3> diff, float t) {
            return Vector3.Cross(diff.GetDerivative(t), diff.GetSecondDerivative(t)).normalized;
        }

        /// <summary>
        ///     Gets the 2D Frenet-Serret frame of the curve at t.
        ///     The Frenet-Serret frame encapsulates the curve's tangenet and normal vectors at a point.
        /// </summary>
        public static FrenetFrame2D GetFrenetFrame(this I2Differentiable<Vector2> diff, float t) {
            return new FrenetFrame2D(diff.curve.Evaluate(t), diff.GetTangent(t), diff.GetNormal(t));
        }

        /// <summary>
        ///     Gets the 3D Frenet-Serret frame of the curve at t.
        ///     The Frenet-Serret frame encapsulates the curve's tangenet, normal, and binormal vectors at a point.
        /// </summary>
        public static FrenetFrame3D GetFrenetFrame(this I2Differentiable<Vector3> diff, float t) {
            return new FrenetFrame3D(diff.curve.Evaluate(t), diff.GetTangent(t), diff.GetNormal(t), diff.GetBinormal(t));
        }

        /// <summary>
        ///     Gets the curvature of the curve at t in radians per distance unit.
        ///     Curvature measures the curve's local resistance to being a straight line.
        ///     A value of 0 represents a curve that is locally straight at t.
        /// </summary>
        public static float GetCurvature(this I2Differentiable<Vector2> diff, float t) {
            Vector3 velocity = diff.GetDerivative(t);
            return diff.curve.vs.Magnitude(Vector3.Cross(velocity, diff.GetSecondDerivative(t)) / velocity.magnitude.Cubed());
        }

        /// <inheritdoc cref="GetCurvature(I2Differentiable{Vector2}, float)"/>
        public static float GetCurvature(this I2Differentiable<Vector3> diff, float t) {
            Vector3 velocity = diff.GetDerivative(t);
            return diff.curve.vs.Magnitude(Vector3.Cross(velocity, diff.GetSecondDerivative(t)) / velocity.magnitude.Cubed());
        }

        /// <summary> 
        ///     Gets the torsion of the curve at t in radians per distance unit. 
        ///     Torsion measures the curve's local resistance to staying on the same plane.
        /// </summary>
        public static float GetTorsion(this I3Differentiable<Vector3> curve, float t) {
            Vector3 v = Vector3.Cross(curve.GetDerivative(t), curve.GetSecondDerivative(t));
            return Vector3.Dot(v, curve.GetThirdDerivative(t)) / v.sqrMagnitude;
        }
    }
}