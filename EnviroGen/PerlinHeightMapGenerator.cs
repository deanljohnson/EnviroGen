using System;
using SFML.Window;

namespace EnviroGen
{
    class PerlinHeightMapGenerator : PerlinNoiseGenerator
    {
        private readonly Random m_random = new Random();

        public float[,] HeightMap { get; private set; }

        public PerlinHeightMapGenerator(Vector2i size, int octaveCount)
            : base(size, octaveCount)
        {

        }

        public PerlinHeightMapGenerator(Vector2i size, int octaveCount, int seed)
            : base(size, octaveCount)
        {
            m_random = new Random(seed);
        }

        public PerlinHeightMapGenerator(Vector2i size, int octaveCount, Random random)
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
