using System;
using System.Collections.Generic;
using SFML.Window;

namespace EnviroGen.Erosion
{
    public class ThermalErosion
    {
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
                        var highestSlopedNeighbor = GetHighestSlopedNeighbor(heightMap, x, y, neighbors, out highestSlope);

                        if (highestSlope > talusAngle)
                        {
                            var heightDif = heightMap[x, y] - heightMap[highestSlopedNeighbor];
                            heightMap[x, y] -= heightDif / 2f;
                            heightMap[highestSlopedNeighbor] += heightDif / 2f;
                        }
                    }
                }
            }
        }

        internal static Vector2i GetHighestSlopedNeighbor(HeightMap heightMap, int x, int y, IReadOnlyList<Vector2i> neighbors, out float slope)
        {
            var slopedNeighbor = neighbors[0];
            var highestSlope = 0f;

            foreach (var neighbor in neighbors)
            {
                var currentSlope = Math.Abs(heightMap[x, y] - heightMap[neighbor.X, neighbor.Y]);
                if (currentSlope > highestSlope)
                {
                    highestSlope = currentSlope;
                    slopedNeighbor = neighbor;
                }
            }

            slope = highestSlope;
            return slopedNeighbor;
        }
    }
}
