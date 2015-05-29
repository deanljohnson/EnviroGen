using System;
using System.Collections.Generic;
using System.Diagnostics;
using SFML.Window;

namespace EnviroGen
{
    class HeightMapGenerator : PerlinNoiseGenerator
    {
        private readonly Random m_random = new Random();

        public int NumContinents { get; set; }
        public int MinContinentSize { get; set; }
        public int MaxContinentSize { get; set; }

        public float[,] HeightMap { get; private set; }

        public HeightMapGenerator(Vector2i size, int octaveCount)
            : base(size, octaveCount)
        {

        }

        public HeightMapGenerator(Vector2i size, int octaveCount, int seed)
            : base(size, octaveCount)
        {
            m_random = new Random(seed);
        }

        public HeightMapGenerator(Vector2i size, int octaveCount, Random random)
            : base(size, octaveCount)
        {
            m_random = random;
        }

        public void GenerateHeightMap()
        {
            GenerateHeightMap(NumContinents, MinContinentSize, MaxContinentSize);
        }

        public void GenerateHeightMap(int numContinents, int minContinentSize, int maxContinentSize)
        {
            HeightMap = GenerateNoise(m_random);
            DebugVerifyHeightMapSize();

            BuildContinents(numContinents, minContinentSize, maxContinentSize);

            NormalizeHeightMap();
        }

        private void BuildContinents(int numContinents, int minSize, int maxSize)
        {
            var startPoints = GetRandomPoints(numContinents);
            //startPoints = new List<Vector2i> {new Vector2i(700, 400)};
            foreach (var start in startPoints)
            {
                var size = m_random.Next(maxSize - minSize) + minSize;
                /*
                var scale = 1f / HeightMap[start.X, start.Y];
                var scaleStep = -(scale - 1f) / size;*/
                
                var scale = 2.5f;
                var scaleStep = -1.5f / size;
                

                ScaleSquareAroundPoint(start, m_random.Next(maxSize - minSize) + minSize, scale, scaleStep);
            }
        }

        /// <summary>
        /// From a start point, scales a box of given size on the HeightMap. The start point will be scaled by the given
        /// scaleValue. scaleValue will then be incremented by scaleStep as we begin scaling points further from the start.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <param name="scaleValue"></param>
        /// <param name="scaleStep"></param>
        private void ScaleSquareAroundPoint(Vector2i start, int size, float scaleValue, float scaleStep)
        {
            MultiplyHeightAtPoint(start.X, start.Y, scaleValue);

            for (var d = 1; d < size; d++, scaleValue += scaleStep)
            {
                //Corner values
                MultiplyHeightAtPoint(start.X - d, start.Y - d, scaleValue);
                MultiplyHeightAtPoint(start.X + d, start.Y - d, scaleValue);
                MultiplyHeightAtPoint(start.X - d, start.Y + d, scaleValue);
                MultiplyHeightAtPoint(start.X + d, start.Y + d, scaleValue);

                //Middle Edge values
                MultiplyHeightAtPoint(start.X - d, start.Y, scaleValue);
                MultiplyHeightAtPoint(start.X, start.Y - d, scaleValue);
                MultiplyHeightAtPoint(start.X, start.Y + d, scaleValue);
                MultiplyHeightAtPoint(start.X + d, start.Y, scaleValue);

                //Left & Right Sides
                for (var y = 1; y < d; y++)
                {
                    MultiplyHeightAtPoint(start.X - d, start.Y - y, scaleValue);
                    MultiplyHeightAtPoint(start.X - d, start.Y + y, scaleValue);
                    MultiplyHeightAtPoint(start.X + d, start.Y - y, scaleValue);
                    MultiplyHeightAtPoint(start.X + d, start.Y + y, scaleValue);
                }

                //Top & Bottom Sides
                for (var x = 1; x < d; x++)
                {
                    MultiplyHeightAtPoint(start.X - x, start.Y - d, scaleValue);
                    MultiplyHeightAtPoint(start.X + x, start.Y - d, scaleValue);
                    MultiplyHeightAtPoint(start.X - x, start.Y + d, scaleValue);
                    MultiplyHeightAtPoint(start.X + x, start.Y + d, scaleValue);
                }
            }
        }

        private void MultiplyHeightAtPoint(int x, int y, float mul)
        {
            //if point is in bounds
            if (x > 0 && x < HeightMap.GetLength(0) && y > 0 && y < HeightMap.GetLength(1))
            {
                HeightMap[x, y] *= mul;
            }
        }

        private IEnumerable<Vector2i> GetRandomPoints(int numPoints)
        {
            var points = new List<Vector2i>(numPoints);

            for (var i = 0; i < numPoints; i++)
            {
                points.Add(new Vector2i(m_random.Next(Size.X), m_random.Next(Size.Y)));
            }

            return points;
        }

        private void NormalizeHeightMap()
        {
            var maxValue = HeightMap[0, 0];
            var minValue = HeightMap[0, 0];

            foreach (var h in HeightMap)
            {
                maxValue = h > maxValue ? h : maxValue;
                minValue = h < minValue ? h : minValue;
            }

            var dif = maxValue - minValue;

            for (var y = 0; y < HeightMap.GetLength(1); y++)
            {
                for (var x = 0; x < HeightMap.GetLength(0); x++)
                {
                    HeightMap[x, y] = (HeightMap[x, y] - minValue) / (dif);
                }
            }

            DebugVerifyNormalization();
        }

        [Conditional("DEBUG")]
        private void DebugVerifyHeightMapSize()
        {
            if (HeightMap.GetLength(0) != Size.X || HeightMap.GetLength(1) != Size.Y)
            {
                throw new Exception("HeightMap size does not match Size variable, most likely an error in perlin noise generation.");
            }
        }

        [Conditional("DEBUG")]
        private void DebugVerifyNormalization()
        {
            foreach (var f in HeightMap)
            {
                if (f < 0f || f > 1f)
                {
                    throw new Exception("HeightMap contains value not in the [0,1] range, error in normalization");
                }
            }
        }
    }
}
