using System;
using System.Collections.Generic;
using SFML.Window;

namespace EnviroGen
{
    static class ContinentGenerator
    {
        private static Random Random { get; set; }

        static ContinentGenerator()
        {
            Random = new Random();
        }

        public static void BuildContinents(HeightMap heightMap, int numContinents, int minSize, int maxSize)
        {
            List<Vector2i> startPoints;
            var mapSize = new Vector2i((int)heightMap.Size.X, (int)heightMap.Size.Y);

            if (numContinents != 1)
            {
                startPoints = GetRandomPoints(numContinents, mapSize.X, mapSize.Y);
            }
            else
            {
                startPoints = new List<Vector2i> { new Vector2i(mapSize.X / 2, mapSize.Y / 2) };
            }


            foreach (var start in startPoints)
            {
                var size = Random.Next(maxSize - minSize) + minSize;

                const float scale = 2.5f;
                var scaleStep = -1.5f / size;

                ScaleSquareAroundPoint(heightMap, start, Random.Next(maxSize - minSize) + minSize, scale, scaleStep);
            }
        }

        /// <summary>
        /// From a start point, scales a box of given size on the HeightMap. The start point will be scaled by the given
        /// scaleValue. scaleValue will then be incremented by scaleStep as we begin scaling points further from the start.
        /// </summary>
        private static void ScaleSquareAroundPoint(HeightMap heightMap, Vector2i start, int size, float scaleValue, float scaleStep)
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

        private static void MultiplyHeightAtPoint(HeightMap heightMap, int x, int y, float mul)
        {
            //if point is in bounds
            if (x > 0 && x < heightMap.Size.X && y > 0 && y < heightMap.Size.Y)
            {
                heightMap[x, y] *= mul;
            }
        }

        private static List<Vector2i> GetRandomPoints(int numPoints, int sizeX, int sizeY)
        {
            var points = new List<Vector2i>(numPoints);

            for (var i = 0; i < numPoints; i++)
            {
                points.Add(new Vector2i(Random.Next(sizeX), Random.Next(sizeY)));
            }

            return points;
        }
    }
}
