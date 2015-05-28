using System;
using System.Collections.Generic;
using SFML.Window;

namespace EnviroGen
{
    class RiverGenerator
    {
        private static Random Random = new Random();
        /// <summary>
        /// Generates a desired number of rivers on the given height map, returning a list of all river tiles.
        /// Will deform the land around the rivers.
        /// </summary>
        /// <param name="heightMap"></param>
        /// <param name="numRivers"></param>
        /// <returns></returns>
        public static List<Vector2i> GenerateRivers(float[,] heightMap, int numRivers, float riverStartHeightMin, float riverStartHeightMax)
        {
            var riverTiles = new List<Vector2i>();

            var startPoints = GetStartPoints(heightMap, numRivers, riverStartHeightMin, riverStartHeightMax);

            foreach (var start in startPoints)
            {
                riverTiles.AddRange(GenerateRiver(heightMap, start));
            }

            return riverTiles;
        }

        private static List<Vector2i> GetStartPoints(float[,] heightMap, int numRivers, float riverStartMin, float riverStartMax)
        {
            var startPoints = new List<Vector2i>();

            while (startPoints.Count < numRivers)
            {
                var point = new Vector2i(Random.Next(heightMap.GetLength(0)), Random.Next(heightMap.GetLength(1)));
                var height = heightMap[point.X, point.Y];

                if (height > riverStartMin && height < riverStartMax)
                {
                    startPoints.Add(point);
                }
            }

            return startPoints;
        }

        private static List<Vector2i> GenerateRiver(float[,] heightMap, Vector2i startPoint)
        {
            var riverTiles = new List<Vector2i> { startPoint };



            return riverTiles;
        }
    }
}
