﻿using EnviroGen.Noise;

namespace EnviroGen.HeightMaps
{
    public static class HeightMapGenerator
    {
        public static HeightMap GenerateHeightMap(int sizeX, int sizeY, int octaveCount, float roughness, float frequency, int seed, bool ridged = false)
        {
            return new HeightMap(SimplexNoiseGenerator.GenerateNoiseArray(sizeX, sizeY, octaveCount, roughness, frequency, seed, ridged));
        }
    }
}
