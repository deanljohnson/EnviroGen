using System;
using System.Collections.Generic;
using EnviroGen.HeightMaps;
using EnviroGen.Internals;

namespace EnviroGen.Continents
{
    public class SquareContinentGenerator : IContinentGenerator
    {
        private static readonly Random Random = new Random();

        public int MinimumContinentSize { get; set; }
        public int MaximumContinentSize { get; set; }
        public float ScaleAmount { get; set; }

        public void GenerateContinents(HeightMap heightMap)
        {
            var mapSize = new IntPoint(heightMap.Size.X, heightMap.Size.Y);
            var startPoints = new List<IntPoint> { new IntPoint(mapSize.X / 2, mapSize.Y / 2) };

            foreach (var start in startPoints)
            {
                var size = Random.Next(MaximumContinentSize - MinimumContinentSize) + MinimumContinentSize;
                var scaleStep = -(ScaleAmount - 1f) / size;

                ScaleSquareAroundPoint(heightMap, start, size, ScaleAmount, scaleStep);
            }
        }

        /// <summary>
        /// From a start point, scales a box of given size on the HeightMap. The start point will be scaled by the given
        /// scaleValue. scaleValue will then be incremented by scaleStep as we begin scaling points further from the start.
        /// </summary>
        private static void ScaleSquareAroundPoint(HeightMap heightMap, IntPoint start, int size, float scaleValue, float scaleStep)
        {
            MultiplyHeightAtPoint(heightMap, start.X, start.Y, scaleValue);

            for (var d = 1; d < size + 1; d++, scaleValue += scaleStep)
            {
                //Corner values
                MultiplyHeightAtPoint(heightMap, start.X - d, start.Y - d, scaleValue);
                MultiplyHeightAtPoint(heightMap, start.X + d, start.Y - d, scaleValue);
                MultiplyHeightAtPoint(heightMap, start.X - d, start.Y + d, scaleValue);
                MultiplyHeightAtPoint(heightMap, start.X + d, start.Y + d, scaleValue);

                //Middle Edge values
                MultiplyHeightAtPoint(heightMap, start.X - d, start.Y, scaleValue);
                MultiplyHeightAtPoint(heightMap, start.X, start.Y - d, scaleValue);
                MultiplyHeightAtPoint(heightMap, start.X, start.Y + d, scaleValue);
                MultiplyHeightAtPoint(heightMap, start.X + d, start.Y, scaleValue);

                //Left & Right Sides
                for (var y = 1; y < d; y++)
                {
                    MultiplyHeightAtPoint(heightMap, start.X - d, start.Y - y, scaleValue);
                    MultiplyHeightAtPoint(heightMap, start.X - d, start.Y + y, scaleValue);
                    MultiplyHeightAtPoint(heightMap, start.X + d, start.Y - y, scaleValue);
                    MultiplyHeightAtPoint(heightMap, start.X + d, start.Y + y, scaleValue);
                }

                //Top & Bottom Sides
                for (var x = 1; x < d; x++)
                {
                    MultiplyHeightAtPoint(heightMap, start.X - x, start.Y - d, scaleValue);
                    MultiplyHeightAtPoint(heightMap, start.X + x, start.Y - d, scaleValue);
                    MultiplyHeightAtPoint(heightMap, start.X - x, start.Y + d, scaleValue);
                    MultiplyHeightAtPoint(heightMap, start.X + x, start.Y + d, scaleValue);
                }
            }
        }

        /// <summary>
        /// Multiplies the height at the given point by mul, with bounds checking.
        /// If the point is out of bounds, the method simply returns.
        /// </summary>
        /// <param name="heightMap"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="mul"></param>
        private static void MultiplyHeightAtPoint(HeightMap heightMap, int x, int y, float mul)
        {
            //if point is in bounds
            if (x > 0 && x < heightMap.Size.X && y > 0 && y < heightMap.Size.Y)
            {
                heightMap[x, y] *= mul;
            }
        }
    }
}
