﻿using System;
using System.Collections.Generic;
using EnviroGen.Noise;
using EnviroGen.Noise.Modifiers;

namespace EnviroGen.HeightMaps
{
    public static class HeightMapGenerator
    {
        public static HeightMap GenerateHeightMap(List<GenerationOptions> options, List<float> weights)
        {
            if (options.Count != weights.Count)
            {
                throw new ArgumentException("The given list of weights must have the same count as the given list of generation options");
            }

            var map = GenerateHeightMap(options[0]);

            for (var i = 1; i < options.Count; i++)
            {
                map.CombineWith(GenerateHeightMap(options[i]), weights[i]);
            }

            return map;
        }

        public static HeightMap GenerateHeightMap(GenerationOptions options)
        {
            return GenerateHeightMap(options.SizeX, options.SizeY, options.OctaveCount, options.Gain, options.Frequency, options.Seed, options.Modifiers);
        }

        /// <summary>
        /// Returns a HeightMap based on the given parameters, normalized [0,1]
        /// </summary>
        public static HeightMap GenerateHeightMap(int sizeX, int sizeY, int octaveCount, float gain, float frequency, int seed = 0, IEnumerable<IModifier> modifiers = null)
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
