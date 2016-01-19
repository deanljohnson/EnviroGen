using System;

namespace EnviroGen.Noise.Modifiers
{
    public class ExponentModifier : IInvertableModifier
    {
        public float Exponent { get; set; }

        public ExponentModifier(float exp)
        {
            Exponent = exp;
        }

        public void InvertModify(ref float[,] map)
        {
            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y] = (float)Math.Pow(map[x, y], Exponent);
                }
            }
        }

        public void InvertModify(ref float[,] map)
        {
            var exp = 1 / Exponent;

            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y] = (float)Math.Pow(map[x, y], exp);
                }
            }
        }
    }
}
