using System.Collections.Generic;
using UnityEngine;
using Nickmiste.UltimateMathLibrary;

[ExecuteAlways]
public class PoissonDiskSamplingDemo : MonoBehaviour {

    private enum SampleRegionShape { Circle, Square, Custom };

    [Header("Sample Region Settings")]
    [Tooltip("The shape of the sample region.")]
    [SerializeField] private SampleRegionShape sampleRegionShape;
    [Tooltip("The radius of the sample region.")]
    [SerializeField] [Range(10f, 30f)] private float sampleRadius = 20f;

    [Header("Poisson Settings")]
    [Tooltip("The guaranteed minimum distance between each sampled point.")]
    [SerializeField] private float minRadius = 1f;

    private List<Vector2> points = new List<Vector2>();

    private void Start() {
        GeneratePoints();
    }

    [ContextMenu("Generate Points")]
    private void GeneratePoints() {
        switch (sampleRegionShape) {

            // To sample in a circle, pass in a float as the second argument to the Evaluate() function
            case SampleRegionShape.Circle:
                points = PoissonDiskSampler.Sample(Vector2.zero, sampleRadius, minRadius);
                break;

            // To sample in a rectangle, pass in a Vector2 as the second argument to the Evaluate() function
            case SampleRegionShape.Square:
                points = PoissonDiskSampler.Sample(Vector2.zero, new Vector2(sampleRadius, sampleRadius), minRadius);
                break;

            // Example of using PoissonDiskSampler on a user-defined region
            // In this example, we will define a region that is the union of two circles
            // The circles will be centered at (10, 0) and (-10, 0) and will both have radius "sampleRadius"
            // The second argument passed into the Evaluate() method is a bounds check - It returns true if a point is in the sample region (and false otherwise)
            // To check if a point is in one of the circles, we check if the distance to the circle's center is <= the circle's radius
            // Since we are taking a union, we want our bounds check to return true if the point is in one circle OR the other circle
            // As an aside, note that we are calculating squared distance instead of distance as an optimization
            case SampleRegionShape.Custom:
                points = PoissonDiskSampler.Sample(Vector2.zero,
                    (Vector2 point) => UML.Dst2(point, new Vector2( 10, 0)) <= sampleRadius.Squared() ||
                                       UML.Dst2(point, new Vector2(-10, 0)) <= sampleRadius.Squared(),
                    minRadius);
                break;
        }
        Debug.Log($"Randomly generated {points.Count} points");
    }

    private void OnDrawGizmos() {
        DrawSampleBounds();
        DrawPoints();
    }

    private void DrawSampleBounds() {
        Gizmos.color = UMLColors.white;
        switch (sampleRegionShape) {
            case SampleRegionShape.Circle:
                UMLGizmos.DrawCircle(Vector3.zero, Vector3.up, sampleRadius);
                break;

            case SampleRegionShape.Square:
                Gizmos.DrawLine(new Vector3(-sampleRadius, 0,  sampleRadius), new Vector3( sampleRadius, 0,  sampleRadius));
                Gizmos.DrawLine(new Vector3( sampleRadius, 0,  sampleRadius), new Vector3( sampleRadius, 0, -sampleRadius));
                Gizmos.DrawLine(new Vector3( sampleRadius, 0, -sampleRadius), new Vector3(-sampleRadius, 0, -sampleRadius));
                Gizmos.DrawLine(new Vector3(-sampleRadius, 0, -sampleRadius), new Vector3(-sampleRadius, 0,  sampleRadius));
                break;

            case SampleRegionShape.Custom:
                static void DrawPartialCircle(Vector3 center, Vector3 other, float radius) {
                    float resolution = 100f;
                    Vector3 e1 = Vector3.RotateTowards(Vector3.up, -Vector3.up, UML.PI / 2f, 0f) * radius;
                    Vector3 e2 = Vector3.Cross(e1, Vector3.up);
                    Vector3 previous = center + e1;
                    for (float theta = UML.TAU / resolution; theta < UML.TAU; theta += UML.TAU / resolution) {
                        Vector3 p = center + e1 * UML.Cos(theta) + e2 * UML.Sin(theta);
                        if (UML.Dst2(p, other) > radius.Squared())
                            Gizmos.DrawLine(previous, p);
                        previous = p;
                    }
                    Gizmos.DrawLine(previous, center + e1);
                }
                DrawPartialCircle(new Vector3( 10f, 0f, 0f), new Vector3(-10f, 0f, 0f), sampleRadius);
                DrawPartialCircle(new Vector3(-10f, 0f, 0f), new Vector3( 10f, 0f, 0f), sampleRadius);
                break;
        }
    }

    private void DrawPoints() {
        Gizmos.color = UMLColors.white;
        foreach (Vector2 point in points)
            Gizmos.DrawSphere(new Vector3(point.x, 0, point.y), 0.15f);
    }
}
