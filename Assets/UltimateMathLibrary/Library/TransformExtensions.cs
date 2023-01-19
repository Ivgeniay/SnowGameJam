using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    public static class TransformExtensions {

        //Cylindrical
        /// <summary> Gets the world space position of the Transform in cylindrical (r, theta, y) coordinates. </summary>
        /// <remarks> <inheritdoc cref="UML.PolarToCartesian(Vector2)"/> </remarks>
        public static Vector3 GetCylindricalPosition(this Transform transform) =>
            UML.CartesianToCylindrical(transform.position);

        /// <summary> Sets the world space position of the Transform in cylindrical (r, theta, y) coordinates. </summary>
        /// <remarks> <inheritdoc cref="GetSphericalPosition(Transform)"/> </remarks>
        public static void SetCylindrialPosition(this Transform transform, Vector3 cylindrical) =>
            transform.position = UML.CylindricalToCartesian(cylindrical);

        /// <summary> Gets the position of the Transform relative to the parent Transform in cylindrical (r, theta, y) coordinates. </summary>
        /// <remarks> <inheritdoc cref="GetSphericalPosition(Transform)"/> </remarks>
        public static Vector3 GetCylindricalLocalPosition(this Transform transform) =>
            UML.CartesianToCylindrical(transform.localPosition);

        /// <summary> Sets the position of the Transform relative to the parent Transform in cylindrical (r, theta, y) coordinates. </summary>
        /// <remarks> <inheritdoc cref="GetSphericalPosition(Transform)"/> </remarks>
        public static void SetCylindrialLocalPosition(this Transform transform, Vector3 cylindrical) =>
            transform.localPosition = UML.CylindricalToCartesian(cylindrical);

        //Spherical
        /// <summary> Gets the world space position of the Transform in spherical (rho, theta, phi) coordinates. </summary>
        /// <remarks> <inheritdoc cref="UML.PolarToCartesian(Vector2)"/> </remarks>
        public static Vector3 GetSphericalPosition(this Transform transform) =>
            UML.CartesianToSpherical(transform.position);

        /// <summary> Sets the world space position of the Transform in spherical (rho, theta, phi) coordinates. </summary>
        /// <remarks> <inheritdoc cref="GetSphericalPosition(Transform)"/> </remarks>
        public static void SetSphericalPosition(this Transform transform, Vector3 spherical) =>
            transform.position = UML.SphericalToCartesian(spherical);

        /// <summary> Gets the position of the Transform relative to the parent Transform in spherical (rho, theta, phi) coordinates. </summary>
        /// <remarks> <inheritdoc cref="GetSphericalPosition(Transform)"/> </remarks>
        public static Vector3 GetSphericalLocalPosition(this Transform transform) =>
            UML.CartesianToSpherical(transform.localPosition);

        /// <summary> Sets the position of the Transform relative to the parent Transform in spherical (rho, theta, phi) coordinates. </summary>
        /// <remarks> <inheritdoc cref="GetSphericalPosition(Transform)"/> </remarks>
        public static void SetSphericalLocalPosition(this Transform transform, Vector3 spherical) =>
            transform.localPosition = UML.SphericalToCartesian(spherical);

        //Polar
        /// <summary> Gets the world space position of the Transform in polar (r, theta) coordinates. The z component is ignored. </summary>
        /// <remarks> <inheritdoc cref="UML.PolarToCartesian(Vector2)"/> </remarks>
        public static Vector2 GetPolarPosition(this Transform transform) =>
            UML.CartesianToPolar(transform.position);

        /// <summary> Sets the world space position of the Transform in polar (r, theta) coordinates. The z component is set to 0. </summary>
        /// <remarks> <inheritdoc cref="GetSphericalPosition(Transform)"/> </remarks>
        public static void SetPolarPosition(this Transform transform, Vector2 polar) =>
            transform.position = UML.PolarToCartesian(polar);

        /// <summary> Gets the position of the Transform relative to the parent Transform in polar (r, theta) coordinates. The z component is ignored. </summary>
        /// <remarks> <inheritdoc cref="GetSphericalPosition(Transform)"/> </remarks>
        public static Vector2 GetPolarLocalPosition(this Transform transform) =>
            UML.CartesianToPolar(transform.localPosition);

        /// <summary> Sets the position of the Transform relative to the parent Transform in polar (r, theta) coordinates. The z component is set to 0. </summary>
        /// <remarks> <inheritdoc cref="GetSphericalPosition(Transform)"/> </remarks>
        public static void SetPolarLocalPosition(this Transform transform, Vector2 polar) =>
            transform.localPosition = UML.PolarToCartesian(polar);
    }

}