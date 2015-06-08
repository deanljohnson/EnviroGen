using System;

namespace EnviroGen.Noise.Modifiers
{
    public static class ExponentModifier
    {
        public static void Modify(ref float[,] map, float exp)
        {
            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y] = (float) Math.Pow(map[x, y], exp);
                }
            }
        }
    }
}
