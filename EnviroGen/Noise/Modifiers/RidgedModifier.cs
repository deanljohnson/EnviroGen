using System;

namespace EnviroGen.Noise.Modifiers
{
    public static class RidgedModifier
    {
        public static void Modify(ref float[,] map)
        {
            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y] = Math.Abs(map[x, y]);
                }
            }
        }
    }
}
