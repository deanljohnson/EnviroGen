using System;

namespace EnviroGen.Noise.Modifiers
{
    public class ExponentModifier : IModifier
    {
        public float Exponent { get; set; }

        public ExponentModifier(float exp)
        {
            Exponent = exp;
        }

        public void Modify(ref float[,] map)
        {
            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y] = (float)Math.Pow(map[x, y], Exponent);
                }
            }
        }
    }
}
