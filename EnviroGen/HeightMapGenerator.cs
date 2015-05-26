using System;
using SFML.Window;

namespace EnviroGen
{
    class HeightMapGenerator : PerlinNoiseGenerator
    {
        private readonly Random m_random = new Random();

        public float[,] HeightMap;

        public HeightMapGenerator(Vector2u size, int octaveCount)
            : base(size, octaveCount)
        {

        }

        public HeightMapGenerator(Vector2u size, int octaveCount, int seed)
            : base(size, octaveCount)
        {
            m_random = new Random(seed);
        }

        public HeightMapGenerator(Vector2u size, int octaveCount, Random random)
            : base(size, octaveCount)
        {
            m_random = random;
        }

        public void GenerateHeightMap()
        {
            HeightMap = GenerateNoise(m_random);
        }
    }
}
