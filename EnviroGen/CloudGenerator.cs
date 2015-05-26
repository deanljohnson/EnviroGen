using System;
using SFML.Window;

namespace EnviroGen
{
    class CloudGenerator : PerlinNoiseGenerator
    {
        private readonly Random m_random = new Random();

        public float[,] CloudMap;

        public CloudGenerator(Vector2u size, int octaveCount)
            : base(size, octaveCount)
        {
        }

        public CloudGenerator(Vector2u size, int octaveCount, int seed)
            : base(size, octaveCount)
        {
            m_random = new Random(seed);
        }

        public CloudGenerator(Vector2u size, int octaveCount, Random random)
            : base(size, octaveCount)
        {
            m_random = random;
        }

        public void GenerateCloudMap()
        {
            CloudMap = GenerateNoise(m_random);
        }
    }
}
