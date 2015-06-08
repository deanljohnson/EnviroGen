using System;
using System.Collections;
using System.Collections.Generic;
using SFML.Window;

namespace EnviroGen.HeightMaps
{
    public class HeightMap : IEnumerable
    {
        public float[,] Map { get; set; }

        public Vector2u Size { get; private set; }

        public HeightMap(float[,] map)
        {
            Size = new Vector2u((uint)map.GetLength(0), (uint)map.GetLength(1));
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

        public float this[Vector2i v]
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
        public List<Vector2i> GetVonNeumannNeighbors(Vector2i point)
        {
            return GetVonNeumannNeighbors(point.X, point.Y);
        }

        /// <summary>
        /// Returns the Vector2i's to the left, right, top, and bottom of the given x,y value.
        /// </summary>
        public List<Vector2i> GetVonNeumannNeighbors(int x, int y)
        {
            var points = new List<Vector2i>();

            if (x > 0) points.Add(new Vector2i(x - 1, y));
            if (x < Size.X - 1) points.Add(new Vector2i(x + 1, y));
            if (y > 0) points.Add(new Vector2i(x, y - 1));
            if (y < Size.Y - 1) points.Add(new Vector2i(x, y + 1));

            return points;
        }

        /// <summary>
        /// Returns all points adjacent to the given point as Vector2i's.
        /// </summary>
        public List<Vector2i> GetMooreNeighbors(Vector2i point)
        {
            return GetMooreNeighbors(point.X, point.Y);
        }

        /// <summary>
        /// Returns all points adjacent to the given x and y positions as Vector2i's.
        /// </summary>
        public List<Vector2i> GetMooreNeighbors(int x, int y)
        {
            var points = new List<Vector2i>();

            if (x > 0) points.Add(new Vector2i(x - 1, y));
            if (x < Size.X - 1) points.Add(new Vector2i(x + 1, y));
            if (y > 0) points.Add(new Vector2i(x, y - 1));
            if (y < Size.Y - 1) points.Add(new Vector2i(x, y + 1));

            if (x > 0 && y > 0) points.Add(new Vector2i(x - 1, y - 1));
            if (x < Size.X - 1 && y > 0) points.Add(new Vector2i(x + 1, y - 1));
            if (x > 0 && y < Size.Y - 1) points.Add(new Vector2i(x - 1, y + 1));
            if (x < Size.X - 1 && y < Size.Y - 1) points.Add(new Vector2i(x + 1, y + 1));

            return points;
        }

        public void CombineWith(HeightMap other)
        {
            CombineWith(other, new Vector2i(0, 0));
        }

        public void CombineWith(HeightMap other, float weight)
        {
            CombineWith(other, new Vector2i(0, 0), weight);
        }

        public void CombineWith(HeightMap other, Vector2i offset, float weight = 1f)
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
