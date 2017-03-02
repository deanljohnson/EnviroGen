using EnviroGen.Noise;

namespace EnviroGen.HeightMaps
{
    public static class HeightMapGenerator
    {
        /// <summary>
        /// Returns a HeightMap based on the given GenerationOptions
        /// </summary>
        public static HeightMap GenerateHeightMap(GenerationOptions options)
        {
            return GenerateHeightMap(options.SizeX, options.SizeY, options.OctaveCount, options.Roughness, options.Frequency, options.Seed, options.NoiseType);
        }

        /// <summary>
        /// Returns a HeightMap based on the given parameters
        /// </summary>
        private static HeightMap GenerateHeightMap(int sizeX, int sizeY, int octaveCount, float gain, float frequency, int seed, NoiseType noiseType)
        {
            var arr = new float[sizeX, sizeY];

            for (var y = seed; y < sizeY + seed; y++)
            {
                for (var x = seed; x < sizeX + seed; x++)
                {
                    if (noiseType == NoiseType.Simplex)
                    {
                        arr[x - seed, y - seed] = FractalBrownianMotion.GenerateNoise(x, y, octaveCount, gain, frequency, 2f,
                        SimplexNoiseGenerator.Noise2D);
                    }
                }
            }

            var map = new HeightMap(arr);

            return map;
        }
    }
}
