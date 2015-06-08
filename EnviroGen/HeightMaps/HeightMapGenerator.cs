using System.Collections.Generic;
using EnviroGen.Noise;
using EnviroGen.Noise.Modifiers;

namespace EnviroGen.HeightMaps
{
    public static class HeightMapGenerator
    {
        public static HeightMap GenerateHeightMap(int sizeX, int sizeY, int octaveCount, float gain, float frequency, int seed = 0, List<IModifier> modifiers = null)
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

            //Make HeightMap object in order to normalize
            var map = new HeightMap(arr);
            map.Normalize();

            //Need variable to pass as ref
            var newMap = map.Map;

            ApplyModifiers(ref newMap, modifiers);

            map.Map = newMap;

            return map;
        }

        private static void ApplyModifiers(ref float[,] arr, IEnumerable<IModifier> modifiers)
        {
            foreach (var modifier in modifiers)
            {
                modifier.Modify(ref arr);
            }
        }
    }
}
