using System;
using System.Collections;
using System.Collections.Generic;
using EnviroGen.Internals;

namespace EnviroGen.HeightMaps
{
    public class HeightMap : IEnumerable
    {
        /// <summary>
        /// The height values associated with this HeightMap
        /// </summary>
        public float[,] Map { get; set; }

        /// <summary>
        /// The dimensions of the HeightMap
        /// </summary>
        public IntPoint Size { get; }

        public HeightMap(float[,] map)
        {
            Size = new IntPoint(map.GetLength(0), map.GetLength(1));
            Map = map;
        }

        public float this[uint x, uint y]
        {
            get { return Map[x, y]; }
            set { Map[x, y] = value; }
        }

        public float this[int x, int y]
        {
            get { return Map[x, y]; }
            set { Map[x, y] = value; }
        }

        public float this[IntPoint v]
        {
            get { return Map[v.X, v.Y]; }
            set { Map[v.X, v.Y] = value; }
        }

        public IEnumerator GetEnumerator()
        {
            return Map.GetEnumerator();
        }

        public void Normalize(float min = 0f, float max = 1f)
        {
            var maxValue = Map[0, 0];
            var minValue = Map[0, 0];

            foreach (var h in Map)
            {
                maxValue = h > maxValue ? h : maxValue;
                minValue = h < minValue ? h : minValue;
            }

            var valueDif = maxValue - minValue;
            var scaleDif = max - min;

            for (uint y = 0; y < Size.Y; y++)
            {
                for (uint x = 0; x < Size.X; x++)
                {
                    Map[x, y] = (scaleDif * (Map[x, y] - minValue)) / (valueDif) + min;
                }
            }
        }

        /// <summary>
        /// Returns the Vector2i's to the left, right, top, and bottom of the given point.
        /// </summary>
        public List<IntPoint> GetVonNeumannNeighbors(IntPoint point)
        {
            return GetVonNeumannNeighbors(point.X, point.Y);
        }

        /// <summary>
        /// Returns the Vector2i's to the left, right, top, and bottom of the given x,y value.
        /// </summary>
        public List<IntPoint> GetVonNeumannNeighbors(int x, int y)
        {
            var points = new List<IntPoint>();

            if (x > 0) points.Add(new IntPoint(x - 1, y));
            if (x < Size.X - 1) points.Add(new IntPoint(x + 1, y));
            if (y > 0) points.Add(new IntPoint(x, y - 1));
            if (y < Size.Y - 1) points.Add(new IntPoint(x, y + 1));

            return points;
        }

        /// <summary>
        /// Returns all points adjacent to the given point as Vector2i's.
        /// </summary>
        public List<IntPoint> GetMooreNeighbors(IntPoint point)
        {
            return GetMooreNeighbors(point.X, point.Y);
        }

        /// <summary>
        /// Returns all points adjacent to the given x and y positions as Vector2i's.
        /// </summary>
        public List<IntPoint> GetMooreNeighbors(int x, int y)
        {
            var points = new List<IntPoint>();

            if (x > 0) points.Add(new IntPoint(x - 1, y));
            if (x < Size.X - 1) points.Add(new IntPoint(x + 1, y));
            if (y > 0) points.Add(new IntPoint(x, y - 1));
            if (y < Size.Y - 1) points.Add(new IntPoint(x, y + 1));

            if (x > 0 && y > 0) points.Add(new IntPoint(x - 1, y - 1));
            if (x < Size.X - 1 && y > 0) points.Add(new IntPoint(x + 1, y - 1));
            if (x > 0 && y < Size.Y - 1) points.Add(new IntPoint(x - 1, y + 1));
            if (x < Size.X - 1 && y < Size.Y - 1) points.Add(new IntPoint(x + 1, y + 1));

            return points;
        }

        /// <summary>
        /// Combines this HeightMap with the given HeightMap, with equal weight given to each HeightMap
        /// </summary>
        public void CombineWith(HeightMap other)
        {
            CombineWith(other, new IntPoint());
        }

        /// <summary>
        /// Combines this HeightMap with the given HeightMap. Modifies the given HeightMap by the given weight value before combining.
        /// </summary>
        public void CombineWith(HeightMap other, float weight)
        {
            CombineWith(other, new IntPoint(), weight);
        }

        /// <summary>
        /// Combines this HeightMap with the given HeightMap, starting at the given offset in this HeightMap.
        /// Modifies the given HeightMap by the given weight value before combining.
        /// Normalizes this HeightMap [0,1].
        /// </summary>
        public void CombineWith(HeightMap other, IntPoint offset, float weight = 1f)
        {
            var maxX = Math.Min(Size.X, offset.X + other.Size.X);
            var maxY = Math.Min(Size.Y, offset.Y + other.Size.Y);

            for (var y = offset.Y; y < maxY; y++)
            {
                for (var x = offset.X; x < maxX; x++)
                {
                    Map[x, y] += (other.Map[x - offset.X, y - offset.Y] * weight);
                }
            }

            Normalize();
        } 
    }
}
