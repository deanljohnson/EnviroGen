﻿using System.Collections;
using SFML.Window;

namespace EnviroGen
{
    public class HeightMap : IEnumerable
    {
        private float[,] Map { get; set; }

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

        public IEnumerator GetEnumerator()
        {
            return Map.GetEnumerator();
        }

        public void Normalize()
        {
            var maxValue = Map[0, 0];
            var minValue = Map[0, 0];

            foreach (var h in Map)
            {
                maxValue = h > maxValue ? h : maxValue;
                minValue = h < minValue ? h : minValue;
            }

            var dif = maxValue - minValue;

            for (uint y = 0; y < Size.X; y++)
            {
                for (uint x = 0; x < Size.Y; x++)
                {
                    Map[x, y] = (Map[x, y] - minValue) / (dif);
                }
            }
        }
        }
    }
}