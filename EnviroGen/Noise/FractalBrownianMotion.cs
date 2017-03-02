using System;

namespace EnviroGen.Noise
{
    /// <summary>
    /// Responsible for combining multiple octaves of noise together.
    /// Each successive octave is at a finer resolution than the previous octave.
    /// </summary>
    public static class FractalBrownianMotion
    {
        public static float GenerateNoise(int x, int y, int numOctaves, float gain, float frequency, float lacunarity, Func<float, float, float> noise)
        {
            var total = 0f;
            var amplitude = gain;

            for (var o = 0; o < numOctaves; o++)
            {
                total += noise(x * frequency, y * frequency) * amplitude;
                frequency *= lacunarity;
                amplitude *= gain;
            }

            return total;
        }
    }
}
