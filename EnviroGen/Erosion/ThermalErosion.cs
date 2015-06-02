using System;
using System.Collections.Generic;
using SFML.Window;

namespace EnviroGen.Erosion
{
    public class ThermalErosion
    {
        /// <summary>
        /// Erodes the given HeightMap based on the idea of gravity leveling out steep areas.
        /// Will completely remove cliffs.
        /// </summary>
        public static void Erode(HeightMap heightMap, float talusAngle, int iterations)
        {
            for (var i = 0; i < iterations; i++)
            {
                for (var y = 0; y < heightMap.Size.Y; y++)
                {
                    for (var x = 0; x < heightMap.Size.X; x++)
                    {
                        var neighbors = heightMap.GetVonNeumannNeighbors(x, y);
                        float highestSlope;
                        var highestSlopedNeighbor = GetHighestSlopedNeighbor(heightMap, neighbors, heightMap[x, y], out highestSlope);

                        if (highestSlope > talusAngle)
                        {
                            BalanceHeightsAtPoints(heightMap, x, y, highestSlopedNeighbor.X, highestSlopedNeighbor.Y);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns the index of the neighboring height value that most differs from the given height.
        /// </summary>
        internal static Vector2i GetHighestSlopedNeighbor(HeightMap heightMap, IReadOnlyList<Vector2i> neighbors, float height, out float slope)
        {
            var slopedNeighbor = neighbors[0];
            var highestSlope = 0f;

            foreach (var neighbor in neighbors)
            {
                var currentSlope = Math.Abs(height - heightMap[neighbor]);
                if (currentSlope > highestSlope)
                {
                    highestSlope = currentSlope;
                    slopedNeighbor = neighbor;
                }
            }

            slope = highestSlope;
            return slopedNeighbor;
        }

        /// <summary>
        /// Makes the heights of the given coordinates equal by subtracting from the higher and adding to the lower.
        /// </summary>
        internal static void BalanceHeightsAtPoints(HeightMap heightMap, int x1, int y1, int x2, int y2)
        {
            var heightDif = heightMap[x1, y1] - heightMap[x2, y2];

            //Balance this point and the neighboring point in height.
            heightMap[x1, y1] -= heightDif / 2f;
            heightMap[x2, y2] += heightDif / 2f;
        }
    }
}
