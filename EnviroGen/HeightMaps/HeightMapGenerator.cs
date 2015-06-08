using System;
using EnviroGen.Noise;
using EnviroGen.Noise.Modifiers;

namespace EnviroGen.HeightMaps
{
    public static class HeightMapGenerator
    {
        public static HeightMap GenerateHeightMap(int sizeX, int sizeY, int octaveCount, float gain, float frequency, int seed, ModifierOptions modOptions = null)
        {
            var arr = new float[sizeX, sizeY];

            for (var y = seed; y < sizeY + seed; y++)
            {
                for (var x = seed; x < sizeX + seed; x++)
                {
                    arr[x - seed, y - seed] = FractalBrownianMotion.GenerateNoise(x, y, octaveCount, gain, frequency, 2f,
                        SimplexNoiseGenerator.Noise2d);
                }
            }

            if (modOptions != null) ApplyModifiers(ref arr, modOptions);

            var map = new HeightMap(arr);
            map.Normalize();

            return map;
        }

        private static void ApplyModifiers(ref float[,] arr, ModifierOptions modOptions)
        {
            if (modOptions.Ridged)
            {
                RidgedModifier.Modify(ref arr);
            }

            if (Math.Abs(modOptions.Exponent - 1f) > float.Epsilon)
            {
                ExponentModifier.Modify(ref arr, modOptions.Exponent);
            }

            if (Math.Abs(modOptions.Scale - 1f) > float.Epsilon)
            {
                ScaleModifier.Modify(ref arr, modOptions.Scale);
            }
        }
    }
}
