using EnviroGen.Noise;

namespace EnviroGen
{
    public static class HeightMapGenerator
    {
        public static HeightMap GenerateHeightMap(int sizeX, int sizeY, int octaveCount, float roughness, float frequency, int seed)
        {
            return new HeightMap(SimplexNoiseGenerator.GenerateNoiseArray(sizeX, sizeY, octaveCount, roughness, frequency, seed));
        }
    }
}
