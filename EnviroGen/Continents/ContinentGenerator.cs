using System;
using System.Collections.Generic;
using EnviroGen.HeightMaps;
using EnviroGen.Internals;

namespace EnviroGen.Continents
{
    public static class ContinentGenerator
    {
        private static Random m_Random { get; }

        static ContinentGenerator()
        {
            m_Random = new Random();
        }

        public static void BuildContinents(HeightMap heightMap, ContinentGenerationData data)
        {
            BuildContinents(heightMap, data.NumContinents, data.MinimumContinentSize, data.MinimumContinentSize, data.Scale);
        }

        /// <summary>
        /// Scales square areas on the given HeightMap to try and make more continent like shapes.
        /// </summary>
        private static void BuildContinents(HeightMap heightMap, int numContinents, int minSize, int maxSize, float scale)
        {
            List<IntPoint> startPoints;
            var mapSize = new IntPoint((int)heightMap.Size.X, (int)heightMap.Size.Y);

            if (numContinents != 1)
            {
                startPoints = GetRandomPoints(numContinents, mapSize.X, mapSize.Y);
            }
            else
            {
                startPoints = new List<IntPoint> { new IntPoint(mapSize.X / 2, mapSize.Y / 2) };
            }


            foreach (var start in startPoints)
            {
                var size = m_Random.Next(maxSize - minSize) + minSize;
                var scaleStep = -(scale - 1f) / size;

                ScaleSquareAroundPoint(heightMap, start, m_Random.Next(maxSize - minSize) + minSize, scale, scaleStep);
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

        /// <summary>
        /// Returns a List of Vector2i with numPoints of random indices.
        /// </summary>
        /// <param name="numPoints"></param>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        /// <returns></returns>
        private static List<IntPoint> GetRandomPoints(int numPoints, int sizeX, int sizeY)
        {
            var points = new List<IntPoint>(numPoints);

            for (var i = 0; i < numPoints; i++)
            {
                points.Add(new IntPoint(m_Random.Next(sizeX), m_Random.Next(sizeY)));
            }

            return points;
        }
    }
}
