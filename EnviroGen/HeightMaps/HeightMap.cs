﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EnviroGen.Internals;

namespace EnviroGen.HeightMaps
{
    public class HeightMap : IEnumerable<float>
    {
        private float[,] m_Map { get; }

        /// <summary>
        /// The dimensions of the HeightMap
        /// </summary>
        public IntPoint Size { get; }

        public HeightMap(float[,] map)
        {
            Size = new IntPoint(map.GetLength(0), map.GetLength(1));
            m_Map = map;
        }

        public HeightMap(HeightMap map)
            : this(map.m_Map)
        {
        }

        public virtual float this[int x, int y]
        {
            get { return m_Map[x, y]; }
            set { m_Map[x, y] = value; }
        }

        IEnumerator<float> IEnumerable<float>.GetEnumerator()
        {
            return m_Map.OfType<float>().GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return m_Map.GetEnumerator();
        }

        public virtual void Normalize(float min = 0f, float max = 1f)
        {
            var maxValue = m_Map[0, 0];
            var minValue = m_Map[0, 0];

            foreach (var h in m_Map)
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
                    m_Map[x, y] = (scaleDif * (m_Map[x, y] - minValue)) / (valueDif) + min;
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
        /*public void CombineWith(HeightMap other)
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
                    m_Map[x, y] += (other.m_Map[x - offset.X, y - offset.Y] * weight);
                }
            }
        }*/

        /// <summary>
        /// Takes this HeightMap and truncates the float values to return an int[,]
        /// </summary>
        public int[,] HeightMapToIntegers()
        {
            var intMap = new int[Size.X, Size.Y];
            for (var y = 0; y < Size.Y; y++)
            {
                for (var x = 0; x < Size.X; x++)
                {
                    intMap[x, y] = (int)this[x, y];
                }
            }

            return intMap;
        }

        /// <summary>
        /// Returns a new Terrain instance with dimensions equal to the greatest multiple 
        /// of m less than or equal to this Terrain's current dimensions.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public HeightMap SizeTruncatedToMultiple(int m)
        {
            var width = Size.X - (Size.X % m);
            var height = Size.Y - (Size.Y % m);

            var heights = new float[width, height];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    heights[x, y] = this[x, y];
                }
            }

            return new HeightMap(heights);
        }
    }
}
