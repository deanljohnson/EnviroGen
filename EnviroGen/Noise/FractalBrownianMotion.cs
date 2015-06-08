﻿using System;

namespace EnviroGen.Noise
{
    public static class FractalBrownianMotion
    {
        public static float GenerateNoise(int x, int y, int numOctaves, float gain, float frequency, float lacunarity, Func<float, float, float> Noise)
        {
            var total = 0f;
            var amplitude = gain;

            for (var o = 0; o < numOctaves; o++)
            {
                total += Noise(x * frequency, y * frequency) * amplitude;
                frequency *= lacunarity;
                amplitude *= gain;
            }

            return total;
        }
    }
}