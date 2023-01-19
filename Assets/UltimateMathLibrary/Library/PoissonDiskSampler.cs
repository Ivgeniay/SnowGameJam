using System.Collections.Generic;
using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> A utility class for randomly generating points using Poisson disk sampling. </summary>
    public static class PoissonDiskSampler {

        /// <summary> Encapsulates a method that determines if <c>p</c> is within the bounds of generation. </summary>
        public delegate bool BoundChecker(Vector2 p);

        /// <summary> Randomly generates a list of points in a circular region using Poisson disk sampling. </summary>
        /// <param name="center"> The center of the random sample region. </param>
        /// <param name="sampleRadius"> The radius of the sample region. </param>
        /// <param name="minRadius"> The miniumum distance guaranteed between any two point in the sample. </param>
        /// <param name="k"> The number of samples around a given point before rejection. Increase this value if you notice gaps in the generation. </param>
        public static List<Vector2> Sample(Vector2 center, float sampleRadius, float minRadius, int k = 30) =>
            Sample(center,
                   (Vector2 p) => p.sqrMagnitude <= sampleRadius.Squared(),
                   minRadius, k);

        /// <summary> Randomly generates a list of points in a rectangular region using Poisson disk sampling. </summary>
        /// <param name="center"> The center of the random sample region. </param>
        /// <param name="bounds"> The size of the sample region. Represents the x and y distance from the center to the edges of the sample region. </param>
        /// <param name="minRadius"> <inheritdoc cref="Sample(Vector2, float, float, int)"/> </param>
        /// <param name="k"> <inheritdoc cref="Sample(Vector2, float, float, int)"/> </param>
        public static List<Vector2> Sample(Vector2 center, Vector2 bounds, float minRadius, int k = 30) =>
            Sample(center,
                   (Vector2 p) => p.x >= center.x - bounds.x && p.x <= center.x + bounds.x && p.y >= center.y - bounds.y && p.y <= center.y + bounds.y,
                   minRadius, k);

        /// <summary> Randomly generates a list of points in a custom region using Poisson disk sampling.  </summary>>
        /// <param name="startPoint"> The point to start generation with. </param>
        /// <param name="contains"> A method that determines if a point is within the bounds of generation. </param>
        /// <param name="minRadius"> <inheritdoc cref="Sample(Vector2, float, float, int)" path="/param[@name='minRadius']"/> </param>
        /// <param name="k"> <inheritdoc cref="Sample(Vector2, float, float, int)" path="/param[@name='k']"/> </param>
        public static List<Vector2> Sample(Vector2 startPoint, BoundChecker contains, float minRadius, int k = 30) {
            List<Vector2> samples = new List<Vector2>();
            List<Vector2> active = new List<Vector2>();

            float minRadiusSq = minRadius * minRadius;
            float cellSize = minRadius / UML.SQRT2;
            var grid = new Dictionary<Cell, int>();

            System.Func<Vector2, Cell> getCell = (Vector2 point)
                => new Cell(Mathf.FloorToInt((point.x - startPoint.x) / cellSize),
                            Mathf.FloorToInt((point.y - startPoint.y) / cellSize));

            samples.Add(startPoint);
            active.Add(startPoint);
            grid.Add(getCell(startPoint), 0);

            while (active.Count > 0) {
                int activeIndex = UMLRandom.Range(0, active.Count);
                Annulus annulus = new Annulus(active[activeIndex], minRadius);

                bool generatedNewSample = false;
                for (int i = 0; i <= k; i++) {
                    bool valid = true;
                    Vector2 point = annulus.GeneratePoint();

                    if (!contains(point))
                        continue;

                    foreach (Cell cell in GetNearbyCells(getCell(point))) {
                        if (grid.TryGetValue(cell, out int index)) {
                            Vector2 testPoint = samples[index];
                            if (UML.Dst2(point, testPoint) < minRadiusSq) {
                                valid = false;
                                break;
                            }
                        }
                    }

                    if (valid) {
                        samples.Add(point);
                        active.Add(point);
                        grid.Add(getCell(point), samples.Count - 1);
                        generatedNewSample = true;
                        break;
                    }
                }

                if (!generatedNewSample)
                    active.RemoveAt(activeIndex);
            }

            return samples;
        }

        private static Cell[] GetNearbyCells(Cell center) {
            Cell[] nearbyCells = new Cell[25];
            int cellIndex = -1;
            for (int i = -2; i <= 2; i++)
                for (int j = -2; j <= 2; j++)
                    nearbyCells[++cellIndex] = center + new Cell(i, j);
            return nearbyCells;
        }

        private readonly struct Annulus {
            public readonly Vector2 center;
            public readonly float r;
            public float R => 2f * r;

            public Annulus(Vector2 center, float r) {
                this.center = center;
                this.r = r;
            }

            public Vector2 GeneratePoint() {
                float s = Mathf.Sqrt(UMLRandom.Range(R * R, 2f * R * R - r * r));
                float theta = UMLRandom.Range(0f, 2f * Mathf.PI);
                return center + new Vector2(s * Mathf.Cos(theta), s * Mathf.Sin(theta));
            }
        }

        private readonly struct Cell {
            public readonly int x, y;

            public Cell(int x, int y) {
                this.x = x;
                this.y = y;
            }

            public static Cell operator +(Cell a, Cell b)
                => new Cell(a.x + b.x, a.y + b.y);

            public override string ToString() => $"[Cell: ({x}, {y})]";

            public override bool Equals(object obj) {
                return obj is Cell cell &&
                       x == cell.x &&
                       y == cell.y;
            }

            public override int GetHashCode() {
                int hashCode = 373119288;
                hashCode = hashCode * -1521134295 + x.GetHashCode();
                hashCode = hashCode * -1521134295 + y.GetHashCode();
                return hashCode;
            }
        }
    }
}