using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> 
    ///     Represents the Frenet-Serret frame field of a 3d curve at a point.
    ///     The Frenet-Serret frame encapsulates the curve's tangenet, normal, and binormal vectors at a point.
    /// </summary>
    public struct FrenetFrame3D {

        /// <summary> The point of application of the frame field. </summary>
        public Vector3 point;

        /// <summary> The tangent vector of the frame field.</summary>
        public Vector3 tangent;

        /// <summary> The normal vector of the frame field. </summary>
        public Vector3 normal;

        /// <summary> The binormal vector of the frame field. </summary>
        public Vector3 binormal;

        public FrenetFrame3D(Vector3 point, Vector3 tangent, Vector3 normal, Vector3 binormal) {
            this.point = point;
            this.tangent = tangent;
            this.normal = normal;
            this.binormal = binormal;
        }

        /// <summary> 
        ///     Returns true if the Frenet frame is valid. 
        ///     A frame field is valid if {T, N, B} is an orthonormal basis of R3 and B = T x N.
        /// </summary>
        public bool isValid =>
            UML.ApproximatelyZero(Vector3.Dot(tangent, normal)) &&
            UML.ApproximatelyZero(Vector3.Dot(normal, binormal)) &&
            UML.ApproximatelyOne(tangent.sqrMagnitude) &&
            UML.ApproximatelyOne(normal.sqrMagnitude) &&
            UML.Approximately(binormal, Vector3.Cross(tangent, normal));

    }

    /// <summary>
    ///     Represents the Frenet-Serret frame field of a 2d curve at a point.
    ///     The Frenet-Serret frame encapsulates the curve's tangenet and normal vectors at a point.
    /// </summary>
    public struct FrenetFrame2D {

        /// <summary> The point of application of the frame field. </summary>
        public Vector2 point;

        /// <summary> The tangent vector of the frame field.</summary>
        public Vector2 tangent;

        /// <summary> The normal vector of the frame field. </summary>
        public Vector2 normal;

        public FrenetFrame2D(Vector2 point, Vector2 tangent, Vector2 normal) {
            this.point = point;
            this.tangent = tangent;
            this.normal = normal;
        }

        /// <summary> 
        ///     Returns true if the Frenet frame is valid. 
        ///     A frame field is valid if {T, N} is an orthonormal basis of R2.
        /// </summary>
        public bool isValid =>
            UML.ApproximatelyZero(Vector3.Dot(tangent, normal)) &&
            UML.ApproximatelyOne(tangent.sqrMagnitude) &&
            UML.ApproximatelyOne(normal.sqrMagnitude);

    }

}