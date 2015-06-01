using EnviroGen.Noise;

namespace EnviroGen
{
    public static class HeightMapGenerator
    {
        public static HeightMap GenerateHeightMap(int sizeX, int sizeY, int octaveCount, int numContinents, int minContinentSize, int maxContinentSize, int seed)
        {
            return new HeightMap(SimplexNoiseGenerator.GenerateNoiseArray(sizeX, sizeY, octaveCount, .55f, .01f, seed));
        }
    }
}
