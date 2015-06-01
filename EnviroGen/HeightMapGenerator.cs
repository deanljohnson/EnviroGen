using EnviroGen.Noise;

namespace EnviroGen
{
    public static class HeightMapGenerator
    {
        public static HeightMap GenerateHeightMap(int sizeX, int sizeY, int octaveCount, float roughness, float scale, int seed)
        {
            return new HeightMap(SimplexNoiseGenerator.GenerateNoiseArray(sizeX, sizeY, octaveCount, roughness, scale, seed));
        }
    }
}
