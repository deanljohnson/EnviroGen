using System;

namespace EnviroGen.Noise
{
    public static class PerlinNoiseGenerator
    {
        /// <summary>
        /// Generates a perlin noise map using the provided Random object
        /// </summary>
        public static float[,] GenerateNoise(int sizeX, int sizeY, int octaveCount, Random random)
        {
            var noise = GenerateWhiteNoise(sizeX, sizeY, random);
            return GeneratePerlinNoise(noise, octaveCount);
        }

        /// <summary>
        /// Return an array of random floats, 0.0f to 1.0f.
        /// </summary>
        /// <returns></returns>
        private static float[,] GenerateWhiteNoise(int sizeX, int sizeY, Random random)
        {
            var whiteNoise = new float[sizeX, sizeY];

            for (var j = 0; j < sizeY; j++)
            {
                for (var i = 0; i < sizeX; i++)
                {
                    whiteNoise[i, j] = (float)random.NextDouble();
                }
            }

            return whiteNoise;
        }

        private static float[,] GeneratePerlinNoise(float[,] baseNoise, int octaveCount)
        {
            const float persistence = .55f;
            var width = baseNoise.GetLength(0);
            var height = baseNoise.GetLength(1);

            var perlinNoise = new float[width, height];
            var amplitude = 1.0f;
            var totalAmplitude = 0.0f;

            //blend noise together
            for (var octave = octaveCount - 1; octave >= 0; octave--)
            {
                var smoothNoise = GenerateSmoothNoise(baseNoise, octave);
                amplitude *= persistence;
                totalAmplitude += amplitude;

                for (var j = 0; j < height; j++)
                {
                    for (var i = 0; i < width; i++)
                    {
                        perlinNoise[i, j] += smoothNoise[i, j] * amplitude;
                    }
                }
            }

            //normalization
            for (var j = 0; j < height; j++)
            {
                for (var i = 0; i < width; i++)
                {
                    perlinNoise[i, j] /= totalAmplitude;
                }
            }

            return perlinNoise;
        }

        private static float[,] GenerateSmoothNoise(float[,] baseNoise, int octave)
        {
            var width = baseNoise.GetLength(0);
            var height = baseNoise.GetLength(1);

            var smoothNoise = new float[width, height];

            var samplePeriod = (int)Math.Pow(2, octave);
            var sampleFrequency = 1f / samplePeriod;

            for (var j = 0; j < height; j++)
            {
                //calculate the vertical sampling indices
                var sampleJ0 = (j / samplePeriod) * samplePeriod;
                var sampleJ1 = (sampleJ0 + samplePeriod) % height;
                var verticalBlend = (j - sampleJ0) * sampleFrequency;

                for (var i = 0; i < width; i++)
                {
                    //calculate the horizontaol sampling indices
                    var sampleI0 = (i / samplePeriod) * samplePeriod;
                    var sampleI1 = (sampleI0 + samplePeriod) % width;
                    var horizontalBlend = (i - sampleI0) * sampleFrequency;

                    var top = Interpolate(baseNoise[sampleI0, sampleJ0], baseNoise[sampleI1, sampleJ0], horizontalBlend);
                    var bottom = Interpolate(baseNoise[sampleI0, sampleJ1], baseNoise[sampleI1, sampleJ1], horizontalBlend);

                    smoothNoise[i, j] = Interpolate(top, bottom, verticalBlend);
                }
            }

            return smoothNoise;
        }

        private static float Interpolate(float x, float y, float alpha)
        {
            //return CosineInterpolation(x, y, alpha);
            return Lerp(x, y, alpha);
        }

        /// <summary>
        /// Has fewer visual artifacts than Lerp, but has a performance hit
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        private static float CosineInterpolation(float x, float y, float alpha)
        {
            var ft = alpha * (float)Math.PI;
            var f = (1f - (float)Math.Cos(ft)) * .5f;

            return x * (1 - f) + y * f;
        }

        /// <summary>
        /// Fast, but creates slight rectangular patterns
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        private static float Lerp(float x, float y, float alpha)
        {
            return x * (1 - alpha) + alpha * y;
        }
    }
}
