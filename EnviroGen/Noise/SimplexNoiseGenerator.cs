﻿using System;
using System.Collections.Generic;

namespace EnviroGen.Noise
{
    //Not my code for the most part, got this from:
    //http://webstaff.itn.liu.se/~stegu/simplexnoise/simplexnoise.pdf

    public static class SimplexNoiseGenerator
    {
        static SimplexNoiseGenerator()
        {
            for (var i = 0; i < 512; i++)
            {
                Perm[i]=P[i & 255];
            }
        }

        // Simplex noise in 2D, 3D and 4D
        private static readonly int[][] Grad3 = {
            new[] {1,1,0}, new[] {-1,1,0}, new[] {1,-1,0}, new[] {-1,-1,0}, 
            new[] {1,0,1}, new[] {-1,0,1}, new[] {1,0,-1}, new[] {-1,0,-1}, 
            new[] {0,1,1}, new[] {0,-1,1}, new[] {0,1,-1}, new[] {0,-1,-1}
        };

        private static readonly int[] P = {151,160,137,91,90,15,
            131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
            190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
            88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
            77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
            102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
            135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
            5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
            223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
            129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
            251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
            49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
            138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180};

        // To remove the need for index wrapping, double the permutation table length
        private static readonly int[] Perm = new int[512];

        public static float[,] GenerateNoiseArray(int xMax, int yMax, int numOctaves, float roughness, float frequency, int seed)
        {
            var arr = new float[xMax, yMax];
            var layerFrequency = frequency;
            var layerWeight = 1f;
            var weightSum = 0f;

            for (var y = seed; y < yMax + seed; y++)
            {
                for (var x = seed; x < xMax + seed; x++)
                {
                    for (var o = 0; o < numOctaves - 1; o++)
                    {
                        arr[x - seed, y - seed] += Noise2d(x * layerFrequency, y * layerFrequency) * layerWeight;
                        layerFrequency *= 2;
                        weightSum += layerWeight;
                        layerWeight *= roughness;
                    }
                    layerFrequency = frequency;
                    layerWeight = 1f;
                    weightSum = 0f;
                }
            }
            arr = Normalize(arr);
            return arr;
        }

        private static float[,] Normalize(float[,] heightMap)
        {
            var maxValue = heightMap[0, 0];
            var minValue = heightMap[0, 0];

            foreach (var h in heightMap)
            {
                maxValue = h > maxValue ? h : maxValue;
                minValue = h < minValue ? h : minValue;
            }

            var dif = maxValue - minValue;

            for (var y = 0; y < heightMap.GetLength(1); y++)
            {
                for (var x = 0; x < heightMap.GetLength(0); x++)
                {
                    heightMap[x, y] = (heightMap[x, y] - minValue) / (dif);
                }
            }

            return heightMap;
        }

        // 2D simplex noise
        private static float Noise2d(float xin, float yin)
        {
            float n0, n1, n2; 

            // Noise contributions from the three corners
            // Skew the input space to determine which simplex cell we're in
            var f2 = (float) (0.5*(Math.Sqrt(3.0)-1.0));
            var s = (xin+yin)*f2; 

            // Hairy factor for 2D
            var i = FastFloor(xin+s);
            var j = FastFloor(yin+s);
            var g2 = (float) ((3.0-Math.Sqrt(3.0))/6.0);
            var t = (i+j)*g2;
            var X0 = i-t; 

            // Unskew the cell origin back to (x,y) space
            var Y0 = j-t;
            var x0 = xin-X0; 
            // The x,y distances from the cell origin
            var y0 = yin-Y0;
            // For the 2D case, the simplex shape is an equilateral triangle.
            // Determine which simplex we are in.
            int i1, j1; 
            // Offsets for second (middle) corner of simplex in (i,j) coords
            if (x0 > y0)
            {
                i1 = 1;
                j1 = 0;
            }
            // lower triangle, XY order: (0,0)->(1,0)->(1,1)
            else
            {
                i1=0; j1=1;
            }      
            // upper triangle, YX order: (0,0)->(0,1)->(1,1)
            // A step of (1,0) in (i,j) means a step of (1-c,-c) in (x,y), and
            // a step of (0,1) in (i,j) means a step of (-c,1-c) in (x,y), where
            // c = (3-sqrt(3))/6
            var x1 = x0 - i1 + g2; 
            // Offsets for middle corner in (x,y) unskewed coords
            var y1 = y0 - j1 + g2;
            var x2 = (float) (x0 - 1.0 + 2.0 * g2); 
            // Offsets for last corner in (x,y) unskewed coords
            var y2 = (float) (y0 - 1.0 + 2.0 * g2);
            // Work out the hashed gradient indices of the three simplex corners
            var ii = i & 255;
            var jj = j & 255;

            var gi0 = Perm[ii+Perm[jj]] % 12;
            var gi1 = Perm[ii+i1+Perm[jj+j1]] % 12;
            var gi2 = Perm[ii+1+Perm[jj+1]] % 12;
            // Calculate the contribution from the three corners
            var t0 = (float) (0.5 - x0*x0-y0*y0);

            if (t0 < 0)
            {
                n0 = (float) 0.0;
            }
            else 
            {
                t0 *= t0;
                n0 = t0 * t0 * Dot(Grad3[gi0], x0, y0);  
                // (x,y) of grad3 used for 2D gradient
            }

            var t1 = (float) (0.5 - x1*x1-y1*y1);
            if (t1 < 0)
            {
                n1 = (float) 0.0;
            }
            else 
            {
                t1 *= t1;
                n1 = t1 * t1 * Dot(Grad3[gi1], x1, y1);
            }
            var t2 = (float) (0.5 - x2*x2-y2*y2);
            if(t2<0) n2 = (float) 0.0;
            else 
            {
                t2 *= t2;
                n2 = t2 * t2 * Dot(Grad3[gi2], x2, y2);
            }
            // Add contributions from each corner to get the final noise value.
            // The result is scaled to return values in the interval [-1,1].
            return (float) (70.0 * (n0 + n1 + n2));
        }

        private static float Dot(IReadOnlyList<int> g, float x, float y)
        {
            return g[0] * x + g[1] * y;
        }

        // This method is a *lot* faster than using (int)Math.floor(x)
        private static int FastFloor(float x)
        {
            return x > 0 ? (int)x : (int)x - 1;
        }
    }
}
