using System;
using System.Collections.Generic;
using System.Linq;
using AStar;
using SFML.Window;

namespace EnviroGen.RiverGen
{
    public static class RiverGenerator
    {
        private static readonly Random Random = new Random();
        private static HeightMap HeightMap { get; set; }

        /// <summary>
        /// Generates a desired number of rivers on the given height map, returning a list of all river tiles.
        /// Will deform the land around the rivers.
        /// </summary>
        public static List<Vector2i> GenerateRivers(HeightMap heightMap, int numRivers, float riverStartHeightMin, float riverStartHeightMax, float seaLevel)
        {
            HeightMap = heightMap;

            var riverTiles = new List<Vector2i>();

            var startPoints = GetStartPoints(numRivers, riverStartHeightMin, riverStartHeightMax);

            foreach (var start in startPoints)
            {
                riverTiles.AddRange(GenerateRiver(start, seaLevel));
            }

            return riverTiles;
        }

        private static List<Vector2i> GetStartPoints(int numRivers, float riverStartMin, float riverStartMax)
        {
            var startPoints = new List<Vector2i>();

            while (startPoints.Count < numRivers)
            {
                var point = new Vector2i(Random.Next((int)HeightMap.Size.X), Random.Next((int)HeightMap.Size.Y));
                var height = HeightMap[point];

                if (height > riverStartMin && height < riverStartMax)
                {
                    startPoints.Add(point);
                }
            }

            return startPoints;
        }

        private static List<Vector2i> GenerateRiver(Vector2i startPoint, float seaLevel)
        {
            var numEndPointsToHave = 10;
            var possibleEndPoints = GetRiverEndPoints(startPoint, numEndPointsToHave, seaLevel);

            var riverTiles = AStar<Vector2i>.PathFind(startPoint, possibleEndPoints, GetNeighbors, DistanceBetweenNodes);

            return riverTiles.ToList();
        }

        private static List<Vector2i> GetRiverEndPoints(Vector2i startPoint, int numEndPointsToHave, float seaLevel)
        {
            var endPoints = new List<Vector2i>();
            var d = 0;
            while (endPoints.Count < numEndPointsToHave)
            {
                d++;

                //Corner Values
                if (HeightMap[startPoint.X - d, startPoint.Y - d] < seaLevel) endPoints.Add(new Vector2i(startPoint.X - d, startPoint.Y - d));
                if (HeightMap[startPoint.X + d, startPoint.Y - d] < seaLevel) endPoints.Add(new Vector2i(startPoint.X + d, startPoint.Y - d));
                if (HeightMap[startPoint.X - d, startPoint.Y + d] < seaLevel) endPoints.Add(new Vector2i(startPoint.X - d, startPoint.Y + d));
                if (HeightMap[startPoint.X + d, startPoint.Y + d] < seaLevel) endPoints.Add(new Vector2i(startPoint.X + d, startPoint.Y + d));
                
                //Middle Edge Values
                if (HeightMap[startPoint.X - d, startPoint.Y] < seaLevel) endPoints.Add(new Vector2i(startPoint.X - d, startPoint.Y));
                if (HeightMap[startPoint.X, startPoint.Y - d] < seaLevel) endPoints.Add(new Vector2i(startPoint.X, startPoint.Y - d));
                if (HeightMap[startPoint.X, startPoint.Y + d] < seaLevel) endPoints.Add(new Vector2i(startPoint.X, startPoint.Y + d));
                if (HeightMap[startPoint.X + d, startPoint.Y] < seaLevel) endPoints.Add(new Vector2i(startPoint.X + d, startPoint.Y));

                //Left & Right Sides
                for (var y = 1; y < d; y++)
                {
                    if (HeightMap[startPoint.X - d, startPoint.Y - y] < seaLevel) endPoints.Add(new Vector2i(startPoint.X - d, startPoint.Y - y));
                    if (HeightMap[startPoint.X - d, startPoint.Y + y] < seaLevel) endPoints.Add(new Vector2i(startPoint.X - d, startPoint.Y + y));
                    if (HeightMap[startPoint.X + d, startPoint.Y - y] < seaLevel) endPoints.Add(new Vector2i(startPoint.X + d, startPoint.Y - y));
                    if (HeightMap[startPoint.X + d, startPoint.Y + y] < seaLevel) endPoints.Add(new Vector2i(startPoint.X + d, startPoint.Y + y));
                }

                //Top & Bottom Sides
                for (var x = 1; x < d; x++)
                {
                    if (HeightMap[startPoint.X - x, startPoint.Y - d] < seaLevel) endPoints.Add(new Vector2i(startPoint.X - x, startPoint.Y - d));
                    if (HeightMap[startPoint.X + x, startPoint.Y - d] < seaLevel) endPoints.Add(new Vector2i(startPoint.X + x, startPoint.Y - d));
                    if (HeightMap[startPoint.X - x, startPoint.Y + d] < seaLevel) endPoints.Add(new Vector2i(startPoint.X - x, startPoint.Y + d));
                    if (HeightMap[startPoint.X + x, startPoint.Y + d] < seaLevel) endPoints.Add(new Vector2i(startPoint.X + x, startPoint.Y + d));
                }
            }

            return endPoints;
        }

        private static List<Vector2i> GetNeighbors(Vector2i node)
        {
            var neighbors = new List<Vector2i>();
            var dimensions = new Vector2i((int)HeightMap.Size.X, (int)HeightMap.Size.Y);

            if (node.X > 1)
            {
                neighbors.Add(new Vector2i(node.X - 1, node.Y));
                if (node.Y > 1) neighbors.Add(new Vector2i(node.X - 1, node.Y - 1));
                if (node.Y < dimensions.Y - 1) neighbors.Add(new Vector2i(node.X - 1, node.Y + 1));
            }
            if (node.X < dimensions.X - 1)
            {
                neighbors.Add(new Vector2i(node.X + 1, node.Y));
                if (node.Y > 1) neighbors.Add(new Vector2i(node.X + 1, node.Y - 1));
                if (node.Y < dimensions.Y - 1) neighbors.Add(new Vector2i(node.X + 1, node.Y + 1));
            }
            if (node.Y > 1) neighbors.Add(new Vector2i(node.X, node.Y - 1));
            if (node.Y < dimensions.Y - 1) neighbors.Add(new Vector2i(node.X, node.Y + 1));

            return neighbors;
        }

        private static double DistanceBetweenNodes(Vector2i a, Vector2i b)
        {
            var aHeight = HeightMap[a];
            var bHeight = HeightMap[b];
            var heightDif = Math.Abs(aHeight - bHeight);
            var dist = Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));

            return dist + (heightDif * 2f);
        }
    }
}
