using System;
using System.Collections.Generic;
using EnviroGen.HeightMaps;
using EnviroGen.Internals;

namespace EnviroGen.Erosion
{
    /// <summary>
    /// Erodes a height map. Tends to make it look very smooth and washed out
    /// </summary>
    public class ThermalEroder : IEroder
    {
        /// <summary>
        /// The number of times to run the erosion process.
        /// </summary>
        public int Iterations { get; set; }
        /// <summary>
        /// The slope of the terrain that will cause erosion.
        /// </summary>
        public float TalusAngle { get; set; }

        public virtual void Erode(HeightMap heightMap)
        {
            for (var i = 0; i < Iterations; i++)
            {
                for (var y = 0; y < heightMap.Size.Y; y++)
                {
                    for (var x = 0; x < heightMap.Size.X; x++)
                    {
                        var neighbors = heightMap.GetVonNeumannNeighbors(x, y);
                        float highestSlope;
                        var highestSlopedNeighbor = GetHighestSlopedNeighbor(heightMap, neighbors, heightMap[x, y], out highestSlope);

                        if (highestSlope > TalusAngle)
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
        internal static IntPoint GetHighestSlopedNeighbor(HeightMap heightMap, IReadOnlyList<IntPoint> neighbors, float height, out float slope)
        {
            var slopedNeighbor = neighbors[0];
            var highestSlope = 0f;

            foreach (var neighbor in neighbors)
            {
                var currentSlope = Math.Abs(height - heightMap[neighbor.X, neighbor.Y]);
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
